using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class TypeRegistryItemExample : ExampleScriptableObject
    {
        const string CATEGORY_PATH = "Sirenix.TypeSelector.Demo";
        const string BASE_ITEM_NAME = "Painting Tools";
        const string PATH = CATEGORY_PATH + "/" + BASE_ITEM_NAME;

        [FoldoutGroup("默认样式")]
        [ShowInInspector]
        [InfoBox("此时并没有序列化")]
        [PolymorphicDrawerSettings(ShowBaseType = true)]
        public BasicClass BasicItem;

        [FoldoutGroup("使用 TypeRegistryItem 特性自定义样式")]
        [InfoBox("此时并没有序列化")]
        [ShowInInspector]
        [PolymorphicDrawerSettings(ShowBaseType = true)]
        public Base PaintingItem;

        #region 声明定义

        public abstract class BasicClass { }

        public class MyClassA : BasicClass
        {
            public string Name;
        }

        public class MyClassB : BasicClass
        {
            public int Number;
        }

        public class MyClassC : BasicClass
        {
            public float Number;
        }

        [TypeRegistryItem(Name = BASE_ITEM_NAME, Icon = SdfIconType.Tools, CategoryPath = CATEGORY_PATH,
            Priority = int.MinValue)]
        public abstract class Base { }

        [TypeRegistryItem(darkIconColorR: 0.8f, darkIconColorG: 0.3f,
            lightIconColorR: 0.3f, lightIconColorG: 0.1f,
            Name = "Brush", CategoryPath = PATH, Icon = SdfIconType.BrushFill, Priority = int.MinValue)]
        public class InheritorA : Base
        {
            public Color Color = Color.red;
            public float PaintRemaining = 0.4f;
        }

        [TypeRegistryItem(darkIconColorG: 0.8f, darkIconColorB: 0.3f,
            lightIconColorG: 0.3f, lightIconColorB: 0.1f,
            Name = "Paint Bucket", CategoryPath = PATH, Icon = SdfIconType.PaintBucket, Priority = int.MinValue)]
        public class InheritorB : Base
        {
            public Color Color = Color.green;
            public float PaintRemaining = 0.8f;
        }

        [TypeRegistryItem(darkIconColorB: 0.8f, darkIconColorG: 0.3f,
            lightIconColorB: 0.3f, lightIconColorG: 0.1f,
            Name = "Palette", CategoryPath = PATH, Icon = SdfIconType.PaletteFill, Priority = int.MinValue)]
        public class InheritorC : Base
        {
            public ColorPaletteItem[] Colors =
            {
                new ColorPaletteItem(Color.blue, 0.8f), new ColorPaletteItem(Color.red, 0.5f),
                new ColorPaletteItem(Color.green, 1.0f), new ColorPaletteItem(Color.white, 0.6f)
            };
        }

        public struct ColorPaletteItem
        {
            public Color Color;
            public float Remaining;

            public ColorPaletteItem(Color color, float remaining)
            {
                Color = color;
                Remaining = remaining;
            }
        }

        #endregion
    }
}
