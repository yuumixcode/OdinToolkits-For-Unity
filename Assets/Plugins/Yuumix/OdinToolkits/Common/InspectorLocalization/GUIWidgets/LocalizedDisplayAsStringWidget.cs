using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Reflection;
using Yuumix.OdinToolkits.Common.InspectorLocalization.Attributes.WidgetConfigs;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

namespace Yuumix.OdinToolkits.Common.InspectorLocalization.GUIWidgets
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

        InspectorLocalizationManagerSO LocalizationManager => InspectorLocalizationManagerSO.Instance;
        bool IsChinese => LocalizationManager.IsChinese;
        bool IsEnglish => LocalizationManager.IsEnglish;

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
                    attributes.Add(configAttribute.CreateDisplayAsStringAttribute());
                    break;
                }
                case nameof(LocalizedDisplayAsStringWidget.EnglishDisplay):
                {
                    var configAttribute = parentProperty.GetAttribute<LocalizedDisplayWidgetConfigAttribute>();
                    attributes.Add(configAttribute.CreateDisplayAsStringAttribute());
                    break;
                }
            }
        }
    }

#endif
}
