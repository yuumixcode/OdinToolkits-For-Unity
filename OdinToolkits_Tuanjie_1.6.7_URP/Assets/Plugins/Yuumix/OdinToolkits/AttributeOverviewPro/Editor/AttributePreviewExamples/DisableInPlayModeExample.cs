using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [AttributeOverviewProExample]
    public class DisableInPlayModeExample : ExampleSO
    {
        [Title("Disabled in edit mode")]
        [DisableInPlayMode]
        public GameObject disableInPlayMode1;

        [DisableInPlayMode]
        public Material disableInPlayMode2;
    }
}
