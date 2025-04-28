using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
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