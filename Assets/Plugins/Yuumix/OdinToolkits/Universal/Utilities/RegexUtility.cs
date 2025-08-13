﻿using System.Text;
using System.Text.RegularExpressions;

namespace Yuumix.Universal
{
    [BilingualComment("正则表达式工具类", "Regular expression utility class")]
    public static class RegexUtility
    {
        const string CanonicalNamespaceRegex = @"([^a-zA-Z0-9._]|[\s]|::|\b(using)\b|\.{2,})";

        // 简写: \w 表示匹配所有字母和数字的字符: [a-zA-Z0-9_]
        // \u4e00-\u9fa5 表示匹配所有中文字符
        // [^\w] 表示匹配所有非字母和数字的字符, [^\u4e00-\u9fa5] 表示匹配所有非中文字符
        const string CanonicalScriptClassNameRegex = @"([^\w\u4e00-\u9fa5])";

        [BilingualComment("规范化命名空间", "Canonicalize a namespace")]
        public static string CanonicalNamespace(string input)
        {
            var foundFirstLetter = false;
            var firstValidator = new StringBuilder();

            foreach (char c in input)
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

            return Regex.Replace(firstResult, CanonicalNamespaceRegex, "");
        }

        [BilingualComment("规范化脚本类名", "Canonicalize a script class name")]
        public static string CanonicalScriptClassName(string className)
        {
            // 移除非法字符
            className = Regex.Replace(className, CanonicalScriptClassNameRegex, "");

            // 确保类名以大写字母开头
            if (className.Length > 0)
            {
                className = char.ToUpper(className[0]) + className[1..];
            }

            return className;
        }
    }
}
