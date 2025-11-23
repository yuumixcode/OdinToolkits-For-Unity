using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.Editor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    [AttributeOverviewProExample]
    public class AssetListExampleSO : EditorScriptableSingleton<AssetListExampleSO>, IOdinToolkitsEditorReset
    {
        #region Serialized Fields

        [FoldoutGroup("$NoParameterTitleLabel", false)]
        [AssetList]
        [PreviewField(70, ObjectFieldAlignment.Center)]
        [InlineButton("LogSingleObject", "$LogButtonLabel")]
        public Texture2D singleObject;

        [FoldoutGroup("$PathTitleLabel", false)]
        [AssetList(Path = "/Plugins/Sirenix/")]
        [InlineButton("LogAssetList", "$LogButtonLabel")]
        public List<ScriptableObject> assetList;

        [FoldoutGroup("$AutoPopulateAndPathTitleLabel", false)]
        [AssetList(AutoPopulate = true, Path = "Plugins/Sirenix/")]
        [InlineButton("LogAutoPopulatedWhenInspected", "$LogButtonLabel")]
        public List<ScriptableObject> autoPopulatedWhenInspected;

        [FoldoutGroup("$TagsTitleLabel", false)]
        [AssetList(Tags = "EditorOnly,Respawn")]
        [InlineButton("LogGameObjectsWithTag", "$LogButtonLabel")]
        public List<GameObject> gameObjectsWithTag;

        [FoldoutGroup("$LayerNamesTitleLabel", false)]
        [AssetList(LayerNames = "Water")]
        [InlineButton("LogGameObjectsWithLayerNames", "$LogButtonLabel")]
        public GameObject[] gameObjectsWithLayerNames;

        [FoldoutGroup("$AssetNamePrefixTitleLabel", false)]
        [AssetList(AssetNamePrefix = "OdinToolkits_")]
        [InlineButton("LogGameObjectsWithNamePrefix", "$LogButtonLabel")]
        public List<GameObject> gameObjectsWithNamePrefix;

        #endregion

        #region IOdinToolkitsEditorReset Members

        public void EditorReset()
        {
            singleObject = null;
            assetList = null;
            autoPopulatedWhenInspected = null;
            gameObjectsWithTag = null;
            gameObjectsWithLayerNames = null;
            gameObjectsWithNamePrefix = null;
        }

        #endregion

        #region Helper

        static readonly BilingualData NoParameterTitleLabel = new BilingualData("无参数", "No Parameter");
        static readonly BilingualData PathTitleLabel = new BilingualData("参数：Path", "Parameter: Path");

        static readonly BilingualData AutoPopulateAndPathTitleLabel =
            new BilingualData("参数：AutoPopulate + Path", "Parameter: AutoPopulate + Path");

        static readonly BilingualData TagsTitleLabel = new BilingualData("参数：Tags", "Parameter: Tags");

        static readonly BilingualData LayerNamesTitleLabel =
            new BilingualData("参数：LayerNames", "Parameter: LayerNames");

        static readonly BilingualData AssetNamePrefixTitleLabel =
            new BilingualData("参数：AssetNamePrefix", "Parameter: AssetNamePrefix");

        static readonly BilingualData LogButtonLabel = new BilingualData("输出信息", "Output Info");

        void LogSingleObject()
        {
            Debug.Log("SingleObject = " + singleObject);
        }

        void LogAssetList()
        {
            Debug.Log("assetList Length = " + assetList.Count);
        }

        void LogAutoPopulatedWhenInspected()
        {
            Debug.Log("autoPopulatedWhenInspected Length = " + autoPopulatedWhenInspected.Count);
        }

        void LogGameObjectsWithTag()
        {
            Debug.Log("GameObjectsWithTag Length = " + gameObjectsWithTag.Count);
        }

        void LogGameObjectsWithLayerNames()
        {
            Debug.Log("gameObjectsWithLayerNames Length = " + gameObjectsWithLayerNames.Length);
        }

        void LogGameObjectsWithNamePrefix()
        {
            Debug.Log("GameObjectsWithNamePrefix Length = " + gameObjectsWithNamePrefix.Count);
        }

        #endregion
    }
}
