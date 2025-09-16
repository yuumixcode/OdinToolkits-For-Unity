using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Modules;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    /// <summary>
    /// Summary 注释数据，包括注释内容和行数，用于插入相关特性到源代码中
    /// </summary>
    [ChineseSummary("Summary 注释数据，包括注释内容和行数，用于插入相关特性到源代码中")]
    public class SummaryData
    {
        /// <summary>
        /// 成员声明所在行数
        /// </summary>
        [ChineseSummary("成员声明所在行数")]
        public int MemberDeclarationLineNumber;

        /// <summary>
        /// Summary 注释的内容
        /// </summary>
        [ChineseSummary("Summary 注释的内容")]
        public string SummaryContent;
    }

    /// <summary>
    /// XML 注释中的 Summary 内容的转换工具，转换为注释特性，并插入到源代码中
    /// </summary>
    [ChineseSummary("XML 注释中的 Summary 内容的转换工具，转换为注释特性，并插入到源代码中")]
    public static class XMLSummaryConverter
    {
        /// <summary>
        /// 从源代码中提取 SummaryData 数据，包含注释内容和行号
        /// </summary>
        [ChineseSummary("从源代码中提取 SummaryData 数据，包含注释内容和行号")]
        public static List<SummaryData> ExtractSummaryData(string[] sourceCodeLines)
        {
            var results = new List<SummaryData>();
            // 状态标记，表示处于 XML 注释中
            var inXMLComment = false;
            var currentCommentLines = new List<string>();
            for (var i = 0; i < sourceCodeLines.Length; i++)
            {
                string line = sourceCodeLines[i].Trim();
                if (line.StartsWith("///"))
                {
                    inXMLComment = true;
                    string xmlContent = line.Replace("///", "").Trim();
                    currentCommentLines.Add(xmlContent);
                    continue;
                }

                // 如果某一行并不是 /// 开头，且当前不是 XML 注释状态，则直接跳过。
                if (!inXMLComment)
                {
                    continue;
                }

                // 如果某一行不是 /// 开头，但是当前处于 XML 注释状态，则表示上一行就是 XML 注释行，当前行已经不是 XML 注释行，可以进行处理
                // 从所有的 XML 注释中提取 Summary。这里注释行列表除了 summary，还会包括其他 XML 注释。
                string currentSummary = ParseSummaryFromCommentLines(currentCommentLines);
                // 获取该 XML 注释对应的成员声明行数
                int memberDeclarationLineNumber = GetNextCodeElementLineNumber(sourceCodeLines, i);
                // 如果 summary 为空，则跳过表示该段 XML 注释没有 summary
                if (string.IsNullOrEmpty(currentSummary) || memberDeclarationLineNumber <= 0)
                {
                    // 重置状态
                    inXMLComment = false;
                    currentCommentLines.Clear();
                    continue;
                }

                results.Add(new SummaryData
                {
                    SummaryContent = currentSummary,
                    MemberDeclarationLineNumber = memberDeclarationLineNumber
                });
                // 重置状态
                inXMLComment = false;
                currentCommentLines.Clear();
            }

            return results;
        }

        /// <summary>
        /// 插入 ChineseSummary 特性到源代码中，以 Assets 开头的相对路径即可
        /// </summary>
        /// <param name="filePath">以 Assets 开头的相对路径即可</param>
        public static void InsertChineseSummaryAttribute(string filePath)
        {
            string[] sourceCodeLines = File.ReadAllLines(filePath);
            List<SummaryData> summaryDataList = ExtractSummaryData(sourceCodeLines);
            var linesList = new List<string>(sourceCodeLines);
            MarkExistedChineseSummaryAttributes(linesList);
            // 第二步：从后往前遍历，避免行号因插入特性而发生变化
            for (int i = summaryDataList.Count - 1; i >= 0; i--)
            {
                SummaryData summaryData = summaryDataList[i];
                // 检查行号是否有效，防御性编程。
                if (summaryData.MemberDeclarationLineNumber <= 0 ||
                    summaryData.MemberDeclarationLineNumber > linesList.Count)
                {
                    continue;
                }

                // 获取要插入特性序号（实际行号-1，因为数组索引从0开始）
                int insertLineNumber = summaryData.MemberDeclarationLineNumber - 1;
                // 获取目标行的缩进
                string targetLine = linesList[insertLineNumber];
                string indentation = GetLineIndentation(targetLine);
                // 构造带有相同缩进的特性字符串
                var attributeLine = $"{indentation}[BilingualComment(\"{summaryData.SummaryContent}\")]";
                // 在指定行号前插入特性
                linesList.Insert(insertLineNumber, attributeLine);
                // 更新后续元素的行号
                for (int j = i - 1; j >= 0; j--)
                {
                    if (summaryDataList[j].MemberDeclarationLineNumber > summaryData.MemberDeclarationLineNumber)
                    {
                        summaryDataList[j].MemberDeclarationLineNumber++;
                    }
                }
            }

            // 第三步：移除标记的已存在特性
            RemoveMarkedBilingualCommentAttributes(linesList);

            // 第四步：确保该脚本引用 BilingualCommentAttribute 所在的命名空间
            EnsureAttributeNamespace(linesList);

            // 写入文件
            File.WriteAllLines(filePath, linesList.ToArray());
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
            string fileName = Path.GetFileName(filePath);
            Debug.Log($"成功提取 {fileName} 中的 XML Summary 注释并插入 BilingualComment 特性！");
        }

        public static void RemoveAllBilingualCommentAttributes(string filePath)
        {
            string[] sourceCodeLines = File.ReadAllLines(filePath);
            var linesList = new List<string>(sourceCodeLines);
            MarkExistedChineseSummaryAttributes(linesList);
            RemoveMarkedBilingualCommentAttributes(linesList);
            File.WriteAllLines(filePath, linesList.ToArray());
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
            string fileName = Path.GetFileName(filePath);
            Debug.Log($"成功移除 {fileName} 中的 BilingualComment 特性！");
        }

        static void EnsureAttributeNamespace(List<string> linesList)
        {
            string requiredNamespace = typeof(ChineseSummaryAttribute).Namespace;
            if (requiredNamespace == null)
            {
                Debug.LogError("无法获取 " + typeof(ChineseSummaryAttribute) + " 的命名空间！");
                return;
            }

            // 检查是否已存在该命名空间引用
            var namespaceExists = false;
            var insertPosition = 0;

            for (var i = 0; i < linesList.Count; i++)
            {
                // 检查单行 using 语句
                if (Regex.IsMatch(linesList[i], $@"^\s*using\s+{Regex.Escape(requiredNamespace)}\s*;"))
                {
                    namespaceExists = true;
                    break;
                }

                // 检查多行 using 语句，防御性编程，防止有人非要换行使用 using
                if (Regex.IsMatch(linesList[i], @"^\s*using\s+.*$") &&
                    !linesList[i].TrimEnd().EndsWith(";"))
                {
                    // 这是一个多行 using 语句的开始
                    int usingStart = i;
                    while (i < linesList.Count && !linesList[i].TrimEnd().EndsWith(";"))
                    {
                        i++;
                    }

                    // 检查这个多行 using 是否是我们需要的命名空间
                    string fullUsingStatement = string.Join("", linesList.GetRange(usingStart, i - usingStart + 1));
                    if (Regex.IsMatch(fullUsingStatement,
                            $@"using\s+{Regex.Escape(requiredNamespace).Replace(@"\.", @"\.\\s*")}\s*;"))
                    {
                        namespaceExists = true;
                        break;
                    }
                }

                // 记录最后一个 using 语句的位置，用于后续插入
                if (Regex.IsMatch(linesList[i], @"^\s*using\s+.*;"))
                {
                    insertPosition = i + 1;
                }

                // 如果遇到 namespace 声明，检查是否与所需命名空间相同
                if (Regex.IsMatch(linesList[i], @"^\s*namespace\s+.*"))
                {
                    string namespaceLine = linesList[i];
                    Match namespaceMatch = Regex.Match(namespaceLine, @"^\s*namespace\s+([^\s{]+)");
                    if (namespaceMatch.Success)
                    {
                        string currentNamespace = namespaceMatch.Groups[1].Value;
                        // 如果当前文件的 namespace 与所需 namespace 相同，则不需要添加 using 语句
                        if (currentNamespace == requiredNamespace)
                        {
                            namespaceExists = true; // 视为已存在
                            break;
                        }
                    }
                }
            }

            // 如果不存在所需的命名空间引用，则插入
            if (!namespaceExists)
            {
                linesList.Insert(insertPosition, $"using {requiredNamespace};");
            }
        }

        static void MarkExistedChineseSummaryAttributes(List<string> lines)
        {
            string attrName = nameof(ChineseSummaryAttribute).Replace("Attribute", "");
            string attrSingleLineRegex = @"\s*\[" + attrName + @"\(""(.*?)""\)\]";
            for (var i = 0; i < lines.Count; i++)
            {
                if (lines[i].Trim().StartsWith("///") || lines[i].Trim().StartsWith("//"))
                {
                    continue;
                }

                if (Regex.IsMatch(lines[i], attrSingleLineRegex))
                {
                    lines[i] = Regex.Replace(lines[i], attrSingleLineRegex,
                        "[" + nameof(EmptyPlaceholderAttribute) + "]");
                }
                // 检查多行特性
                else if (Regex.IsMatch(lines[i], @"\s*\[BilingualComment\(""[^""]*(?:\s*,\s*""[^""]*)?$"))
                {
                    int startLine = i;
                    // 查找多行特性的结束行
                    while (i < lines.Count && !lines[i].Contains("\")]"))
                    {
                        i++;
                    }

                    if (i < lines.Count) // 找到了结束行
                    {
                        // 标记所有相关行
                        for (int j = startLine; j <= i; j++)
                        {
                            lines[j] = "[EmptyPlaceholder]";
                        }
                    }
                }
            }
        }

        static void RemoveMarkedBilingualCommentAttributes(List<string> lines)
        {
            // 从后往前移除，避免索引变化问题
            for (int i = lines.Count - 1; i >= 0; i--)
            {
                if (lines[i] == "// 删除已经存在的注释特性")
                {
                    lines.RemoveAt(i);
                }
            }
        }

        static string GetLineIndentation(string line)
        {
            var index = 0;
            while (index < line.Length && (line[index] == ' ' || line[index] == '\t'))
            {
                index++;
            }

            return line[..index];
        }

        static string ParseSummaryFromCommentLines(List<string> commentLines)
        {
            string fullXml = string.Join(" ", commentLines);
            // Single line 模式是把多行的文本看成一行匹配，实际上就是把 \n 也算在匹配里
            // 忽略大小写
            // () 表示捕获组
            Match match = Regex.Match(fullXml,
                "<summary>(.*?)</summary>",
                RegexOptions.Singleline | RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                return "";
            }

            // 在正则表达式中，Groups[0] 总是代表整个匹配结果，包含 <summary> 和 </summary>
            // Groups[1] 才是第一个捕获组，根据上文描述，Groups[1] 就是 summary 的内容，只有 (.*?) 这一块
            string content = match.Groups[1].Value;
            // @ 符号：这是 C# 中的"逐字字符串"（verbatim string literal）前缀，表示字符串中的反斜杠不需要转义。
            // \s+ 表示匹配一个或多个空白字符，包括空格，制表符，换行符等预定义字符类
            // 将多个连续空白字符替换为单个空格
            content = Regex.Replace(content, @"\s+", " ").Trim();
            // 处理标点符号后的多余空格
            // $1 表示用第一个捕获组的内容，这里表示用标点符号替换匹配到的标点符号加空格的内容
            content = Regex.Replace(content, @"([,;.!?，；。！？、])\s+", "$1");
            return content;
        }

        static int GetNextCodeElementLineNumber(string[] sourceCodeLines, int startIndex)
        {
            for (int i = startIndex; i < sourceCodeLines.Length; i++)
            {
                string line = sourceCodeLines[i].Trim();

                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (line.StartsWith("//"))
                {
                    continue;
                }

                // 跳过单行特性
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    continue;
                }

                // 跳过多行特性
                if (line.StartsWith("[") && !line.EndsWith("]"))
                {
                    // while 循环直接修改 i，减少 for 循环的执行次数，直到匹配到 ]
                    while (i < sourceCodeLines.Length && !sourceCodeLines[i].Trim().EndsWith("]"))
                    {
                        i++;
                    }

                    continue;
                }

                // 跳过预处理指令
                if (line.StartsWith("#region") || line.StartsWith("#endregion") ||
                    line.StartsWith("#if") || line.StartsWith("#else") ||
                    line.StartsWith("#elif") || line.StartsWith("#endif") ||
                    line.StartsWith("#define") || line.StartsWith("#undef") ||
                    line.StartsWith("#warning") || line.StartsWith("#error") ||
                    line.StartsWith("#line") || line.StartsWith("#pragma"))
                {
                    continue;
                }

                return i + 1;
            }

            return -1;
        }
    }
}
