using System;
using System.Text.RegularExpressions;

namespace Yuumix.OdinToolkits.Core.Editor
{
    /// <summary>
    /// XML 注释部分和代码块的组合
    /// </summary>
    [Summary("XML 注释部分和代码块的组合")]
    [Serializable]
    public class XMLCodePart
    {
        #region Serialized Fields

        /// <summary>
        /// 注释部分的源代码，以 /// 开头
        /// </summary>
        [Summary("注释部分的源代码，以 /// 开头")]
        public string xml;

        /// <summary>
        /// 不以注释开头的代码块，除了注释对应的成员外，可能包含多个成员
        /// </summary>
        [Summary("不以注释开头的代码块，除了注释对应的成员外，可能包含多个成员")]
        public string code;

        #endregion

        public XMLCodePart(string xml, string code)
        {
            this.xml = xml;
            this.code = code;
        }

        /// <summary>
        /// 从 xml 中提取 Summary 的内容
        /// </summary>
        [Summary("从 xml 中提取 Summary 的内容")]
        public string SummaryValue
        {
            get
            {
                var match = Regex.Match(xml, "/// <summary>(.*?)</summary>",
                    RegexOptions.Singleline);
                if (match.Success)
                {
                    var summaryContent = match.Groups[1].Value.Trim();
                    // 移除 XML 子标签（如 <param>, <returns> 等）
                    var cleanedSummaryContent = Regex.Replace(summaryContent, "<[^>]+>", "");
                    // 移除多余的注释符号（///）
                    cleanedSummaryContent =
                        Regex.Replace(cleanedSummaryContent, @"^\s*///\s*", "", RegexOptions.Multiline);
                    // 移除空行
                    cleanedSummaryContent =
                        Regex.Replace(cleanedSummaryContent, @"^\s*$\r?\n", "", RegexOptions.Multiline);
                    // 压缩连续的空白字符
                    cleanedSummaryContent = Regex.Replace(cleanedSummaryContent, @"\s+", " ").Trim();
                    return cleanedSummaryContent;
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// 获取 SummaryAttribute 的代码文本
        /// </summary>
        [Summary("获取 SummaryAttribute 的代码文本")]
        public string SummaryAttributeText
        {
            get
            {
                if (string.IsNullOrEmpty(SummaryValue))
                {
                    return string.Empty;
                }

                var indent = Regex.Match(xml, @"^\s*").Value;
                var attr = nameof(SummaryAttribute).Replace("Attribute", "");
                return indent + "[" + attr + "(\"" + SummaryValue + "\")]\n";
            }
        }

        /// <summary>
        /// 删除了 summary 标签部分的 xml
        /// </summary>
        [Summary("删除了 summary 标签部分的 xml")]
        public string RemovedSummaryXml
        {
            get
            {
                var processedXml = xml;
                var match = Regex.Match(xml, @"/// <summary>(.*?)</summary>",
                    RegexOptions.Singleline);
                if (match.Success)
                {
                    processedXml = xml.Replace(match.Value, "");
                    processedXml = Regex.Replace(processedXml, @"^\s*$\r?\n", "", RegexOptions.Multiline);
                }

                return processedXml;
            }
        }

        /// <summary>
        /// 删除了第一个 [Summary()] 部分的代码块
        /// </summary>
        [Summary("删除了第一个 [Summary()] 部分的代码块")]
        public string RemovedFirstSummaryAttributeCode
        {
            get
            {
                var processedCode = code;
                var attr = nameof(SummaryAttribute).Replace("Attribute", "");
                // 修改正则表达式以支持多行特性
                // 使用 [\s\S]*? 匹配包括换行符在内的任意字符，直到找到闭合的括号
                var match = Regex.Match(code,
                    @"(?m)(?:^|\s)\s*\[" + attr + @"\(""(?<content>[\s\S]*?)""\)\]",
                    RegexOptions.Multiline);
                if (match.Success)
                {
                    processedCode = code.Replace(match.Value, "");
                    // 替换后，删除多余的空行，但保留缩进的空格
                    processedCode = Regex.Replace(processedCode, @"^\s*$\r?\n", "", RegexOptions.Multiline);
                }

                return processedCode;
            }
        }

        /// <summary>
        /// 删除了所有 [Summary()] 特性部分的代码块
        /// </summary>
        [Summary("删除了所有 [Summary()] 部分的代码块")]
        public string RemoveAllSummaryAttributeCode
        {
            get
            {
                var processedCode = code;
                var attr = nameof(SummaryAttribute).Replace("Attribute", "");
                // 修改正则表达式以匹配所有情况：
                // 1. 单独一行的 SummaryAttribute
                // 2. 与其他特性在同一行的 SummaryAttribute
                // 3. 多行的 SummaryAttribute
                // 使用 (?m) 多行模式，并移除对行首的要求
                processedCode = Regex.Replace(processedCode,
                    @"(?m)(?:^|\s)\s*\[" + attr + @"\(""(?<content>[\s\S]*?)""\)\]",
                    "", RegexOptions.Multiline);
                // 删除替换后可能留下的空行，但保留缩进的空格
                processedCode = Regex.Replace(processedCode, @"^\s*$\r?\n", "", RegexOptions.Multiline);
                return processedCode;
            }
        }

        /// <summary>
        /// 获取同步了 summary 标签的代码
        /// </summary>
        [Summary("获取同步了 summary 标签的代码")]
        public string GetSyncOutput() => xml + SummaryAttributeText + RemovedFirstSummaryAttributeCode;

        /// <summary>
        /// 获取替换了 summary 标签的代码
        /// </summary>
        [Summary("获取替换了 summary 标签的代码")]
        public string GetReplaceOutput() => RemovedSummaryXml + SummaryAttributeText + RemovedFirstSummaryAttributeCode;

        /// <summary>
        /// 获取删除了 SummaryAttribute 的代码
        /// </summary>
        [Summary("获取删除了 SummaryAttribute 的代码")]
        public string GetReplaceAllOutput() => xml + RemoveAllSummaryAttributeCode;
    }
}
