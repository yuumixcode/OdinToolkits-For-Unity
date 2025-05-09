using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class HideInTablesExample : ExampleScriptableObject
    {
        [Indent] public MyItem item = new();

        [TableList] public List<MyItem> table = new()
        {
            new MyItem(),
            new MyItem(),
            new MyItem()
        };

        [Serializable]
        public class MyItem
        {
            public string a;

            public int b;

            [HideInTables] public int hidden;
        }
    }
}