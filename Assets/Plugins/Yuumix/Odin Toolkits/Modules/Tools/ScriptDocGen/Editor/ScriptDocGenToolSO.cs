using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.Contributors;
using Yuumix.OdinToolkits.Common.Editor;
using Yuumix.OdinToolkits.Common.InspectorMultiLanguage;
using Yuumix.OdinToolkits.Common.ResetTool;
using Yuumix.YuumixEditor;
using Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Runtime;
using Yuumix.OdinToolkits.Modules.Utilities;

namespace Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Editor
{
    public class ScriptDocGenToolSO : OdinEditorScriptableSingleton<ScriptDocGenToolSO>, IOdinToolkitsReset
    {
        public static MultiLanguageData ScriptDocGenToolMenuPathData =
            new MultiLanguageData("脚本文档生成工具", "Script Doc Generate Tool");

        public const string DefaultStorageFolderPath = "Assets/OdinToolkitsData/TypeListConfigSO";
        public const string DefaultDocFolderPath = "Assets/OdinToolkitsData/Editor/Documents/";
        public const string Identifier = "Addition Description";
        public const string IdentifierTitle = "## " + Identifier;

        static StringBuilder _userIdentifier = new StringBuilder()
            .AppendLine(IdentifierTitle)
            .AppendLine("- 不要修改标题级别和内容，" + IdentifierTitle + " 是标识符")
            .AppendLine("- " + Identifier + " 之后的内容不会被新生成的文档覆盖，可以对特定的方法进行额外说明");

        public static event Action<ToastPosition, SdfIconType, string, Color, float> OnToastEvent;

        public MultiLanguageHeaderWidget header = new MultiLanguageHeaderWidget(
            ScriptDocGenToolMenuPathData.GetChinese(),
            ScriptDocGenToolMenuPathData.GetEnglish(),
            "给定一个 `Type` 类型的值，生成 Markdown 格式的文档，可选 Scripting API 或者 Complete References，默认支持 MkDocs-Material。Scripting API 表示对外的，用户可以调用的程序接口文档，Complete References 表示包含所有成员的参考文档",
            "Given a value of type `Type`, generate a document in the format of Markdown, optional Scripting API and Complete References, and MkDocs-Material is supported by default. Scripting API refers to the external, user-accessible interface documentation, and Complete References refers to the documentation containing all members"
        );

        [PropertyOrder(1)]
        [HorizontalGroup("Mode", 0.75f)]
        [BoxGroup("Mode/1", showLabel: false)]
        [ShowIf("_singleDoc", false)]
        [MultiLanguageDisplayWidgetConfig(fontSize: 14)]
        [GUIColor("green")]
        public MultiLanguageDisplayAsStringWidget singleMode = new MultiLanguageDisplayAsStringWidget(
            "单文档生成模式",
            "Single Document Generation Mode"
        );

        [PropertyOrder(1)]
        [HorizontalGroup("Mode", 0.75f)]
        [BoxGroup("Mode/1", showLabel: false)]
        [HideIf("_singleDoc", false)]
        [MultiLanguageDisplayWidgetConfig(fontSize: 14)]
        [GUIColor("green")]
        public MultiLanguageDisplayAsStringWidget multiMode = new MultiLanguageDisplayAsStringWidget(
            "多文档批量生成模式",
            "Multi-Documents Batch Generation Mode"
        );

        [PropertyOrder(1)]
        [HorizontalGroup("Mode", 0.24f)]
        [MultiLanguageButtonWidgetConfig("切换工具模式", "Switch Tool Mode", buttonHeight: 24, icon: SdfIconType.Hurricane)]
        public MultiLanguageButtonWidget switchMode = new MultiLanguageButtonWidget(SwitchToolMode);

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
        [MultiLanguageBoxGroup("GroupA", "临时类型列表", "Temp Type List", showLabel: true)]
        [ShowIf(nameof(ShowSaveFolderPath))]
        [InlineButton("CompleteConfig", SdfIconType.Check, "确认")]
        [InlineButton("ResetSOSaveFolderPath", SdfIconType.ArrowClockwise, "")]
        [MultiLanguageText("存放类型列表配置资源的文件夹路径", "Storage TypeListConfigSO Folder Path")]
        [LabelWidth(200f)]
        public string storageTypeListConfigSOFolderPath = DefaultStorageFolderPath;

