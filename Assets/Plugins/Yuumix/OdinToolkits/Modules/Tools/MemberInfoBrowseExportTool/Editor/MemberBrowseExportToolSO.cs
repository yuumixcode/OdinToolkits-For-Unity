using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.Editor.ScriptableSingleton;
using Yuumix.OdinToolkits.Common.InspectorLocalization;
using Yuumix.OdinToolkits.Common.InspectorLocalization.Attributes;
using Yuumix.OdinToolkits.Common.Runtime.ResetTool;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Attributes;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Classes.InspectorGUIWidgets;

namespace Yuumix.OdinToolkits.Modules.Tools.MemberInfoBrowseExportTool.Editor
{
    public class MemberBrowseExportToolSO : OdinEditorScriptableSingleton<MemberBrowseExportToolSO>,
        IPluginReset
    {
        public void PluginReset()
        {
            targetAssemblyName = string.Empty;
            TargetType = null;
            finalAllApiDisplayItems = new List<MemberDisplayItem>();
        }

        public InspectorHeaderWidget headerWidget =
            new InspectorHeaderWidget("成员信息浏览导出工具", "MemberInfo Browser & Exporter",
                "快速浏览目标类型的所有成员信息，包括但不限于字段，方法。以及可以导出特殊的 Markdown 格式的 API 文档或者所有成员文档",
                "Quickly browse all MemberInfo of the target type, including but not limited to FieldInfo, MethodInfo. " +
                "And you can export API documents or all MemberInfo documents in special Markdown format.",
                "https://www.odintoolkits.cn/");

        #region 选择类型配置

        [LocalizedTitle("类型选择器", "Type Selector")]
        [LocalizedText("选择程序集", "Select Assembly")]
        [ValueDropdown(nameof(GetDomainAssemblies))]
        [OnValueChanged(nameof(OnAssemblyChanged))]
        public string targetAssemblyName;

        [ShowIf(nameof(IsCanSelected))]
        [LocalizedText("选择目标类型", "Select Type")]
        [ValueDropdown(nameof(GetCanSelectedType))]
        public Type TargetType;

        bool IsCanSelected => !string.IsNullOrEmpty(targetAssemblyName) && GetCanSelectedType().Count > 0;

        void OnAssemblyChanged()
        {
            if (targetAssemblyName == string.Empty)
            {
                TargetType = null;
            }
        }

        ValueDropdownList<string> GetDomainAssemblies()
        {
            var list = new ValueDropdownList<string>
            {
                new ValueDropdownItem<string>("None", "")
            };
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
            if (assembly == null)
            {
                return list;
            }

            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                list.Add(type.Name, type);
            }

            return list;
        }

        #endregion

        #region 访问修饰符筛选

        [PropertyOrder(1)]
        [LocalizedTitle("访问修饰符筛选", "Member Access Modifier Filter")]
        [OnInspectorGUI]
        public void AccessModifierTitleGUI() { }

        [PropertyOrder(2)]
        [SwitchButton]
        [HorizontalGroup("AccessModifier")]
        [LabelWidth(120)]
        [LocalizedText("公共成员", "Public Members")]
        public bool containPublic = true;

        [PropertyOrder(2)]
        [SwitchButton]
        [HorizontalGroup("AccessModifier")]
        [LabelWidth(120)]
        [LocalizedText("私有成员", "Private Members")]
        public bool containPrivate;

        [PropertyOrder(2)]
        [SwitchButton]
        [HorizontalGroup("AccessModifier")]
        [LabelWidth(120)]
        [LocalizedText("受保护的成员", "Protected Members")]
        public bool containProtected;

        [PropertyOrder(2)]
        [SwitchButton]
        [HorizontalGroup("AccessModifier")]
        [LabelWidth(120)]
        [LocalizedText("内部成员", "Internal Members")]
        public bool containInternal;

        #endregion

        #region 成员类别筛选

