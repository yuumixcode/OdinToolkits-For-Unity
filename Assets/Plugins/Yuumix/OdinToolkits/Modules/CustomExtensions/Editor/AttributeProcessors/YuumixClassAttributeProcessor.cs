using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.Reflection;
using Yuumix.OdinToolkits.Common.EditorLocalization;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Attributes;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Structs;

namespace Yuumix.OdinToolkits.Modules.CustomExtensions.Editor.AttributeProcessors
{
    public class YuumixClassAttributeProcessor<T> : OdinAttributeProcessor<T> where T : class
    {
        static EditorLocalizationManagerSO EditorLocalizationManagerSO => EditorLocalizationManagerSO.Instance;

        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            if (member.MemberType == MemberTypes.Method &&
                member.GetCustomAttribute<LocalizedButtonAttribute>() != null)
            {
                var localizedButton = member.GetCustomAttribute<LocalizedButtonAttribute>();
                if (EditorLocalizationManagerSO.IsSimplifiedChinese)
                {
                    attributes.Add(ButtonAttributeConfig.CreateChineseButtonAttribute(localizedButton.Config));
                }
                else if (EditorLocalizationManagerSO.IsEnglish)
                {
                    attributes.Add(ButtonAttributeConfig.CreateEnglishButtonAttribute(localizedButton.Config));
                }
            }
        }
    }
}
