using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Editor
{
    [OdinToolkitsAttributeExample]
    public class DisableInEditorModeExample : ExampleSO
    {
        [Title("Disabled in edit mode")]
        [DisableInEditorMode]
        public GameObject disableInEditorMode1;

        [DisableInEditorMode]
        public Material disableInEditorMode2;
    }
}