        [PropertyOrder(3)]
        [LocalizedTitle("成员类别筛选", "Member Type Filter")]
        [OnInspectorGUI]
        public void MemberTypeTitleGUI() { }

        [PropertyOrder(4)]
        [SwitchButton]
        [HorizontalGroup("Member Type Filter")]
        [LabelWidth(100)]
        [LocalizedText("方法", "Methods")]
        public bool containMethod = true;

        [PropertyOrder(4)]
        [SwitchButton]
        [HorizontalGroup("Member Type Filter")]
        [LabelWidth(100)]
        [LocalizedText("字段", "Fields")]
        public bool containField;

        [PropertyOrder(4)]
        [SwitchButton]
        [HorizontalGroup("Member Type Filter")]
        [LabelWidth(100)]
        [LocalizedText("属性", "Properties")]
        public bool containProperty;

        [PropertyOrder(4)]
        [SwitchButton]
        [HorizontalGroup("Member Type Filter")]
        [LabelWidth(100)]
        [LocalizedText("事件", "Events")]
        public bool containEvent;

        [PropertyOrder(4)]
        [SwitchButton]
        [HorizontalGroup("Member Type Filter")]
        [LabelWidth(100)]
        [LocalizedText("构造函数", "Constructor")]
        public bool containConstructor;

        #endregion

        #region 操作按钮

        bool IsChinese => InspectorLocalizationManagerSO.Instance.IsChinese;

        [PropertyOrder(8)]
        [LocalizedTitle("操作按钮", "Operation Buttons")]
        [OnInspectorGUI]
        public void ButtonTitleGUI() { }

        [PropertyOrder(10)]
        [ButtonGroup("操作")]
        [ShowIf(nameof(IsChinese), false)]
        [Button("收集所有成员信息", ButtonSizes.Large)]
        public void ChineseExecuteGUI()
        {
            Execute();
        }

        [PropertyOrder(10)]
        [ButtonGroup("操作")]
        [HideIf(nameof(IsChinese), false)]
        [Button("Collect All Member Info", ButtonSizes.Large)]
        public void EnglishExecuteGUI()
        {
            Execute();
        }

        [PropertyOrder(11)]
        [ButtonGroup("操作")]
        [ShowIf(nameof(IsChinese), false)]
        [Button("根据条件筛选成员", ButtonSizes.Large)]
        public void ChineseFilterGUI()
        {
            Filter();
        }

        [PropertyOrder(11)]
        [ButtonGroup("操作")]
        [HideIf(nameof(IsChinese), false)]
        [Button("Filter Member Info By Condition", ButtonSizes.Large)]
        public void EnglishFilterGUI()
        {
            Filter();
        }

