using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [AttributeOverviewProExample]
    [TypeInfoBox("编辑器模式下隐藏字段")]
    public class HideInEditorModeExample : ExampleSO
    {
        #region Serialized Fields

        [Title("Hide in edit mode")]
        [HideInEditorMode]
        public GameObject hideInEditorMode1;

        [HideInEditorMode]
        public Material hideInEditorMode2;

        #endregion
    }
}
