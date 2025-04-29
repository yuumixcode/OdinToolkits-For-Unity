using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class AssetListExample : ExampleScriptableObject
    {
        [FoldoutGroup("无参数使用")] [AssetList] [InlineButton("Log1", "输出值信息")]
        public ExampleScriptableObject example;

        [FoldoutGroup("AutoPopulate 自动填充列表")] [AssetList(AutoPopulate = true)] [InlineButton("Log2", "输出值信息")]
        public List<ExampleOdinScriptableObject> odinExamples;

        [FoldoutGroup("Path 文件夹相对路径")]
        [InfoBox("文件夹路径省略 Assets/，且 Rider 中可以智能补全路径")]
        [AssetList(Path = "Plugins/OdinToolkits")]
        [InlineButton("Log3", "输出值信息")]
        public List<ExampleOdinScriptableObject> odinExamples2;

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

        private bool IsPrefab(GameObject asset)
        {
            return PrefabUtility.GetPrefabAssetType(asset) == PrefabAssetType.Regular;
        }

        private void Log1()
        {
            Debug.Log("Example = " + example);
        }

        private void Log2()
        {
            Debug.Log("OdinExamples 列表的长度为: " + odinExamples.Count);
        }

        private void Log3()
        {
            Debug.Log("OdinExamples2 列表的长度为: " + odinExamples2.Count);
        }

        private void Log4()
        {
            Debug.Log("GameObjectsWithTags 列表的长度为: " + gameObjectsWithTags.Count);
        }

        private void Log5()
        {
            Debug.Log("GameObjectsWithLayerName 列表的长度为: " + gameObjectsWithLayerName.Count);
        }

        private void Log6()
        {
            Debug.Log("GameObjectsWithPrefix 列表的长度为: " + gameObjectsWithPrefix.Count);
        }

        private void Log7()
        {
            Debug.Log("GameObjectsWithCustomMethod 列表的长度为: " + gameObjectsWithCustomMethod.Count);
        }
    }
}