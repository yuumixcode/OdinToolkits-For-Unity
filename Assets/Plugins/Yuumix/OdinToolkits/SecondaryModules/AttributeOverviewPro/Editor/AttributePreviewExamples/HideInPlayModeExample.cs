using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    [TypeInfoBox("Play 运行模式下隐藏字段")]
    public class HideInPlayModeExample : ExampleSO
    {
        #region Serialized Fields

        [Title("Hide in play mode")]
        [HideInPlayMode]
        public GameObject hideInPlayMode1;

        [HideInPlayMode]
        public Material hideInPlayMode2;

        #endregion
    }
}
