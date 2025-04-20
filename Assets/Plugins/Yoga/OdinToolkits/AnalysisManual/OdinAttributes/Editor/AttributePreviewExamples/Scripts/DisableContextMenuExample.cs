using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class DisableContextMenuExample : ExampleScriptableObject
    {
        [Title("默认只关闭标记的 Property 的右键菜单")]
        [InfoBox("DisableContextMenu 只能禁用 Odin 提供的 ContextMenu，并不会关闭 Unity 原生部分",
            InfoMessageType.Warning)]
        [DisableContextMenu]
        public int[] noRightClickList = new int[] { 2, 3, 5 };

        [DisableContextMenu]
        public int noRightClickField = 19;

        [Title("关闭数组元素右键菜单，不关闭数组 Property 的右键菜单")]
        [DisableContextMenu(disableForMember: false, disableCollectionElements: true)]
        public int[] noRightClickListOnListElements = new int[] { 7, 11 };

        [Title("关闭数组 Property 的右键菜单，并关闭数组元素右键菜单")]
        [DisableContextMenu(disableForMember: true, disableCollectionElements: true)]
        public int[] disableRightClickCompletely = new int[] { 13, 17 };

        [Title("Unity 内置的 ContextMenuItem 特性")]
        [InfoBox("当标记 [DrawWithUnity] 时，由 Unity 负责 ContextMenu 绘制，[DisableContextMenu] 无效")]
        [DrawWithUnity]
        [DisableContextMenu]
        [ContextMenuItem("测试 Unity 特性", "SayHello")]
        public int unityProperty;
    }
}