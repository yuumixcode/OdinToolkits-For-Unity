using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [AttributeOverviewProExample]
    public class AssetSelectorExample : ExampleSO
    {
        [FoldoutGroup("无参数使用")]
        [AssetSelector]
        public ExampleSO example;

        [FoldoutGroup("FlattenTreeView")]
        [AssetSelector(FlattenTreeView = true)]
        public ExampleSO example2;

        [FoldoutGroup("Path")]
        [InfoBox("相对路径，Rider 中支持自动补全，| 表示分隔多条路径")]
        [AssetSelector(Paths = "Assets/Plugins/OdinToolkits/ChineseManual/ChineseAttributesOverview/RuntimeExamples")]
        public GameObject gameObject;

        [FoldoutGroup("IsUniqueList == false")]
        [AssetSelector(IsUniqueList = false)]
        public List<GameObject> gameObjects;

        [FoldoutGroup("DrawDropdownForListElements == false")]
        [AssetSelector(DrawDropdownForListElements = false)]
        public List<GameObject> gameObjects2;

        [FoldoutGroup("DisableListAddButtonBehaviour == true")]
        [AssetSelector(DisableListAddButtonBehaviour = true)]
        public List<GameObject> gameObjects3;

        [FoldoutGroup("ExcludeExistingValuesInList == true")]
        [AssetSelector(ExcludeExistingValuesInList = true)]
        public List<GameObject> gameObjects4;

        [FoldoutGroup("ExpandAllMenuItems == false")]
        [AssetSelector(ExpandAllMenuItems = false)]
        public List<GameObject> gameObjects5;

        [FoldoutGroup("DropdownSettings")]
        [AssetSelector(DropdownWidth = 600, DropdownHeight = 300, DropdownTitle = "下拉框标题")]
        public GameObject gameObject6;

        [FoldoutGroup("AssetDatabase")]
        [TitleGroup("AssetDatabase/SearchInFolders")]
        [AssetSelector(SearchInFolders = new[]
        {
            "Assets/Plugins/OdinToolkits/ChineseManual/ChineseAttributesOverview/Editor/AttributeContainers/SO",
            "Assets/Plugins/OdinToolkits/ChineseManual/ChineseAttributesOverview/Editor/AttributePreviewExamples/SO"
        })]
        public List<ScriptableObject> gameObjects7;

        [FoldoutGroup("AssetDatabase")]
        [TitleGroup("AssetDatabase/Filter")]
        [AssetSelector(Filter = "t:MonoScript")]
        [InfoBox("填写的字符串是用作 AssetDatabase.FindAssets() 的参数")]
        public List<MonoScript> gameObjects8;

        [FoldoutGroup("AssetDatabase")]
        [TitleGroup("AssetDatabase/Button")]
        [Button("跳转到 AssetDatabase.FindAssets 文档", ButtonSizes.Large)]
        void Browse()
        {
            Help.BrowseURL(
                "https://docs.unity3d.com/6000.0/Documentation/ScriptReference/AssetDatabase.FindAssets.html");
        }
    }
}
