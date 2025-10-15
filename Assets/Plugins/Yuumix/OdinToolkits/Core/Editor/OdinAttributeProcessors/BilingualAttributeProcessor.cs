using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Yuumix.OdinToolkits.Core.Editor
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
                var chineseButton = button.CreateButton();
                attributes.Add(chineseButton);
            }
        }
    }
}
