using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.Reflection;
using Yuumix.OdinToolkits.Core.SafeEditor;
using YuumixEditor;

namespace Yuumix.OdinToolkits.Core.Editor
{
    public class BilingualDisplayAsStringProcessor : OdinAttributeProcessor<BilingualDisplayAsStringWidget>
    {
        public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes)
        {
            var config = property.GetAttribute<BilingualDisplayAsStringWidgetConfigAttribute>();
            if (config == null)
            {
                attributes.Add(new BilingualInfoBoxAttribute(
                    "BilingualDisplayAsStringWidget 字段必须添加 BilingualDisplayAsStringWidgetConfigAttribute 才能生效",
                    "BilingualDisplayAsStringWidget field must add BilingualDisplayAsStringWidgetConfigAttribute to take effect"));
            }
        }

        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            switch (member.Name)
            {
                case nameof(BilingualDisplayAsStringWidget.ChineseDisplay)
                    or nameof(BilingualDisplayAsStringWidget.EnglishDisplay):
                    var config = parentProperty.GetAttribute<BilingualDisplayAsStringWidgetConfigAttribute>();
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
