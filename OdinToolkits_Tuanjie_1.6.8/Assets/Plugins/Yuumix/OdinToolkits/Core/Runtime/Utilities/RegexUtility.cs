using System.Text;
using System.Text.RegularExpressions;

namespace Yuumix.OdinToolkits.Core
{
    public static class RegexUtility
    {
        /// <summary>
        /// 匹配 XML 的 Summary，直到下一个分号、大括号
        /// </summary>
        public static readonly Regex XmlToDeclarationEndRegex =
            new Regex(@"(\s*///\s*<summary>(.*?)</summary>\s*)(.*?)((?=;|\{))",
                RegexOptions.Singleline | RegexOptions.Multiline);

        public static readonly Regex InvalidNamespaceRegex =
            new Regex(@"([^a-zA-Z0-9._]|[\s]|::|\b(using)\b|\.{2,})");

        public static string CanonicalNamespace(string input)
        {
            const string canonicalNamespaceRegex = @"([^a-zA-Z0-9._]|[\s]|::|\b(using)\b|\.{2,})";
            var foundFirstLetter = false;
            var firstValidator = new StringBuilder();

            foreach (var c in input)
            {
                // 检查字符是否为字母
                if (char.IsLetter(c))
                {
                    foundFirstLetter = true;
                    firstValidator.Append(c);
                }
                // 如果已经找到了第一个字母，则保留数字和特殊字符
                else if (foundFirstLetter)
                {
                    firstValidator.Append(c);
                }
            }

            var firstResult = firstValidator.ToString();

            return Regex.Replace(firstResult, canonicalNamespaceRegex, "");
        }

        public static string CanonicalScriptClassName(string className)
        {
            // 简写: \w 表示匹配所有字母和数字的字符: [a-zA-Z0-9_]
            // \u4e00-\u9fa5 表示匹配所有中文字符
            // [^\w] 表示匹配所有非字母和数字的字符, [^\u4e00-\u9fa5] 表示匹配所有非中文字符
            const string canonicalScriptClassNameRegex = @"([^\w\u4e00-\u9fa5])";
            // 移除非法字符
            className = Regex.Replace(className, canonicalScriptClassNameRegex, "");

            // 确保类名以大写字母开头
            if (className.Length > 0)
            {
                className = char.ToUpper(className[0]) + className[1..];
            }

            return className;
        }
    }
}
