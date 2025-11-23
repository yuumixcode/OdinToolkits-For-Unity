using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    public class AssetListExample : ExampleSO
    {
        #region Serialized Fields

        [FoldoutGroup("NoParameterTitleLabel")]
        [AssetList]
        [InlineButton("Log1", "LogButtonLabel")]
        public ExampleSO example;

        [FoldoutGroup("AutoPopulateTitleLabel")]
        [AssetList(AutoPopulate = true)]
        [InlineButton("Log2", "LogButtonLabel")]
        public List<ExampleOdinSO> odinExamples;

        [FoldoutGroup("PathTitleLabel")]
        [InfoBox("文件夹路径可以省略 Assets/。另外 Rider 提供智能补全路径")]
        [AssetList(Path = "Plugins/Yuumix/OdinToolkits")]
        [InlineButton("Log3", "LogButtonLabel")]
        public List<ExampleOdinSO> odinExamples2;

        [FoldoutGroup("TagsTitleLabel")]
        [InfoBox("Tags = \"Respawn\"。可以使用逗号分隔多个 Tag")]
        [AssetList(Tags = "Respawn")]
        [InlineButton("Log4", "LogButtonLabel")]
        public List<GameObject> gameObjectsWithTags;

        [FoldoutGroup("LayerNamesTitleLabel")]
        [InfoBox("LayerNames = \"Water\"。可以使用逗号分隔多个 LayerName")]
        [AssetList(LayerNames = "Water")]
        [InlineButton("Log5", "LogButtonLabel")]
        public List<GameObject> gameObjectsWithLayerName;

        [FoldoutGroup("AssetNamePrefixTitleLabel")]
        [InfoBox("AssetNamePrefix = \"Shared\"")]
        [AssetList(AssetNamePrefix = "Shared")]
        [InlineButton("Log6", "LogButtonLabel")]
        public List<GameObject> gameObjectsWithPrefix;

        [FoldoutGroup("CustomMethod")]
        [InfoBox("CustomMethod = \"IsPrefab\"")]
        [AssetList(CustomFilterMethod = "IsPrefab")]
        [InlineButton("Log7", "$LogButtonLabel")]
        public List<GameObject> gameObjectsWithCustomMethod;

        bool IsPrefab(GameObject asset) => PrefabUtility.GetPrefabAssetType(asset) == PrefabAssetType.Regular;

        #endregion

        #region Helper

        static readonly BilingualData LogButtonLabel = new BilingualData("输出值信息", "Output Value");
        static readonly BilingualData NoParameterTitleLabel = new BilingualData("无参数", "No Parameter");

        static readonly BilingualData AutoPopulateTitleLabel =
            new BilingualData("参数：AutoPopulate", "Parameter: AutoPopulate");

        static readonly BilingualData PathTitleLabel = new BilingualData("参数：Path", "Parameter: Path");

        static readonly BilingualData TagsTitleLabel = new BilingualData("参数：Tags", "Parameter: Tags");

        static readonly BilingualData LayerNamesTitleLabel =
            new BilingualData("参数：LayerNames", "Parameter: LayerNames");

        static readonly BilingualData AssetNamePrefixTitleLabel =
            new BilingualData("参数：AssetNamePrefix", "Parameter: AssetNamePrefix");

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

        #endregion
    }
}