        [PropertyOrder(5)]
        [MultiLanguageBoxGroup("GroupA", "临时类型列表", "Temp Type List", showLabel: true)]
        [HideIf("HideTempTypeList")]
        [HideLabel]
        [ListDrawerSettings(OnTitleBarGUI = nameof(SaveAsTypeListConfigSO), NumberOfItemsPerPage = 10)]
        public List<Type> TempTypeList = new List<Type>();

        [PropertyOrder(10)]
        [MultiLanguageTitle("生成文档的文件夹路径", "Folder Path For Doc")]
        [HideLabel]
        [FolderPath(AbsolutePath = true)]
        [InlineButton(nameof(ResetDocFolderPath), SdfIconType.ArrowClockwise, "")]
        public string folderPath;

        [PropertyOrder(20)]
        [ShowIf("_singleDoc")]
        [MultiLanguageButtonWidgetConfig("解析单个 Type", "Analyze Single Type",
            ButtonSizes.Large, ButtonStyle.Box, SdfIconType.Activity)]
        public MultiLanguageButtonWidget analyzeSingleType = new MultiLanguageButtonWidget(AnalyzeSingle);

        [PropertyOrder(20)]
        [HideIf("_singleDoc")]
        [MultiLanguageButtonWidgetConfig("解析 Type 列表", "Analyze Type List",
            ButtonSizes.Large, ButtonStyle.Box, SdfIconType.Activity)]
        public MultiLanguageButtonWidget analyzeMultiTypes = new MultiLanguageButtonWidget(AnalyzeMultiple);

        [PropertyOrder(25)]
        [ShowIf(nameof(CanGeneratedSingle))]
        [MultiLanguageButtonWidgetConfig("生成 Markdown 文档", "Generate Markdown Document",
            ButtonSizes.Large, ButtonStyle.Box, SdfIconType.FileEarmarkPlus)]
        public MultiLanguageButtonWidget generateButtonSingle = new MultiLanguageButtonWidget(GenerateMkDocsSingle);

        [PropertyOrder(25)]
        [ShowIf(nameof(CanGeneratedMultiple))]
        [MultiLanguageButtonWidgetConfig("批量生成 Markdown 文档", "Generate Multiple Markdown Documents",
            ButtonSizes.Large, ButtonStyle.Box, SdfIconType.FileEarmarkPlus)]
        public MultiLanguageButtonWidget generateButtonMulti = new MultiLanguageButtonWidget(GenerateMkDocsMultiple);

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
            new[]
            {
                new ContributorInfo("2025/06/23", "Yuumix Zeus", "zeriying@gmail.com", "https://github.com/Yuumi-Zeus")
            },
            "2025/6/23",
            new[]
            {
                new MultiLanguageData("目前支持构造函数，方法，属性，事件，字段，运算符重载成员分析",
                    "Currently supports constructor, method, property, event, field, operator overloading member analysis")
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

        #region PluginReset

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
        }

        #endregion

        public void ResetSOSaveFolderPath()
        {
            storageTypeListConfigSOFolderPath = DefaultStorageFolderPath;
        }

        public void ResetDocFolderPath()
        {
            folderPath = DefaultDocFolderPath;
        }

        public void CompleteConfig()
        {
            _customizingSaveConfig = false;
        }

