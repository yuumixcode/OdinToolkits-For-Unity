using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.Reflection;
using Yuumix.OdinToolkits.Common.InspectorLocalization.Attributes.WidgetConfigs;

namespace Yuumix.OdinToolkits.Common.InspectorLocalization.GUIWidgets
{
    [Serializable]
    [HideLabel]
    [InlineProperty]
    public class LocalizedDisplayAsStringWidget
    {
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

        public LocalizedDisplayAsStringWidget(string chinese, string english = null)
        {
            ChineseDisplay = chinese;
            EnglishDisplay = english ?? chinese;
        }
    }

#if UNITY_EDITOR
    internal class LocalizedDisplayAsStringProcessor : OdinAttributeProcessor<LocalizedDisplayAsStringWidget>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            if (member.Name == nameof(LocalizedDisplayAsStringWidget.ChineseDisplay))
            {
                var configAttribute = parentProperty.GetAttribute<LocalizedDisplayWidgetConfigAttribute>();
                attributes.Add(configAttribute.CreateDisplayAsStringAttribute());
            }
            else if (member.Name == nameof(LocalizedDisplayAsStringWidget.EnglishDisplay))
            {
                var configAttribute = parentProperty.GetAttribute<LocalizedDisplayWidgetConfigAttribute>();
                attributes.Add(configAttribute.CreateDisplayAsStringAttribute());
            }
        }
    }

#endif
}
