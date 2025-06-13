using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.Reflection;
using Yuumix.OdinToolkits.Common.InspectorLocalization.Attributes;
using Yuumix.OdinToolkits.Common.InspectorLocalization.Structs;

namespace Yuumix.OdinToolkits.Common.InspectorLocalization.Editor.AttributeProcessors
{
    public class LocalizationAttributeProcessor<T> : OdinAttributeProcessor<T> where T : class
    {
        static InspectorLocalizationManagerSO InspectorLocalizationManagerSO => InspectorLocalizationManagerSO.Instance;

        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            if (member.MemberType == MemberTypes.Method &&
                member.GetCustomAttribute<LocalizedButtonAttribute>() != null)
            {
                var localizedButton = member.GetCustomAttribute<LocalizedButtonAttribute>();
                if (InspectorLocalizationManagerSO.IsChinese)
                {
                    attributes.Add(ButtonAttributeConfig.CreateChineseButtonAttribute(localizedButton.Config));
                }
                else if (InspectorLocalizationManagerSO.IsEnglish)
                {
                    attributes.Add(ButtonAttributeConfig.CreateEnglishButtonAttribute(localizedButton.Config));
                }
            }
        }
    }
}
