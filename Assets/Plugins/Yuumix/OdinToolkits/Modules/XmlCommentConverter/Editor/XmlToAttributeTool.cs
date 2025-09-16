using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public static class XmlToAttributeTool
{
    /// <summary>
    /// 同步XML摘要到ChineseSummary特性
    /// </summary>
    public static string SyncSummaryToAttribute(string sourceCode)
    {
        if (string.IsNullOrEmpty(sourceCode)) return sourceCode;
        
        string newline = DetectNewlineStyle(sourceCode);
        string[] lines = sourceCode.Split(new[]{"\r\n", "\n", "\r"}, StringSplitOptions.None);
        var modifiedLines = new List<string>(lines);

        // 正则匹配XML摘要块
        var summaryPattern = new Regex(@"(?<indent>\s*)///\s*<summary>(?<content>.*?)<\/summary>", 
                                      RegexOptions.Singleline | RegexOptions.Compiled);
        
        for (int i = 0; i < modifiedLines.Count; i++) {
            Match match = summaryPattern.Match(modifiedLines[i]);
            if (!match.Success) continue;
            
            // 提取并清理摘要内容
            string summary = Regex.Replace(match.Groups["content"].Value, @"(\s+|\/\/\/)", " ").Trim();
            if (string.IsNullOrEmpty(summary)) continue;
            
            string indent = match.Groups["indent"].Value;
            int memberLineIndex = FindMemberLine(modifiedLines, i);
            
            if (memberLineIndex == -1) continue;
            
            // 检查现有特性并更新/添加
            if (TryUpdateExistingAttribute(ref modifiedLines, memberLineIndex, summary)) {
                i = Math.Max(i, memberLineIndex);
            } else {
                InsertNewAttribute(ref modifiedLines, memberLineIndex, summary, indent, newline);
                i = memberLineIndex; // 调整偏移
            }
        }
        return string.Join(newline, modifiedLines);
    }

    /// <summary>
    /// 移除所有ChineseSummary特性
    /// </summary>
    public static string RemoveAllChineseSummary(string sourceCode)
    {
        if (string.IsNullOrEmpty(sourceCode)) return sourceCode;
        
        // 匹配单行或多行ChineseSummary特性
        string pattern = @"(\s*\[\s*ChineseSummary\s*\(.*?\)\s*\]\s*)+";
        string cleanCode = new Regex(pattern, RegexOptions.Singleline)
                          .Replace(sourceCode, "");
        
        // 清理多余空行
        return Regex.Replace(cleanCode, @"(\r?\n\s*){2,}", "$1");
    }

    // ===== 私有辅助方法 =====
    private static string DetectNewlineStyle(string source)
    {
        if (source.Contains("\r\n")) return "\r\n";
        return source.Contains('\r') ? "\r" : "\n";
    }

    private static int FindMemberLine(List<string> lines, int summaryLine)
    {
        for (int i = summaryLine + 1; i < lines.Count; i++) {
            if (!string.IsNullOrWhiteSpace(lines[i]) && 
               !lines[i].Trim().StartsWith("//")) return i;
        }
        return -1;
    }

    private static bool TryUpdateExistingAttribute(
        ref List<string> lines, 
        int memberLine, 
        string newSummary)
    {
        var attribPattern = new Regex(
            @"(?<preAttrib>[^\]]*)\[ChineseSummary\s*\(\s*\""(?<oldSummary>.*?)\""\s*\)\](?<postAttrib>[^\[]*)");

        // 检查成员行及以上3行内
        for (int i = Math.Max(0, memberLine - 3); i <= memberLine; i++) {
            Match match = attribPattern.Match(lines[i]);
            if (!match.Success) continue;
            
            string updatedContent = match.Groups["preAttrib"].Value +
                                   $"[ChineseSummary(\"{newSummary}\")]" +
                                   match.Groups["postAttrib"].Value;
            
            lines[i] = Regex.Replace(lines[i],
                @"\[ChineseSummary\s*\(.*?\)\]", 
                updatedContent);
            
            return true;
        }
        return false;
    }

    private static void InsertNewAttribute(
        ref List<string> lines,
        int memberLine,
        string summary,
        string indent,
        string newline)
    {
        string attrib = $"{indent}[ChineseSummary(\"{summary}\")]";
        
        // 已有其他特性则插入到特性块
        if (lines[memberLine].Trim().StartsWith("[")) {
            lines.Insert(memberLine, attrib);
            return;
        }
        
        // 方案1：直接插入在成员前
        if (memberLine > 0 && lines[memberLine - 1].Trim().StartsWith("[")) {
            lines.Insert(memberLine - 1, indent + attrib);
        }
        // 方案2：无现有特性时创建新行
        else {
            lines.Insert(memberLine, attrib + newline);
        }
    }
}
