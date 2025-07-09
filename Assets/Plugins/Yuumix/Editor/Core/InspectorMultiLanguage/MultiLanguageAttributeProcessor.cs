using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Editor.Core
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
                ButtonAttribute chineseButton = button.CreateChineseButton();
                attributes.Add(chineseButton);
            }
        }
    }
}
