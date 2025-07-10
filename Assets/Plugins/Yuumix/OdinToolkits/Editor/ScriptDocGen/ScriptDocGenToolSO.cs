using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Yuumix.OdinToolkits.Editor.Common;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common;
using Yuumix.OdinToolkits.Core;
using YuumixEditor;

namespace Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Editor
{
    public class ScriptDocGenToolSO : OdinEditorScriptableSingleton<ScriptDocGenToolSO>, IOdinToolkitsReset
    {
        public static MultiLanguageData ScriptDocGenToolMenuPathData =
            new MultiLanguageData("脚本文档生成工具", "Script Doc Generate Tool");

        public const string DefaultStorageFolderPath =
            OdinToolkitsPaths.ODIN_TOOLKITS_ANY_DATA_ROOT_FOLDER + "/TypeListConfigSO";

        public const string DefaultDocFolderPath =
            OdinToolkitsPaths.ODIN_TOOLKITS_ANY_DATA_ROOT_FOLDER + "/Editor/Documents/";

        public const string IdentifierTitle = "## Additional Description";

        static StringBuilder _userIdentifier = new StringBuilder()
            .AppendLine(IdentifierTitle)
            .AppendLine()
            .AppendLine("- `" + IdentifierTitle + "` 是标识符，由 `Odin Toolkits` 文档生成工具自动生成，请勿修改标题级别和内容。")
            .AppendLine("- `" + IdentifierTitle + "` 之后的内容将不会被覆盖，可以对文档补充说明。");

        public static event Action<ToastPosition, SdfIconType, string, Color, float> OnToastEvent;

        [PropertyOrder(-5)]
        public MultiLanguageHeaderWidget header = new MultiLanguageHeaderWidget(
            ScriptDocGenToolMenuPathData.GetChinese(),
            ScriptDocGenToolMenuPathData.GetEnglish(),
            "给定一个 `Type` 类型的值，生成 Markdown 格式的文档，可选 Scripting API 或者 Complete References，默认支持 MkDocs-Material。Scripting API 表示对外的，用户可以调用的程序接口文档，Complete References 表示包含所有成员的参考文档",
            "Given a value of type `Type`, generate a document in the format of Markdown, optional Scripting API and Complete References, and MkDocs-Material is supported by default. Scripting API refers to the external, user-accessible interface documentation, and Complete References refers to the documentation containing all members"
        );

        [PropertyOrder(0)]
        [OnInspectorGUI]
        [MultiLanguageTitle("工具使用模式", "Tool Usage Mode ")]
        public void FirstTitle() { }

        [PropertyOrder(1)]
        [HorizontalGroup("Mode", 0.75f)]
        [BoxGroup("Mode/1", false)]
        [ShowIf("_singleDoc", false)]
        [MultiLanguageDisplayAsStringWidgetConfig(fontSize: 14)]
        [GUIColor("green")]
        public MultiLanguageDisplayAsStringWidget singleMode = new MultiLanguageDisplayAsStringWidget(
            "单文档生成模式",
            "Single Document Generation Mode"
        );

        [PropertyOrder(1)]
        [HorizontalGroup("Mode", 0.75f)]
        [BoxGroup("Mode/1", false)]
        [HideIf("_singleDoc", false)]
        [MultiLanguageDisplayAsStringWidgetConfig(fontSize: 14)]
        [GUIColor("green")]
        public MultiLanguageDisplayAsStringWidget multiMode = new MultiLanguageDisplayAsStringWidget(
            "多文档批量生成模式",
            "Multi-Documents Batch Generation Mode"
        );

        [PropertyOrder(1)]
        [HorizontalGroup("Mode", 0.24f)]
        [MultiLanguageButton("切换工具模式", "Switch Tool Mode", buttonHeight: 24, icon: SdfIconType.Hurricane)]
        static void SwitchToolMode()
        {
            Instance._singleDoc = !Instance._singleDoc;
            RaiseOnToastEvent(ToastPosition.BottomRight, SdfIconType.InfoSquareFill,
                Instance._singleDoc ? "成功切换为单文档生成模式" : "成功切换为多文档批量生成模式",
                Color.white, 5f);
        }

        [PropertyOrder(1)]
        [EnumToggleButtons]
        [MultiLanguageTitle("文档种类", "Document Category")]
        [HideLabel]
        public DocCategory docCategory = DocCategory.ScriptingAPI;

