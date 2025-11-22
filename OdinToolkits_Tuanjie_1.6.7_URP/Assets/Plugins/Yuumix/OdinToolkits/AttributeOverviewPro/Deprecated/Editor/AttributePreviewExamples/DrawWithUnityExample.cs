using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    public class DrawWithUnityExample : ExampleSO
    {
        #region Serialized Fields

        [Title("Odin 绘制")]
        public GameObject objectDrawnWithOdin;

        [Title("调用 Unity 绘制")]
        [DrawWithUnity]
        public GameObject objectDrawnWithUnity;

        #endregion
    }
}
