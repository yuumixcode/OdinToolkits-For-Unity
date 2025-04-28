using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class OnValueChangedExample : ExampleScriptableObject
    {
        [OnValueChanged("CreateMaterial")] public Shader shader;

        [ReadOnly] [InlineEditor(InlineEditorModes.LargePreview)]
        public Material material;

        [OnValueChanged("ValueChange")] [InlineButton("ChangeValue", "代码修改值")]
        public int value;

        private void CreateMaterial()
        {
            if (material != null) DestroyImmediate(material);

            if (shader != null) material = new Material(shader);
        }

        private void ChangeValue()
        {
            value++;
        }

        private void ValueChange()
        {
            Debug.Log("Value changed to " + value);
        }
    }
}