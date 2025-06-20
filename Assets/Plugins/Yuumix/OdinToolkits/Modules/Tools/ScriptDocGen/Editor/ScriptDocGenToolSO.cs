using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.Editor;
using Yuumix.OdinToolkits.Common.InspectorLocalization;
using Yuumix.OdinToolkits.Common.ResetTool;
using Yuumix.OdinToolkits.Common.YuumixEditor;
using Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Runtime;

#pragma warning disable CS0414
namespace Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Editor
{
    public class ScriptDocGenToolSO : OdinEditorScriptableSingleton<ScriptDocGenToolSO>, IPluginReset,
        ICanSetBelongToWindow
    {
        public const string DefaultSaveFolderPath = "Assets/OdinToolkitsData/TypeStorageSO";

        public const string DefaultDocFolderPath = "Assets/OdinToolkitsData/Editor/Documents/";

        static StringBuilder _userRemarks = new StringBuilder().AppendLine("## Remarks")
            .AppendLine("- Remarks 之后的内容不会被覆盖，可以对特定的方法进行特殊说明")
            .AppendLine("- 不要修改标题级别和内容，`## Remarks` 是识别符");

        static OdinMenuEditorWindow _menuEditorWindow;

        public LocalizedHeaderWidget header = new LocalizedHeaderWidget(
            "脚本文档生成工具",
            "Script Doc Generate Tool",
            "给定一个 `Type` 类型的值，生成 Markdown 格式的文档，可选 Scripting API 或者 Complete References，默认支持 mkdocs-material。Scripting API 表示对外的，用户可以调用的程序接口文档，Complete References 表示包含所有成员的参考文档",
            "Given a value of type `Type`, generate a document in the format of Markdown, optional Scripting API and Complete References, and mkdocs-material is supported by default. Scripting API refers to the external, user-accessible interface documentation, and Complete References refers to the documentation containing all members"
        );

        [PropertyOrder(1)]
        [HorizontalGroup("Mode", 0.75f)]
        [ShowIf("_singleDoc", false)]
        [LocalizedDisplayWidgetConfig(fontSize: 14)]
        [GUIColor("green")]
        public LocalizedDisplayAsStringWidget singleMode = new LocalizedDisplayAsStringWidget(
            "单文档生成模式",
            "Single Document Generation Mode"
        );

        [PropertyOrder(1)]
        [HorizontalGroup("Mode", 0.75f)]
        [HideIf("_singleDoc", false)]
        [LocalizedDisplayWidgetConfig(fontSize: 14)]
        [GUIColor("green")]
        public LocalizedDisplayAsStringWidget multiMode = new LocalizedDisplayAsStringWidget(
            "多文档批量生成模式",
            "Multi-Documents Batch Generation Mode"
        );

        [PropertyOrder(1)]
        [HorizontalGroup("Mode", 0.24f)]
        [LocalizedButtonWidgetConfig("切换工具模式", "Switch Tool Mode", icon: SdfIconType.Hurricane)]
        public LocalizedButtonWidget switchMode = new LocalizedButtonWidget(SwitchToolMode);

        [PropertyOrder(1)]
        [EnumToggleButtons]
        [LocalizedTitle("文档种类", "Document Category")]
        [HideLabel]
        public DocCategory docCategory = DocCategory.ScriptingAPI;

        [PropertyOrder(1)]
        [EnumToggleButtons]
        [LocalizedTitle("Markdown 格式适配选择", "Markdown Format Selector")]
        [HideLabel]
        public MarkdownCategory markdownCategory = MarkdownCategory.MkDocsMaterial;

        [PropertyOrder(2)]
        [ShowIf("_singleDoc")]
        [HideLabel]
        public Type TargetType;

        [PropertyOrder(2)]
        [HideIf("_singleDoc")]
        [LabelWidth(200f)]
        [LocalizedText("类型存储器", "Type Storage")]
        [InfoBox("多文档批量生成模式中，TypeStorageSO 配置优先，会检测是否存在 SO 配置，存在则覆盖 typeDataList")]
        public TypeStorageSO typeStorage;

        [PropertyOrder(3)]
        [FolderPath]
        [BoxGroup("TempList")]
        [ShowIf(nameof(ShowSaveFolderPath))]
        [InlineButton("CompleteConfig", SdfIconType.Check, "确认")]
        [InlineButton("ResetSOSaveFolderPath", SdfIconType.ArrowClockwise, "")]
        [LocalizedText("类型配置文件存储文件夹路径", "Save Folder Path")]
        [LabelWidth(200f)]
        public string saveFolderPath = DefaultSaveFolderPath;

        [PropertyOrder(5)]
        [BoxGroup("TempList")]
        [HideIf("HideTempTypeList")]
        [LocalizedText("临时配置类型列表", "Temporary Type List")]
        [ListDrawerSettings(OnTitleBarGUI = nameof(SaveToTypeStorageSOAsset), NumberOfItemsPerPage = 10)]
        public List<Type> TempTypeList = new List<Type>();

        [PropertyOrder(10)]
        [LocalizedTitle("生成文档的文件夹路径", "Folder Path For Doc")]
        [HideLabel]
        [FolderPath(AbsolutePath = true)]
        [InlineButton(nameof(ResetDocFolderPath), SdfIconType.ArrowClockwise, "")]
        public string folderPath;

        [PropertyOrder(20)]
        [ShowIf("_singleDoc")]
        [LocalizedButtonWidgetConfig("解析单个 Type", "Analyze Single Type",
            ButtonSizes.Large, ButtonStyle.Box, SdfIconType.Activity)]
        public LocalizedButtonWidget analyzeSingleType = new LocalizedButtonWidget(AnalyzeSingle);

        [PropertyOrder(20)]
        [HideIf("_singleDoc")]
        [LocalizedButtonWidgetConfig("解析 Type 列表", "Analyze Type List",
            ButtonSizes.Large, ButtonStyle.Box, SdfIconType.Activity)]
        public LocalizedButtonWidget analyzeMultiTypes = new LocalizedButtonWidget(AnalyzeMultiple);

        [PropertyOrder(25)]
        [ShowIf(nameof(CanGeneratedSingle))]
        [LocalizedButtonWidgetConfig("生成 Markdown 文档", "Generate Markdown Document",
            ButtonSizes.Large, ButtonStyle.Box, SdfIconType.FileEarmarkPlus)]
        public LocalizedButtonWidget generateButtonSingle = new LocalizedButtonWidget(GenerateMkDocsSingle);

        [PropertyOrder(25)]
        [ShowIf(nameof(CanGeneratedMultiple))]
        [LocalizedButtonWidgetConfig("批量生成 Markdown 文档", "Generate Multiple Markdown Documents",
            ButtonSizes.Large, ButtonStyle.Box, SdfIconType.FileEarmarkPlus)]
        public LocalizedButtonWidget generateButtonMulti = new LocalizedButtonWidget(GenerateMkDocsMultiple);

        [PropertyOrder(105)]
        [ShowIf("_singleDoc")]
        public TypeData typeData;

        [PropertyOrder(105)]
        [HideIf("_singleDoc")]
        [ListDrawerSettings(HideAddButton = true, NumberOfItemsPerPage = 5)]
        public List<TypeData> typeDataList = new List<TypeData>();

        bool _thisHasAnalyzedSingle;
        bool _thisHasAnalyzedMultiple;
        bool _customizingSaveConfig;
        bool _singleDoc = true;

        bool ShowSaveFolderPath => _customizingSaveConfig && !_singleDoc && !typeStorage;
        bool HideTempTypeList => _singleDoc || typeStorage;
        bool CanShowGenerateTitle => CanGeneratedSingle || CanGeneratedMultiple;
        bool CanGeneratedSingle => _thisHasAnalyzedSingle && _singleDoc;
        bool CanGeneratedMultiple => _thisHasAnalyzedMultiple && !_singleDoc;

        public void SetWindow(OdinMenuEditorWindow window)
        {
            _menuEditorWindow = window;
        }

        public void ClearWindow()
        {
            _menuEditorWindow = null;
        }

        #region PluginReset

        public void PluginReset()
        {
            _singleDoc = true;
            docCategory = DocCategory.ScriptingAPI;
            markdownCategory = MarkdownCategory.MkDocsMaterial;
            TargetType = null;
            folderPath = DefaultDocFolderPath;
            typeData = new TypeData();
            _thisHasAnalyzedSingle = false;
            ClearWindow();
            typeDataList.Clear();
            _thisHasAnalyzedMultiple = false;
            typeStorage = null;
            _customizingSaveConfig = false;
            TempTypeList = null;
            ResetSOSaveFolderPath();
        }

        #endregion

        public void ResetSOSaveFolderPath()
        {
            saveFolderPath = DefaultSaveFolderPath;
        }

        public void ResetDocFolderPath()
        {
            folderPath = DefaultDocFolderPath;
        }

        public void CompleteConfig()
        {
            _customizingSaveConfig = false;
        }

        void SaveToTypeStorageSOAsset()
        {
            var image =
                SdfIcons.CreateTransparentIconTexture(SdfIconType.SaveFill, Color.white, 16 /*0x10*/, 16 /*0x10*/,
                    0);
            var content = new GUIContent(" 保存为SO资源 ", image, "保存为 TypeStorageSO 到 " + saveFolderPath);
            var filePath = saveFolderPath + "/TypeStorageSO.asset";

            if (TempTypeList.Count > 0)
            {
                if (SirenixEditorGUI.ToolbarButton(content))
                {
                    var so = CreateInstance<TypeStorageSO>();
                    PathEditorUtil.EnsureFolderRecursively(saveFolderPath);
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
            var content2 = new GUIContent(" 自定义资源存储位置 ", image2, "当前路径为 " + saveFolderPath);
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
            _menuEditorWindow.ShowToast(ToastPosition.BottomRight, SdfIconType.InfoSquareFill,
                Instance._singleDoc ? "成功切换为单文档生成模式" : "成功切换为多文档批量生成模式",
                Color.white, 5f);
        }

        #region OnInspectorGUI

        [OnInspectorGUI]
        [LocalizedTitle("工具使用模式", "Tool Usage Mode ")]
        public void FirstTitle() { }

        [PropertyOrder(1)]
        [LocalizedTitle("目标类型", "Target Type")]
        [OnInspectorGUI]
        public void TypeTitle() { }

        [PropertyOrder(19)]
        [LocalizedTitle("解析操作按钮", "Analyze Button")]
        [OnInspectorGUI]
        public void AnalyzeTitle() { }

        [PropertyOrder(24)]
        [LocalizedTitle("生成文档按钮", "Generate Document Buttons")]
        [ShowIf("CanShowGenerateTitle")]
        [OnInspectorGUI]
        public void GenerateTitle() { }

        [PropertyOrder(100)]
        [LocalizedTitle("过程数据", "Process Data")]
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
            if (_menuEditorWindow)
            {
                _menuEditorWindow.ShowToast(ToastPosition.BottomRight, SdfIconType.LightningFill,
                    "正在解析目标类型，请勿重复点击！等待文档生成按钮显示",
                    Color.yellow, 5f);
            }
        }

        static void AnalyzeMultiple()
        {
            Instance.typeDataList.Clear();
            var targetTypes = Instance.typeStorage ? Instance.typeStorage.Types : Instance.TempTypeList;
            targetTypes.RemoveAll(x => x == null);
            foreach (var typeData in targetTypes.Select(TypeData.FromType))
            {
                Instance.typeDataList.Add(typeData);
            }

            if (_menuEditorWindow)
            {
                _menuEditorWindow.ShowToast(ToastPosition.BottomRight, SdfIconType.LightningFill,
                    "正在解析目标类型，请勿重复点击！等待文档生成按钮显示",
                    Color.yellow, 5f);
            }

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
                finalStringBuilder.Append(_userRemarks);
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
                        var remarkIndex = Array.FindIndex(readAllLines, line => line.StartsWith("## Remarks"));
                        if (remarkIndex >= 0)
                        {
                            for (var i = remarkIndex; i < readAllLines.Length; i++)
                            {
                                remarkStringBuilder.AppendLine(readAllLines[i]);
                            }

                            finalStringBuilder.Replace(_userRemarks.ToString(), remarkStringBuilder.ToString());
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
                        finalStringBuilder.Append(_userRemarks);
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
                                var remarkIndex = Array.FindIndex(readAllLines, line => line.StartsWith("## Remarks"));
                                if (remarkIndex >= 0)
                                {
                                    for (var j = remarkIndex; j < readAllLines.Length; j++)
                                    {
                                        remarkStringBuilder.AppendLine(readAllLines[j]);
                                    }

                                    finalStringBuilder.Replace(_userRemarks.ToString(), remarkStringBuilder.ToString());
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
            if (data.NamespaceName.IsNullOrWhitespace())
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
    }
}
