using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorLocalization;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

namespace Yuumix.OdinToolkits.Common.InspectorLocalization
{
    /// <summary>
    /// 多语言字符串显示部件，以字段的形式支持多语言
    /// 支持兼容 Odin Inspector 绘制系统
    /// 支持实时语言切换
    /// </summary>
    /// <remarks>
    /// <c>2025/06/15 Yuumix Zeus 确认注释</c>
    /// </remarks>
    [Serializable]
    [HideLabel]
    [InlineProperty]
    public class LocalizedDisplayAsStringWidget
    {
        public LocalizedDisplayAsStringWidget(string chinese, string english = null)
        {
            ChineseDisplay = chinese;
            EnglishDisplay = english ?? chinese;
        }

        bool IsChinese => InspectorLocalizationManagerSO.IsChinese;
        bool IsEnglish => InspectorLocalizationManagerSO.IsEnglish;

        [ShowIf(nameof(IsChinese), false)]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string ChineseDisplay { get; }

        [ShowIf(nameof(IsEnglish), false)]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string EnglishDisplay { get; }
    }

#if UNITY_EDITOR
    internal class LocalizedDisplayAsStringProcessor : OdinAttributeProcessor<LocalizedDisplayAsStringWidget>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            switch (member.Name)
            {
                case nameof(LocalizedDisplayAsStringWidget.ChineseDisplay):
                {
                    var configAttribute = parentProperty.GetAttribute<LocalizedDisplayWidgetConfigAttribute>();
                    if (configAttribute == null)
                    {
                        Debug.LogError("LocalizedDisplayWidget 类型字段必须添加 LocalizedDisplayWidgetConfigAttribute 特性");
                    }
                    else
                    {
                        attributes.Add(configAttribute.CreateDisplayAsStringAttribute());
                    }

                    break;
                }
                case nameof(LocalizedDisplayAsStringWidget.EnglishDisplay):
                {
                    var configAttribute = parentProperty.GetAttribute<LocalizedDisplayWidgetConfigAttribute>();
                    if (configAttribute == null)
                    {
                        Debug.LogError("LocalizedDisplayWidget 类型字段必须添加 LocalizedDisplayWidgetConfigAttribute 特性");
                    }
                    else
                    {
                        attributes.Add(configAttribute.CreateDisplayAsStringAttribute());
                    }

                    break;
                }
            }
        }
    }

#endif
}
