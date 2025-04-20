using Sirenix.OdinInspector;
using YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Common.Scripts;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class DisableInInlineEditorExample : ExampleScriptableObject
    {
        [InlineEditor]
        public CommonInlineObject commonInlineObject;
    }
}