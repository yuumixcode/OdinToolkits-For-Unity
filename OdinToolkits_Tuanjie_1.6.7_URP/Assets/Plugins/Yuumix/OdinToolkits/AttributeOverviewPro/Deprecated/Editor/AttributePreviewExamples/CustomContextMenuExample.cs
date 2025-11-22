using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    public class CustomContextMenuExample : ExampleSO
    {
        #region Serialized Fields

        [Title("Unity 内置的 ContextMenuItem 特性")]
        [InfoBox("Odin 会默认覆盖 Context 菜单，需要标记 [DrawWithUnity]，" +
                 "使用 Unity 原生菜单，才可以查看此特性新增的菜单项")]
        [DrawWithUnity]
        [ContextMenuItem("测试 Unity 特性", "SayHello")]
        public int unityProperty;

        [Title("Odin Inspector 的 CustomContextMenu 特性")]
        [InfoBox("新增右键菜单方法")]
        [CustomContextMenu("Say Hello/Twice", "SayHello")]
        public int myProperty;

        #endregion

        void SayHello()
        {
            Debug.Log("Hello Twice");
        }
    }
}
