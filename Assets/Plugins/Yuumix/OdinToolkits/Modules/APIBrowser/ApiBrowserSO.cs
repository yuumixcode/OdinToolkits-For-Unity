using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.Common.EditorLocalization;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Attributes;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Classes.InspectorGUI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Yuumix.OdinToolkits.Modules.APIBrowser
{
    public class ApiBrowserSO : SerializedScriptableObject
    {
        bool IsChinese => EditorLocalizationManagerSO.Instance.IsSimplifiedChinese;

        [BoxGroup("Header", false)]
        [HorizontalGroup("Header/Hori", 0.7f, MarginRight = -10)]
        [VerticalGroup("Header/Hori/L")]
        [PropertyOrder(-10)]
        [ShowIf("IsChinese", false)]
        [DisplayAsString(TextAlignment.Left, FontSize = 36)]
        [HideLabel]
        public string apiBrowserChineseName = "API 浏览器";

        [BoxGroup("Header")]
        [HorizontalGroup("Header/Hori", 0.7f)]
        [VerticalGroup("Header/Hori/L")]
        [PropertyOrder(-10)]
        [HideIf("IsChinese", false)]
        [DisplayAsString(TextAlignment.Left, FontSize = 36)]
        [HideLabel]
        public string apiBrowserEnglishName = "API Browser";

        [PropertyOrder(-5)]
        [BoxGroup("Header")]
        [HorizontalGroup("Header/Hori", 0.3f)]
        [VerticalGroup("Header/Hori/R")]
        public SwitchEditorLanguageButton switchEditorLanguageButton = new SwitchEditorLanguageButton();

        [PropertyOrder(-4)]
        [BoxGroup("Header")]
        [HorizontalGroup("Header/Hori", 0.3f)]
        [VerticalGroup("Header/Hori/R")]
        [LocalizedButtonConfig("打开文档", "Open Documentation")]
        public LocalizedButton languageButton = new LocalizedButton(EnglishToDocs);

        [PropertyOrder(-2)]
        [LocalizedTitle("目标类型选择器", "Target Type Selector")]
        [OnInspectorGUI]
        public void TypeSelectorTitle() { }

        [LocalizedText("选择程序集", "Select Assembly")]
        [ValueDropdown(nameof(GetDomainAssemblies))]
        public string targetAssemblyName;

        [ShowIf(nameof(TargetAssemblyNotNull))]
        [LocalizedText("选择目标类型", "Select Type")]
        [ValueDropdown(nameof(GetCanSelectedType))]
        public Type TargetType;

        bool TargetAssemblyNotNull => !string.IsNullOrEmpty(targetAssemblyName);

        #region 访问修饰符筛选

        [PropertyOrder(1)]
        [LocalizedTitle("访问修饰符筛选器", "Access Modifier Filter")]
        [OnInspectorGUI]
        public void AccessModifierTitleGUI() { }

        [PropertyOrder(2)]
        [SwitchButton]
        [HorizontalGroup("访问修饰符")]
        [LabelWidth(100)]
        [LocalizedText("公共成员", "Public Members")]
        public bool containPublic = true;

        [PropertyOrder(2)]
        [SwitchButton]
        [HorizontalGroup("访问修饰符")]
        [LabelWidth(100)]
        [LocalizedText("私有成员", "Private Members")]
        public bool containPrivate;

        [PropertyOrder(2)]
        [SwitchButton]
        [HorizontalGroup("访问修饰符")]
        [LabelWidth(120)]
        [LocalizedText("受保护的成员", "Protected Members")]
        public bool containProtected;

        [PropertyOrder(2)]
        [SwitchButton]
        [HorizontalGroup("访问修饰符")]
        [LabelWidth(110)]
        [LocalizedText("内部成员", "Internal Members")]
        public bool containInternal;

        #endregion

        #region 成员类别筛选

        [PropertyOrder(3)]
        [LocalizedTitle("成员类别筛选器", "Member Type Filter")]
        [OnInspectorGUI]
        public void MemberTypeTitleGUI() { }

        [PropertyOrder(4)]
        [SwitchButton]
        [HorizontalGroup("成员类别")]
        [LabelWidth(80)]
        [LocalizedText("方法", "Methods")]
        public bool containMethod = true;

        [PropertyOrder(4)]
        [SwitchButton]
        [HorizontalGroup("成员类别")]
        [LabelWidth(80)]
        [LocalizedText("字段", "Fields")]
        public bool containField;

        [PropertyOrder(4)]
        [SwitchButton]
        [HorizontalGroup("成员类别")]
        [LabelWidth(80)]
        [LocalizedText("属性", "Properties")]
        public bool containProperty;

        [PropertyOrder(4)]
        [SwitchButton]
        [HorizontalGroup("成员类别")]
        [LabelWidth(80)]
        [LocalizedText("事件", "Events")]
        public bool containEvent;

        [PropertyOrder(4)]
        [SwitchButton]
        [HorizontalGroup("成员类别")]
        [LabelWidth(80)]
        [LocalizedText("构造函数", "Constructor")]
        public bool containConstructor;

        #endregion

        #region 操作按钮

        [PropertyOrder(8)]
        [LocalizedTitle("操作按钮", "Operation Buttons")]
        [OnInspectorGUI]
        public void ButtonTitleGUI() { }

        [PropertyOrder(10)]
        [ButtonGroup("操作")]
        [ShowIf(nameof(IsChinese), false)]
        [Button("收集所有 API", ButtonSizes.Large)]
        public void ChineseExecuteGUI()
        {
            Execute();
        }

        [PropertyOrder(10)]
        [ButtonGroup("操作")]
        [HideIf(nameof(IsChinese), false)]
        [Button("Collect All API", ButtonSizes.Large)]
        public void EnglishExecuteGUI()
        {
            Execute();
        }

        [PropertyOrder(11)]
        [ButtonGroup("操作")]
        [ShowIf(nameof(IsChinese), false)]
        [Button("根据条件筛选 API", ButtonSizes.Large)]
        public void ChineseFilterGUI()
        {
            Filter();
        }

        [PropertyOrder(11)]
        [ButtonGroup("操作")]
        [HideIf(nameof(IsChinese), false)]
        [Button("Filter API By Condition", ButtonSizes.Large)]
        public void EnglishFilterGUI()
        {
            Filter();
        }

        #endregion

        [PropertyOrder(28)]
        [LocalizedTitle("最终显示 API 数据", "Final Display Of API Data")]
        [OnInspectorGUI]
        public void APIListTitleGUI() { }

        [PropertyOrder(30)]
        [TableList]
        [HideLabel]
        public List<ApiDisplayItem> finalAllApiDisplayItems;

        static void EnglishToDocs()
        {
            Debug.Log("Open xxx web, function test");
        }

        void Execute()
        {
            if (TargetType == null)
            {
                return;
            }

            var apiRawDataList = new List<ApiRawData>();
            apiRawDataList.AddRange(ApiBrowserUtil.CollectRawMethodInfo(TargetType).HandleMethodInfo());
            apiRawDataList.AddRange(ApiBrowserUtil.CollectRawFieldInfo(TargetType));
            apiRawDataList.AddRange(ApiBrowserUtil.CollectRawPropertyInfo(TargetType).HandlePropertyInfo());
            apiRawDataList.AddRange(ApiBrowserUtil.CollectRawEventInfo(TargetType).HandleEventInfo());
            apiRawDataList.AddRange(ApiBrowserUtil.CollectRawConstructors(TargetType));
            apiRawDataList.Sort(new ApiDataComparer());
            finalAllApiDisplayItems = ApiBrowserUtil.CreateApiDisplayItemList(apiRawDataList);
        }

        void Filter()
        {
            Execute();
            if (finalAllApiDisplayItems == null || finalAllApiDisplayItems.Count == 0)
            {
                Debug.LogWarning("没有可筛选的 API 数据，请先选择有效类型");
                return;
            }

            var filteredList = finalAllApiDisplayItems
                .Where(FilterByMemberTypes)
                .Where(FilterByAccessModifier)
                .ToList();
            finalAllApiDisplayItems = filteredList;
            Debug.Log($"成功筛选出 {filteredList.Count} 条符合条件的 API 数据");
#if UNITY_EDITOR
            EditorUtility.DisplayDialog(
                "筛选完成",
                $"{filteredList.Count} 条 API 数据已按条件过滤",
                "确定"
            );
#endif
        }

        bool FilterByMemberTypes(ApiDisplayItem item)
        {
            if (!containMethod && !containField && !containProperty && !containEvent && !containConstructor)
            {
                return false;
            }

            return (item.rawData.memberType == MemberTypes.Method && containMethod) ||
                   (item.rawData.memberType == MemberTypes.Field && containField) ||
                   (item.rawData.memberType == MemberTypes.Property && containProperty) ||
                   (item.rawData.memberType == MemberTypes.Event && containEvent) ||
                   (item.rawData.memberType == MemberTypes.Constructor && containConstructor);
        }

        bool FilterByAccessModifier(ApiDisplayItem item)
        {
            if (!containPublic && !containPrivate && !containProtected && !containInternal)
            {
                return false;
            }

            var matchPublic = containPublic && item.rawData.IsPublic;
            var matchPrivate = containPrivate && item.rawData.IsPrivate;
            var matchProtected = containProtected && item.rawData.IsProtected;
            var matchInternal = containInternal && item.rawData.IsInternal;

            return matchPublic || matchPrivate || matchProtected || matchInternal;
        }

        ValueDropdownList<string> GetDomainAssemblies()
        {
            var list = new ValueDropdownList<string>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                list.Add(assembly.GetName().Name, assembly.ToString());
            }

            return list;
        }

        ValueDropdownList<Type> GetCanSelectedType()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(x => x.ToString() == targetAssemblyName);
            var list = new ValueDropdownList<Type>();
            if (assembly != null)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    list.Add(type.Name, type);
                }
            }

            return list;
        }
    }
}
