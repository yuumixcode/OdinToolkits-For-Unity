using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.Editor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.NEXT
{
    [AttributeOverviewProExample]
    public class AssetListExampleSO : EditorScriptableSingleton<AssetListExampleSO>, IOdinToolkitsEditorReset
    {
        #region Serialized Fields

        [AssetList]
        [PreviewField(70, ObjectFieldAlignment.Center)]
        public Texture2D singleObject;

        [AssetList(Path = "/Plugins/Sirenix/")]
        public List<ScriptableObject> assetList;

        [FoldoutGroup("Filtered Odin ScriptableObjects", false)]
        [AssetList(Path = "Plugins/Sirenix/")]
        public ScriptableObject scriptableObject;

        [AssetList(AutoPopulate = true, Path = "Plugins/Sirenix/")]
        [FoldoutGroup("Filtered Odin ScriptableObjects", false)]
        public List<ScriptableObject> autoPopulatedWhenInspected;

        [AssetList(LayerNames = "MyLayerName")]
        [FoldoutGroup("Filtered AssetLists examples")]
        public GameObject[] allPrefabsWithLayerName;

        [AssetList(AssetNamePrefix = "Rock")]
        [FoldoutGroup("Filtered AssetLists examples")]
        public List<GameObject> prefabsStartingWithRock;

        [FoldoutGroup("Filtered AssetLists examples")]
        [AssetList(Tags = "MyTagA, MyTabB", Path = "/Plugins/Sirenix/")]
        public List<GameObject> gameObjectsWithTag;

        #endregion

        #region IOdinToolkitsEditorReset Members

#if UNITY_EDITOR
        public void EditorReset()
        {
            singleObject = null;
            assetList = null;
            scriptableObject = null;
            autoPopulatedWhenInspected = null;
            allPrefabsWithLayerName = null;
            prefabsStartingWithRock = null;
            gameObjectsWithTag = null;
        }
#endif

        #endregion
    }
}
