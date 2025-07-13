using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Editor
{
    [OdinToolkitsAttributeExample]
    public class AssetListExample : ExampleSO
    {
        [FoldoutGroup("无参数使用")]
        [AssetList]
        [InlineButton("Log1", "输出值信息")]
        public ExampleSO example;

        [FoldoutGroup("AutoPopulate 自动填充列表")]
        [AssetList(AutoPopulate = true)]
        [InlineButton("Log2", "输出值信息")]
        public List<ExampleOdinSO> odinExamples;

        [FoldoutGroup("Path 文件夹相对路径")]
        [InfoBox("文件夹路径省略 Assets/，且 Rider 中可以智能补全路径")]
        [AssetList(Path = "Plugins/OdinToolkits")]
        [InlineButton("Log3", "输出值信息")]
        public List<ExampleOdinSO> odinExamples2;

        [FoldoutGroup("Tags 使用逗号分隔 Respawn Tag")]
        [InfoBox("Tags = \"Respawn\"")]
        [AssetList(Tags = "Respawn")]
        [InlineButton("Log4", "输出值信息")]
        public List<GameObject> gameObjectsWithTags;

        [FoldoutGroup("TaLayerNames 使用逗号分隔 Layer")]
        [InfoBox("LayerNames = \"Water\"")]
        [AssetList(LayerNames = "Water")]
        [InlineButton("Log5", "输出值信息")]
        public List<GameObject> gameObjectsWithLayerName;

        [FoldoutGroup("AssetNamePrefix")]
        [InfoBox("Prefix = \"Shared\"")]
        [AssetList(AssetNamePrefix = "Shared")]
        [InlineButton("Log6", "输出值信息")]
        public List<GameObject> gameObjectsWithPrefix;

        [FoldoutGroup("CustomMethod")]
        [InfoBox("CustomMethod = \"IsPrefab\"")]
        [AssetList(CustomFilterMethod = "IsPrefab")]
        [InlineButton("Log7", "输出值信息")]
        public List<GameObject> gameObjectsWithCustomMethod;

        bool IsPrefab(GameObject asset) => PrefabUtility.GetPrefabAssetType(asset) == PrefabAssetType.Regular;

        void Log1()
        {
            Debug.Log("Example = " + example);
        }

        void Log2()
        {
            Debug.Log("OdinExamples 列表的长度为: " + odinExamples.Count);
        }

        void Log3()
        {
            Debug.Log("OdinExamples2 列表的长度为: " + odinExamples2.Count);
        }

        void Log4()
        {
            Debug.Log("GameObjectsWithTags 列表的长度为: " + gameObjectsWithTags.Count);
        }

        void Log5()
        {
            Debug.Log("GameObjectsWithLayerName 列表的长度为: " + gameObjectsWithLayerName.Count);
        }

        void Log6()
        {
            Debug.Log("GameObjectsWithPrefix 列表的长度为: " + gameObjectsWithPrefix.Count);
        }

        void Log7()
        {
            Debug.Log("GameObjectsWithCustomMethod 列表的长度为: " + gameObjectsWithCustomMethod.Count);
        }
    }
}
