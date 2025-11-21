using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEditor;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator.Editor
{
    /// <summary>
    /// 脚本文档生成器逻辑控制类，负责处理文档生成的核心逻辑
    /// </summary>
    public class ScriptDocGeneratorController
    {
        const string IDENTIFIER_CN = "## 额外说明";
        const string NONE_ASSEMBLY = "None Assembly";

        static readonly StringBuilder UserIdentifierDescriptionParagraph = new StringBuilder()
            .AppendLine(IDENTIFIER_CN)
            .AppendLine()
            .AppendLine("> 首个 `" + IDENTIFIER_CN + "` 是增量生成文档标识符，请勿修改标题级别和内容！" +
                        "本文档由 [`Odin Toolkits For Unity`](" + OdinToolkitsWebLinks.GITHUB_REPOSITORY +
                        ") 辅助生成。");

        readonly IAnalysisDataFactory _analysisDataFactory = new YuumixDefaultAnalysisDataFactory();

        #region 类型分析方法

        public ITypeData AnalyzeSingleType(Type targetType)
        {
            if (targetType != null)
            {
                return _analysisDataFactory.CreateTypeData(targetType, _analysisDataFactory);
            }

            YuumixLogger.OdinToolkitsError("请选择有效的目标类型");
            return null;
        }

        public List<ITypeData> AnalyzeMultipleTypes(List<Type> types)
        {
            if (types is not { Count: > 0 })
            {
                YuumixLogger.OdinToolkitsError("设置有效的 Type 对象列表");
                return null;
            }

            types.RemoveAll(x => x == null);
            return types.Select(type => _analysisDataFactory.CreateTypeData(type, _analysisDataFactory))
                .ToList();
        }

        public List<ITypeData> AnalyzeMultipleTypes(TypesCacheSO typesCache)
        {
            if (typesCache && typesCache.Types.Count > 0)
            {
                return AnalyzeMultipleTypes(typesCache.Types);
            }

            YuumixLogger.OdinToolkitsError("TypesCacheSO 为空或不包含有效的 Type 对象");
            return null;
        }

        public List<ITypeData> AnalyzeSingleAssembly(string assemblyFullName)
        {
            if (string.IsNullOrEmpty(assemblyFullName) || assemblyFullName == NONE_ASSEMBLY)
            {
                YuumixLogger.OdinToolkitsError("请选择目标程序集，不能为 " + NONE_ASSEMBLY);
                return null;
            }

            var targetAssembly = Assembly.Load(assemblyFullName);

            return targetAssembly.GetTypes()
                .Where(t => t.GetCustomAttribute<CompilerGeneratedAttribute>() == null)
                .Select(type => _analysisDataFactory.CreateTypeData(type, _analysisDataFactory))
                .ToList();
        }

        #endregion

        #region 文档生成方法

        public static void GenerateSingleTypeDoc(ITypeData typeData, DocGeneratorSettingsSO generatorSettings,
            string targetFolderPath)
        {
            if (typeData == null || !generatorSettings || string.IsNullOrEmpty(targetFolderPath))
            {
                YuumixLogger.OdinToolkitsError("参数无效，无法生成文档");
                return;
            }

            typeData.TryAsIMemberData(out var memberData);
            if (memberData.IsObsolete && !EditorUtility.DisplayDialog("警告提示", "此类已经被标记为过时，继续生成文档吗？", "确认", "取消"))
            {
                return;
            }

            ReadDocGeneratorSettingSO(typeData, generatorSettings, targetFolderPath, memberData, out var markdownText,
                out var filePathWithExtensions);

            if (File.Exists(filePathWithExtensions))
            {
                if (!EditorUtility.DisplayDialog("提示",
                        "已经存在该文档，继续生成将覆盖部分内容，保留首个 " + IDENTIFIER_CN + " 之后的内容，是否继续生成？", "确认", "取消"))
                {
                    return;
                }

                var readAllLines = File.ReadAllLines(filePathWithExtensions);
                if (TryGetFrontMatter(readAllLines, out var frontMatter))
                {
                    markdownText = frontMatter + markdownText;
                }

                var additionalDescription = GetAdditionalDescriptionFromExistingFile(readAllLines);
                if (!string.IsNullOrEmpty(additionalDescription))
                {
                    var userIdentifierParagraphString = UserIdentifierDescriptionParagraph.ToString();
                    if (markdownText.Contains(userIdentifierParagraphString))
                    {
                        markdownText = markdownText.Replace(userIdentifierParagraphString,
                            additionalDescription);
                    }
                    else
                    {
                        markdownText += additionalDescription;
                    }
                }
            }

            var directoryPath = Path.GetDirectoryName(filePathWithExtensions);
            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var utf8WithoutBom = new UTF8Encoding(false);
            File.WriteAllText(filePathWithExtensions, markdownText, utf8WithoutBom);

            AssetDatabase.Refresh();
            EditorUtility.OpenWithDefaultApp(filePathWithExtensions);
        }

        public static void GenerateMultipleTypeDocs(List<ITypeData> typeDataCollection,
            DocGeneratorSettingsSO generatorSettings,
            string targetFolderPath)
        {
            if (typeDataCollection is not { Count: > 0 } || generatorSettings ||
                string.IsNullOrEmpty(targetFolderPath))
            {
                YuumixLogger.OdinToolkitsError("参数无效，无法生成文档");
                return;
            }

            try
            {
                for (var i = 0; i < typeDataCollection.Count; i++)
                {
                    var typeData = typeDataCollection[i];
                    typeData.TryAsIMemberData(out var memberData);
                    var dataTypeName = memberData.Name;

                    EditorUtility.DisplayProgressBar("脚本文档生成", $"正在生成 {dataTypeName} 文档",
                        (float)i / typeDataCollection.Count);

                    ReadDocGeneratorSettingSO(typeData, generatorSettings, targetFolderPath, memberData,
                        out var markdownText, out var filePathWithExtensions);

                    if (File.Exists(filePathWithExtensions))
                    {
                        var readAllLines = File.ReadAllLines(filePathWithExtensions);
                        if (TryGetFrontMatter(readAllLines, out var frontMatter))
                        {
                            markdownText = frontMatter + markdownText;
                        }

                        var additionalDescription = GetAdditionalDescriptionFromExistingFile(readAllLines);
                        if (!string.IsNullOrEmpty(additionalDescription))
                        {
                            var userIdentifierParagraphString = UserIdentifierDescriptionParagraph.ToString();
                            if (markdownText.Contains(userIdentifierParagraphString))
                            {
                                markdownText = markdownText.Replace(userIdentifierParagraphString,
                                    additionalDescription);
                            }
                            else
                            {
                                markdownText += additionalDescription;
                            }
                        }
                    }

                    // 确保目录存在
                    var directoryPath = Path.GetDirectoryName(filePathWithExtensions);
                    if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var utf8WithoutBom = new UTF8Encoding(false);
                    File.WriteAllText(filePathWithExtensions, markdownText, utf8WithoutBom);
                }
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }

            AssetDatabase.Refresh();
            EditorUtility.OpenWithDefaultApp(targetFolderPath);
        }

        static string GetAdditionalDescriptionFromExistingFile(string[] readAllLines)
        {
            if (readAllLines.Length == 0)
            {
                return string.Empty;
            }

            var identifierIndex = Array.FindIndex(readAllLines, line => line.StartsWith(IDENTIFIER_CN));
            if (identifierIndex <= 0)
            {
                return string.Empty;
            }

            var additionalDescriptionStringBuilder = new StringBuilder();
            for (var i = identifierIndex; i < readAllLines.Length; i++)
            {
                additionalDescriptionStringBuilder.AppendLine(readAllLines[i]);
            }

            return additionalDescriptionStringBuilder.ToString();
        }

        #endregion

        #region 辅助方法

        static void ReadDocGeneratorSettingSO(ITypeData typeData, DocGeneratorSettingsSO generatorSettings,
            string targetFolderPath, IMemberData memberData, out string markdownText, out string filePathWithExtensions)
        {
            markdownText = generatorSettings.GetGeneratedDoc(typeData);

            if (generatorSettings.generateIdentifier)
            {
                markdownText = markdownText.EndsWith('\n') || markdownText.EndsWith("\r\n")
                    ? markdownText + UserIdentifierDescriptionParagraph
                    : markdownText + ("\n" + UserIdentifierDescriptionParagraph);
            }

            var fileNameWithoutExtension = memberData.Name.Replace('<', '[').Replace('>', ']');

            if (generatorSettings.generateNamespaceFolder)
            {
                var namespaceString = typeData.NamespaceName;
                if (!string.IsNullOrEmpty(namespaceString))
                {
                    var namespaceFolders = namespaceString.Split('.');
                    targetFolderPath = namespaceFolders.Aggregate(targetFolderPath, Path.Combine);
                }
                else
                {
                    targetFolderPath = Path.Combine(targetFolderPath, "WithoutNamespace");
                }

                Directory.CreateDirectory(targetFolderPath);
            }

            filePathWithExtensions = Path.Combine(targetFolderPath, fileNameWithoutExtension);

            if (generatorSettings.customizeDocFileExtensionName)
            {
                filePathWithExtensions += generatorSettings.docFileExtensionName.EnsureStartsWith(".");
            }
            else
            {
                filePathWithExtensions += ".md";
            }
        }

        static bool TryGetFrontMatter(string[] sourceLines, out string frontMatter)
        {
            var frontMatterStringBuilder = new StringBuilder();

            if (sourceLines.Length == 0 || (sourceLines[0] != "---" && sourceLines[0] != "+++"))
            {
                frontMatter = string.Empty;
                return false;
            }

            frontMatterStringBuilder.AppendLine(sourceLines[0]);

            for (var i = 1; i < sourceLines.Length; i++)
            {
                frontMatterStringBuilder.AppendLine(sourceLines[i]);

                if ((sourceLines[0] == "---" && sourceLines[i] == "---") ||
                    (sourceLines[0] == "+++" && sourceLines[i] == "+++"))
                {
                    frontMatterStringBuilder.AppendLine();
                    break;
                }
            }

            frontMatter = frontMatterStringBuilder.ToString();
            return true;
        }

        #endregion
    }
}
