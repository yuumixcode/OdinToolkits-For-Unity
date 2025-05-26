using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class PropertyOrderExample : ExampleScriptableObject
    {
        public string propertyOrder1 = "默认序号为 0，通常是按代码编写顺序进行绘制";

        [PropertyOrder(-5)]
        public string propertyOrder2 = "此时序号为 -5 ，小于 0，优先绘制";

        [PropertyOrder(10)]
        [InfoBox("控制属性的绘制顺序")]
        [ShowInInspector]
        public string Property => "可以控制属性的绘制顺序，序号为 10";

        [PropertyOrder(20)]
        [OnInspectorGUI]
        public void OnGUI()
        {
            GUILayout.Label("可以控制 GUI 方法的绘制顺序，序号为 20");
        }

        public override void SetDefaultValue()
        {
            propertyOrder1 = "默认序号为 0，通常是按代码编写顺序进行绘制";
            propertyOrder2 = "此时序号为 -5 ，小于 0，优先绘制";
        }
    }
}
