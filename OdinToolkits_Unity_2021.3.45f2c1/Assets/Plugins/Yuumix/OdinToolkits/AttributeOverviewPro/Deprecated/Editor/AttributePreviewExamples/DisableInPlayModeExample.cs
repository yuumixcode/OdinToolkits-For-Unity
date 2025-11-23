using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    public class DisableInPlayModeExample : ExampleSO
    {
        #region Serialized Fields

        [Title("Disabled in edit mode")]
        [DisableInPlayMode]
        public GameObject disableInPlayMode1;

        [DisableInPlayMode]
        public Material disableInPlayMode2;

        #endregion
    }
}
