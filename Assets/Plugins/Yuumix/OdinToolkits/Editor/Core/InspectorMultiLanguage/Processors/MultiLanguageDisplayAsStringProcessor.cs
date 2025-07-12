#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using Yuumix.OdinToolkits.Core;
using Sirenix.OdinInspector.Editor;
using Yuumix.OdinToolkits.Shared;

namespace Yuumix.OdinToolkits.Editor.Shared
{
    public class MultiLanguageDisplayAsStringProcessor : OdinAttributeProcessor<MultiLanguageDisplayAsStringWidget>
    {
        public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes)
        {
            var config = property.GetAttribute<MultiLanguageDisplayAsStringWidgetConfigAttribute>();
            if (config == null)
            {
                attributes.Add(new MultiLanguageInfoBoxAttribute(
                    "MultiLanguageDisplayAsStringWidget 字段必须添加 MultiLanguageDisplayAsStringWidgetConfigAttribute 才能生效",
                    "MultiLanguageDisplayAsStringWidget field must add MultiLanguageDisplayAsStringWidgetConfigAttribute to take effect"));
            }
        }

        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            switch (member.Name)
            {
                case nameof(MultiLanguageDisplayAsStringWidget.ChineseDisplay)
                    or nameof(MultiLanguageDisplayAsStringWidget.EnglishDisplay):
                    var config = parentProperty.GetAttribute<MultiLanguageDisplayAsStringWidgetConfigAttribute>();
                    if (config == null)
                    {
                        break;
                    }

                    attributes.Add(config.CreateDisplayAsStringAttribute());
                    break;
            }
        }
    }
}

#endif