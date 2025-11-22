using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    public class InfoBoxExample : ExampleSO
    {
        #region Serialized Fields

        [FoldoutGroup("message 参数")]
        [InfoBox("提示信息")]
        public int first;

        [FoldoutGroup("InfoMessageType 参数")]
        [InfoBox("Default info box.")]
        public int A;

        [FoldoutGroup("InfoMessageType 参数")]
        [InfoBox("Warning info box.", InfoMessageType.Warning)]
        public int B;

        [FoldoutGroup("InfoMessageType 参数")]
        [InfoBox("Error info box.", InfoMessageType.Error)]
        public int C;

        [FoldoutGroup("InfoMessageType 参数")]
        [InfoBox("Info box without an icon.", InfoMessageType.None)]
        public int D;

        [FoldoutGroup("控制显示 visibleIfMemberName 参数")]
        public bool toggleInfoBoxes;

        [FoldoutGroup("控制显示 visibleIfMemberName 参数")]
        [InfoBox("toggleInfoBoxes 参数控制", nameof(toggleInfoBoxes))]
        public float E;

        [FoldoutGroup("控制显示 visibleIfMemberName 参数")]
        [InfoBox("编辑器状态才显示", InfoMessageType.Error, nameof(IsInEditMode))]
        public float G;

        [FoldoutGroup("支持引用成员和表达式")]
        [InfoBox("$infoBoxMessage")]
        [InfoBox("@\"Time: \" + DateTime.Now.ToString(\"HH:mm:ss\")")]
        public string infoBoxMessage = "My dynamic info box message";

        [FoldoutGroup("GUIAlwaysEnabled")]
        [InfoBox("强制显示", GUIAlwaysEnabled = true)]
        [ReadOnly]
        public int H;

        [FoldoutGroup("Icon 和 IconColor")]
        [InfoBox("控制图标颜色和图标", Icon = SdfIconType.App, IconColor = "lightblue")]
        public int i;

        #endregion

        static bool IsInEditMode() => !Application.isPlaying;
    }
}
