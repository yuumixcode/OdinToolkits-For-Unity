using System;
using System.Collections.Generic;
using System.Reflection;
using Yuumix.OdinToolkits.Core;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Yuumix.OdinToolkits.Shared;

namespace Yuumix.OdinToolkits.Editor.Shared
{
    public sealed class MultiLanguageAttributeProcessor<T> : OdinAttributeProcessor<T> where T : class
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            if (member.MemberType == MemberTypes.Method &&
                member.GetCustomAttribute<MultiLanguageButtonAttribute>() != null)
            {
                var button = member.GetCustomAttribute<MultiLanguageButtonAttribute>();
                ButtonAttribute chineseButton = button.CreateButton();
                attributes.Add(chineseButton);
            }
        }
    }
}
