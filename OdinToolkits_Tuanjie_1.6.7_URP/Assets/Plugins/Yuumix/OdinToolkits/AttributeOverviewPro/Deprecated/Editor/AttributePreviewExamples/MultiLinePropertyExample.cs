using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    public class MultiLinePropertyExample : ExampleSO
    {
        [FoldoutGroup("Odin MultiLineProperty")]
        [InfoBox("Odin 支持属性, 但是 Unity 内置的 Multiline 只能作用于字段")]
        [ShowInInspector]
        [MultiLineProperty(10)]
        public string OdinMultilineProperty { get; set; }

        public override void SetDefaultValue()
        {
            unityMultilineField = null;
            unityTextAreaField = null;
            wideMultilineTextField = null;
            OdinMultilineProperty = null;
        }

        #region Serialized Fields

        [FoldoutGroup("Unity 内置")]
        [Multiline(10)]
        public string unityMultilineField = "";
        // Unity 的 TextArea 和 Multiline 以及 Odin 的 MultiLineProperty 都很相似
        // TextArea 指定最小和最大行数。它至少会显示最小行数，但会随着内容扩展到最大，然后显示滚动条
        // Multiline 和 MultiLineProperty 被指定占用的精确行数
        // 永远不会根据内容收缩或扩展

        [FoldoutGroup("Unity 内置")]
        [TextArea(4, 10)]
        public string unityTextAreaField = "";

        [FoldoutGroup("Odin MultiLineProperty")]
        [Title("Wide Multiline Text Field", bold: false)]
        [HideLabel]
        [MultiLineProperty(10)]
        public string wideMultilineTextField = "";

        #endregion
    }
}
