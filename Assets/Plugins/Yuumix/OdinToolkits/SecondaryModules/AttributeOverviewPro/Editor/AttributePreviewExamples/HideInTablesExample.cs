using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class HideInTablesExample : ExampleSO
    {
        [Indent]
        public MyItem item = new MyItem();

        [TableList]
        public List<MyItem> table = new List<MyItem>
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

            [HideInTables]
            public int hidden;
        }
    }
}
