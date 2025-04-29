using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using System;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class TypeInfoBoxExample : ExampleScriptableObject
    {
        [PropertyOrder(1)] public MyType myObject = new();

        [Serializable]
        [TypeInfoBox("将会直接绘制一个 InfoBox 在类的顶部，这个特性是放在 class 上")]
        public class MyType
        {
            public int value;
        }
    }
}