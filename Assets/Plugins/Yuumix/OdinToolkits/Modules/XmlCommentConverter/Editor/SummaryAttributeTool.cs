
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace UnityTools
{
    /// <summary>
    /// XML 文档注释与 ChineseSummary 特性同步工具类
    /// 提供特性同步和移除功能，遵循无状态、参数驱动设计规范
    /// </summary>
    public static class SummaryAttributeTool
    {
        #region 核心方法

        /// <summary>
        /// 同步 XML 文档注释到 ChineseSummary 特性
        /// </summary>
        /// <param name="sourceCode">源代码字符串</param>
        /// <returns>处理后的源代码字符串</returns>
        public static string SyncSummaryToAttribute(string sourceCode)
        {
            if (string.IsNullOrEmpty(sourceCode))
                return sourceCode;

            // 检测换行符类型
            string lineEnding = DetectLineEnding(sourceCode);
            
            // 使用正则表达式匹配带有 summary 注释的代码成员
            // 匹配模式：XML注释 + 可选的特性 + 成员声明
            string pattern = @"(///\s*<summary>(.*?)</summary>\s*)(.*?)((?=;|\{|$))";
            Regex regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Multiline);
            
            return regex.Replace(sourceCode, match =>
            {
                string summaryComment = match.Groups[1].Value;
                string summaryContent = match.Groups[2].Value;
                string attributesAndDeclaration = match.Groups[3].Value;
                string memberDeclaration = match.Groups[4].Value.Trim();
                
                // 清理 summary 内容
                string cleanedSummary = CleanSummaryContent(summaryContent);
                
                if (string.IsNullOrEmpty(cleanedSummary))
                    return match.Value; // 如果清理后内容为空，保持原样
                
                // 获取正确的缩进 - 从成员声明行获取缩进
                string indentation = GetIndentationFromMemberDeclaration(attributesAndDeclaration, memberDeclaration);
                
                // 转义特殊字符
                string escapedSummary = EscapeSpecialCharacters(cleanedSummary);
                
                // 分离特性和成员声明
                string[] parts = SplitAttributesAndDeclaration(attributesAndDeclaration, memberDeclaration);
                string existingAttributes = parts[0];
                string actualMemberDeclaration = parts[1];
                
                // 处理特性，传入缩进和换行符信息
                string processedAttributes = ProcessAttributes(existingAttributes, escapedSummary, indentation, lineEnding);
                
                // 重新构建代码
                return $"{summaryComment}{processedAttributes}{actualMemberDeclaration}";
            });
        }

        /// <summary>
        /// 移除所有 ChineseSummary 特性
        /// </summary>
        /// <param name="sourceCode">源代码字符串</param>
        /// <returns>处理后的源代码字符串</returns>
        public static string RemoveAllChineseSummary(string sourceCode)
        {
            if (string.IsNullOrEmpty(sourceCode))
                return sourceCode;

            // 检测换行符类型
            string lineEnding = DetectLineEnding(sourceCode);

            // 处理单行 ChineseSummary 特性
            string pattern1 = @"\[ChineseSummary\s*\(\s*""(?:[^""\\]|\\.)*""\s*\)\]\s*";
            string result = Regex.Replace(sourceCode, pattern1, "", RegexOptions.Multiline);

            // 处理多行 ChineseSummary 特性
            string pattern2 = @"\[ChineseSummary\s*\(\s*""(?:[^""\\]|\\.)*""\s*\)\s*\]";
            result = Regex.Replace(result, pattern2, "", RegexOptions.Singleline);

            // 处理与其他特性在同一行的情况
            result = Regex.Replace(result, @"\]\s*\[ChineseSummary\s*\(\s*""(?:[^""\\]|\\.)*""\s*\)\]", "]", RegexOptions.Multiline);
            result = Regex.Replace(result, @"\[ChineseSummary\s*\(\s*""(?:[^""\\]|\\.)*""\s*\)\]\s*\[", "[", RegexOptions.Multiline);

            // 清理多余的空格和换行
            result = Regex.Replace(result, @"\]\s+\]", "]", RegexOptions.Multiline);
            result = Regex.Replace(result, @"\[\s+\[", "[[", RegexOptions.Multiline);
            result = Regex.Replace(result, $@"{lineEnding}(\s*){lineEnding}", lineEnding, RegexOptions.Multiline);

            return result;
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 分离特性和成员声明
        /// </summary>
        /// <param name="attributesAndDeclaration">特性和声明的组合字符串</param>
        /// <param name="memberDeclaration">成员声明</param>
        /// <returns>数组，包含[特性部分, 成员声明部分]</returns>
        private static string[] SplitAttributesAndDeclaration(string attributesAndDeclaration, string memberDeclaration)
        {
            if (string.IsNullOrEmpty(attributesAndDeclaration))
                return new string[] { "", memberDeclaration };

            // 查找成员声明的开始位置
            int declarationStart = attributesAndDeclaration.IndexOf(memberDeclaration);
            if (declarationStart == -1)
                return new string[] { attributesAndDeclaration, memberDeclaration };

            string attributesPart = attributesAndDeclaration.Substring(0, declarationStart).TrimEnd();
            string declarationPart = attributesAndDeclaration.Substring(declarationStart);

            return new string[] { attributesPart, declarationPart };
        }

        /// <summary>
        /// 处理特性，添加或更新 ChineseSummary 特性
        /// </summary>
        /// <param name="existingAttributes">现有特性字符串</param>
        /// <param name="escapedSummary">转义后的摘要内容</param>
        /// <param name="indentation">缩进字符串</param>
        /// <param name="lineEnding">换行符字符串</param>
        /// <returns>处理后的特性字符串</returns>
        private static string ProcessAttributes(string existingAttributes, string escapedSummary, string indentation, string lineEnding)
        {
            // 检查是否已存在 ChineseSummary 特性
            bool hasChineseSummary = Regex.IsMatch(existingAttributes, @"\[ChineseSummary\s*\(");
            
            if (hasChineseSummary)
            {
                // 替换现有的 ChineseSummary 特性，只替换内容，保持原有格式
                string result = Regex.Replace(existingAttributes, 
                    @"\[ChineseSummary\s*\(\s*""(?:[^""\\]|\\.)*""\s*\)\]", 
                    $"[ChineseSummary(\"{escapedSummary}\")]");
                return result;
            }
            else
            {
                // 新添加 ChineseSummary 特性，必须在单独一行，带缩进
                string newChineseSummaryLine = $"{indentation}[ChineseSummary(\"{escapedSummary}\")]{lineEnding}";
                
                if (string.IsNullOrEmpty(existingAttributes))
                {
                    // 没有现有特性，直接添加 ChineseSummary 在单独一行
                    return newChineseSummaryLine;
                }
                else
                {
                    // 有现有特性，在现有特性后添加 ChineseSummary 在单独一行
                    // 修复问题1：确保新特性在现有特性之后，而不是之前
                    // 修复问题2：确保后续特性有正确的缩进
                    string trimmedExisting = existingAttributes.TrimEnd();
                    
                    // 如果现有特性以换行符结束，直接添加新特性行
                    if (trimmedExisting.EndsWith(lineEnding))
                    {
                        return $"{trimmedExisting}{newChineseSummaryLine}";
                    }
                    else
                    {
                        // 否则先添加换行符，再添加新特性行
                        return $"{trimmedExisting}{lineEnding}{newChineseSummaryLine}";
                    }
                }
            }
        }

        /// <summary>
        /// 清理 XML summary 内容
        /// </summary>
        /// <param name="summaryContent">原始 summary 内容</param>
        /// <returns>清理后的内容</returns>
        public static string CleanSummaryContent(string summaryContent)
        {
            if (string.IsNullOrEmpty(summaryContent))
                return summaryContent;

            // 移除 XML 子标签（如 <param>, <returns> 等）
            string cleaned = Regex.Replace(summaryContent, @"<[^>]+>", "");
            
            // 移除多余的注释符号（///）
            cleaned = Regex.Replace(cleaned, @"^\s*///\s*", "", RegexOptions.Multiline);
            
            // 移除空行
            cleaned = Regex.Replace(cleaned, @"^\s*$\r?\n", "", RegexOptions.Multiline);
            
            // 压缩连续的空白字符
            cleaned = Regex.Replace(cleaned, @"\s+", " ").Trim();
            
            return cleaned;
        }

        /// <summary>
        /// 检测源代码的换行符类型
        /// </summary>
        /// <param name="sourceCode">源代码字符串</param>
        /// <returns>换行符字符串（"\r\n" 或 "\n"）</returns>
        public static string DetectLineEnding(string sourceCode)
        {
            if (string.IsNullOrEmpty(sourceCode))
                return Environment.NewLine;

            // 检查是否包含 Windows 换行符
            if (sourceCode.Contains("\r\n"))
                return "\r\n";
            
            // 默认使用 Unix/Linux 换行符
            return "\n";
        }

        /// <summary>
        /// 获取代码行的缩进
        /// </summary>
        /// <param name="codeLine">代码行</param>
        /// <returns>缩进字符串</returns>
        public static string GetIndentation(string codeLine)
        {
            if (string.IsNullOrEmpty(codeLine))
                return "";

            // 匹配行首的空白字符
            Match match = Regex.Match(codeLine, @"^[ \t]+");
            return match.Success ? match.Value : "";
        }

        /// <summary>
        /// 从成员声明获取正确的缩进
        /// </summary>
        /// <param name="attributesAndDeclaration">特性和声明的组合字符串</param>
        /// <param name="memberDeclaration">成员声明</param>
        /// <returns>缩进字符串</returns>
        private static string GetIndentationFromMemberDeclaration(string attributesAndDeclaration, string memberDeclaration)
        {
            if (string.IsNullOrEmpty(memberDeclaration))
                return "";

            // 查找成员声明在组合字符串中的位置
            int memberIndex = attributesAndDeclaration.IndexOf(memberDeclaration);
            if (memberIndex == -1)
                return "";

            // 获取成员声明之前的所有内容
            string beforeMember = attributesAndDeclaration.Substring(0, memberIndex);
            
            // 按行分割，找到最后一行（即成员声明所在行）的缩进
            string[] lines = beforeMember.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            if (lines.Length == 0)
                return "";

            // 最后一行是成员声明所在行的开始部分，获取其缩进
            string lastLine = lines[lines.Length - 1];
            Match match = Regex.Match(lastLine, @"^[ \t]*");
            return match.Success ? match.Value : "";
        }

        /// <summary>
        /// 转义字符串中的特殊字符
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>转义后的字符串</returns>
        public static string EscapeSpecialCharacters(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input.Replace("\\", "\\\\")
                        .Replace("\"", "\\\"")
                        .Replace("\n", "\\n")
                        .Replace("\r", "\\r")
                        .Replace("\t", "\\t");
        }

        #endregion
    }
}
