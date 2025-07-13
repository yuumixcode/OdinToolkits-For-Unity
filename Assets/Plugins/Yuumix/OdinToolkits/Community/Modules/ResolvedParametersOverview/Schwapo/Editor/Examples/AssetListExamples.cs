using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Community.Schwapo.Editor
{
    [ResolvedParameterExample]
    public class AssetListExamples_CustomFilterMethod
    {
        [FoldoutGroup("Attribute Expression Example")]
        [InfoBox("Should Only Contain Textures That Start With TMP")]
        [AssetList(CustomFilterMethod = "@$asset.name.StartsWith(\"TMP\")")]
        public List<Texture> AttributeExpressionExample = new List<Texture>();

        [FoldoutGroup("Method Name Example")]
        [InfoBox("Should Only Contain Prefabs")]
        [AssetList(CustomFilterMethod = "IsPrefab")]
        public List<GameObject> MethodNameExample = new List<GameObject>();

        bool IsPrefab(GameObject asset) => PrefabUtility.GetPrefabAssetType(asset) == PrefabAssetType.Regular;
    }
    // End
}
