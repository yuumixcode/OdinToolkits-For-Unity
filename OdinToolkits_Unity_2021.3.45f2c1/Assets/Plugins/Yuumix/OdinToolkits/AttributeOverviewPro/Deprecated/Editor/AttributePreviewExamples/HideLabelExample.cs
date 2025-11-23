using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    public class HideLabelExample : ExampleSO
    {
        public override void SetDefaultValue()
        {
            hideLabel1 = 0;
            hideLabel2 = "";
            wideColor1 = Color.white;
            wideVector1 = Vector3.zero;
            wideVector2 = Vector4.zero;
            wideMultilineTextField = "";
        }

        #region Serialized Fields

        [InfoBox("Unity 默认情况，一个完整的 Property 由字段名标签和值组成")]
        public int hideLabel1;

        [PropertyOrder(5)]
        [HideLabel]
        [InfoBox("使用 HideLabel 特性，隐藏字段名标签")]
        [ReadOnly]
        [EnableGUI]
        public string hideLabel2 = "字段名被隐藏了";

        [PropertyOrder(10)]
        [FoldoutGroup("Hide Label 扩展")]
        [InfoBox("可以和 Unity 内置的 Header 结合")]
        [Header("Wide Colors")]
        [HideLabel]
        [ColorPalette("Fall")]
        public Color wideColor1;

        [PropertyOrder(20)]
        [FoldoutGroup("Hide Label 扩展")]
        [Title("Wide Vector 2")]
        [HideLabel]
        public Vector3 wideVector1;

        [PropertyOrder(30)]
        [FoldoutGroup("Hide Label 扩展")]
        [Title("Wide Vector 4")]
        [HideLabel]
        public Vector4 wideVector2;

        [PropertyOrder(40)]
        [FoldoutGroup("Hide Label 扩展")]
        [Title("Wide Multiline Text Field")]
        [HideLabel]
        [MultiLineProperty]
        public string wideMultilineTextField = "";

        #endregion
    }
}
