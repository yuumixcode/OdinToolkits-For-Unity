using Sirenix.OdinInspector;
using UnityEditor;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class EnumPagingExample : ExampleSO
    {
        #region SomeEnum enum

        public enum SomeEnum
        {
            A,
            B,
            C
        }

        #endregion

        #region Serialized Fields

        [PropertyOrder(1)]
        [FoldoutGroup("EnumPaging 基础使用")]
        [EnumPaging]
        public SomeEnum someEnumField;

        #endregion

#if UNITY_EDITOR
        [PropertyOrder(20)]
        [FoldoutGroup("EnumPaging 进阶使用")]
        [EnumPaging]
        [OnValueChanged("SetCurrentTool")]
        [InfoBox("可以和其他结合使用，该字段可以改变 Unity 编辑器当前选择的工具")]
        public Tool sceneTool;

        void SetCurrentTool()
        {
            Tools.current = sceneTool;
        }
#endif
    }
}