        void Execute()
        {
            if (TargetType == null)
            {
                finalAllApiDisplayItems = new List<MemberDisplayItem>();
                return;
            }

            var apiRawDataList = new List<MemberRawData>();
            apiRawDataList.AddRange(MemberInfoBrowseExportUtil.CollectRawMethodInfo(TargetType).HandleMethodInfo());
            apiRawDataList.AddRange(MemberInfoBrowseExportUtil.CollectRawFieldInfo(TargetType));
            apiRawDataList.AddRange(MemberInfoBrowseExportUtil.CollectRawPropertyInfo(TargetType).HandlePropertyInfo());
            apiRawDataList.AddRange(MemberInfoBrowseExportUtil.CollectRawEventInfo(TargetType).HandleEventInfo());
            apiRawDataList.AddRange(MemberInfoBrowseExportUtil.CollectRawConstructors(TargetType));
            apiRawDataList.Sort(new MemberRawDataComparer());
            finalAllApiDisplayItems = MemberInfoBrowseExportUtil.CreateApiDisplayItemList(apiRawDataList);
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

        bool FilterByMemberTypes(MemberDisplayItem item)
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

        bool FilterByAccessModifier(MemberDisplayItem item)
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

        #endregion

        #region 结果显示

        [PropertyOrder(28)]
        [LocalizedTitle("最终显示 API 数据", "Final Display Of API Data")]
        [OnInspectorGUI]
        public void APIListTitleGUI() { }

        [PropertyOrder(30)]
        [TableList]
        [HideLabel]
        public List<MemberDisplayItem> finalAllApiDisplayItems;

        #endregion

        #region 生成文档

        [FolderPath]
        public string targetPath;

        [Button("生成文档")]
        public void GenerateDocument()
        {
            if (!Directory.Exists(targetPath))
            {
                Debug.LogError("请选择有效的目标路径");
                return;
            }

            Execute();
            var sb = new StringBuilder();
            // --- 头部
            sb.AppendLine("# " + TargetType.Name);
            sb.AppendLine("> " + TargetType.FullName);
            sb.AppendLine();
            // --- 介绍
            if (TargetType.GetCustomAttribute<LocalizedCommentAttribute>() != null)
            {
                sb.AppendLine("## 介绍 Description");
                sb.AppendLine(TargetType.GetCustomAttribute<LocalizedCommentAttribute>().ChineseComment);
                var english = TargetType.GetCustomAttribute<LocalizedCommentAttribute>().EnglishComment;
                if (!string.IsNullOrEmpty(english))
                {
                    sb.AppendLine();
                    sb.AppendLine(TargetType.GetCustomAttribute<LocalizedCommentAttribute>().EnglishComment);
                }

                sb.AppendLine();
            }

            // --- 构造函数
            if (finalAllApiDisplayItems.Any(x => x.rawData.memberType == MemberTypes.Constructor))
            {
                sb.AppendLine("## 构造 Constructors");
                sb.AppendLine("| 构造函数 | 注释 Comment |");
                sb.AppendLine("| :--- | :--- |");
                foreach (var item in finalAllApiDisplayItems
                             .Where(x => x.rawData.memberType == MemberTypes.Constructor
                                         && !x.IsObsolete))
                {
                    sb.AppendLine("| " + item.FullMemberSignature + " | " + item.ChineseComment + " |");
                }

                if (finalAllApiDisplayItems.Any(x => x.rawData.memberType == MemberTypes.Constructor
                                                     && x.IsObsolete))
                {
                    sb.AppendLine();
                    sb.AppendLine("### 弃用 Obsolete");
                    sb.AppendLine("| 构造函数 | 注释 Comment |");
                    sb.AppendLine("| :--- | :--- |");
                    foreach (var item in finalAllApiDisplayItems
                                 .Where(x => x.rawData.memberType == MemberTypes.Constructor && x.IsObsolete))
                    {
                        sb.AppendLine("| " + item.FullMemberSignature + " | " + item.ChineseComment + " |");
                    }
                }

                sb.AppendLine();
            }

            // --- 静态字段
            if (finalAllApiDisplayItems.Any(x => x.rawData.memberType == MemberTypes.Field && x.rawData.isStatic))
            {
                sb.AppendLine("## 静态字段 Static Fields");
                sb.AppendLine("| 静态字段 | 完整签名 | 注释 Comment |");
                sb.AppendLine("| :--- | :--- | :--- |");
                foreach (var item in finalAllApiDisplayItems
                             .Where(x => x.rawData.memberType == MemberTypes.Field && x.rawData.isStatic &&
                                         !x.IsObsolete))
                {
                    sb.AppendLine("| " + item.MemberName + " | " + item.FullMemberSignature + " | " +
                                  item.ChineseComment + " |");
                }

                if (finalAllApiDisplayItems.Any(x => x.rawData.memberType == MemberTypes.Field && x.rawData.isStatic
                        && x.IsObsolete))
                {
                    sb.AppendLine();
                    sb.AppendLine("### 弃用 Obsolete");
                    sb.AppendLine("| 静态字段成员 | 完整签名 | 注释 Comment |");
                    sb.AppendLine("| :--- | :--- | :--- |");
                    foreach (var item in finalAllApiDisplayItems
                                 .Where(x => x.rawData.memberType == MemberTypes.Field && x.rawData.isStatic &&
                                             x.IsObsolete))
                    {
                        sb.AppendLine("| " + item.MemberName + " | " + item.FullMemberSignature + " | " +
                                      item.ChineseComment + " |");
                    }
                }

                sb.AppendLine();
            }

            // --- 静态属性
            if (finalAllApiDisplayItems.Any(x => x.rawData.memberType == MemberTypes.Property && x.rawData.isStatic))
            {
                sb.AppendLine("## 静态属性 Static Properties");
                sb.AppendLine("| 静态属性成员 | 完整签名 | 注释 Comment |");
                sb.AppendLine("| :--- | :--- | :--- |");
                foreach (var item in finalAllApiDisplayItems.Where(x =>
                             x.rawData.memberType == MemberTypes.Field && x.rawData.isStatic &&
                             !x.IsObsolete))
                {
                    sb.AppendLine("| " + item.MemberName + " | " + item.FullMemberSignature + " | " +
                                  item.ChineseComment + " |");
                }

                if (finalAllApiDisplayItems.Any(x => x.rawData.memberType == MemberTypes.Property && x.rawData.isStatic
                        && x.IsObsolete))
                {
                    sb.AppendLine();
                    sb.AppendLine("### 弃用 Obsolete");
                    sb.AppendLine("| 静态属性成员 | 完整签名 | 注释 Comment |");
                    sb.AppendLine("| :--- | :--- | :--- |");
                    foreach (var item in finalAllApiDisplayItems
                                 .Where(x => x.rawData.memberType == MemberTypes.Property && x.rawData.isStatic &&
                                             x.IsObsolete))
                    {
                        sb.AppendLine("| " + item.MemberName + " | " + item.FullMemberSignature + " | " +
                                      item.ChineseComment + " |");
                    }
                }
            }

            // --- 静态方法
            if (finalAllApiDisplayItems.Any(x => x.rawData.memberType == MemberTypes.Method))
            {
                sb.AppendLine("## 静态方法 Static Methods");
                sb.AppendLine("| 静态方法成员 | 完整签名 | 注释 Comment |");
                sb.AppendLine("| :--- | :--- | :--- |");
                foreach (var item in finalAllApiDisplayItems.Where(x =>
                             x.rawData.memberType == MemberTypes.Method && x.rawData.isStatic &&
                             !x.IsObsolete))
                {
                    sb.AppendLine("| " + item.MemberName + " | " + item.FullMemberSignature + " | " +
                                  item.ChineseComment + " |");
                }

                if (finalAllApiDisplayItems.Any(x => x.rawData.memberType == MemberTypes.Method && x.rawData.isStatic
                        && x.IsObsolete))
                {
                    sb.AppendLine();
                    sb.AppendLine("### 弃用 Obsolete");
                    sb.AppendLine("| 静态方法成员 | 完整签名 | 注释 Comment |");
                    sb.AppendLine("| :--- | :--- | :--- |");
                    foreach (var item in finalAllApiDisplayItems
                                 .Where(x => x.rawData.memberType == MemberTypes.Method && x.rawData.isStatic &&
                                             x.IsObsolete))
                    {
                        sb.AppendLine("| " + item.MemberName + " | " + item.FullMemberSignature + " | " +
                                      item.ChineseComment + " |");
                    }
                }

                sb.AppendLine();
            }

            // --- 静态事件
            if (finalAllApiDisplayItems.Any(x => x.rawData.memberType == MemberTypes.Event))
            {
                sb.AppendLine("## 静态事件 Static Events");
                sb.AppendLine("| 静态事件成员 | 完整签名 | 注释 Comment |");
                sb.AppendLine("| :--- | :--- | :--- |");
                foreach (var item in finalAllApiDisplayItems.Where(x =>
                             x.rawData.memberType == MemberTypes.Event && x.rawData.isStatic &&
                             !x.IsObsolete))
                {
                    sb.AppendLine("| " + item.MemberName + " | " + item.FullMemberSignature + " | " +
                                  item.ChineseComment + " |");
                }

                if (finalAllApiDisplayItems.Any(x =>
                        x.rawData.memberType == MemberTypes.Event && x.rawData.isStatic && x.IsObsolete))
                {
                    sb.AppendLine();
                    sb.AppendLine("### 弃用 Obsolete");
                    sb.AppendLine("| 静态事件成员 | 完整签名 | 注释 Comment |");
                    sb.AppendLine("| :--- | :--- | :--- |");
                    foreach (var item in finalAllApiDisplayItems
                                 .Where(x => x.rawData.memberType == MemberTypes.Event && x.rawData.isStatic &&
                                             x.IsObsolete))
                    {
                        sb.AppendLine("| " + item.MemberName + " | " + item.FullMemberSignature + " | " +
                                      item.ChineseComment + " |");
                    }
                }

                sb.AppendLine();
            }

            // --- 实例字段
            if (finalAllApiDisplayItems.Any(x => x.rawData.memberType == MemberTypes.Field && !x.rawData.isStatic))
            {
                sb.AppendLine("## 实例字段 Fields");
                sb.AppendLine("| 实例字段成员 | 完整签名 | 注释 Comment |");
                sb.AppendLine("| :--- | :--- | :--- |");
                foreach (var item in finalAllApiDisplayItems.Where(x =>
                             x.rawData.memberType == MemberTypes.Field && !x.rawData.isStatic &&
                             !x.IsObsolete))
                {
                    sb.AppendLine("| " + item.MemberName + " | " + item.FullMemberSignature + " | " +
                                  item.ChineseComment + " |");
                }

                if (finalAllApiDisplayItems.Any(x => x.rawData.memberType == MemberTypes.Field && !x.rawData.isStatic
                        && x.IsObsolete))
                {
                    sb.AppendLine();
                    sb.AppendLine("### 弃用 Obsolete");
                    sb.AppendLine("| 静态字段成员 | 完整签名 | 注释 Comment |");
                    sb.AppendLine("| :--- | :--- | :--- |");
                    foreach (var item in finalAllApiDisplayItems
                                 .Where(x => x.rawData.memberType == MemberTypes.Field && !x.rawData.isStatic &&
                                             x.IsObsolete))
                    {
                        sb.AppendLine("| " + item.MemberName + " | " + item.FullMemberSignature + " | " +
                                      item.ChineseComment + " |");
                    }
                }

                sb.AppendLine();
            }

            // --- 实例属性
            if (finalAllApiDisplayItems.Any(x => x.rawData.memberType == MemberTypes.Property && !x.rawData.isStatic))
            {
                sb.AppendLine("## 实例属性 Properties");
                sb.AppendLine("| 实例属性成员 | 完整签名 | 注释 Comment |");
                sb.AppendLine("| :--- | :--- | :--- |");
                foreach (var item in finalAllApiDisplayItems.Where(x =>
                             x.rawData.memberType == MemberTypes.Property && !x.rawData.isStatic &&
                             !x.IsObsolete))
                {
                    sb.AppendLine("| " + item.MemberName + " | " + item.FullMemberSignature + " | " +
                                  item.ChineseComment + " |");
                }

                if (finalAllApiDisplayItems.Any(x => x.rawData.memberType == MemberTypes.Property && !x.rawData.isStatic
                        && x.IsObsolete))
                {
                    sb.AppendLine();
                    sb.AppendLine("### 弃用 Obsolete");
                    sb.AppendLine("| 实例属性成员 | 完整签名 | 注释 Comment |");
                    sb.AppendLine("| :--- | :--- | :--- |");
                    foreach (var item in finalAllApiDisplayItems
                                 .Where(x => x.rawData.memberType == MemberTypes.Property && !x.rawData.isStatic &&
                                             x.IsObsolete))
                    {
                        sb.AppendLine("| " + item.MemberName + " | " + item.FullMemberSignature + " | " +
                                      item.ChineseComment + " |");
                    }
                }

                sb.AppendLine();
            }

            // --- 实例方法
            if (finalAllApiDisplayItems.Any(x => x.rawData.memberType == MemberTypes.Method && !x.rawData.isStatic))
            {
                sb.AppendLine("## 实例方法 Methods");
                sb.AppendLine("| 实例方法成员 | 完整签名 | 注释 Comment |");
                sb.AppendLine("| :--- | :--- | :--- |");
                foreach (var item in finalAllApiDisplayItems.Where(x =>
                             x.rawData.memberType == MemberTypes.Method && !x.rawData.isStatic &&
                             !x.IsObsolete))
                {
                    sb.AppendLine("| " + item.MemberName + " | " + item.FullMemberSignature + " | " +
                                  item.ChineseComment + " |");
                }

                if (finalAllApiDisplayItems.Any(x => x.rawData.memberType == MemberTypes.Method && !x.rawData.isStatic
                        && x.IsObsolete))
                {
                    sb.AppendLine();
                    sb.AppendLine("### 弃用 Obsolete");
                    sb.AppendLine("| 静态方法成员 | 完整签名 | 注释 Comment |");
                    sb.AppendLine("| :--- | :--- | :--- |");
                    foreach (var item in finalAllApiDisplayItems
                                 .Where(x => x.rawData.memberType == MemberTypes.Method && !x.rawData.isStatic &&
                                             x.IsObsolete))
                    {
                        sb.AppendLine("| " + item.MemberName + " | " + item.FullMemberSignature + " | " +
                                      item.ChineseComment + " |");
                    }
                }

                sb.AppendLine();
            }

            // --- 实例事件
            if (finalAllApiDisplayItems.Any(x => x.rawData.memberType == MemberTypes.Event && !x.rawData.isStatic))
            {
                sb.AppendLine("## 实例事件 Events");
                sb.AppendLine("| 实例事件成员 | 完整签名 | 注释 Comment |");
                sb.AppendLine("| :--- | :--- | :--- |");
                foreach (var item in finalAllApiDisplayItems.Where(x =>
                             x.rawData.memberType == MemberTypes.Event && !x.rawData.isStatic &&
                             !x.IsObsolete))
                {
                    sb.AppendLine("| " + item.MemberName + " | " + item.FullMemberSignature + " | " +
                                  item.ChineseComment + " |");
                }

                if (finalAllApiDisplayItems.Any(x =>
                        x.rawData.memberType == MemberTypes.Event && !x.rawData.isStatic && x.IsObsolete))
                {
                    sb.AppendLine();
                    sb.AppendLine("### 弃用 Obsolete");
                    sb.AppendLine("| 实例事件成员 | 完整签名 | 注释 Comment |");
                    sb.AppendLine("| :--- | :--- | :--- |");
                    foreach (var item in finalAllApiDisplayItems
                                 .Where(x => x.rawData.memberType == MemberTypes.Event && !x.rawData.isStatic &&
                                             x.IsObsolete))
                    {
                        sb.AppendLine("| " + item.MemberName + " | " + item.FullMemberSignature + " | " +
                                      item.ChineseComment + " |");
                    }
                }

                sb.AppendLine();
            }

            sb.AppendLine();
            // --- 写入文档
            File.WriteAllText(targetPath + "/" + TargetType.Name + ".md", sb.ToString(), Encoding.UTF8);
            Debug.Log("文档生成完毕");
            AssetDatabase.Refresh();
            EditorUtility.OpenWithDefaultApp(targetPath + "/" + TargetType.Name + ".md");
        }

        #endregion
    }
}
