using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.Editor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.NEXT
{
    [AttributeOverviewProExample]
    public class AssetListExampleSOForCustomFilterMethod :
        EditorScriptableSingleton<AssetListExampleSOForCustomFilterMethod>, IOdinToolkitsEditorReset
    {
        #region Serialized Fields

        [AssetList(CustomFilterMethod = "$HasRigidbodyComponent")]
        public List<GameObject> myRigidbodyPrefabs;

        #endregion

        #region IOdinToolkitsEditorReset Members

        public void EditorReset()
        {
            myRigidbodyPrefabs = null;
        }

        #endregion

        bool HasRigidbodyComponent(GameObject obj) => obj.GetComponent<Rigidbody>();
    }
}
