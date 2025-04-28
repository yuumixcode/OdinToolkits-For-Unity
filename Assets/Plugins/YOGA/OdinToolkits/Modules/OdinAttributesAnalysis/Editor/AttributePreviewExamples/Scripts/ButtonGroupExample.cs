using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class ButtonGroupExample : ExampleScriptableObject
    {
        [PropertyOrder(20)] [FoldoutGroup("ButtonGroup 基础使用")] [InfoBox("直接使用 ButtonGroup，内置一个默认分组名 \"_DefaultGroup\"")]
        public BasicExample basicExample;

        [PropertyOrder(40)]
        [FoldoutGroup("ButtonGroup 基础使用")]
        [InfoBox("一个包含了多个方法的结构体示例，ButtonGroup 和 Button 结合使用，可以控制高度")]
        public IconButtonGroupExamples iconButtonGroupExamples;

        [PropertyOrder(50)]
        [FoldoutGroup("ButtonGroup 进阶使用")]
        [InfoBox("Order 参数可以控制 ButtonGroup 组内的顺序，越小越在左侧，" +
                 "H 的 Order = 20，I 的 Order = 10，尽管代码中先编写 H，但是此时 H 后绘制")]
        public OrderParameterExample orderParameterExample;

        [PropertyOrder(100)]
        [FoldoutGroup("特殊情况")]
        [InfoBox("官方表示作用于任何实例方法（非静态），" +
                 "但是这个静态方法依旧有效，可能在某些情况下静态方法会失效")]
        public StaticFunctionExample staticExample;

        [Serializable]
        [HideLabel]
        public struct BasicExample
        {
            [ButtonGroup]
            private void A()
            {
                Debug.Log("执行 A 方法");
            }

            [ButtonGroup]
            private void B()
            {
                Debug.Log("执行 B 方法");
            }

            [ButtonGroup]
            private void C()
            {
                Debug.Log("执行 C 方法");
            }
        }

        [Serializable]
        [HideLabel]
        public struct OrderParameterExample
        {
            [ButtonGroup("Order", Order = 20)]
            private void H()
            {
            }

            [ButtonGroup("Order", Order = 10)]
            private void I()
            {
            }
        }

        [Serializable]
        [HideLabel]
        public struct StaticFunctionExample
        {
            [ButtonGroup]
            private static void G()
            {
                Debug.Log("这是一个静态方法");
            }
        }

        [Serializable]
        [HideLabel]
        public struct IconButtonGroupExamples
        {
            [ButtonGroup(ButtonHeight = 30)]
            [Button(SdfIconType.ArrowsMove, "")]
            private void ArrowsMove()
            {
                Debug.Log("这是 ArrowsMove 方法");
            }

            [ButtonGroup]
            [Button(SdfIconType.Crop, "")]
            private void Crop()
            {
                Debug.Log("这是 Crop 方法");
            }

            [ButtonGroup]
            [Button(SdfIconType.TextLeft, "")]
            private void TextLeft()
            {
                Debug.Log("这是 TextLeft 方法");
            }

            [ButtonGroup]
            [Button(SdfIconType.TextRight, "")]
            private void TextRight()
            {
                Debug.Log("这是 TextRight 方法");
            }

            [ButtonGroup]
            [Button(SdfIconType.TextParagraph, "")]
            private void TextParagraph()
            {
                Debug.Log("这是 TextParagraph 方法");
            }

            [ButtonGroup]
            [Button(SdfIconType.Textarea, "")]
            private void Textarea()
            {
                Debug.Log("这是 TextArea 方法");
            }
        }
    }
}