using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [AttributeOverviewProExample]
    public class OnValueChangedExample : ExampleSO
    {
        #region Serialized Fields

        [OnValueChanged("CreateMaterial")]
        public Shader shader;

        [ReadOnly]
        [InlineEditor(InlineEditorModes.LargePreview)]
        public Material material;

        [OnValueChanged("ValueChange")]
        [InlineButton("ChangeValue", "代码修改值")]
        public int value;

        #endregion

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
