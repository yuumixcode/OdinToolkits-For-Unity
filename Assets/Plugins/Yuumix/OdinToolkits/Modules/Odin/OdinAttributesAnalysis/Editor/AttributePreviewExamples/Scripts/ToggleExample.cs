using Sirenix.OdinInspector;
using System;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class ToggleExample : ExampleScriptableObject
    {
        // 对单一字段进行修改
        [Toggle(nameof(MyToggleable.enabled))]
        public MyToggleable toggler = new MyToggleable();

        public ToggleableClass toggleable = new ToggleableClass();

        [Serializable]
        public class MyToggleable
        {
            public bool enabled;
            public int myValue;
        }

        // 可以直接把特性赋值给类，不需要对单一字段进行修改
        [Serializable]
        [Toggle(nameof(enabled))]
        public class ToggleableClass
        {
            public bool enabled;
            public string text;
        }
    }
}