        [PropertyOrder(1)]
        [EnumToggleButtons]
        [MultiLanguageTitle("Markdown 格式适配选择", "Markdown Format Selector")]
        [HideLabel]
        public MarkdownCategory markdownCategory = MarkdownCategory.MkDocsMaterial;

        [PropertyOrder(1)]
        [MultiLanguageTitle("目标类型", "Target Type")]
        [OnInspectorGUI]
        public void TypeTitle() { }

        [PropertyOrder(2)]
        [ShowIf("_singleDoc")]
        [HideLabel]
        public Type TargetType;

        [PropertyOrder(2)]
        [HideIf("_singleDoc")]
        [LabelWidth(200f)]
        [MultiLanguageText("类型列表配置资源", "TypeList Config SO")]
        [MultiLanguageInfoBox("TypeListConfigSO 配置资源不为空时，会强制覆盖 TempTypeList",
            "When the TypeListConfigSO asset is not empty, TempTypeList is forced to be overridden")]
        public TypeListConfigSO typeListConfig;

        [PropertyOrder(3)]
        [FolderPath]
        [MultiLanguageBoxGroup("GroupA", "临时类型列表", "Temp Type List", true)]
        [ShowIf(nameof(ShowSaveFolderPath))]
        [InlineButton("CompleteConfig", SdfIconType.Check, "确认")]
        [InlineButton("ResetSOSaveFolderPath", SdfIconType.ArrowClockwise, "")]
        [MultiLanguageText("存放类型列表配置资源的文件夹路径", "Storage TypeListConfigSO Folder Path")]
        [CustomContextMenu("Reset Default", nameof(ResetSOSaveFolderPath))]
        [LabelWidth(200f)]
        public string storageTypeListConfigSOFolderPath = DefaultStorageFolderPath;

        [PropertyOrder(5)]
        [MultiLanguageBoxGroup("GroupA", "临时类型列表", "Temp Type List", true)]
        [HideIf("HideTempTypeList")]
        [HideLabel]
        [ListDrawerSettings(OnTitleBarGUI = nameof(SaveAsTypeListConfigSO), NumberOfItemsPerPage = 10)]
        public List<Type> TempTypeList = new List<Type>();

        [PropertyOrder(10)]
        [MultiLanguageTitle("生成文档的文件夹路径", "Folder Path For Doc")]
        [HideLabel]
        [FolderPath(AbsolutePath = true)]
        [InlineButton(nameof(ResetDocFolderPath), SdfIconType.ArrowClockwise, "")]
        [CustomContextMenu("Reset Default", nameof(ResetDocFolderPath))]
        public string folderPath;

        [PropertyOrder(19)]
        [MultiLanguageTitle("解析操作按钮", "Analyze Button")]
        [OnInspectorGUI]
        public void AnalyzeTitle() { }

        [PropertyOrder(20)]
        [ShowIf("_singleDoc")]
        [MultiLanguageButton("解析单个 Type", "Analyze Single Type",
            ButtonSizes.Large, ButtonStyle.Box, SdfIconType.Activity)]
        static void AnalyzeSingle()
        {
            if (Instance.TargetType == null)
            {
                Debug.LogError("请选择有效的目标类型");
                return;
            }

            Instance.typeData = TypeData.FromType(Instance.TargetType);
            Instance._thisHasAnalyzedSingle = true;
            RaiseOnToastEvent(ToastPosition.BottomRight, SdfIconType.LightningFill,
                "正在解析目标类型，请勿重复点击！等待文档生成按钮显示",
                Color.yellow, 5f);
        }

        [PropertyOrder(20)]
        [HideIf("_singleDoc")]
        [MultiLanguageButton("解析 Type 列表", "Analyze Type List",
            ButtonSizes.Large, ButtonStyle.Box, SdfIconType.Activity)]
        static void AnalyzeMultiple()
        {
            Instance.typeDataList.Clear();
            List<Type> targetTypes = Instance.typeListConfig ? Instance.typeListConfig.Types : Instance.TempTypeList;
            targetTypes.RemoveAll(x => x == null);
            foreach (TypeData typeData in targetTypes.Select(TypeData.FromType))
            {
                Instance.typeDataList.Add(typeData);
            }

            RaiseOnToastEvent(ToastPosition.BottomRight, SdfIconType.LightningFill,
                "正在解析目标类型，请勿重复点击！等待文档生成按钮显示",
                Color.yellow, 5f);
            Instance._thisHasAnalyzedMultiple = true;
        }

