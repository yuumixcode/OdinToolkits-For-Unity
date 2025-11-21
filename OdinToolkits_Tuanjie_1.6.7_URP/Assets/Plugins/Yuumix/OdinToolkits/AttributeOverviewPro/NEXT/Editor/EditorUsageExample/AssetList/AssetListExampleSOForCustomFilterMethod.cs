using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Yuumix.OdinToolkits.Core.Editor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.NEXT
{
    public class AssetListExampleSOForCustomFilterMethod :
        EditorScriptableSingleton<AssetListExampleSOForCustomFilterMethod>
    {
        [AssetList(CustomFilterMethod = "$HasRigidbodyComponent")]
        public List<GameObject> myRigidbodyPrefabs;

        bool HasRigidbodyComponent(GameObject obj) => obj.GetComponent<Rigidbody>();
    }
}
