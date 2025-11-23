using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.Editor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    [AttributeOverviewProExample]
    public class AssetListExampleForCustomFilterMethodSO :
        EditorScriptableSingleton<AssetListExampleForCustomFilterMethodSO>, IOdinToolkitsEditorReset
    {
        #region Serialized Fields

        [Title("$CustomFilterMethodTitleLabel")]
        [AssetList(CustomFilterMethod = "$HasRigidbodyComponent")]
        [InlineButton("LogRigidbodyPrefabs", "$LogButtonLabel")]
        public List<GameObject> rigidbodyPrefabs;

        #endregion

        #region IOdinToolkitsEditorReset Members

        public void EditorReset()
        {
            rigidbodyPrefabs = null;
        }

        #endregion

        bool HasRigidbodyComponent(GameObject obj) => obj.GetComponent<Rigidbody>();

        #region Helper

        static readonly BilingualData CustomFilterMethodTitleLabel = new BilingualData(
            "参数: Custom Filter Method",
            "Parameter: Custom Filter Method");

        static readonly BilingualData LogButtonLabel = new BilingualData("输出信息", "Output Info");

        void LogRigidbodyPrefabs()
        {
            foreach (var prefab in rigidbodyPrefabs)
            {
                Debug.Log("Rigidbody Prefab: " + prefab.name);
            }
        }

        #endregion
    }
}
