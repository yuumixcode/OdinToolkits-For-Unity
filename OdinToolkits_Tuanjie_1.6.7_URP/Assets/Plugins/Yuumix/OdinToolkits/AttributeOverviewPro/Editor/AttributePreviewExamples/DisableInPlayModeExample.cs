using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class DisableInPlayModeExample : ExampleSO
    {
        [Title("Disabled in edit mode")]
        [DisableInPlayMode]
        public GameObject disableInPlayMode1;

        [DisableInPlayMode]
        public Material disableInPlayMode2;
    }
}
