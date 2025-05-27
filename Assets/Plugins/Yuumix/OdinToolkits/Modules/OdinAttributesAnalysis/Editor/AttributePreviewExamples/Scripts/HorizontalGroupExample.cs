using Sirenix.OdinInspector;
using System;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class HorizontalGroupExample : ExampleScriptableObject
    {
        [TitleGroup("基础使用", "无参数，自动控制宽度")]
        [HorizontalGroup("基础使用/row0")]
        public SomeFieldType left1;

        [TitleGroup("基础使用", "无参数，自动控制宽度")]
        [HorizontalGroup("基础使用/row0")]
        public SomeFieldType right1;

        [TitleGroup("PaddingRight", "PaddingRight = 0.4f")]
        [HorizontalGroup("PaddingRight/row1", PaddingRight = 0.4f)]
        public SomeFieldType leftCustom1;

        [TitleGroup("PaddingRight", "PaddingRight = 0.4f")]
        [HorizontalGroup("PaddingRight/row1")]
        public SomeFieldType rightCustom1;

        [TitleGroup("MarginRight", "MarginRight == 0.4f")]
        [HorizontalGroup("MarginRight/row2", MarginRight = 0.4f)]
        public SomeFieldType left2;

        [TitleGroup("MarginRight", "MarginRight == 0.4f")]
        [HorizontalGroup("MarginRight/row2")]
        public SomeFieldType right2;

        [TitleGroup("order")]
        [HorizontalGroup("order/row1", order: 20)]
        public SomeFieldType leftCustom2;

        [TitleGroup("order")]
        [HorizontalGroup("order/row2", order: 10)]
        public SomeFieldType rightCustom2;

        [TitleGroup("Width", "自定义宽度，0 ~ 1 表示百分比，> 1 表示具体像素")]
        [HorizontalGroup("Width/row1", 0.25f)]
        public SomeFieldType left3;

        [TitleGroup("Width", "自定义宽度，0 ~ 1 表示百分比，> 1 表示具体像素")]
        [HorizontalGroup("Width/row1", 150)]
        public SomeFieldType center3;

        [TitleGroup("Width", "自定义宽度，0 ~ 1 表示百分比，> 1 表示具体像素")]
        [HorizontalGroup("Width/row1")]
        public SomeFieldType right3;

        [TitleGroup("Gap", "Gap = 40")]
        [HorizontalGroup("Gap/row3", Gap = 40)]
        public SomeFieldType left4;

        [TitleGroup("Gap", "Gap = 40")]
        [HorizontalGroup("Gap/row3")]
        public SomeFieldType center4;

        [TitleGroup("Gap", "Gap = 40")]
        [HorizontalGroup("Gap/row3")]
        public SomeFieldType right4;

        [TitleGroup("Title 参数", "设置 HorizontalGroup 自带的标题")]
        [HorizontalGroup("Title 参数/row4", Title = "Horizontal Group Title")]
        public SomeFieldType left5;

        [TitleGroup("Title 参数")]
        [HorizontalGroup("Title 参数/row4")]
        public SomeFieldType center5;

        [TitleGroup("Title 参数")]
        [HorizontalGroup("Title 参数/row4")]
        public SomeFieldType right5;

        [PropertyOrder(10)]
        [HorizontalGroup("row6", DisableAutomaticLabelWidth = true, LabelWidth = 30f)]
        public SomeFieldType left6;

        [PropertyOrder(10)]
        [HorizontalGroup("row6")]
        public SomeFieldType center6;

        [PropertyOrder(10)]
        [HorizontalGroup("row6")]
        public SomeFieldType right6;

        [Title("LabelWidth", "没有发现实际生效的方法")]
        [OnInspectorGUI]
        [PropertyOrder(5)]
        void OnGUI1() { }

        [HideLabel]
        [Serializable]
        public struct SomeFieldType
        {
            [LabelText("@$property.Parent.NiceName")]
            [ListDrawerSettings(ShowIndexLabels = true)]
            public float[] x;
        }
    }
}
