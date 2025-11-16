using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.Editor;
using YuumixEditor;

namespace Yuumix.OdinToolkits.ScriptDocGenerator.Editor
{
    /// <summary>
    /// ScriptDocGenerator 可视化操作面板类
    /// </summary>
    public class ScriptDocGeneratorVisualPanelSO : OdinEditorScriptableSingleton<ScriptDocGeneratorVisualPanelSO>,
        IOdinToolkitsEditorReset
    {
        public const string DEFAULT_DOC_FOLDER_PATH =
            OdinToolkitsEditorPaths.ODIN_TOOLKITS_ANY_DATA_ROOT_FOLDER + "/Editor/Documents/";

        const string NONE_ASSEMBLY = "None Assembly";

        public static readonly BilingualData ModuleName =
            new BilingualData("脚本文档生成工具", "Script Doc Generator");

        ScriptDocGeneratorController _controller;

        bool IsSingleType => typeSource == TypeSource.SingleType;
        bool IsMultipleType => typeSource == TypeSource.MultipleTypes;
        bool IsSingleAssembly => typeSource == TypeSource.SingleAssembly;

        #region Event Functions

        void OnEnable()
        {
            headerWidget = new BilingualHeaderWidget(
                "脚本文档生成工具", "Script Doc Generator",
                "用户提供一个 Type 类型的值，分析 Type 数据，选择合适的文档生成器，一键生成对应的文档。默认提供中文 API 文档生成器，可以自定义适合项目的生成器。",
                "The user provides a value of the Type, analyze the Type data, selects the appropriate document generator, " +
                "and generates the corresponding document with one click. By default, a Chinese API document generator is provided, " +
                "and a custom generator suitable for the project can also be defined."
            );
            _controller ??= new ScriptDocGeneratorController();
        }

        #endregion

        #region Serialized Fields

        [PropertyOrder(-5)]
        public BilingualHeaderWidget headerWidget;

        [PropertyOrder(2)]
        [BilingualTitle("生成脚本文档的目标文件夹路径 [可拖拽]", "Folder Path For Document [Drag And Drop Allowed]")]
        [HideLabel]
        [FolderPath(AbsolutePath = true)]
        [InlineButton(nameof(ResetDocFolderPath), SdfIconType.ArrowClockwise, "")]
        [CustomContextMenu("Reset To Default", nameof(ResetDocFolderPath))]
        public string docFolderPath;

        #endregion

        #region 文档生成器选择

        [PropertyOrder(10)]
        [Title("$GetDocGeneratorTitle")]
        [HideLabel]
        [InlineButton(nameof(ResetDocGeneratorSettingSO), SdfIconType.ArrowClockwise, "")]
        [CustomContextMenu("Reset To Default", nameof(ResetDocGeneratorSettingSO))]
        [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        public DocGeneratorSettingSO docGeneratorSetting;

        string GetDocGeneratorTitle()
        {
            var chineseTitle = "文档生成器设置";
            var englishTitle = "Doc Generator Setting";
            if (docGeneratorSetting && docGeneratorSetting.GetType() == typeof(CnAPIDocGeneratorSettingSO))
            {
                chineseTitle += " - [当前选择: 中文 API Markdown 文档]";
                englishTitle += " - [Current Selection: Chinese API Markdown Document]";
            }

            return new BilingualData(chineseTitle, englishTitle);
        }

        #endregion

        #region 类型来源枚举

        [PropertyOrder(20)]
        [EnumPaging]
        [Title("$GetTypeSourceEnumLabelText")]
        [HideLabel]
        [InlineButton("ResetTypeSource", SdfIconType.ArrowClockwise, "")]
        [CustomContextMenu("Reset To Default", "ResetTypeSource")]
        public TypeSource typeSource;

        string GetTypeSourceEnumLabelText()
        {
            var chineseText = "类型来源模式";
            var englishText = "Type Source Mode";
            switch (typeSource)
            {
                case TypeSource.SingleType:
                    chineseText += " - [当前选择: 单类型模式]";
                    englishText += " - [Current Selection: Single Type Mode]";
                    break;
                case TypeSource.MultipleTypes:
                    chineseText += " - [当前选择: 多类型模式]";
                    englishText += " - [Current Selection: Multiple Type Mode]";
                    break;
                case TypeSource.SingleAssembly:
                    chineseText += " - [当前选择: 单程序集模式]";
                    englishText += " - [Current Selection: Single Assembly Mode]";
                    break;
            }

            return new BilingualData(chineseText, englishText);
        }

        public enum TypeSource
        {
            SingleType,
            MultipleTypes,
            SingleAssembly
        }

        #endregion

        #region 单类型模式

        [PropertyOrder(25)]
        [ShowIf("IsSingleType")]
        [Title("$_singleTypeDataLabel")]
        [HideLabel]
        [InlineButton("ResetSingleType", SdfIconType.ArrowClockwise, "")]
        [CustomContextMenu("Reset To Default", "ResetSingleType")]
        public Type TargetType;

        BilingualData _singleTypeDataLabel = new BilingualData("目标 Type", "Single Target Type");

        #endregion

        #region 多类型模式

        [PropertyOrder(25)]
        [ShowIf("IsMultipleType")]
        [BilingualTitle("目标 Types 列表配置", "Types Config")]
        [BilingualInfoBox("TypesConfigSO 不为空时，会强制覆盖 Type 列表",
            "When the TypesConfigSO asset is not empty, TemporaryTypes Config is forced to be overridden")]
        [HideLabel]
        [AssetSelector(FlattenTreeView = true)]
        [InlineButton("ResetTypesCacheSO", SdfIconType.ArrowClockwise, "")]
        [CustomContextMenu("Reset To Default", "ResetTypesCacheSO")]
        public TypesCacheSO typesCache;

        [PropertyOrder(25)]
        [ShowIf("CanShowTemporaryTypes")]
        [BilingualTitle("Type 列表", "TemporaryTypes Config")]
        [ListDrawerSettings(OnTitleBarGUI = nameof(DrawTemporaryTypesTitleBarGUI), NumberOfItemsPerPage = 5)]
        [HideLabel]
        [InlineButton("ResetTemporaryTypes", SdfIconType.ArrowClockwise, "")]
        [CustomContextMenu("Reset To Default", "ResetTemporaryTypes")]
        public List<Type> TemporaryTypes = new List<Type>();

        [PropertyOrder(25)]
        [FolderPath]
        [ShowIf("ShowSaveFolderPath")]
        [HideLabel]
        [InlineButton("CompleteConfig", SdfIconType.Check, "$_completeConfigButtonLabel")]
        [InlineButton("ResetTypesCacheSOFolderPath", SdfIconType.ArrowClockwise, "$_resetSOSaveFolderPathButtonLabel")]
        [BilingualTitle("存放 TypesCacheSO 的文件夹路径", "Folder Path For TypesCacheSO")]
        [CustomContextMenu("Reset To Default", nameof(ResetTypesCacheSOFolderPath))]
        public string typesCacheSOFolderPath = DEFAULT_TYPES_CACHE_SO_FOLDER_PATH;

        BilingualData _completeConfigButtonLabel = new BilingualData("完成设置", "Complete Setting");

        BilingualData _resetSOSaveFolderPathButtonLabel =
            new BilingualData("重置路径", "Reset Folder Path");

        bool ShowSaveFolderPath => IsMultipleType && _isCustomizingSaveConfig && !typesCache;
        bool CanShowTemporaryTypes => IsMultipleType && !typesCache;

        const string DEFAULT_TYPES_CACHE_SO_FOLDER_PATH =
            OdinToolkitsEditorPaths.ODIN_TOOLKITS_ANY_DATA_ROOT_FOLDER + "/Editor/TypesCacheSO";

        bool _isCustomizingSaveConfig;

        void DrawTemporaryTypesTitleBarGUI()
        {
            var image =
                SdfIcons.CreateTransparentIconTexture(SdfIconType.SaveFill, Color.white, 16 /*0x10*/, 16 /*0x10*/,
                    0);
            var content = new GUIContent(" 保存为SO资源 ", image,
                "保存为 " + nameof(TypesCacheSO) + " 资源到 " + typesCacheSOFolderPath);
            var filePathWithExtension = typesCacheSOFolderPath + "/" + nameof(TypesCacheSO) + ".asset";
            if (TemporaryTypes.Count > 0 && SirenixEditorGUI.ToolbarButton(content))
            {
                var so = CreateInstance<TypesCacheSO>();
                PathEditorUtility.CreateDirectoryRecursivelyInAssets(typesCacheSOFolderPath);
                so.Types = TemporaryTypes;
                ProjectWindowUtil.CreateAsset(so, filePathWithExtension);
                ProjectEditorUtility.PingAndSelectAsset(filePathWithExtension);
                YuumixLogger.OdinToolkitsLog("请更改资源名称，避免下次生成时覆盖内容");
            }

            var image2 =
                SdfIcons.CreateTransparentIconTexture(SdfIconType.GearFill, Color.white, 16 /*0x10*/, 16 /*0x10*/,
                    0);
            var content2 = new GUIContent(" 自定义资源存储位置 ", image2, "当前路径为 " + typesCacheSOFolderPath);
            if (_isCustomizingSaveConfig)
            {
                return;
            }

            if (SirenixEditorGUI.ToolbarButton(content2))
            {
                _isCustomizingSaveConfig = true;
            }
        }

        void CompleteConfig()
        {
            _isCustomizingSaveConfig = false;
        }

        #endregion

        #region 单程序集模式

        [PropertyOrder(35)]
        [ShowIf("IsSingleAssembly")]
        [BilingualTitle("目标程序集配置", "Single Assembly Config")]
        [ValueDropdown("GetAssemblyNameToFullName")]
        [HideLabel]
        [InlineButton("ResetSingleAssemblyFullName", SdfIconType.ArrowClockwise, "")]
        [CustomContextMenu("Reset To Default", "ResetSingleAssemblyFullName")]
        public string targetAssemblyFullName;

        static ValueDropdownList<string> _currentDomainAssemblies;

        static ValueDropdownList<string> GetAssemblyNameToFullName()
        {
            if (_currentDomainAssemblies != null &&
                (_currentDomainAssemblies != null || _currentDomainAssemblies.Count > 0))
            {
                return _currentDomainAssemblies;
            }

            _currentDomainAssemblies = new ValueDropdownList<string> { { NONE_ASSEMBLY, NONE_ASSEMBLY } };
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                _currentDomainAssemblies.Add(assembly.GetName().Name, assembly.FullName);
            }

            return _currentDomainAssemblies;
        }

        #endregion

        #region 类型分析操作

        bool _hasFinishedAnalyze;

        [PropertyOrder(50)]
        [BilingualTitle("分析按钮", "Analyze Button")]
        [BilingualButton("基于当前模式执行类型分析", "Analyze Type based on the current mode", ButtonSizes.Large, ButtonStyle.Box,
            SdfIconType.FileEarmarkPlus)]
        public void AnalyzeType()
        {
            switch (typeSource)
            {
                case TypeSource.SingleType:
                    if (TargetType == null)
                    {
                        YuumixLogger.OdinToolkitsError("请选择有效的目标类型");
                        return;
                    }

                    TypeData = _controller.AnalyzeSingleType(TargetType);
                    break;
                case TypeSource.MultipleTypes:
                    if (!typesCache && TemporaryTypes.Count <= 0)
                    {
                        YuumixLogger.OdinToolkitsError("设置有效的 Type 对象列表或者设置 TypeCacheSO 资源");
                        return;
                    }

                    TypeDataList = typesCache
                        ? _controller.AnalyzeMultipleTypes(typesCache)
                        : _controller.AnalyzeMultipleTypes(TemporaryTypes);

                    break;
                case TypeSource.SingleAssembly:
                    if (targetAssemblyFullName is null or NONE_ASSEMBLY)
                    {
                        YuumixLogger.OdinToolkitsError("请选择目标程序集，不能为 " + NONE_ASSEMBLY);
                        return;
                    }

                    TypeDataList = _controller.AnalyzeSingleAssembly(targetAssemblyFullName);
                    break;
            }

            _hasFinishedAnalyze = true;
            ToastRequested?.Invoke(ToastPosition.BottomRight, SdfIconType.LightningFill,
                "分析中，等待生成按钮显示。请勿连续点击！",
                Color.yellow, 4f);
        }

        public static event Action<ToastPosition, SdfIconType, string, Color, float> ToastRequested;

        #endregion

        #region 生成操作

        [PropertyOrder(70)]
        [ShowIf("CanShowGenerateButton")]
        [BilingualTitle("生成按钮", "Generate Button")]
        [BilingualButton("基于解析结果和文档生成器生成 Markdown 文档",
            "Generate Markdown Document Based On Analysis Result And Doc Generator",
            ButtonSizes.Large, ButtonStyle.Box, SdfIconType.FileEarmarkPlus)]
        public void GenerateDoc()
        {
            if (!Directory.Exists(docFolderPath))
            {
                if (!EditorUtility.DisplayDialog("自动路径补全提示", "当前的文档导出路径不存在，是否自动生成文件夹路径？", "确认", "取消"))
                {
                    return;
                }

                PathEditorUtility.CreateDirectoryRecursivelyInAssets(docFolderPath);
            }

            switch (typeSource)
            {
                case TypeSource.SingleType:
                    _controller.GenerateSingleTypeDoc(TypeData, docGeneratorSetting, docFolderPath);
                    break;
                case TypeSource.MultipleTypes:
                case TypeSource.SingleAssembly:
                    _controller.GenerateMultipleTypeDocs(TypeDataList, docGeneratorSetting, docFolderPath);
                    break;
            }

            _hasFinishedAnalyze = false;
        }

        bool CanShowGenerateButton => _hasFinishedAnalyze && (
            (IsSingleType && TypeData != null) ||
            (IsMultipleType && TypeDataList is { Count: > 0 }) ||
            (IsSingleAssembly && TypeDataList is { Count: > 0 }));

        #endregion

        #region 类型解析结果

        [PropertyOrder(90)]
        [TitleGroup("$_typeAnalysisResultLabel")]
        [ShowIf("$IsSingleType")]
        public ITypeData TypeData;

        [PropertyOrder(90)]
        [ShowIf("$IsNeedTypeAnalysisDataList")]
        [TitleGroup("$_typeAnalysisResultLabel")]
        public List<ITypeData> TypeDataList;

        bool IsNeedTypeAnalysisDataList => IsMultipleType || IsSingleAssembly;
        BilingualData _typeAnalysisResultLabel = new BilingualData("类型分析数据结果", "Type Analysis Result");

        #endregion

        #region 恢复默认值

        public void EditorReset()
        {
            ResetDocFolderPath();
            ResetDocGeneratorSettingSO();
            ResetTypeSource();
            ResetSingleType();
            ResetTypesCacheSO();
            ResetTemporaryTypes();
            ResetTypesCacheSOFolderPath();
            ResetIsCustomizingSaveConfig();
            ResetSingleAssemblyFullName();
            ResetHasFinishedAnalyzed();
            ResetTypeAnalysisData();
        }

        void ResetTypeSource()
        {
            typeSource = TypeSource.SingleType;
        }

        void ResetDocFolderPath()
        {
            docFolderPath = DEFAULT_DOC_FOLDER_PATH;
        }

        void ResetDocGeneratorSettingSO()
        {
            docGeneratorSetting = Resources.Load<CnAPIDocGeneratorSettingSO>("DocGeneratorSettings/中文API文档生成设置");
        }

        void ResetSingleType()
        {
            TargetType = null;
        }

        void ResetTypesCacheSO()
        {
            typesCache = null;
        }

        void ResetTemporaryTypes()
        {
            TemporaryTypes = new List<Type>();
        }

        void ResetTypesCacheSOFolderPath()
        {
            typesCacheSOFolderPath = DEFAULT_TYPES_CACHE_SO_FOLDER_PATH;
        }

        void ResetIsCustomizingSaveConfig()
        {
            _isCustomizingSaveConfig = false;
        }

        void ResetSingleAssemblyFullName()
        {
            targetAssemblyFullName = string.Empty;
        }

        void ResetHasFinishedAnalyzed()
        {
            _hasFinishedAnalyze = false;
        }

        void ResetTypeAnalysisData()
        {
            TypeData = null;
            TypeDataList = null;
        }

        #endregion
    }
}
