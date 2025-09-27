using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    /// <summary>
    /// XML 文档注释与 ChineseSummary 特性同步工具类
    /// 提供特性同步和移除功能，遵循无状态、参数驱动设计规范
    /// </summary>
    public static class SummaryAttributeConverter
    {
        #region 实际写入文件

        public static void WriteSyncChineseSummaryText(string filePath)
        {
            string sourceCode = File.ReadAllText(filePath);
            sourceCode = GetCodeWithSyncToChineseSummary(sourceCode);
            File.WriteAllText(filePath, sourceCode);
            Debug.Log("同步 " + filePath + " 的 Summary 注释为 ChineseSummary 成功！");
            AssetDatabase.Refresh();
        }

        public static void WriteRemoveChineseSummaryText(string filePath)
        {
            string sourceCode = File.ReadAllText(filePath);
            sourceCode = GetCodeWithRemoveAllChineseSummary(sourceCode);
            File.WriteAllText(filePath, sourceCode);
            Debug.Log("移除 " + filePath + " 的 ChineseSummary 特性成功！");
            AssetDatabase.Refresh();
        }

        #endregion

        #region 同步 ChineseSummary 特性

        /// <summary>
        /// 同步 XML 文档注释到 ChineseSummary 特性
        /// </summary>
        /// <param name="sourceCode">源代码字符串</param>
        /// <returns>处理后的源代码字符串</returns>
        public static string GetCodeWithSyncToChineseSummary(string sourceCode)
        {
            if (string.IsNullOrEmpty(sourceCode))
            {
                return sourceCode;
            }

            string newLineSymbol = DetectNewLine(sourceCode);
            var namespaceSymbolRegex = new Regex("using " + typeof(SummaryAttribute).Namespace + ";");
            var namespaceSymbolRegex2 = new Regex("namespace " + typeof(SummaryAttribute).Namespace);
            if (!namespaceSymbolRegex.IsMatch(sourceCode) && !namespaceSymbolRegex2.IsMatch(sourceCode))
            {
                sourceCode = "using " + typeof(SummaryAttribute).Namespace + ";" + newLineSymbol + sourceCode;
            }

            // 使用正则表达式匹配带有 summary 注释的代码成员
            // 匹配模式：XML注释 + 可选的特性 + 成员声明
            // (?=;|\{|$) 先行断言，匹配下一个字符为分号、左大括号或行末，不匹配字符，所以捕获组内容为空字符串。
            var regex = new Regex(@"(\s*///\s*<summary>(.*?)</summary>\s*)(.*?)((?=;|\{))",
                RegexOptions.Singleline | RegexOptions.Multiline);

            MatchCollection matches = regex.Matches(sourceCode);
            for (var i = 0; i < matches.Count; i++)
            {
                string summaryComment = matches[i].Groups[1].Value;
                string summaryContent = matches[i].Groups[2].Value;
                string otherXmlAndAttributesAndDeclaration = matches[i].Groups[3].Value;

                // 清理 summary 内容
                string cleanedSummary = CleanSummaryContent(summaryContent);
                if (string.IsNullOrEmpty(cleanedSummary))
                {
                    continue; // 如果清理后内容为空，则跳过当前匹配项
                }

                // 获取对应成员的 xml 注释的缩进
                string indent = Regex.Match(summaryComment, @"^\s*").Value;
                // Debug.Log(indent + "，indent 的长度为：" + indent.Length);
                (string xmlComments, string attributesAndDeclaration) =
                    SplitCodeParts(otherXmlAndAttributesAndDeclaration, newLineSymbol);
                string newAttributesAndDeclaration =
                    SyncAttribute(attributesAndDeclaration, cleanedSummary, indent);
                string newSummaryAndOther = MergeThreeCodeParts(summaryComment, xmlComments,
                    newAttributesAndDeclaration, newLineSymbol);
                sourceCode = sourceCode.Replace(matches[i].Groups[0].Value, newSummaryAndOther);
            }

            return sourceCode;
        }

        /// <summary>
        /// 同步ChineseSummary特性，支持单行和多行格式
        /// </summary>
        /// <returns>更新后的代码</returns>
        static string SyncAttribute(string attributesAndDeclaration, string cleanedSummaryContent, string indent)
        {
            if (string.IsNullOrEmpty(attributesAndDeclaration))
            {
                return attributesAndDeclaration;
            }

            // 匹配单行和多行的 ChineseSummary 特性
            // 支持格式：
            // [ChineseSummary("xxx")]
            // [ChineseSummary("xxx" + "yyy")]
            // [ChineseSummary("xxx" +
            //     "yyy" +
            //     "zzz")]
            const string pattern = @"(\[ChineseSummary\s*\()(.*?)(\)\s*\])";
            const RegexOptions regexOptions = RegexOptions.Singleline; // 让.匹配换行符

            // 检查是否已存在ChineseSummary特性
            if (Regex.IsMatch(attributesAndDeclaration, pattern, regexOptions))
            {
                // 替换特性内容，保留特性的前后结构
                return Regex.Replace(attributesAndDeclaration, pattern,
                    match => $"{match.Groups[1].Value}\"{cleanedSummaryContent}\"{match.Groups[3].Value}",
                    regexOptions);
            }

            var sb = new StringBuilder();
            sb.Append($"[ChineseSummary(\"{cleanedSummaryContent}\")]")
                .Append(indent)
                .Append(attributesAndDeclaration);
            return sb.ToString();
        }

        static string MergeThreeCodeParts(string summaryComment, string xmlComments,
            string newAttributesAndDeclaration, string newLineSymbol)
        {
            string result = summaryComment;
            if (!string.IsNullOrEmpty(xmlComments))
            {
                result += xmlComments + newLineSymbol;
            }

            if (!string.IsNullOrEmpty(newAttributesAndDeclaration))
            {
                result += newAttributesAndDeclaration;
            }

            return result;
        }

        /// <summary>
        /// 将正则表达式匹配捕获到的一组元素，包括其他特性和声明，分离成两部分
        /// </summary>
        /// <param name="code">要处理的代码字符串</param>
        /// <param name="newLine">使用的换行符</param>
        /// <returns>包含 2 个部分的元组 (xml注释, 特性和成员声明)</returns>
        static (string XmlComments, string AttributesAndDeclaration) SplitCodeParts(string code,
            string newLine)
        {
            if (string.IsNullOrEmpty(code))
            {
                return (string.Empty, string.Empty);
            }

            // 使用指定的换行符拆分
            string[] lines = code.Split(new[] { newLine }, StringSplitOptions.None);
            var xmlSection = new List<string>();
            var attributeAndDeclarationSection = new List<string>();
            var index = 0;
            while (index < lines.Length && string.IsNullOrWhiteSpace(lines[index]))
            {
                index++;
            }

            // 提取 /// 注释部分
            while (index < lines.Length)
            {
                string line = lines[index];
                string trimmedLine = line.TrimStart();
                if (trimmedLine.StartsWith("///") || trimmedLine.StartsWith("//"))
                {
                    xmlSection.Add(line);
                    index++;
                }
                else
                {
                    break;
                }
            }

            while (index < lines.Length && string.IsNullOrWhiteSpace(lines[index]))
            {
                index++;
            }

            // 提取特性部分
            while (index < lines.Length)
            {
                string line = lines[index];
                string trimmedLine = line.TrimStart();
                if (trimmedLine.StartsWith("["))
                {
                    attributeAndDeclarationSection.Add(line);
                    index++;
                }
                else
                {
                    break;
                }
            }

            // 跳特性和声明之间的空白行
            while (index < lines.Length && string.IsNullOrWhiteSpace(lines[index]))
            {
                index++;
            }

            // 提取成员声明部分
            while (index < lines.Length)
            {
                attributeAndDeclarationSection.Add(lines[index]);
                index++;
            }

            // 使用指定的换行符拼接回字符串
            return (
                string.Join(newLine, xmlSection),
                string.Join(newLine, attributeAndDeclarationSection)
            );
        }

        /// <summary>
        /// 清理 XML summary 内容
        /// </summary>
        /// <param name="summaryContent">原始 summary 内容</param>
        /// <returns>清理后的内容</returns>
        static string CleanSummaryContent(string summaryContent)
        {
            if (string.IsNullOrEmpty(summaryContent))
            {
                return summaryContent;
            }

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
        /// 检测源代码中使用的换行符
        /// </summary>
        /// <param name="text">要检测的源代码文本</param>
        /// <returns>检测到的换行符，默认返回环境默认换行符</returns>
        static string DetectNewLine(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Environment.NewLine;
            }

            // 优先检测 Windows 换行符
            if (text.Contains("\r\n"))
            {
                return "\r\n";
            }

            // 检测 Linux/macOS 换行符
            if (text.Contains("\n"))
            {
                return "\n";
            }

            // 检测旧 Mac 换行符
            if (text.Contains("\r"))
            {
                return "\r";
            }

            // 如果没有检测到任何换行符，使用当前环境的默认换行符
            return Environment.NewLine;
        }

        #endregion

        #region 移除 ChineseSummary

        enum ParseState
        {
            Code,         // 代码区域
            LineComment,  // 单行注释 (//)
            XmlComment,   // XML注释 (///)
            StringLiteral // 字符串字面量
        }

        public static string GetCodeWithRemoveAllChineseSummary(string sourceCode)
        {
            if (string.IsNullOrEmpty(sourceCode))
            {
                return sourceCode;
            }

            var result = new StringBuilder();
            var currentState = ParseState.Code;
            var i = 0;
            int length = sourceCode.Length;

            while (i < length)
            {
                switch (currentState)
                {
                    case ParseState.Code:
                        // 检查是否进入XML注释(///)
                        if (i + 2 < length && sourceCode[i] == '/' && sourceCode[i + 1] == '/' &&
                            sourceCode[i + 2] == '/')
                        {
                            currentState = ParseState.XmlComment;
                            result.Append("///");
                            i += 3;
                        }
                        // 再检查是否进入单行注释(//)
                        else if (i + 1 < length && sourceCode[i] == '/' && sourceCode[i + 1] == '/')
                        {
                            currentState = ParseState.LineComment;
                            result.Append("//");
                            i += 2;
                        }
                        // 检查是否进入字符串字面量
                        else if (sourceCode[i] == '"')
                        {
                            currentState = ParseState.StringLiteral;
                            result.Append('"');
                            i++;
                        }
                        // 检查是否是ChineseSummary特性
                        else if (IsChineseSummaryAttribute(sourceCode, i))
                        {
                            // 跳过整个ChineseSummary特性
                            i = SkipChineseSummaryAttribute(sourceCode, i);
                        }
                        else
                        {
                            result.Append(sourceCode[i]);
                            i++;
                        }

                        break;

                    case ParseState.LineComment:
                    case ParseState.XmlComment:
                        // 在注释中，直接添加字符直到行尾（兼容所有换行符）
                        while (i < length && !IsNewLine(sourceCode, i))
                        {
                            result.Append(sourceCode[i]);
                            i++;
                        }

                        // 遇到换行符，完整添加并退出注释状态
                        if (i < length)
                        {
                            // 完整添加当前换行符（可能是\r、\n或\r\n）
                            int newLineLength = GetNewLineLength(sourceCode, i);
                            result.Append(sourceCode, i, newLineLength);
                            i += newLineLength;
                            currentState = ParseState.Code;
                        }

                        break;

                    case ParseState.StringLiteral:
                        // 在字符串中，直接添加字符直到闭合引号
                        while (i < length)
                        {
                            result.Append(sourceCode[i]);

                            // 处理转义引号
                            if (sourceCode[i] == '"' && (i == 0 || sourceCode[i - 1] != '\\'))
                            {
                                i++;
                                currentState = ParseState.Code;
                                break;
                            }

                            i++;
                        }

                        break;
                }
            }

            return result.ToString();
        }

        // 判断当前位置是否是换行符
        static bool IsNewLine(string code, int index)
        {
            if (index >= code.Length)
            {
                return false;
            }

            return code[index] == '\r' || code[index] == '\n';
        }

        // 获取换行符的完整长度（1或2）
        static int GetNewLineLength(string code, int index)
        {
            if (index + 1 < code.Length && code[index] == '\r' && code[index + 1] == '\n')
            {
                return 2; // Windows换行符\r\n
            }

            return 1; // Linux\n或旧Mac\r
        }

        // 检查当前位置是否是ChineseSummary特性的开始
        static bool IsChineseSummaryAttribute(string code, int index)
        {
            // 跳过前面的空白
            int i = index;
            while (i < code.Length && char.IsWhiteSpace(code[i]))
            {
                i++;
            }

            // 检查是否是特性开始
            if (i >= code.Length || code[i] != '[')
            {
                return false;
            }

            i++;

            // 跳过空白
            while (i < code.Length && char.IsWhiteSpace(code[i]))
            {
                i++;
            }

            // 检查特性名称
            var attributeName = "ChineseSummary";
            if (i + attributeName.Length > code.Length)
            {
                return false;
            }

            for (var j = 0; j < attributeName.Length; j++)
            {
                if (code[i + j] != attributeName[j])
                {
                    return false;
                }
            }

            i += attributeName.Length;

            // 检查是否有括号
            while (i < code.Length && char.IsWhiteSpace(code[i]))
            {
                i++;
            }

            return i < code.Length && code[i] == '(';
        }

        // 跳过整个ChineseSummary特性（兼容多行）
        static int SkipChineseSummaryAttribute(string code, int index)
        {
            int i = index;
            var bracketDepth = 0;
            var inString = false;
            var stringDelimiter = '"';
            var escaped = false;

            while (i < code.Length)
            {
                char c = code[i];

                // 处理字符串
                if (c == stringDelimiter && !escaped)
                {
                    inString = !inString;
                    i++;
                    continue;
                }

                // 处理转义字符
                if (c == '\\' && inString && !escaped)
                {
                    escaped = true;
                    i++;
                    continue;
                }

                escaped = false;

                // 不在字符串中才处理括号
                if (!inString)
                {
                    if (c == '[' || c == '(')
                    {
                        bracketDepth++;
                    }
                    else if (c == ']' || c == ')')
                    {
                        bracketDepth--;

                        // 特性结束
                        if (c == ']' && bracketDepth <= 0)
                        {
                            i++;
                            break;
                        }
                    }
                }

                i++;
            }

            return i;
        }

        #endregion

        #region 测试独立步骤方法

        /// <summary>
        /// 测试 XmlToDeclarationEndRegex 正则表达式
        /// </summary>
        public static void TestXmlToDeclarationEndRegex(string sourceCode)
        {
            if (string.IsNullOrEmpty(sourceCode))
            {
                return;
            }

            // 使用正则表达式匹配带有 summary 注释的代码成员
            // 匹配模式：XML注释 + 可选的特性 + 成员声明
            // (?=;|\{|$) 先行断言，匹配下一个字符为分号、左大括号或行末，不匹配字符，所以捕获组内容为空字符串。
            var regex = new Regex(@"(\s*///\s*<summary>(.*?)</summary>\s*)(.*?)((?=;|\{))",
                RegexOptions.Singleline | RegexOptions.Multiline);
            MatchCollection matches = regex.Matches(sourceCode);
            Debug.Log("匹配到的数量：" + matches.Count);
            foreach (Match match in matches)
            {
                Debug.Log("match.Groups[0].Value = " + match.Groups[0].Value);
                Debug.Log("match.Groups[1].Value = " + match.Groups[1].Value);
                Debug.Log("match.Groups[2].Value = " + match.Groups[2].Value);
                Debug.Log("match.Groups[3].Value = " + match.Groups[3].Value);
                Debug.Log("match.Groups[4].Value = 先行断言，仅表示结束位置，不实际捕获内容" + match.Groups[4].Value);
            }
        }

        /// <summary>
        /// 测试 SplitCodeParts 方法
        /// </summary>
        public static (string xmlComments, string attributesAndDeclaration) TestSplitCodeParts(
            string otherXmlAndAttributesAndDeclaration,
            string newLine)
        {
            (string xmlComments, string attributesAndDeclaration) =
                SplitCodeParts(otherXmlAndAttributesAndDeclaration, newLine);
            return (xmlComments, attributesAndDeclaration);
        }

        #endregion
    }
}
