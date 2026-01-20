using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
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
