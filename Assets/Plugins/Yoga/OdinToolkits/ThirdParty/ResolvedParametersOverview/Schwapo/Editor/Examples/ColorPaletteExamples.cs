using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.Modules.OdinToolkits.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class ColorPaletteExamples_PaletteName
    {
        [FoldoutGroup("Attribute Expression Example")]
        [ColorPalette("@UseTropical ? TropicalPaletteName : SepiaPaletteName")]
        public Color AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [ColorPalette("$SepiaPaletteName")]
        public Color FieldNameExample;

        [FoldoutGroup("Method Name Example")]
        [ColorPalette("$GetPaletteName")]
        public Color MethodNameExample;

        [FoldoutGroup("Property Name Example")]
        [ColorPalette("$PaletteNameProperty")]
        public Color PropertyNameExample;

        public string SepiaPaletteName = "Sepia";
        public string TropicalPaletteName = "Tropical";
        public bool UseTropical;
        public string PaletteNameProperty => UseTropical ? TropicalPaletteName : SepiaPaletteName;

        string GetPaletteName() => UseTropical ? TropicalPaletteName : SepiaPaletteName;
    }
    // End
}