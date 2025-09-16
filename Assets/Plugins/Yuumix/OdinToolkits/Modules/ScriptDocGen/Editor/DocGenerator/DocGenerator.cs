using System.Globalization;
using System.Text;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public static class DocGenerator
    {
        /// <summary>
        /// 根据类型分析数据生成文档文本，此方法使用默认文本生成策略
        /// </summary>
        public static string GenerateDocText(TypeAnalysisData data)
        {
            var sb = new StringBuilder();
            return sb.ToString();
        }

        public static StringBuilder CreateHeaderTypeIntroduction(TypeAnalysisData data)
        {
            var sb = new StringBuilder();
            if (data.isStatic)
            {
                sb.AppendLine(
                    $"# `{data.typeName} static {data.typeCategory.ToString().ToLower(CultureInfo.CurrentCulture)}`");
            }
            else if (data.isAbstract)
            {
                sb.AppendLine(
                    $"# `{data.typeName} abstract {data.typeCategory.ToString().ToLower(CultureInfo.CurrentCulture)}`");
            }
            else
            {
                sb.AppendLine(
                    $"# `{data.typeName} {data.typeCategory.ToString().ToLower(CultureInfo.CurrentCulture)}`");
            }

            sb.AppendLine();
            sb.AppendLine("## Introduction");
            sb.AppendLine();
            sb.AppendLine($"- Assembly: `{data.assemblyName}`");
            if (!data.namespaceName.IsNullOrWhiteSpace())
            {
                sb.AppendLine($"- Namespace: `{data.namespaceName}`");
            }

            if (data.referenceWebLinkArray.Length >= 1)
            {
                for (var i = 0; i < data.referenceWebLinkArray.Length; i++)
                {
                    sb.AppendLine($"- Reference Link [{i + 1}] : {data.referenceWebLinkArray[i]}");
                }
            }

            sb.AppendLine();
            sb.AppendLine("``` csharp");
            sb.AppendLine(data.typeDeclaration);
            sb.AppendLine("```");
            if (string.IsNullOrEmpty(data.chineseDescription) && string.IsNullOrEmpty(data.englishDescription))
            {
                return sb;
            }
            
            sb.AppendLine("### Description");
            sb.AppendLine();
            if (!string.IsNullOrEmpty(data.chineseDescription))
            {
                sb.AppendLine("- " + data.chineseDescription);
            }

            if (!string.IsNullOrEmpty(data.englishDescription))
            {
                sb.AppendLine("- " + data.englishDescription);
            }

            sb.AppendLine();
            return sb;
        }
    }
}