        [PropertyOrder(24)]
        [MultiLanguageTitle("生成文档按钮", "Generate Document Buttons")]
        [ShowIf("CanShowGenerateTitle")]
        [OnInspectorGUI]
        public void GenerateTitle() { }

        [PropertyOrder(25)]
        [ShowIf(nameof(CanGeneratedSingle))]
        [MultiLanguageButton("生成 Markdown 文档", "Generate Markdown Document",
            ButtonSizes.Large, ButtonStyle.Box, SdfIconType.FileEarmarkPlus)]
        public void GenerateSingle()
        {
            GenerateMkDocsSingle();
        }

        [PropertyOrder(25)]
        [ShowIf(nameof(CanGeneratedMultiple))]
        [MultiLanguageButton("批量生成 Markdown 文档", "Generate Multiple Markdown Documents",
            ButtonSizes.Large, ButtonStyle.Box, SdfIconType.FileEarmarkPlus)]
        public void GenerateMultiple()
        {
            GenerateMkDocsMultiple();
        }

        [PropertyOrder(100)]
        [MultiLanguageTitle("过程数据", "Process Data")]
        [OnInspectorGUI]
        public void Title() { }

        [PropertyOrder(105)]
        [ShowIf("_singleDoc")]
        public TypeData typeData;

        [PropertyOrder(105)]
        [HideIf("_singleDoc")]
        [ListDrawerSettings(HideAddButton = true, NumberOfItemsPerPage = 5)]
        public List<TypeData> typeDataList = new List<TypeData>();

        #region Footer

        [PropertyOrder(120)]
        public MultiLanguageFooterWidget footer = new MultiLanguageFooterWidget(
            "2025/07/01",
            new[]
            {
                new MultiLanguageData("目前支持构造函数，方法，属性，事件，字段，运算符重载成员分析",
                    "Currently supports constructor, method, property, event, field, operator overloading member analysis"),
                new MultiLanguageData(VersionSpecialString.Fixed + "修复批量生成模式的数组越界问题-2025/07/01",
                    VersionSpecialString.Fixed +
                    "Fix the array out-of-bounds issue in batch generation mode.-2025/07/01")
            });

        #endregion

        bool _thisHasAnalyzedSingle;
        bool _thisHasAnalyzedMultiple;
        bool _customizingSaveConfig;
        bool _singleDoc = true;

        bool ShowSaveFolderPath => _customizingSaveConfig && !_singleDoc && !typeListConfig;
        bool HideTempTypeList => _singleDoc || typeListConfig;
        bool CanShowGenerateTitle => CanGeneratedSingle || CanGeneratedMultiple;
        bool CanGeneratedSingle => _thisHasAnalyzedSingle && _singleDoc;
        bool CanGeneratedMultiple => _thisHasAnalyzedMultiple && !_singleDoc;

        #region OdinToolkitsReset

        public void OdinToolkitsReset()
        {
            _singleDoc = true;
            docCategory = DocCategory.ScriptingAPI;
            markdownCategory = MarkdownCategory.MkDocsMaterial;
            TargetType = null;
            folderPath = DefaultDocFolderPath;
            typeData = new TypeData();
            _thisHasAnalyzedSingle = false;
            typeDataList.Clear();
            _thisHasAnalyzedMultiple = false;
            typeListConfig = null;
            _customizingSaveConfig = false;
            TempTypeList = new List<Type>();
            ResetSOSaveFolderPath();
            ResetDocFolderPath();
        }

        public void ResetSOSaveFolderPath()
        {
            storageTypeListConfigSOFolderPath = DefaultStorageFolderPath;
        }

        public void ResetDocFolderPath()
        {
            folderPath = DefaultDocFolderPath;
        }

        #endregion

        public void CompleteConfig()
        {
            _customizingSaveConfig = false;
        }

