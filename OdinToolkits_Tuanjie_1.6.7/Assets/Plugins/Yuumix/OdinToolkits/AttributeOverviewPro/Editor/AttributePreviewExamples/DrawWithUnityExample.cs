using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
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
