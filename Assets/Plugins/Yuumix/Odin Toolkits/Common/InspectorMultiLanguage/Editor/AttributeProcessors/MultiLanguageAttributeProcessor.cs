using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Yuumix.OdinToolkits.Common.InspectorMultiLanguage.Editor
{
    public class MultiLanguageAttributeProcessor<T> : OdinAttributeProcessor<T> where T : class
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            if (member.MemberType == MemberTypes.Method &&
                member.GetCustomAttribute<MultiLanguageButtonAttribute>() != null)
            {
                var button = member.GetCustomAttribute<MultiLanguageButtonAttribute>();
                if (InspectorMultiLanguageManagerSO.IsChinese)
                {
                    attributes.Add(button.CreateChineseButton());
                }
                else if (InspectorMultiLanguageManagerSO.IsEnglish)
                {
                    attributes.Add(button.CreateEnglishButton());
                }
            }
        }
    }
}