        void SaveAsTypeListConfigSO()
        {
            Texture2D image =
                SdfIcons.CreateTransparentIconTexture(SdfIconType.SaveFill, Color.white, 16 /*0x10*/, 16 /*0x10*/,
                    0);
            var content = new GUIContent(" 保存为SO资源 ", image,
                "保存为 " + nameof(TypeListConfigSO) + " 到 " + storageTypeListConfigSOFolderPath);
            string filePath = storageTypeListConfigSOFolderPath + "/" + typeof(TypeListConfigSO) + ".asset";
            if (TempTypeList.Count > 0)
            {
                if (SirenixEditorGUI.ToolbarButton(content))
                {
                    var so = CreateInstance<TypeListConfigSO>();
                    PathEditorUtil.EnsureFolderRecursively(storageTypeListConfigSOFolderPath);
                    so.Types = TempTypeList;
                    AssetDatabase.CreateAsset(so, filePath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                    ProjectEditorUtil.PingAndSelectAsset(filePath);
                }
            }

            Texture2D image2 =
                SdfIcons.CreateTransparentIconTexture(SdfIconType.GearFill, Color.white, 16 /*0x10*/, 16 /*0x10*/,
                    0);
            var content2 = new GUIContent(" 自定义资源存储位置 ", image2, "当前路径为 " + storageTypeListConfigSOFolderPath);
            if (_customizingSaveConfig)
            {
                return;
            }

            if (SirenixEditorGUI.ToolbarButton(content2))
            {
                _customizingSaveConfig = true;
            }
        }

        #region GenerateMkDocsFile

        static void GenerateMkDocsSingle()
        {
            DocCategory docCategory = Instance.docCategory;
            MarkdownCategory markdownCategory = Instance.markdownCategory;
            TypeData typeData = Instance.typeData;
            string folderPath = Instance.folderPath;
            Type targetType = Instance.TargetType;
            if (!Directory.Exists(folderPath))
            {
                if (!EditorUtility.DisplayDialog("自动路径补全提示", "当前的文档导出路径不存在，是否自动生成？", "确认", "取消"))
                {
                    return;
                }

                PathEditorUtil.EnsureFolderRecursively(folderPath);
            }

            if (typeData.IsObsolete)
            {
                if (!EditorUtility.DisplayDialog("警告提示", "此类已经被标记为过时，继续生成文档吗？", "确认", "取消"))
                {
                    return;
                }
            }

            if (markdownCategory == MarkdownCategory.MkDocsMaterial)
            {
                StringBuilder headerIntroduction = CreateHeaderIntroductionMkDocs(typeData);
                StringBuilder constructorsContent = CreateConstructorsContentMkDocs(typeData, docCategory);
                StringBuilder fieldsContent = CreateCurrentFieldsContentMkDocs(typeData, docCategory);
                StringBuilder methodsContent = CreateCurrentMethodsContentMkDocs(typeData, docCategory);
                StringBuilder propertiesContent = CreateCurrentPropertiesContentMkDocs(typeData, docCategory);
                StringBuilder eventsContent = CreateCurrentEventsContentMkDocs(typeData, docCategory);
                StringBuilder inheritedContent = CreateInheritedContentMkDocs(typeData, docCategory);
                StringBuilder finalStringBuilder = headerIntroduction
                    .Append(constructorsContent)
                    .Append(methodsContent)
                    .Append(eventsContent)
                    .Append(propertiesContent)
                    .Append(fieldsContent)
                    .Append(inheritedContent);
                finalStringBuilder.Append(_userIdentifier);
                string filePathWithExtensions = folderPath + "/" + targetType.Name + ".md";
                if (docCategory == DocCategory.ScriptingAPI)
                {
                    filePathWithExtensions = folderPath + "/" + targetType.Name + ".md";
                }

                if (File.Exists(filePathWithExtensions))
                {
                    if (!EditorUtility.DisplayDialog("提示",
                            "已经存在该文件，是否重新生成覆盖文档部分，保留 " + IdentifierTitle + " 之后的内容？", "确认", "取消"))
                    {
                        return;
                    }

                    string[] readAllLines = File.ReadAllLines(filePathWithExtensions);
                    var remarkStringBuilder = new StringBuilder();
                    if (readAllLines.Length > 0)
                    {
                        // 首次出现的标记
                        int remarkIndex = Array.FindIndex(readAllLines, line => line.StartsWith(IdentifierTitle));
                        if (remarkIndex >= 0)
                        {
                            for (int i = remarkIndex; i < readAllLines.Length; i++)
                            {
                                remarkStringBuilder.AppendLine(readAllLines[i]);
                            }

                            finalStringBuilder.Replace(_userIdentifier.ToString(), remarkStringBuilder.ToString());
                        }
                    }
                }

                File.WriteAllText(filePathWithExtensions,
                    finalStringBuilder.ToString(),
                    Encoding.UTF8);
                Instance._thisHasAnalyzedSingle = false;
                AssetDatabase.Refresh();
                EditorUtility.OpenWithDefaultApp(filePathWithExtensions);
            }
        }

        static void GenerateMkDocsMultiple()
        {
            DocCategory docCategory = Instance.docCategory;
            MarkdownCategory markdownCategory = Instance.markdownCategory;
            List<Type> targetTypes = Instance.typeListConfig ? Instance.typeListConfig.Types : Instance.TempTypeList;
            List<TypeData> typeDataList = Instance.typeDataList;
            string folderPath = Instance.folderPath;

            if (!Directory.Exists(folderPath))
            {
                YuumixLogger.LogError("请选择有效的目标路径");
                return;
            }

            if (markdownCategory == MarkdownCategory.MkDocsMaterial)
            {
                if (typeDataList.Count <= 0)
                {
                    return;
                }

                try
                {
                    for (var i = 0; i < typeDataList.Count; i++)
                    {
                        if (i <= 1)
                        {
                            EditorUtility.DisplayProgressBar("脚本文档生成", $"正在生成 {targetTypes[i].Name} 文档",
                                (float)1 / typeDataList.Count);
                        }
                        else
                        {
                            EditorUtility.DisplayProgressBar("脚本文档生成", $"正在生成 {targetTypes[i].Name} 文档",
                                (float)i / typeDataList.Count);
                        }

                        TypeData typeData = typeDataList[i];
                        StringBuilder headerIntroduction = CreateHeaderIntroductionMkDocs(typeData);
                        StringBuilder constructorsContent = CreateConstructorsContentMkDocs(typeData, docCategory);
                        StringBuilder fieldsContent = CreateCurrentFieldsContentMkDocs(typeData, docCategory);
                        StringBuilder methodsContent = CreateCurrentMethodsContentMkDocs(typeData, docCategory);
                        StringBuilder propertiesContent = CreateCurrentPropertiesContentMkDocs(typeData, docCategory);
                        StringBuilder eventsContent = CreateCurrentEventsContentMkDocs(typeData, docCategory);
                        StringBuilder inheritedContent = CreateInheritedContentMkDocs(typeData, docCategory);
                        StringBuilder finalStringBuilder = headerIntroduction
                            .Append(constructorsContent)
                            .Append(methodsContent)
                            .Append(eventsContent)
                            .Append(propertiesContent)
                            .Append(fieldsContent)
                            .Append(inheritedContent);
                        finalStringBuilder.Append(_userIdentifier);
                        string filePathWithExtensions = folderPath + "/" + targetTypes[i].Name + ".md";
                        if (docCategory == DocCategory.ScriptingAPI)
                        {
                            filePathWithExtensions = folderPath + "/" + targetTypes[i].Name + ".md";
                        }

                        if (File.Exists(filePathWithExtensions))
                        {
                            string[] readAllLines = File.ReadAllLines(filePathWithExtensions);
                            var remarkStringBuilder = new StringBuilder();
                            if (readAllLines.Length > 0)
                            {
                                int remarkIndex = Array.FindIndex(readAllLines,
                                    line => line.StartsWith(IdentifierTitle));
                                if (remarkIndex >= 0)
                                {
                                    for (int j = remarkIndex; j < readAllLines.Length; j++)
                                    {
                                        remarkStringBuilder.AppendLine(readAllLines[j]);
                                    }

                                    finalStringBuilder.Replace(_userIdentifier.ToString(),
                                        remarkStringBuilder.ToString());
                                }
                            }
                        }

                        File.WriteAllText(filePathWithExtensions,
                            finalStringBuilder.ToString(),
                            Encoding.UTF8);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
                finally
                {
                    EditorUtility.ClearProgressBar();
                }

                Instance._thisHasAnalyzedMultiple = false;
                AssetDatabase.Refresh();
                EditorUtility.OpenWithDefaultApp(folderPath);
            }
        }

        #endregion

        #region StringBuilder MkDocs-Material

        static StringBuilder CreateHeaderIntroductionMkDocs(TypeData data)
        {
            var sb = new StringBuilder();
            if (data.IsStatic)
            {
                sb.AppendLine(
                    $"# `{data.TypeName} static {data.TypeCategory.ToString().ToLower(CultureInfo.CurrentCulture)}`");
            }
            else if (data.IsAbstract)
            {
                sb.AppendLine(
                    $"# `{data.TypeName} abstract {data.TypeCategory.ToString().ToLower(CultureInfo.CurrentCulture)}`");
            }
            else
            {
                sb.AppendLine(
                    $"# `{data.TypeName} {data.TypeCategory.ToString().ToLower(CultureInfo.CurrentCulture)}`");
            }

            sb.AppendLine();
            sb.AppendLine("## Introduction");
            if (!data.NamespaceName.IsNullOrWhiteSpace())
            {
                sb.AppendLine($"- NameSpace: `{data.NamespaceName}`");
            }

            sb.AppendLine($"- Assembly: `{data.AssemblyName}`");
            if (data.SeeAlsoLinks.Length >= 1)
            {
                for (var i = 0; i < data.SeeAlsoLinks.Length; i++)
                {
                    sb.AppendLine($"- See Also [{i + 1}] : {data.SeeAlsoLinks[i]}");
                }
            }

            sb.AppendLine();
            sb.AppendLine("``` csharp");
            sb.AppendLine(data.TypeDeclaration);
            sb.AppendLine("```");
            sb.AppendLine();
            if (!string.IsNullOrEmpty(data.ChineseComment) || !string.IsNullOrEmpty(data.EnglishComment))
            {
                sb.AppendLine("### Description");
                sb.AppendLine();
                if (!string.IsNullOrEmpty(data.ChineseComment))
                {
                    sb.AppendLine("- " + data.ChineseComment);
                }

                if (!string.IsNullOrEmpty(data.EnglishComment))
                {
                    sb.AppendLine("- " + data.EnglishComment);
                }
            }

            sb.AppendLine();
            return sb;
        }

        static StringBuilder CreateConstructorsContentMkDocs(TypeData data, DocCategory docCategory)
        {
            var sb = new StringBuilder();
            if (data.IsStatic || data.Constructors.Length <= 0)
            {
                return sb;
            }

            if (docCategory == DocCategory.ScriptingAPI)
            {
                if (!data.Constructors.Any(x => x.IsAPI))
                {
                    return sb;
                }

                AppendHeader();
                AppendMemberLine(sb, data.Constructors.Where(x => !x.isObsolete && x.IsAPI));
            }
            else
            {
                AppendHeader();
                AppendMemberLine(sb, data.Constructors.Where(x => !x.isObsolete));
            }

            if (!data.Constructors.Any(x => x.isObsolete))
            {
                return sb;
            }

            if (docCategory == DocCategory.ScriptingAPI)
            {
                AppendMemberLine(sb, data.Constructors.Where(x => x.isObsolete && x.IsAPI), true);
            }
            else
            {
                AppendMemberLine(sb, data.Constructors.Where(x => x.isObsolete), true);
            }

            sb.AppendLine();
            return sb;

            void AppendHeader()
            {
                sb.AppendLine("## Constructors");
                sb.AppendLine();
                sb.AppendLine("| 构造函数 | 注释 | Comment |");
                sb.AppendLine("| :--- | :--- | :--- |");
            }
        }

        static StringBuilder CreateCurrentMethodsContentMkDocs(TypeData data, DocCategory docCategory)
        {
            var sb = new StringBuilder();
            if (data.CurrentMethods.Length <= 0)
            {
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    if (!data.CurrentMethods.Any(x => x.IsAPI))
                    {
                        // YuumixLogger.EditorLog("!data.CurrentMethods.Any(x => x.IsAPI)");
                        return sb;
                    }

                    AppendHeader();
                    AppendMemberLine(sb, data.CurrentMethods.Where(x => !x.isObsolete && x.IsAPI));
                    break;
                case DocCategory.CompleteReferences:
                    AppendHeader();
                    AppendMemberLine(sb, data.CurrentMethods.Where(x => !x.isObsolete));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            if (!data.CurrentMethods.Any(x => x.isObsolete))
            {
                // YuumixLogger.EditorLog("!data.CurrentMethods.Any(x => x.isObsolete)");
                sb.AppendLine();
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    AppendMemberLine(sb, data.CurrentMethods.Where(x => x.isObsolete && x.IsAPI), true);
                    break;
                case DocCategory.CompleteReferences:
                    AppendMemberLine(sb, data.CurrentMethods.Where(x => x.isObsolete), true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            sb.AppendLine();
            return sb;

            void AppendHeader()
            {
                sb.AppendLine("## Methods");
                sb.AppendLine();
                sb.AppendLine("| 方法 | 注释 | Comment |");
                sb.AppendLine("| :--- | :--- | :--- |");
            }
        }

        static StringBuilder CreateCurrentEventsContentMkDocs(TypeData data, DocCategory docCategory)
        {
            var sb = new StringBuilder();
            if (data.CurrentEvents.Length <= 0)
            {
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    if (!data.CurrentEvents.Any(x => x.IsAPI))
                    {
                        // YuumixLogger.EditorLog("!data.CurrentEvents.Any(x => x.IsAPI)");
                        return sb;
                    }

                    AppendHeader();
                    AppendMemberLine(sb, data.CurrentEvents.Where(x => !x.isObsolete && x.IsAPI));
                    break;
                case DocCategory.CompleteReferences:
                    AppendHeader();
                    AppendMemberLine(sb, data.CurrentEvents.Where(x => !x.isObsolete));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            if (!data.CurrentEvents.Any(x => x.isObsolete))
            {
                // YuumixLogger.EditorLog("!data.CurrentEvents.Any(x => x.isObsolete)");
                sb.AppendLine();
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    AppendMemberLine(sb, data.CurrentEvents.Where(x => x.isObsolete && x.IsAPI), true);
                    break;
                case DocCategory.CompleteReferences:
                    AppendMemberLine(sb, data.CurrentEvents.Where(x => x.isObsolete), true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            sb.AppendLine();
            return sb;

            void AppendHeader()
            {
                sb.AppendLine("## Events");
                sb.AppendLine();
                sb.AppendLine("| 事件 | 注释 | Comment |");
                sb.AppendLine("| :--- | :--- | :--- |");
            }
        }

        static StringBuilder CreateCurrentPropertiesContentMkDocs(TypeData data, DocCategory docCategory)
        {
            var sb = new StringBuilder();
            if (data.CurrentProperties.Length <= 0)
            {
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    if (!data.CurrentProperties.Any(x => x.IsAPI))
                    {
                        // YuumixLogger.EditorLog("!data.CurrentProperties.Any(x => x.IsAPI)");
                        return sb;
                    }

                    AppendHeader();
                    AppendMemberLine(sb, data.CurrentProperties.Where(x => !x.isObsolete && x.IsAPI));
                    break;
                case DocCategory.CompleteReferences:
                    AppendHeader();
                    AppendMemberLine(sb, data.CurrentProperties.Where(x => !x.isObsolete));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            if (!data.CurrentProperties.Any(x => x.isObsolete))
            {
                // YuumixLogger.EditorLog("!data.CurrentProperties.Any(x => x.isObsolete)");
                sb.AppendLine();
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    AppendMemberLine(sb, data.CurrentProperties.Where(x => x.isObsolete && x.IsAPI), true);
                    break;
                case DocCategory.CompleteReferences:
                    AppendMemberLine(sb, data.CurrentProperties.Where(x => x.isObsolete), true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            sb.AppendLine();
            return sb;

            void AppendHeader()
            {
                sb.AppendLine("## Properties");
                sb.AppendLine();
                sb.AppendLine("| 属性 | 注释 | Comment |");
                sb.AppendLine("| :--- | :--- | :--- |");
            }
        }

        static StringBuilder CreateCurrentFieldsContentMkDocs(TypeData data, DocCategory docCategory)
        {
            var sb = new StringBuilder();
            if (data.CurrentFields.Length <= 0)
            {
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    if (!data.CurrentFields.Any(x => x.IsAPI))
                    {
                        // YuumixLogger.EditorLog("!data.CurrentFields.Any(x => x.IsAPI)");
                        return sb;
                    }

                    AppendHeader();
                    AppendMemberLine(sb, data.CurrentFields.Where(x => !x.isObsolete && x.IsAPI));
                    break;
                case DocCategory.CompleteReferences:
                    AppendHeader();
                    AppendMemberLine(sb, data.CurrentFields.Where(x => !x.isObsolete));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            if (!data.CurrentFields.Any(x => x.isObsolete))
            {
                // YuumixLogger.EditorLog("!data.CurrentFields.Any(x => x.isObsolete)");
                sb.AppendLine();
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    AppendMemberLine(sb, data.CurrentFields.Where(x => x.isObsolete && x.IsAPI), true);
                    break;
                case DocCategory.CompleteReferences:
                    AppendMemberLine(sb, data.CurrentFields.Where(x => x.isObsolete), true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            sb.AppendLine();
            return sb;

            void AppendHeader()
            {
                sb.AppendLine("## Fields");
                sb.AppendLine();
                sb.AppendLine("| 字段 | 注释 | Comment |");
                sb.AppendLine("| :--- | :--- | :--- |");
            }
        }

        static StringBuilder CreateInheritedContentMkDocs(TypeData data, DocCategory docCategory)
        {
            var sb = new StringBuilder();
            if (IsNonExistInheritedMember(data))
            {
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    if (IsNonExistAPIMember(data))
                    {
                        // YuumixLogger.EditorLog("IsNonExistAPIMember(data)");
                        return sb;
                    }

                    AppendInheritedHeader(sb);
                    CreateInheritedMemberString(data, sb, x => !x.isObsolete && x.IsAPI);
                    break;
                case DocCategory.CompleteReferences:
                    AppendInheritedHeader(sb);
                    CreateInheritedMemberString(data, sb, x => !x.isObsolete);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            if (IsNonExistObsoleteInheritedMember(data))
            {
                // YuumixLogger.EditorLog("IsNonExistObsoleteInheritedMember(data)");
                sb.AppendLine();
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    CreateInheritedMemberString(data, sb, x => x.isObsolete && x.IsAPI, true);
                    break;
                case DocCategory.CompleteReferences:
                    CreateInheritedMemberString(data, sb, x => x.isObsolete, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            sb.AppendLine();
            return sb;

            bool IsNonExistInheritedMember(TypeData dataArg)
            {
                return dataArg.InheritedMethods.Length <= 0 && dataArg.InheritedProperties.Length <= 0 &&
                       dataArg.InheritedEvents.Length <= 0 && dataArg.InheritedFields.Length <= 0;
            }

            bool IsNonExistObsoleteInheritedMember(TypeData dataArg)
            {
                return !dataArg.InheritedMethods.Any(x => x.isObsolete) &&
                       !dataArg.InheritedEvents.Any(x => x.isObsolete) &&
                       !dataArg.InheritedProperties.Any(x => x.isObsolete) &&
                       !dataArg.InheritedFields.Any(x => x.isObsolete);
            }

            void AppendInheritedHeader(StringBuilder stringBuilder)
            {
                stringBuilder.AppendLine("## Inherited Members");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("| 成员 | 注释 | 声明此方法的类 |");
                stringBuilder.AppendLine("| :--- | :--- | :--- |");
            }

            bool IsNonExistAPIMember(TypeData dataArg)
            {
                return !dataArg.InheritedMethods.Any(x => x.IsAPI) && !dataArg.InheritedEvents.Any(x => x.IsAPI) &&
                       !dataArg.InheritedProperties.Any(x => x.IsAPI) && !dataArg.InheritedFields.Any(x => x.IsAPI);
            }
        }

        static void CreateInheritedMemberString(TypeData data, StringBuilder sb,
            Func<MemberData, bool> filterPredicate,
            bool addObsoleteSign = false)
        {
            AppendInheritedMemberLine(sb, data.InheritedMethods.Where(filterPredicate), addObsoleteSign);
            AppendInheritedMemberLine(sb, data.InheritedEvents.Where(filterPredicate), addObsoleteSign);
            AppendInheritedMemberLine(sb, data.InheritedProperties.Where(filterPredicate), addObsoleteSign);
            AppendInheritedMemberLine(sb, data.InheritedFields.Where(filterPredicate), addObsoleteSign);
        }

        static void AppendMemberLine(StringBuilder sb, IEnumerable<MemberData> items,
            bool addObsoleteSign = false)
        {
            foreach (MemberData item in items)
            {
                string fullSignature = item.fullSignature;
                if (addObsoleteSign)
                {
                    fullSignature = $"[Obsolete] {fullSignature}";
                }

                sb.AppendLine("| " + $"`{fullSignature}`" + " | " + item.chineseComment + " | " +
                              $"`{item.englishComment}`" + " |");
            }
        }

        static void AppendInheritedMemberLine(StringBuilder sb, IEnumerable<MemberData> items,
            bool addObsoleteSign = false)
        {
            foreach (MemberData item in items)
            {
                string fullSignature = item.fullSignature;
                if (addObsoleteSign)
                {
                    fullSignature = $"[Obsolete] {fullSignature}";
                }

                sb.AppendLine("| " + $"`{fullSignature}`" + " | " + item.chineseComment + " | " +
                              $"`{item.declaringType}`" + " |");
            }
        }

        #endregion

        static void RaiseOnToastEvent(ToastPosition position, SdfIconType icon, string msg,
            Color color, float duration)
        {
            OnToastEvent?.Invoke(position, icon, msg, color, duration);
        }
    }
}
