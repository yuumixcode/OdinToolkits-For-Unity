#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Editor
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
#endif
