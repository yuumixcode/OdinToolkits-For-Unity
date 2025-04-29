using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class PropertySpaceExample : ExampleScriptableObject
    {
        [BoxGroup("距离上一个 property | Unity")] [Space(10)]
        public string propertySpaceInfo = "使用了 Unity 原生的 Space";

        [BoxGroup("距离上一个 property")] [PropertySpace(10)]
        public string propertySpace1 = "距离上一个 property 有 10 个像素";

        [BoxGroup("同时设置")]
        [ShowInInspector]
        [InfoBox("这是一个属性，不是字段，上方间隔 10 像素，下方间隔 20 像素")]
        [PropertySpace(10F, 20F)]
        public string PropertySpace2 => "这是一个属性，不是字段，距离上一个 property 有 10 个像素，下一个有 20 个像素";

        public override void SetDefaultValue()
        {
            propertySpaceInfo = "使用了 Unity 原生的 Space";
            propertySpace1 = "距离上一个 property 有 10 个像素";
        }
    }
}