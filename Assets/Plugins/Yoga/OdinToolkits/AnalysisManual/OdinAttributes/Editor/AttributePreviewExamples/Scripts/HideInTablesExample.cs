using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class HideInTablesExample : ExampleScriptableObject
    {
        [Indent]
        public MyItem item = new MyItem();

        [TableList]
        public List<MyItem> table = new List<MyItem>()
        {
            new MyItem(),
            new MyItem(),
            new MyItem(),
        };

        [Serializable]
        public class MyItem
        {
            public string a;

            public int b;

            [HideInTables]
            public int hidden;
        }
    }
}