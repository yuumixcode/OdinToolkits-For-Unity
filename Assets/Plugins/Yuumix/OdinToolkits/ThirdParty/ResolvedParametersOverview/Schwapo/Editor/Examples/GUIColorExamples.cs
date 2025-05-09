using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.Modules.OdinToolkits.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class GUIColorExamples_GetColor
    {
        [FoldoutGroup("Attribute Expression Example")]
        [GUIColor("@UseRed ? UnityEngine.Color.red : UnityEngine.Color.green")]
        public string AttributeExpressionExample;

        public Color Color = Color.green;

        [FoldoutGroup("Field Name Example")] [GUIColor("$Color")]
        public string FieldNameExample;

        [FoldoutGroup("Method Name Example")] [GUIColor("$GetColor")]
        public string MethodNameExample;

        [FoldoutGroup("Property Name Example")] [GUIColor("$ColorProperty")]
        public string PropertyNameExample;

        public bool UseRed;
        public Color ColorProperty => Color;

        private Color GetColor()
        {
            return UseRed ? Color.red : Color.green;
        }
    }
    // End
}