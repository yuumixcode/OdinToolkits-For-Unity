using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.Examples;
using System;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class ResponsiveButtonGroupExample : ExampleScriptableObject
    {
        [PropertyOrder(10)]
        [FoldoutGroup("ResponsiveButtonGroup 基础使用")]
        [InfoBox("内部方法直接使用 [ResponsiveButtonGroup] ")]
        public BasicExample basicExample;

        [PropertyOrder(20)]
        [FoldoutGroup("ResponsiveButtonGroup 基础使用")]
        [InfoBox("内部方法使用 [ResponsiveButtonGroup(\"UniformGroup\", UniformLayout = true)]，" +
                 "UniformLayout 表示控制同一行中所有按钮宽度一样")]
        public BasicExample2 basicExample2;

        [PropertyOrder(30)]
        [FoldoutGroup("ResponsiveButtonGroup 基础使用")]
        [InfoBox("内部方法使用 [ResponsiveButtonGroup(\"DefaultButtonSize\", " +
                 "DefaultButtonSize = ButtonSizes.Small)]，" +
                 "DefaultButtonSize 默认的按钮大小，但是会被 Button 特性覆盖这个 Group 设置的按钮大小")]
        public ButtonSizeExample buttonSizeExample;

        [PropertyOrder(40)]
        [FoldoutGroup("ResponsiveButtonGroup 进阶使用-与其他 Group 结合")]
        [TitleGroup("ResponsiveButtonGroup 进阶使用-与其他 Group 结合/Title1")]
        [ResponsiveButtonGroup("ResponsiveButtonGroup 进阶使用-与其他 Group 结合/Title1/SomeBtnGroup")]
        public void Baz1() { }

        [PropertyOrder(50)]
        [FoldoutGroup("ResponsiveButtonGroup 进阶使用-与其他 Group 结合")]
        [TitleGroup("ResponsiveButtonGroup 进阶使用-与其他 Group 结合/Title1")]
        [ResponsiveButtonGroup("ResponsiveButtonGroup 进阶使用-与其他 Group 结合/Title1/SomeBtnGroup")]
        public void Baz2() { }

        [PropertyOrder(60)]
        [FoldoutGroup("ResponsiveButtonGroup 进阶使用-与其他 Group 结合")]
        [TitleGroup("ResponsiveButtonGroup 进阶使用-与其他 Group 结合/Title2")]
        [ResponsiveButtonGroup("ResponsiveButtonGroup 进阶使用-与其他 Group 结合/Title2/SomeBtnGroup")]
        public void Baz3() { }

        [Serializable]
        [HideLabel]
        public struct BasicExample
        {
            [ResponsiveButtonGroup]
            public void Foo() { }

            [ResponsiveButtonGroup]
            public void Bar() { }

            [ResponsiveButtonGroup]
            public void Baz() { }
        }

        [Serializable]
        [HideLabel]
        public struct BasicExample2
        {
            [ResponsiveButtonGroup("UniformGroup", UniformLayout = true)]
            public void Foo1() { }

            [ResponsiveButtonGroup("UniformGroup")]
            public void Foo2() { }

            [ResponsiveButtonGroup("UniformGroup")]
            public void LongNameWins() { }

            [ResponsiveButtonGroup("UniformGroup")]
            public void Foo4() { }
        }

        [Serializable]
        [HideLabel]
        public struct ButtonSizeExample
        {
            [ResponsiveButtonGroup("DefaultButtonSize", DefaultButtonSize = ButtonSizes.Small)]
            public void Bar1() { }

            [ResponsiveButtonGroup("DefaultButtonSize")]
            public void Bar2() { }

            [ResponsiveButtonGroup("DefaultButtonSize")]
            [Button(ButtonSizes.Large)]
            public void Bar4() { }

            [ResponsiveButtonGroup("DefaultButtonSize")]
            [Button(ButtonSizes.Large)]
            public void Bar5() { }

            [ResponsiveButtonGroup("DefaultButtonSize")]
            public void Bar6() { }
        }
#if UNITY_EDITOR
        [InfoBox("Odin 内置的一个 Example")]
        [OnInspectorGUI]
        void InfoBox1() { }

        [PropertyOrder(1)]
        [Button(ButtonSizes.Large)]
        [GUIColor(0, 1, 0)]
        void OpenDockableWindowExample()
        {
            var window = EditorWindow.GetWindow<MyDockableGameDashboard>();
            window.WindowPadding = new Vector4();
        }
#endif
    }
}
