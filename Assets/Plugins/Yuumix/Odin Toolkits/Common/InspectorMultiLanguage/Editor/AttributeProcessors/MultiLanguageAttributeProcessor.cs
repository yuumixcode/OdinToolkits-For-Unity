using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

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
                var chineseButton = button.CreateChineseButton();
                attributes.Add(chineseButton);
            }
        }
    }
}
