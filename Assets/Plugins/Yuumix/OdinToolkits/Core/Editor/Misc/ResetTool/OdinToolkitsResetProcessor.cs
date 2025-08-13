using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace Yuumix.OdinToolkits.Core.Editor
{
    public class OdinToolkitsResetProcessor : OdinAttributeProcessor<IOdinToolkitsReset>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            attributes.Add(new CustomContextMenuAttribute("Odin Toolkits Reset", "OdinToolkitsReset"));
        }
    }
}
