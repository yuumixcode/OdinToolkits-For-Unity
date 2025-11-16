using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class ColorPaletteExample : ExampleSO
    {
        [Title("Unity 提供的 Color 相关的特性")]
        [ColorUsage(true)]
        public Color color1;

        [Title("Odin 的 ColorPalette")]
        [ColorPalette]
        public Color color2;

        [InfoBox("PaletteName 是用于自定义 Palette 的")]
        [ColorPalette(PaletteName = "Color3")]
        public Color color3;

        [ColorPalette(ShowAlpha = true)]
        public Color color4;

        // ------------------------------------
        // Color palettes can be accessed and modified from code.
        // Note that the color palettes will NOT automatically be included in your builds.
        // But you can easily fetch all color palettes via the ColorPaletteManager 
        // and include them in your game like so:
        // ------------------------------------
        // ColorPalette 这个类不会自动添加到构建中，也就是说不能在业务逻辑中获取对应的颜色，因为它属于 Editor，
        // 但是可以自己创建一个同名的类 ColorPalette，通过 ColorPaletteManager 获取到所有的颜色，存入一个字段中，
        // 然后在运行时可以通过代码获取对应的的颜色
        // ------------------------------------
        [FoldoutGroup("Color Palettes", false)]
        [ListDrawerSettings(IsReadOnly = true)]
        [PropertyOrder(9)]
        public List<ColorPalette> colorPalettes;

        [FoldoutGroup("Color Palettes")]
        [Button(ButtonSizes.Large)]
        [GUIColor(0, 1, 0)]
        [PropertyOrder(8)]
        void FetchColorPalettes()
        {
            // Sirenix.OdinInspector.Editor.ColorPaletteManager.Instance.ColorPalettes 这个字段默认是编辑器状态才可以使用
            // 运行时获取不到，不能在业务逻辑中使用，但是可以创造一个同样的类（但是属于 Assembly - CSharp 程序集），
            // 然后在编辑器状态拉取存入一个字段中
            colorPalettes = ColorPaletteManager.Instance.ColorPalettes
                .Select(x => new ColorPalette
                {
                    paletteName = x.Name,
                    colors = x.Colors.ToArray()
                })
                .ToList();
        }

        [Serializable]
        public class ColorPalette
        {
            [HideInInspector]
            public string paletteName;

            [LabelText("$paletteName")]
            [ListDrawerSettings(ShowFoldout = false)]
            public Color[] colors;
        }
    }
}
