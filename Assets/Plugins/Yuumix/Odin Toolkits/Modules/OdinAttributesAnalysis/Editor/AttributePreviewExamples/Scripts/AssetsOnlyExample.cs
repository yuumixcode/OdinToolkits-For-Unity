using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class AssetsOnlyExample : ExampleScriptableObject
    {
        // PropertyOrder 默认为 0
        [LabelText("普通的引用选择器: ")]
        public GameObject normal;

        [PropertyOrder(1)]
        [AssetsOnly]
        [LabelText("选择项目资产: ")]
        public GameObject onlyPrefabAsset;

        [PropertyOrder(10)]
        [AssetsOnly]
        [LabelText("选择项目中的材质文件: ")]
        public Material materialAsset;

        [PropertyOrder(20)]
        [AssetsOnly]
        [LabelText("AssetsOnly 标记的列表: ")]
        public List<GameObject> prefabs;

        public override void SetDefaultValue()
        {
            normal = null;
            onlyPrefabAsset = null;
            materialAsset = null;
            prefabs = new List<GameObject>();
        }
    }
}
