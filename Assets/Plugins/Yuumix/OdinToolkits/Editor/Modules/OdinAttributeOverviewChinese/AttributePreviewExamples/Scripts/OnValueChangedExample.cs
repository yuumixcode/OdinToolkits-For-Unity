using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class OnValueChangedExample : ExampleScriptableObject
    {
        [OnValueChanged("CreateMaterial")]
        public Shader shader;

        [ReadOnly]
        [InlineEditor(InlineEditorModes.LargePreview)]
        public Material material;

        [OnValueChanged("ValueChange")]
        [InlineButton("ChangeValue", "代码修改值")]
        public int value;

        void CreateMaterial()
        {
            if (material != null)
            {
                DestroyImmediate(material);
            }

            if (shader != null)
            {
                material = new Material(shader);
            }
        }

        void ChangeValue()
        {
            value++;
        }

        void ValueChange()
        {
            Debug.Log("Value changed to " + value);
        }
    }
}
