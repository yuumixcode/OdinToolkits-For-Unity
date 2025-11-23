using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.Editor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    [AttributeOverviewProExample]
    public class AssetsOnlyExampleSO : EditorScriptableSingleton<AssetsOnlyExampleSO>, IOdinToolkitsEditorReset
    {
        #region Serialized Fields

        [Title("No Parameters")]
        [AssetsOnly]
        public GameObject somePrefab;

        [AssetsOnly]
        public Material materialAsset;

        [AssetsOnly]
        public MeshRenderer someMeshRendererOnPrefab;

        #endregion

        #region IOdinToolkitsEditorReset Members

        public void EditorReset()
        {
            somePrefab = null;
            materialAsset = null;
            someMeshRendererOnPrefab = null;
        }

        #endregion
    }
}
