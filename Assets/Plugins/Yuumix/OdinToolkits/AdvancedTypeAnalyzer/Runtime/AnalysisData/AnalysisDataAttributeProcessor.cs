using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Yuumix.OdinToolkits.AdvancedTypeAnalyzer
{
    public class AnalysisDataAttributeProcessor : OdinAttributeProcessor<IDerivedMemberData>
    {
        public override bool CanProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member) =>
            member.MemberType == MemberTypes.Property;

        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes) { }
    }
}
