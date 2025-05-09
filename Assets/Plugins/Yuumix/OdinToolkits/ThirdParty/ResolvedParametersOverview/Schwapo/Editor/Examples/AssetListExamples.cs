using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace YOGA.Modules.OdinToolkits.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class AssetListExamples_CustomFilterMethod
    {
        [FoldoutGroup("Attribute Expression Example")]
        [InfoBox("Should Only Contain Textures That Start With TMP")]
        [AssetList(CustomFilterMethod = "@$asset.name.StartsWith(\"TMP\")")]
        public List<Texture> AttributeExpressionExample = new();

        [FoldoutGroup("Method Name Example")]
        [InfoBox("Should Only Contain Prefabs")]
        [AssetList(CustomFilterMethod = "IsPrefab")]
        public List<GameObject> MethodNameExample = new();

        private bool IsPrefab(GameObject asset)
        {
            return PrefabUtility.GetPrefabAssetType(asset) == PrefabAssetType.Regular;
        }
    }
    // End
}