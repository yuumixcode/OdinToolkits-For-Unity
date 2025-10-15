using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class DisableInEditorModeExample : ExampleSO
    {
        #region Serialized Fields

        [Title("Disabled in edit mode")]
        [DisableInEditorMode]
        public GameObject disableInEditorMode1;

        [DisableInEditorMode]
        public Material disableInEditorMode2;

        #endregion
    }
}
