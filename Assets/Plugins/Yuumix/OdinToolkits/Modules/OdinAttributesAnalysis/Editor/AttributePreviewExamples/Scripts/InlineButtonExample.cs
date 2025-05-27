using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class InlineButtonExample : ExampleScriptableObject
    {
        #region Example

        [PropertyOrder(1)]
        [FoldoutGroup("InlineButton 基础使用")]
        [InfoBox("第一个参数是方法名，会自动生成按钮")]
        [InlineButton("A")]
        public int inlineButton;

        [PropertyOrder(10)]
        [FoldoutGroup("InlineButton 基础使用")]
        [InfoBox("可以同时有多个 InlineButton")]
        [InlineButton("A")]
        [InlineButton("B", "Custom Button Name")]
        public int chainedButtons;

        [PropertyOrder(20)]
        [FoldoutGroup("InlineButton 基础使用")]
        [InfoBox("还可以增加图标样式")]
        [InlineButton("C", SdfIconType.Dice6Fill, "Random")]
        public int iconButton;

        [PropertyOrder(30)]
        [FoldoutGroup("InlineButton 进阶使用")]
        public bool showIf;

        [PropertyOrder(40)]
        [FoldoutGroup("InlineButton 进阶使用")]
        [InfoBox("可以控制是否显示")]
        [InlineButton("C", SdfIconType.Apple, "Show", ShowIf = "ShowIf")]
        public int showIfButton;

        public bool ShowIf() => showIf;

        [PropertyOrder(50)]
        [FoldoutGroup("InlineButton 进阶使用")]
        [InfoBox("自定义颜色")]
        [InlineButton("@Debug.Log(\"C\")", SdfIconType.MenuApp, "颜色", ButtonColor = "lightgreen",
            TextColor = "darkcyan")]
        public int colorButton;

        void A()
        {
            Debug.Log("A");
        }

        void B()
        {
            Debug.Log("B");
        }

        void C()
        {
            Debug.Log("C");
        }

        #endregion
    }
}
