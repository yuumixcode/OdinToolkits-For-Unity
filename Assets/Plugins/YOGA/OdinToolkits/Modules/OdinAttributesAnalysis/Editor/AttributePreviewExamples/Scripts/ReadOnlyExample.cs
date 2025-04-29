using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class ReadOnlyExample : ExampleScriptableObject
    {
        [ReadOnly] public string readOnly1 = "这个字段是只读的，不可以在面板上修改，但是可以通过代码修改";

        [PropertyOrder(1)] [InfoBox("可以用在集合类型上 | Array | List，让集合也变成只读")] [ReadOnly]
        public List<int> readOnlyList = new() { 1, 2, 3 };

        [ShowInInspector] [ReadOnly] public string ReadOnly2 => "这个属性是只读的";
    }
}