        void SaveAsTypeListConfigSO()
        {
            var image =
                SdfIcons.CreateTransparentIconTexture(SdfIconType.SaveFill, Color.white, 16 /*0x10*/, 16 /*0x10*/,
                    0);
            var content = new GUIContent(" 保存为SO资源 ", image,
                "保存为 " + typeof(TypeListConfigSO) + " 到 " + storageTypeListConfigSOFolderPath);
            var filePath = storageTypeListConfigSOFolderPath + "/" + typeof(TypeListConfigSO) + ".asset";
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

            var image2 =
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

        static void SwitchToolMode()
        {
            Instance._singleDoc = !Instance._singleDoc;
            RaiseOnToastEvent(ToastPosition.BottomRight, SdfIconType.InfoSquareFill,
                Instance._singleDoc ? "成功切换为单文档生成模式" : "成功切换为多文档批量生成模式",
                Color.white, 5f);
        }

        #region OnInspectorGUI

        [OnInspectorGUI]
        [MultiLanguageTitle("工具使用模式", "Tool Usage Mode ")]
        public void FirstTitle() { }

        [PropertyOrder(1)]
        [MultiLanguageTitle("目标类型", "Target Type")]
        [OnInspectorGUI]
        public void TypeTitle() { }

        [PropertyOrder(19)]
        [MultiLanguageTitle("解析操作按钮", "Analyze Button")]
        [OnInspectorGUI]
        public void AnalyzeTitle() { }

        [PropertyOrder(24)]
        [MultiLanguageTitle("生成文档按钮", "Generate Document Buttons")]
        [ShowIf("CanShowGenerateTitle")]
        [OnInspectorGUI]
        public void GenerateTitle() { }

        [PropertyOrder(100)]
        [MultiLanguageTitle("过程数据", "Process Data")]
        [OnInspectorGUI]
        public void Title() { }

        #endregion

        #region Analyze Type

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

        static void AnalyzeMultiple()
        {
            Instance.typeDataList.Clear();
            var targetTypes = Instance.typeListConfig ? Instance.typeListConfig.Types : Instance.TempTypeList;
            targetTypes.RemoveAll(x => x == null);
            foreach (var typeData in targetTypes.Select(TypeData.FromType))
            {
                Instance.typeDataList.Add(typeData);
            }

            RaiseOnToastEvent(ToastPosition.BottomRight, SdfIconType.LightningFill,
                "正在解析目标类型，请勿重复点击！等待文档生成按钮显示",
                Color.yellow, 5f);
            Instance._thisHasAnalyzedMultiple = true;
        }

        #endregion

        #region GenerateMkDocsFile

        static void GenerateMkDocsSingle()
        {
            var docCategory = Instance.docCategory;
            var markdownCategory = Instance.markdownCategory;
            var typeData = Instance.typeData;
            var folderPath = Instance.folderPath;
            var targetType = Instance.TargetType;
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
                var headerIntroduction = CreateHeaderIntroductionMkDocs(typeData);
                var constructorsContent = CreateConstructorsContentMkDocs(typeData, docCategory);
                var fieldsContent = CreateCurrentFieldsContentMkDocs(typeData, docCategory);
                var methodsContent = CreateCurrentMethodsContentMkDocs(typeData, docCategory);
                var propertiesContent = CreateCurrentPropertiesContentMkDocs(typeData, docCategory);
                var eventsContent = CreateCurrentEventsContentMkDocs(typeData, docCategory);
                var inheritedContent = CreateInheritedContentMkDocs(typeData, docCategory);
                var finalStringBuilder = headerIntroduction
                    .Append(constructorsContent)
                    .Append(methodsContent)
                    .Append(eventsContent)
                    .Append(propertiesContent)
                    .Append(fieldsContent)
                    .Append(inheritedContent);
                finalStringBuilder.Append(_userIdentifier);
                var filePathWithExtensions = folderPath + "/" + targetType.Name + ".md";
                if (docCategory == DocCategory.ScriptingAPI)
                {
                    filePathWithExtensions = folderPath + "/" + targetType.Name + ".api.md";
                }

                if (File.Exists(filePathWithExtensions))
                {
                    if (!EditorUtility.DisplayDialog("提示",
                            "已经存在该文件，是否重新生成覆盖文档部分，保留 Remarks 部分？", "确认", "取消"))
                    {
                        return;
                    }

                    var readAllLines = File.ReadAllLines(filePathWithExtensions);
                    var remarkStringBuilder = new StringBuilder();
                    if (readAllLines.Length > 0)
                    {
                        var remarkIndex = Array.FindIndex(readAllLines, line => line.StartsWith(IdentifierTitle));
                        if (remarkIndex >= 0)
                        {
                            for (var i = remarkIndex; i < readAllLines.Length; i++)
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
                // Debug.Log("文档生成完毕");
                Instance._thisHasAnalyzedSingle = false;
                AssetDatabase.Refresh();
                EditorUtility.OpenWithDefaultApp(filePathWithExtensions);
            }
        }

        static void GenerateMkDocsMultiple()
        {
            var docCategory = Instance.docCategory;
            var markdownCategory = Instance.markdownCategory;
            var targetTypes = Instance.TempTypeList;
            var typeDataList = Instance.typeDataList;
            var folderPath = Instance.folderPath;

            if (!Directory.Exists(folderPath))
            {
                Debug.LogError("请选择有效的目标路径");
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
                        EditorUtility.DisplayProgressBar("脚本文档生成", $"正在生成 {targetTypes[i].Name} 文档",
                            (float)i / typeDataList.Count);
                        var typeData = typeDataList[i];
                        var headerIntroduction = CreateHeaderIntroductionMkDocs(typeData);
                        var constructorsContent = CreateConstructorsContentMkDocs(typeData, docCategory);
                        var fieldsContent = CreateCurrentFieldsContentMkDocs(typeData, docCategory);
                        var methodsContent = CreateCurrentMethodsContentMkDocs(typeData, docCategory);
                        var propertiesContent = CreateCurrentPropertiesContentMkDocs(typeData, docCategory);
                        var eventsContent = CreateCurrentEventsContentMkDocs(typeData, docCategory);
                        var inheritedContent = CreateInheritedContentMkDocs(typeData, docCategory);
                        var finalStringBuilder = headerIntroduction
                            .Append(constructorsContent)
                            .Append(methodsContent)
                            .Append(eventsContent)
                            .Append(propertiesContent)
                            .Append(fieldsContent)
                            .Append(inheritedContent);
                        finalStringBuilder.Append(_userIdentifier);
                        var filePathWithExtensions = folderPath + "/" + targetTypes[i].Name + ".md";
                        if (docCategory == DocCategory.ScriptingAPI)
                        {
                            filePathWithExtensions = folderPath + "/" + targetTypes[i].Name + ".api.md";
                        }

                        if (File.Exists(filePathWithExtensions))
                        {
                            var readAllLines = File.ReadAllLines(filePathWithExtensions);
                            var remarkStringBuilder = new StringBuilder();
                            if (readAllLines.Length > 0)
                            {
                                var remarkIndex = Array.FindIndex(readAllLines,
                                    line => line.StartsWith(IdentifierTitle));
                                if (remarkIndex >= 0)
                                {
                                    for (var j = remarkIndex; j < readAllLines.Length; j++)
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

            sb.AppendLine("## Introduction");
            if (data.NamespaceName.IsNullOrWhiteSpace())
            {
                sb.AppendLine($"- NameSpace: `{data.NamespaceName}`");
            }

            sb.AppendLine($"- Assembly: `{data.AssemblyName}`");
            sb.AppendLine();
            sb.AppendLine("``` csharp");
            sb.AppendLine(data.TypeDeclaration);
            sb.AppendLine("```");
            if (!string.IsNullOrEmpty(data.ChineseComment) || !string.IsNullOrEmpty(data.EnglishComment))
            {
                sb.AppendLine("### Description");
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

                sb.AppendLine("## Constructors");
                sb.AppendLine("| 构造函数 | 注释 | Comment |");
                sb.AppendLine("| :--- | :--- | :--- |");
                foreach (var item in data.Constructors.Where(x =>
                             !x.isObsolete && x.IsAPI))
                {
                    sb.AppendLine("| " + $"`{item.fullSignature}`" + " | " + item.chineseComment + " | " +
                                  item.englishComment + " |");
                }
            }
            else
            {
                sb.AppendLine("## Constructors");
                sb.AppendLine("| 构造函数 | 注释 | Comment |");
                sb.AppendLine("| :--- | :--- | :--- |");
                foreach (var item in data.Constructors.Where(x => !x.isObsolete))
                {
                    sb.AppendLine("| " + $"`{item.fullSignature}`" + " | " + item.chineseComment + " | " +
                                  item.englishComment + " |");
                }
            }

            if (!data.Constructors.Any(x => x.isObsolete))
            {
                return sb;
            }

            if (docCategory == DocCategory.ScriptingAPI)
            {
                foreach (var item in data.Constructors.Where(x => x.isObsolete && x.IsAPI))
                {
                    sb.AppendLine("| " + $"`[Obsolete] {item.fullSignature}`" + " | " + item.chineseComment + " | " +
                                  item.englishComment + " |");
                }
            }
            else
            {
                foreach (var item in data.Constructors.Where(x => x.isObsolete))
                {
                    sb.AppendLine("| " + $"`[Obsolete] {item.fullSignature}`" + " | " + item.chineseComment + " | " +
                                  item.englishComment + " |");
                }
            }

            sb.AppendLine();
            return sb;
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
                        sb.AppendLine();
                        return sb;
                    }

                    sb.AppendLine("## Methods");
                    sb.AppendLine("| 方法 | 注释 | Comment |");
                    sb.AppendLine("| :--- | :--- | :--- |");
                    foreach (var item in data.CurrentMethods.Where(x => !x.isObsolete && x.IsAPI))
                    {
                        sb.AppendLine("| " + $"`{item.fullSignature}`" + " | " + item.chineseComment + " | " +
                                      item.englishComment + " |");
                    }

                    break;
                case DocCategory.CompleteReferences:
                    sb.AppendLine("## Methods");
                    sb.AppendLine("| 方法 | 注释 | Comment |");
                    sb.AppendLine("| :--- | :--- | :--- |");
                    foreach (var item in data.CurrentMethods.Where(x => !x.isObsolete))
                    {
                        sb.AppendLine("| " + $"`{item.fullSignature}`" + " | " + item.chineseComment + " | " +
                                      item.englishComment + " |");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            if (!data.CurrentMethods.Any(x => x.isObsolete))
            {
                sb.AppendLine();
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    foreach (var item in data.CurrentMethods.Where(x => x.isObsolete))
                    {
                        sb.AppendLine("| " + $"`[Obsolete] {item.fullSignature}`" + " | " + item.chineseComment +
                                      " | " +
                                      item.englishComment + " |");
                    }

                    break;
                case DocCategory.CompleteReferences:
                    foreach (var item in data.CurrentMethods.Where(x => x.isObsolete && x.IsAPI))
                    {
                        sb.AppendLine("| " + $"`[Obsolete] {item.fullSignature}`" + " | " + item.chineseComment +
                                      " | " +
                                      item.englishComment + " |");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            sb.AppendLine();
            return sb;
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
                        sb.AppendLine();
                        return sb;
                    }

                    sb.AppendLine("## Events");
                    sb.AppendLine("| 事件 | 注释 | Comment |");
                    sb.AppendLine("| :--- | :--- | :--- |");
                    foreach (var item in data.CurrentEvents.Where(x => !x.isObsolete && x.IsAPI))
                    {
                        sb.AppendLine("| " + $"`{item.fullSignature}`" + " | " + item.chineseComment + " | " +
                                      item.englishComment + " |");
                    }

                    break;
                case DocCategory.CompleteReferences:
                    sb.AppendLine("## Events");
                    sb.AppendLine("| 事件 | 注释 | Comment |");
                    sb.AppendLine("| :--- | :--- | :--- |");
                    foreach (var item in data.CurrentEvents.Where(x => !x.isObsolete))
                    {
                        sb.AppendLine("| " + $"`{item.fullSignature}`" + " | " + item.chineseComment + " | " +
                                      item.englishComment + " |");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            if (!data.CurrentEvents.Any(x => x.isObsolete))
            {
                sb.AppendLine();
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    foreach (var item in data.CurrentEvents.Where(x => x.isObsolete && x.IsAPI))
                    {
                        sb.AppendLine("| " + $"`[Obsolete] {item.fullSignature}`" + " | " + item.chineseComment +
                                      " | " +
                                      item.englishComment + " |");
                    }

                    break;
                case DocCategory.CompleteReferences:
                    foreach (var item in data.CurrentEvents.Where(x => x.isObsolete))
                    {
                        sb.AppendLine("| " + $"`[Obsolete] {item.fullSignature}`" + " | " + item.chineseComment +
                                      " | " +
                                      item.englishComment + " |");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            sb.AppendLine();
            return sb;
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
                        sb.AppendLine();
                        return sb;
                    }

                    sb.AppendLine("## Properties");
                    sb.AppendLine("| 属性 | 注释 | Comment |");
                    sb.AppendLine("| :--- | :--- | :--- |");
                    foreach (var item in data.CurrentProperties.Where(x => !x.isObsolete && x.IsAPI))
                    {
                        sb.AppendLine("| " + $"`{item.fullSignature}`" + " | " + item.chineseComment + " | " +
                                      item.englishComment + " |");
                    }

                    break;
                case DocCategory.CompleteReferences:
                    sb.AppendLine("## Properties");
                    sb.AppendLine("| 属性 | 注释 | Comment |");
                    sb.AppendLine("| :--- | :--- | :--- |");
                    foreach (var item in data.CurrentProperties.Where(x => !x.isObsolete))
                    {
                        sb.AppendLine("| " + $"`{item.fullSignature}`" + " | " + item.chineseComment + " | " +
                                      item.englishComment + " |");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            if (!data.CurrentProperties.Any(x => x.isObsolete))
            {
                sb.AppendLine();
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    foreach (var item in data.CurrentProperties.Where(x => x.isObsolete && x.IsAPI))
                    {
                        sb.AppendLine("| " + $"`[Obsolete] {item.fullSignature}`" + " | " + item.chineseComment +
                                      " | " +
                                      item.englishComment + " |");
                    }

                    break;
                case DocCategory.CompleteReferences:
                    foreach (var item in data.CurrentProperties.Where(x => x.isObsolete))
                    {
                        sb.AppendLine("| " + $"`[Obsolete] {item.fullSignature}`" + " | " + item.chineseComment +
                                      " | " +
                                      item.englishComment + " |");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            sb.AppendLine();
            return sb;
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
                        sb.AppendLine();
                        return sb;
                    }

                    sb.AppendLine("## Fields");
                    sb.AppendLine("| 字段 | 注释 | Comment |");
                    sb.AppendLine("| :--- | :--- | :--- |");
                    foreach (var item in data.CurrentFields.Where(x => !x.isObsolete && x.IsAPI))
                    {
                        sb.AppendLine("| " + $"`{item.fullSignature}`" + " | " + item.chineseComment + " | " +
                                      item.englishComment + " |");
                    }

                    break;
                case DocCategory.CompleteReferences:
                    sb.AppendLine("## Fields");
                    sb.AppendLine("| 字段 | 注释 | Comment |");
                    sb.AppendLine("| :--- | :--- | :--- |");
                    foreach (var item in data.CurrentFields.Where(x => !x.isObsolete))
                    {
                        sb.AppendLine("| " + $"`{item.fullSignature}`" + " | " + item.chineseComment + " | " +
                                      item.englishComment + " |");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            if (!data.CurrentFields.Any(x => x.isObsolete))
            {
                sb.AppendLine();
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    foreach (var item in data.CurrentFields.Where(x => x.isObsolete && x.IsAPI))
                    {
                        sb.AppendLine("| " + $"`[Obsolete] {item.fullSignature}`" + " | " + item.chineseComment +
                                      " | " +
                                      item.englishComment + " |");
                    }

                    break;
                case DocCategory.CompleteReferences:
                    foreach (var item in data.CurrentFields.Where(x => x.isObsolete))
                    {
                        sb.AppendLine("| " + $"`[Obsolete] {item.fullSignature}`" + " | " + item.chineseComment +
                                      " | " +
                                      item.englishComment + " |");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            sb.AppendLine();
            return sb;
        }

        static StringBuilder CreateInheritedContentMkDocs(TypeData data, DocCategory docCategory)
        {
            var sb = new StringBuilder();
            if (data.InheritedMethods.Length <= 0 && data.InheritedProperties.Length <= 0 &&
                data.InheritedEvents.Length <= 0 && data.InheritedFields.Length <= 0)
            {
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    if (!data.InheritedMethods.Any(x => x.IsAPI) && !data.InheritedEvents.Any(x => x.IsAPI) &&
                        !data.InheritedProperties.Any(x => x.IsAPI) && !data.InheritedFields.Any(x => x.IsAPI))
                    {
                        sb.AppendLine();
                        return sb;
                    }

                    sb.AppendLine("## Inherited Members");
                    sb.AppendLine("| 成员 | 注释 | 声明此方法的类 |");
                    sb.AppendLine("| :--- | :--- | :--- |");
                    foreach (var item in data.InheritedMethods.Where(x => !x.isObsolete && x.IsAPI))
                    {
                        sb.AppendLine("| " + $"`{item.fullSignature}`" + " | " + item.chineseComment + " | " +
                                      $"`{item.declaringType}`" + " |");
                    }

                    foreach (var item in data.InheritedEvents.Where(x => !x.isObsolete && x.IsAPI))
                    {
                        sb.AppendLine("| " + $"`{item.fullSignature}`" + " | " + item.chineseComment + " | " +
                                      $"`{item.declaringType}`" + " |");
                    }

                    foreach (var item in data.InheritedProperties.Where(x => !x.isObsolete && x.IsAPI))
                    {
                        sb.AppendLine("| " + $"`{item.fullSignature}`" + " | " + item.chineseComment + " | " +
                                      $"`{item.declaringType}`" + " |");
                    }

                    foreach (var item in data.InheritedFields.Where(x => !x.isObsolete && x.IsAPI))
                    {
                        sb.AppendLine("| " + $"`{item.fullSignature}`" + " | " + item.chineseComment + " | " +
                                      $"`{item.declaringType}`" + " |");
                    }

                    break;
                case DocCategory.CompleteReferences:
                    sb.AppendLine("## Inherited Members");
                    sb.AppendLine("| 成员 | 注释 | 声明此方法的类 |");
                    sb.AppendLine("| :--- | :--- | :--- |");
                    foreach (var item in data.InheritedMethods.Where(x => !x.isObsolete))
                    {
                        sb.AppendLine("| " + $"`{item.fullSignature}`" + " | " + item.chineseComment + " | " +
                                      $"`{item.declaringType}`" + " |");
                    }

                    foreach (var item in data.InheritedEvents.Where(x => !x.isObsolete))
                    {
                        sb.AppendLine("| " + $"`{item.fullSignature}`" + " | " + item.chineseComment + " | " +
                                      $"`{item.declaringType}`" + " |");
                    }

                    foreach (var item in data.InheritedProperties.Where(x => !x.isObsolete))
                    {
                        sb.AppendLine("| " + $"`{item.fullSignature}`" + " | " + item.chineseComment + " | " +
                                      $"`{item.declaringType}`" + " |");
                    }

                    foreach (var item in data.InheritedFields.Where(x => !x.isObsolete))
                    {
                        sb.AppendLine("| " + $"`{item.fullSignature}`" + " | " + item.chineseComment + " | " +
                                      $"`{item.declaringType}`" + " |");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            if (!data.InheritedMethods.Any(x => x.isObsolete) && !data.InheritedEvents.Any(x => x.isObsolete) &&
                !data.InheritedProperties.Any(x => x.isObsolete) && !data.InheritedFields.Any(x => x.isObsolete))
            {
                sb.AppendLine();
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    foreach (var item in data.InheritedMethods.Where(x => x.isObsolete && x.IsAPI))
                    {
                        sb.AppendLine("| " + $"`[Obsolete] {item.fullSignature}`" + " | " + item.chineseComment +
                                      " | " +
                                      $"`{item.declaringType}`" + " |");
                    }

                    foreach (var item in data.InheritedEvents.Where(x => x.isObsolete && x.IsAPI))
                    {
                        sb.AppendLine("| " + $"`[Obsolete] {item.fullSignature}`" + " | " + item.chineseComment +
                                      " | " +
                                      $"`{item.declaringType}`" + " |");
                    }

                    foreach (var item in data.InheritedProperties.Where(x => x.isObsolete && x.IsAPI))
                    {
                        sb.AppendLine("| " + $"`[Obsolete] {item.fullSignature}`" + " | " + item.chineseComment +
                                      " | " +
                                      $"`{item.declaringType}`" + " |");
                    }

                    foreach (var item in data.InheritedFields.Where(x => x.isObsolete && x.IsAPI))
                    {
                        sb.AppendLine("| " + $"`[Obsolete] {item.fullSignature}`" + " | " + item.chineseComment +
                                      " | " +
                                      $"`{item.declaringType}`" + " |");
                    }

                    break;
                case DocCategory.CompleteReferences:
                    foreach (var item in data.InheritedMethods.Where(x => x.isObsolete))
                    {
                        sb.AppendLine("| " + $"`[Obsolete] {item.fullSignature}`" + " | " + item.chineseComment +
                                      " | " +
                                      $"`{item.declaringType}`" + " |");
                    }

                    foreach (var item in data.InheritedEvents.Where(x => x.isObsolete))
                    {
                        sb.AppendLine("| " + $"`[Obsolete] {item.fullSignature}`" + " | " + item.chineseComment +
                                      " | " +
                                      $"`{item.declaringType}`" + " |");
                    }

                    foreach (var item in data.InheritedProperties.Where(x => x.isObsolete))
                    {
                        sb.AppendLine("| " + $"`[Obsolete] {item.fullSignature}`" + " | " + item.chineseComment +
                                      " | " +
                                      $"`{item.declaringType}`" + " |");
                    }

                    foreach (var item in data.InheritedFields.Where(x => x.isObsolete))
                    {
                        sb.AppendLine("| " + $"`[Obsolete] {item.fullSignature}`" + " | " + item.chineseComment +
                                      " | " +
                                      $"`{item.declaringType}`" + " |");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            sb.AppendLine();
            return sb;
        }

        #endregion

        static void RaiseOnToastEvent(ToastPosition position, SdfIconType icon, string msg, Color color, float duration)
        {
            OnToastEvent?.Invoke(position, icon, msg, color, duration);
        }
    }
}
