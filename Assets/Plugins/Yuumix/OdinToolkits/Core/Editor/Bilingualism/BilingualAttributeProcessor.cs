using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace Yuumix.OdinToolkits.Core.Runtime.Editor
{
    public sealed class BilingualAttributeProcessor<T> : OdinAttributeProcessor<T> where T : class
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            if (member.MemberType == MemberTypes.Method &&
                member.GetCustomAttribute<BilingualButtonAttribute>() != null)
            {
                var button = member.GetCustomAttribute<BilingualButtonAttribute>();
                ButtonAttribute chineseButton = button.CreateButton();
                attributes.Add(chineseButton);
            }
        }
    }
}
