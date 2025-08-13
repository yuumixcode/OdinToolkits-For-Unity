using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Community.Schwapo.Editor
{
    [ResolvedParameterExample]
    public class TableMatrixExamples_DrawElementMethod
    {
        public Color FalseColor = new Color(1f, 0.4f, 0.14f, 1f);

        [FoldoutGroup("Method Name Example")]
        [InfoBox("Paste this code into a Odin serialized object.")]
        [TableMatrix(DrawElementMethod = "DrawAsColoredRect")]
        public bool[,] MethodNameExample = new bool[5, 5];
        /*
            Note that this example requires Odin's serialization to be enabled to work,
            since it uses types that Unity will not serialize.
            Copy this code into a SerializedMonoBehaviour to preview it.
        */

        public Color TrueColor = new Color(0.11f, 0.77f, 0.5f, 1f);

        bool DrawAsColoredRect(Rect rect, bool[,] table, int x,
            int y)
        {
            bool value = table[x, y];

            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
            {
                table[x, y] = !value;
            }

            EditorGUI.DrawRect(rect, value ? TrueColor : FalseColor);

            return value;
        }
    }
    // End

    [ResolvedParameterExample]
    public class TableMatrixExamples_HorizontalTitle
    {
        public string AlternativeTitle = "Peace, Love & Fenrir";

        [FoldoutGroup("Attribute Expression Example")]
        [TableMatrix(HorizontalTitle = "@UseAlternativeTitle ? AlternativeTitle : Title")]
        public bool[,] AttributeExpressionExample = new bool[5, 5];

        [FoldoutGroup("Field Name Example")]
        [TableMatrix(HorizontalTitle = "$Title")]
        public bool[,] FieldNameExample = new bool[5, 5];

        [FoldoutGroup("Method Name Example")]
        [TableMatrix(HorizontalTitle = "$GetTitle")]
        public bool[,] MethodNameExample = new bool[5, 5];

        [FoldoutGroup("Property Name Example")]
        [TableMatrix(HorizontalTitle = "$TitleProperty")]
        public bool[,] PropertyNameExample = new bool[5, 5];

        public string Title = "Peace, Love & Ducks";
        /*
            Note that this example requires Odin's serialization to be enabled to work,
            since it uses types that Unity will not serialize.
            Copy this code into a SerializedMonoBehaviour to preview it.

            Make sure to reselect the object after changing UseAlternativeTitle to see the change in effect.
        */

        public bool UseAlternativeTitle;
        public string TitleProperty => UseAlternativeTitle ? AlternativeTitle : Title;

        string GetTitle() => UseAlternativeTitle ? AlternativeTitle : Title;
    }
    // End

    [ResolvedParameterExample]
    public class TableMatrixExamples_VerticalTitle
    {
        public string AlternativeTitle = "Peace, Love & Fenrir";

        [FoldoutGroup("Attribute Expression Example")]
        [TableMatrix(VerticalTitle = "@UseAlternativeTitle ? AlternativeTitle : Title")]
        public bool[,] AttributeExpressionExample = new bool[5, 5];

        [FoldoutGroup("Field Name Example")]
        [TableMatrix(VerticalTitle = "$Title")]
        public bool[,] FieldNameExample = new bool[5, 5];

        [FoldoutGroup("Method Name Example")]
        [TableMatrix(VerticalTitle = "$GetTitle")]
        public bool[,] MethodNameExample = new bool[5, 5];

        [FoldoutGroup("Property Name Example")]
        [TableMatrix(VerticalTitle = "$TitleProperty")]
        public bool[,] PropertyNameExample = new bool[5, 5];

        public string Title = "Peace, Love & Ducks";
        /*
            Note that this example requires Odin's serialization to be enabled to work,
            since it uses types that Unity will not serialize.
            Copy this code into a SerializedMonoBehaviour to preview it.

            Make sure to reselect the object after changing UseAlternativeTitle to see the change in effect.
        */

        public bool UseAlternativeTitle;
        public string TitleProperty => UseAlternativeTitle ? AlternativeTitle : Title;

        string GetTitle() => UseAlternativeTitle ? AlternativeTitle : Title;
    }
    // End
}
