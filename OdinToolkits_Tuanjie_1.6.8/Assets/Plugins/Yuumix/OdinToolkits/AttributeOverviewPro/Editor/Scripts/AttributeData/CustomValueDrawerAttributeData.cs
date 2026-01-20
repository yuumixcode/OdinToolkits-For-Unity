using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.SafeEditor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    public class CustomValueDrawerAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderWidget HeaderWidget { get; set; } = new BilingualHeaderWidget(
            "Custom Value Drawer", "Custom Value Drawer",
            "使用 CustomValueDrawer 特性，代替声明一个 Attribute，同时声明一个对应 Drawer 类的流程。CustomValueDrawer 支持撤销，重做，多选。",
            "Instead of making a new attribute, and a new drawer, for a one-time thing, you can with this attribute, make a method that acts as a custom property drawer. These drawers will out of the box have support for undo/redo and multi-selection.",
            OdinInspectorDocumentationLinks.CUSTOM_VALUE_DRAWER_URL);

        public override BilingualData[] UsageTips { get; set; } = null;
        public override ParameterValue[] AttributeParameters { get; set; } = null;
        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = null;
        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } = null;
    }
}
