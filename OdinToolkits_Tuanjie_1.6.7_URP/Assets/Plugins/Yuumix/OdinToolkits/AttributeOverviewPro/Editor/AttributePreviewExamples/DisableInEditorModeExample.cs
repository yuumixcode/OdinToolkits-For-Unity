using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [AttributeOverviewProExample]
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
