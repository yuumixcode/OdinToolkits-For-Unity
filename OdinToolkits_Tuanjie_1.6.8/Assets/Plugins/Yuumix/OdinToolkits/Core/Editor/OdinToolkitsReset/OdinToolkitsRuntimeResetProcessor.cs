using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace Yuumix.OdinToolkits.Core.Editor
{
    /// <summary>
    /// OdinToolkits 的 RuntimeReset 的 AttributeProcessor，给实现了这个接口的所有子成员添加 Reset 右键菜单项
    /// </summary>
    public class OdinToolkitsRuntimeResetProcessor : OdinAttributeProcessor<IOdinToolkitsRuntimeReset>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            attributes.Add(new CustomContextMenuAttribute("Runtime Reset",
                nameof(IOdinToolkitsRuntimeReset.RuntimeReset)));
        }
    }
}
