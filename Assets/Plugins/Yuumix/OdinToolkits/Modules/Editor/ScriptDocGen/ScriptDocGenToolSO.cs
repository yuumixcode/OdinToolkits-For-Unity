using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.Editor;
using YuumixEditor;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class ScriptDocGenToolSO : OdinEditorScriptableSingleton<ScriptDocGenToolSO>, IOdinToolkitsReset
    {
        public static BilingualData ScriptDocGenToolMenuPathData =
            new BilingualData("脚本文档生成工具", "Script Doc Generate Tool");

        public const string DEFAULT_STORAGE_FOLDER_PATH =
            OdinToolkitsPaths.ODIN_TOOLKITS_ANY_DATA_ROOT_FOLDER + "/Editor/ScriptDocGenToolConfigSO";

        public const string DEFAULT_DOC_FOLDER_PATH =
            OdinToolkitsPaths.ODIN_TOOLKITS_ANY_DATA_ROOT_FOLDER + "/Editor/Documents/";

        public const string IDENTIFIER = "## Additional Description";

        static StringBuilder _userIdentifierParagraph = new StringBuilder()
            .AppendLine(IDENTIFIER)
            .AppendLine()
            .AppendLine("> 首个 `" + IDENTIFIER + "` 是标识符，请勿修改标题级别和内容，重新生成文档时将保留标识符之后的内容，可以对某一个成员添加详细说明或者对此类添加额外说明。")
            .AppendLine("> ")
            .AppendLine("> 由 [`Odin Toolkits For Unity`](" + OdinToolkitsWebLinks.GITHUB_REPOSITORY + ") 的文档工具辅助生成");

        public static event Action<ToastPosition, SdfIconType, string, Color, float> ToastEvent;

        [PropertyOrder(-5)]
        public BilingualHeaderWidget header = new BilingualHeaderWidget(
            ScriptDocGenToolMenuPathData.GetChinese(),
            ScriptDocGenToolMenuPathData.GetEnglish(),
            "根据配置的 `Type` 对象，生成 Markdown 格式的文档，可选 Scripting API 或者 Complete References，默认支持 MkDocs-Material。Scripting API 表示对外的，用户可以调用的程序接口文档，Complete References 表示包含所有成员的参考文档",
            "Based on the value of the configured `Type`, generate a Markdown format document., generate a document in the format of Markdown, optional Scripting API and Complete References, and MkDocs-Material is supported by default. Scripting API refers to the external, user-accessible interface documentation, and Complete References refers to the documentation containing all members"
        );

        [PropertyOrder(1)]
        [EnumToggleButtons]
        [BilingualTitle("文档种类", "Document Category")]
        [HideLabel]
        public DocCategory docCategory = DocCategory.ScriptingAPI;

        [PropertyOrder(1)]
        [EnumToggleButtons]
        [BilingualTitle("Markdown 样式自定义选择", "Markdown Style Selector")]
        [HideLabel]
        public MarkdownStyleSO markdownStyle;

        [PropertyOrder(2)]
        [BilingualTitle("生成文档的文件夹路径", "Folder Path For Doc")]
        [HideLabel]
        [FolderPath(AbsolutePath = true)]
        [InlineButton(nameof(ResetDocFolderPath), SdfIconType.ArrowClockwise, "")]
        [CustomContextMenu("Reset Default", nameof(ResetDocFolderPath))]
        public string folderPath;

        [PropertyOrder(8)]
        [EnumToggleButtons]
        [HideLabel]
        [BilingualTitle("工具使用模式", "Tool Usage Mode")]
        [BilingualInfoBox("目前为单个类型生成单个文档模式", "Single Type To Single Document Mode",
            visibleIf: nameof(IsSingleType))]
        [BilingualInfoBox("目前为多个类型生成多个文档模式", "Multiple Types To Multiple Documents Mode",
            visibleIf: nameof(IsMultipleTypes))]
        [BilingualInfoBox("目前为单个程序集生成多个文档模式", "Single Assembly To Multiple Documents Mode",
            visibleIf: nameof(IsSingleAssembly))]
        [BilingualInfoBox("目前为多个程序集生成多个文档模式", "Multiple Assemblies To Multiple Documents Mode",
            visibleIf: nameof(IsMultipleAssemblies))]
        public ToolUsageMode toolUsageMode;

        bool IsSingleType => toolUsageMode == ToolUsageMode.SingleType;
        bool IsMultipleTypes => toolUsageMode == ToolUsageMode.MultipleTypes;
        bool IsSingleAssembly => toolUsageMode == ToolUsageMode.SingleAssembly;
        bool IsMultipleAssemblies => toolUsageMode == ToolUsageMode.MultipleAssemblies;

        [PropertyOrder(9)]
        [ShowInInspector]
        public HorizontalSeparateEditorWidget SeparateEditor => new HorizontalSeparateEditorWidget(2, 10, 3);

        [PropertyOrder(10)]
        [ShowIf("IsSingleType")]
        [BilingualTitle("单个 Type 配置", "Single Type Config")]
        [HideLabel]
        public Type TargetType;

        [PropertyOrder(10)]
        [ShowIf("IsMultipleTypes")]
        [LabelWidth(200f)]
        [BilingualText("类型列表配置资源", "TypeList Config SO")]
        [BilingualInfoBox("TypeListConfigSO 配置资源不为空时，会强制覆盖 TempTypeList",
            "When the TypeListConfigSO asset is not empty, TempTypeList is forced to be overridden")]
        public TypeListConfigSO typeListConfig;

        [PropertyOrder(15)]
        [ShowIf("CanShowTemporaryTypes")]
        [BilingualTitle("临时 Type 列表", "Temporary Types List Config")]
        [ListDrawerSettings(OnTitleBarGUI = nameof(DrawTemporaryTypesTitleBarGUI), NumberOfItemsPerPage = 5)]
        [HideLabel]
        public List<Type> TemporaryTypes = new List<Type>();

        bool CanShowTemporaryTypes => IsMultipleTypes && !typeListConfig;

        void DrawTemporaryTypesTitleBarGUI()
        {
            Texture2D image =
                SdfIcons.CreateTransparentIconTexture(SdfIconType.SaveFill, Color.white, 16 /*0x10*/, 16 /*0x10*/,
                    0);
            var content = new GUIContent(" 保存为SO资源 ", image,
                "保存为 " + nameof(TypeListConfigSO) + " 资源到 " + storageConfigSOFolderPath);
            string filePath = storageConfigSOFolderPath + "/" + nameof(TypeListConfigSO) + ".asset";
            if (TemporaryTypes.Count > 0)
            {
                if (SirenixEditorGUI.ToolbarButton(content))
                {
                    var so = CreateInstance<TypeListConfigSO>();
                    PathEditorUtility.EnsureFolderRecursively(storageConfigSOFolderPath);
                    so.Types = TemporaryTypes;
                    AssetDatabase.CreateAsset(so, filePath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                    ProjectEditorUtility.PingAndSelectAsset(filePath);
                    YuumixLogger.OdinToolkitsLog("请更改资源名称，避免下次生成时覆盖内容");
                }
            }

            Texture2D image2 =
                SdfIcons.CreateTransparentIconTexture(SdfIconType.GearFill, Color.white, 16 /*0x10*/, 16 /*0x10*/,
                    0);
            var content2 = new GUIContent(" 自定义资源存储位置 ", image2, "当前路径为 " + storageConfigSOFolderPath);
            if (_customizingSaveConfig)
            {
                return;
            }

            if (SirenixEditorGUI.ToolbarButton(content2))
            {
                _customizingSaveConfig = true;
            }
        }

        [PropertyOrder(10)]
        [ShowIf("IsSingleAssembly")]
        [BilingualTitle("单个程序集配置", "Single Assembly Config")]
        [HideLabel]
        [ValueDropdown("GetAssemblyString")]
        public string targetAssemblyString;

        [PropertyOrder(10)]
        [ShowIf("IsMultipleAssemblies")]
        [LabelWidth(200f)]
        [BilingualText("程序集配置列表", "Assembly List Config SO")]
        [BilingualInfoBox("AssemblyListConfigSO 配置资源不为空时，会强制覆盖 TemporaryAssemblies",
            "When the AssemblyListConfigSO asset is not empty, TemporaryAssemblies is forced to be overridden")]
        public AssemblyListConfigSO assemblyListConfig;

        bool CanShowTemporaryAssemblies => IsMultipleAssemblies && !assemblyListConfig;

        [ShowIf("CanShowTemporaryAssemblies")]
        [BilingualTitle("临时 Type 列表", "Temporary Types List Config")]
        [ListDrawerSettings(OnTitleBarGUI = nameof(DrawTemporaryAssembliesStringTitleBarGUI), NumberOfItemsPerPage = 5)]
        [HideLabel]
        [PropertyOrder(15)]
        [ShowIf("IsMultipleAssemblies")]
        [ValueDropdown("GetAssemblyString", IsUniqueList = true, ExcludeExistingValuesInList = true)]
        public List<string> temporaryAssembliesString = new List<string>();

        void DrawTemporaryAssembliesStringTitleBarGUI()
        {
            Texture2D image =
                SdfIcons.CreateTransparentIconTexture(SdfIconType.SaveFill, Color.white, 16 /*0x10*/, 16 /*0x10*/,
                    0);
            var content = new GUIContent(" 保存为SO资源 ", image,
                "保存为 " + nameof(AssemblyListConfigSO) + " 资源到 " + storageConfigSOFolderPath);
            string filePath = storageConfigSOFolderPath + "/" + nameof(AssemblyListConfigSO) + ".asset";
            if (temporaryAssembliesString.Count > 0)
            {
                if (SirenixEditorGUI.ToolbarButton(content))
                {
                    var so = CreateInstance<AssemblyListConfigSO>();
                    PathEditorUtility.EnsureFolderRecursively(storageConfigSOFolderPath);
                    foreach (string assemblyString in temporaryAssembliesString)
                    {
                        Assembly assembly = Assembly.Load(assemblyString);
                        so.AssemblyStringTypesMap.TryAdd(assemblyString, assembly.GetTypes().ToList());
                    }

                    AssetDatabase.CreateAsset(so, filePath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                    ProjectEditorUtility.PingAndSelectAsset(filePath);
                    YuumixLogger.OdinToolkitsLog("请更改资源名称，避免下次生成时覆盖内容");
                }
            }

            Texture2D image2 =
                SdfIcons.CreateTransparentIconTexture(SdfIconType.GearFill, Color.white, 16 /*0x10*/, 16 /*0x10*/,
                    0);
            var content2 = new GUIContent(" 自定义资源存储位置 ", image2, "当前路径为 " + storageConfigSOFolderPath);
            if (_customizingSaveConfig)
            {
                return;
            }

            if (SirenixEditorGUI.ToolbarButton(content2))
            {
                _customizingSaveConfig = true;
            }
        }

        static ValueDropdownList<string> _currentDomainAssemblies;

        ValueDropdownList<string> GetAssemblyString()
        {
            if (_currentDomainAssemblies != null &&
                (_currentDomainAssemblies != null || _currentDomainAssemblies.Count > 0))
            {
                return _currentDomainAssemblies;
            }

            _currentDomainAssemblies = new ValueDropdownList<string>();
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                _currentDomainAssemblies.Add(assembly.GetName().Name, assembly.FullName);
            }

            return _currentDomainAssemblies;
        }

        bool _customizingSaveConfig;

        bool ShowSaveFolderPath => _customizingSaveConfig &&
                                   (
                                       (IsMultipleTypes && !typeListConfig) ||
                                       (IsMultipleAssemblies && !assemblyListConfig)
                                   );

        BilingualData _resetSOSaveFolderPathButtonLabel =
            new BilingualData("重置路径", "Reset SO Folder Path");

        BilingualData _completeConfigButtonLabel = new BilingualData("完成配置", "Complete Config");

        public void CompleteConfig()
        {
            _customizingSaveConfig = false;
        }

        [PropertyOrder(12)]
        [FolderPath]
        [ShowIf("ShowSaveFolderPath")]
        [InlineButton("CompleteConfig", SdfIconType.Check, "$_completeConfigButtonLabel")]
        [InlineButton("ResetSOSaveFolderPath", SdfIconType.ArrowClockwise, "$_resetSOSaveFolderPathButtonLabel")]
        [BilingualText("存放配置资源的文件夹路径", "Storage Config SO Folder Path")]
        [CustomContextMenu("Reset Default", nameof(ResetSOSaveFolderPath))]
        [LabelWidth(200f)]
        public string storageConfigSOFolderPath = DEFAULT_STORAGE_FOLDER_PATH;

        [PropertyOrder(50)]
        [BilingualTitle("解析按钮", "Analyze Button")]
        [BilingualButton("根据当前模式执行解析", "Analyze based on the current mode", ButtonSizes.Large, ButtonStyle.Box,
            SdfIconType.FileEarmarkPlus)]
        public void AnalyzeButton()
        {
            switch (toolUsageMode)
            {
                case ToolUsageMode.SingleType:
                    SingleTypeAnalyze();
                    break;
                case ToolUsageMode.MultipleTypes:
                    MultipleTypesAnalyze();
                    break;
                case ToolUsageMode.SingleAssembly:
                    SingleAssemblyAnalyze();
                    break;
                case ToolUsageMode.MultipleAssemblies:
                    MultipleAssembliesAnalyze();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _hasFinishedAnalyze = true;
            RaiseToastEvent(ToastPosition.BottomRight, SdfIconType.LightningFill,
                "正在解析目标类型，请勿重复点击！等待文档生成按钮显示",
                Color.yellow, 5f);
        }

        bool CanShowGenerateButton => _hasFinishedAnalyze && (
            (IsSingleType && TargetType != null) ||
            (IsMultipleTypes && (typeListConfig || TemporaryTypes.Count > 0)) ||
            (IsSingleAssembly && targetAssemblyString != null) ||
            (IsMultipleAssemblies && (assemblyListConfig || temporaryAssembliesString.Count > 0))
        );

        [PropertyOrder(70)]
        [ShowIf("CanShowGenerateButton")]
        [BilingualTitle("生成按钮", "Generate Button")]
        [BilingualButton("根据解析结果生成 Markdown 文档", "Generate Markdown Document Based On Analysis Result",
            ButtonSizes.Large, ButtonStyle.Box, SdfIconType.FileEarmarkPlus)]
        public void GenerateButton()
        {
            if (!Directory.Exists(folderPath))
            {
                if (!EditorUtility.DisplayDialog("自动路径补全提示", "当前的文档导出路径不存在，是否自动生成文件夹路径？", "确认", "取消"))
                {
                    return;
                }

                PathEditorUtility.EnsureFolderRecursively(folderPath);
            }

            switch (toolUsageMode)
            {
                case ToolUsageMode.SingleType:
                    SingleTypeGenerate();
                    break;
                case ToolUsageMode.MultipleTypes:
                    MultipleTypesGenerate(typeDataList);
                    break;
                case ToolUsageMode.SingleAssembly:
                    SingleAssemblyGenerate(typeDataList);
                    break;
                case ToolUsageMode.MultipleAssemblies:
                    MultipleAssembliesGenerate(typeDataList);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _hasFinishedAnalyze = false;
        }

        [PropertyOrder(100)]
        [BilingualTitle("过程数据", "Process Data")]
        [OnInspectorGUI]
        public void ProcessorTitle() { }

        [PropertyOrder(105)]
        [ShowIf("IsSingleType")]
        [ReadOnly]
        public TypeData typeData;

        bool CanShowTypeDataList => IsMultipleTypes || IsSingleAssembly || IsMultipleAssemblies;

        [PropertyOrder(105)]
        [ShowIf("CanShowTypeDataList")]
        [ReadOnly]
        public List<TypeData> typeDataList = new List<TypeData>();

        bool _hasFinishedAnalyze;

        #region Analyze

        void SingleTypeAnalyze()
        {
            if (TargetType == null)
            {
                YuumixLogger.OdinToolkitsError("请选择有效的目标类型");
                return;
            }

            typeData = TypeDataAnalyzer.Analyze(TargetType);
        }

        void MultipleTypesAnalyze()
        {
            if (!typeListConfig && TemporaryTypes.Count <= 0)
            {
                YuumixLogger.OdinToolkitsError("设置有效的 Type 对象列表");
                return;
            }

            typeDataList = new List<TypeData>();
            if (typeListConfig)
            {
                typeListConfig.Types.RemoveAll(x => x == null);
                foreach (Type type in typeListConfig.Types)
                {
                    typeDataList.Add(TypeData.FromType(type));
                }
            }
            else
            {
                TemporaryTypes.RemoveAll(x => x == null);
                foreach (Type type in TemporaryTypes)
                {
                    typeDataList.Add(TypeData.FromType(type));
                }
            }
        }

        void SingleAssemblyAnalyze()
        {
            typeDataList = new List<TypeData>();
            Assembly targetAssembly = Assembly.Load(targetAssemblyString);
            if (targetAssembly == null)
            {
                YuumixLogger.OdinToolkitsError("请选择有效的目标程序集");
                return;
            }

            // Debug.Log(targetAssembly);
            foreach (Type type in targetAssembly.GetTypes()
                         .Where(t => t.GetCustomAttribute<CompilerGeneratedAttribute>() == null))
            {
                typeDataList.Add(TypeData.FromType(type));
            }
        }

        void MultipleAssembliesAnalyze()
        {
            if (!assemblyListConfig && temporaryAssembliesString.Count <= 0)
            {
                YuumixLogger.OdinToolkitsError("设置有效的 Assembly String 对象列表");
                return;
            }

            typeDataList = new List<TypeData>();
            if (assemblyListConfig)
            {
                foreach (KeyValuePair<string, List<Type>> variable in assemblyListConfig.AssemblyStringTypesMap)
                {
                    variable.Value.RemoveAll(x => x == null);
                    variable.Value.RemoveAll(t => t.GetCustomAttribute<CompilerGeneratedAttribute>() != null);
                }

                foreach (Type type in assemblyListConfig.AssemblyStringTypesMap.SelectMany(variable => variable.Value))
                {
                    typeDataList.Add(TypeData.FromType(type));
                }
            }
            else
            {
                foreach (string assemblyString in temporaryAssembliesString)
                {
                    Assembly targetAssembly = Assembly.Load(assemblyString);
                    if (targetAssembly == null)
                    {
                        continue;
                    }

                    foreach (Type type in targetAssembly.GetTypes()
                                 .Where(t => t.GetCustomAttribute<CompilerGeneratedAttribute>() == null))
                    {
                        typeDataList.Add(TypeData.FromType(type));
                    }
                }
            }
        }

        #endregion

        #region Generate

        void SingleTypeGenerate()
        {
            if (typeData.IsObsolete)
            {
                if (!EditorUtility.DisplayDialog("警告提示", "此类已经被标记为过时，继续生成文档吗？", "确认", "取消"))
                {
                    return;
                }
            }

            string markdownText = markdownStyle.GetMarkdownText(typeData, docCategory, _userIdentifierParagraph);
            string fileNameWithoutExtension = TargetType.Name.Replace('<', '[').Replace('>', ']');
            string filePathWithExtensions = folderPath + "/" + fileNameWithoutExtension + ".md";
            if (docCategory == DocCategory.CompleteReferences)
            {
                filePathWithExtensions = folderPath + "/" + fileNameWithoutExtension + ".complete.md";
            }

            if (File.Exists(filePathWithExtensions))
            {
                if (!EditorUtility.DisplayDialog("提示",
                        "已经存在该文档，继续生成将覆盖部分内容，保留首个 " + IDENTIFIER + " 之后的内容，是否继续生成？", "确认", "取消"))
                {
                    return;
                }

                string[] readAllLines = File.ReadAllLines(filePathWithExtensions);
                var additionalDescriptionStringBuilder = new StringBuilder();
                if (readAllLines.Length > 0)
                {
                    // 首次出现的标记
                    int identifierIndex = Array.FindIndex(readAllLines, line => line.StartsWith(IDENTIFIER));
                    if (identifierIndex > 0)
                    {
                        for (int i = identifierIndex; i < readAllLines.Length; i++)
                        {
                            additionalDescriptionStringBuilder.AppendLine(readAllLines[i]);
                        }

                        var additionalDescription = additionalDescriptionStringBuilder.ToString();
                        var userIdentifierParagraphString = _userIdentifierParagraph.ToString();
                        markdownText =
                            markdownText.Replace(userIdentifierParagraphString, additionalDescription);
                    }
                }
            }

            File.WriteAllText(filePathWithExtensions,
                markdownText,
                Encoding.UTF8);
            AssetDatabase.Refresh();
            EditorUtility.OpenWithDefaultApp(filePathWithExtensions);
        }

        void MultipleTypesGenerate(List<TypeData> typeDataCollection)
        {
            if (typeDataCollection.Count <= 0)
            {
                return;
            }

            try
            {
                for (var i = 0; i < typeDataCollection.Count; i++)
                {
                    TypeData data = typeDataCollection[i];
                    string dataTypeName = data.TypeName;
                    EditorUtility.DisplayProgressBar("脚本文档生成", $"正在生成 {dataTypeName} 文档",
                        (float)i / typeDataCollection.Count);
                    string markdownText = markdownStyle.GetMarkdownText(data, docCategory, _userIdentifierParagraph);
                    string fileNameWithoutExtension = dataTypeName.Replace('<', '[').Replace('>', ']');
                    string filePathWithExtensions = folderPath + "/" + fileNameWithoutExtension + ".md";
                    if (docCategory == DocCategory.CompleteReferences)
                    {
                        filePathWithExtensions = folderPath + "/" + fileNameWithoutExtension + ".complete.md";
                    }

                    if (File.Exists(filePathWithExtensions))
                    {
                        string[] readAllLines = File.ReadAllLines(filePathWithExtensions);
                        var additionalDescriptionStringBuilder = new StringBuilder();
                        if (readAllLines.Length > 0)
                        {
                            // 首次出现的标记
                            int identifierIndex = Array.FindIndex(readAllLines, line => line.StartsWith(IDENTIFIER));
                            if (identifierIndex > 0)
                            {
                                for (int j = identifierIndex; j < readAllLines.Length; j++)
                                {
                                    additionalDescriptionStringBuilder.AppendLine(readAllLines[j]);
                                }

                                var additionalDescription = additionalDescriptionStringBuilder.ToString();
                                var userIdentifierParagraphString = _userIdentifierParagraph.ToString();
                                markdownText =
                                    markdownText.Replace(userIdentifierParagraphString, additionalDescription);
                            }
                        }
                    }

                    File.WriteAllText(filePathWithExtensions,
                        markdownText,
                        Encoding.UTF8);
                }
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }

            AssetDatabase.Refresh();
            EditorUtility.OpenWithDefaultApp(folderPath);
        }

        void SingleAssemblyGenerate(List<TypeData> typeDataCollection) => MultipleTypesGenerate(typeDataCollection);
        void MultipleAssembliesGenerate(List<TypeData> typeDataCollection) => MultipleTypesGenerate(typeDataCollection);

        #endregion

        [PropertyOrder(120)]
        public BilingualFooterWidget footer = new BilingualFooterWidget(
            "2025/07/22",
            new[]
            {
                new BilingualData("目前支持构造函数，方法，属性，事件，字段，运算符重载成员分析",
                    "Currently supports constructor, method, property, event, field, operator overloading member analysis"),
                new BilingualData(ChangeCategory.Fixed + "修复批量生成模式的数组越界问题-2025/07/01",
                    ChangeCategory.Fixed +
                    "Fix the array out-of-bounds issue in batch generation mode.-2025/07/01"),
                new BilingualData(ChangeCategory.Added + "依据程序集生成文档 - 2025/07/22",
                    ChangeCategory.Added +
                    "Generate documentation based on the assembly. - 2025/07/22")
            });

        static void RaiseToastEvent(ToastPosition position, SdfIconType icon, string msg,
            Color color, float duration)
        {
            ToastEvent?.Invoke(position, icon, msg, color, duration);
        }

        #region OdinToolkitsReset

        public void OdinToolkitsReset()
        {
            toolUsageMode = ToolUsageMode.SingleType;
            docCategory = DocCategory.ScriptingAPI;
            markdownStyle = Resources.Load<MkDocsMaterialStyleSO>("MkDocsMaterialStyle");
            ResetDocFolderPath();
            TargetType = null;
            typeListConfig = null;
            TemporaryTypes = new List<Type>();
            targetAssemblyString = null;
            assemblyListConfig = null;
            temporaryAssembliesString = new List<string>();
            typeData = null;
            typeDataList = new List<TypeData>();
            _hasFinishedAnalyze = false;
            _customizingSaveConfig = false;
            ResetSOSaveFolderPath();
        }

        public void ResetSOSaveFolderPath()
        {
            storageConfigSOFolderPath = DEFAULT_STORAGE_FOLDER_PATH;
        }

        public void ResetDocFolderPath()
        {
            folderPath = DEFAULT_DOC_FOLDER_PATH;
        }

        #endregion
    }
}
