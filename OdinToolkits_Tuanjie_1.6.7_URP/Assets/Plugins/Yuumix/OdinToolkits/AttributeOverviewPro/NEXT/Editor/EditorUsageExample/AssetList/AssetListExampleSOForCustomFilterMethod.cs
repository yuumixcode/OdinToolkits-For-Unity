using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;
using Yuumix.OdinToolkits.Core.Editor;
using Yuumix.OdinToolkits.Modules.Editor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.NEXT
{
    [AttributeOverviewProExample]
    public class AssetListExampleSOForCustomFilterMethod :
        EditorScriptableSingleton<AssetListExampleSOForCustomFilterMethod>
    {
        [AssetList(CustomFilterMethod = "$HasRigidbodyComponent")]
        public List<GameObject> myRigidbodyPrefabs;

        bool HasRigidbodyComponent(GameObject obj) => obj.GetComponent<Rigidbody>();
    }
}
