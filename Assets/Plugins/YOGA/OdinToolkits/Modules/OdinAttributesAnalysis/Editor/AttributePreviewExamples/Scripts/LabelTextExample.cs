using Sirenix.OdinInspector;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class LabelTextExample : ExampleScriptableObject
    {
        [FoldoutGroup("text 参数")] [LabelText("1")]
        public int myInt1 = 1;

        [FoldoutGroup("text 参数")] [LabelText("2")]
        public int myInt2 = 12;

        [FoldoutGroup("text 参数")] [InfoBox("Use $ to refer to a member string.")] [LabelText("$myInt2")]
        public string labelText = "The label is taken from the number 3 above";

        [FoldoutGroup("text 参数")]
        [InfoBox("Use @ to execute an expression.")]
        [LabelText("@DateTime.Now.ToString(\"HH:mm:ss\")")]
        public string dateTimeLabel;

        [FoldoutGroup("nicifyText 参数")] [LabelText("m_someField", true)]
        public int mTestField;

        [FoldoutGroup("icon 和 IconColor")] [LabelText("Test", SdfIconType.HeartFill)]
        public int labelIcon1 = 123;

        [FoldoutGroup("icon 和 IconColor")] [LabelText("", SdfIconType.HeartFill, IconColor = "lightpurple")]
        public int labelIcon2 = 123;
    }
}