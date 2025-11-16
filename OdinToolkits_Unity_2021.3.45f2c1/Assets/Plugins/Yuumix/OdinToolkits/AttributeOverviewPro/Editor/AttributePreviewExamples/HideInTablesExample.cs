using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class HideInTablesExample : ExampleSO
    {
        #region Serialized Fields

        [Indent]
        public MyItem item = new MyItem();

        [TableList]
        public List<MyItem> table = new List<MyItem>
        {
            new MyItem(),
            new MyItem(),
            new MyItem()
        };

        #endregion

        #region Nested type: ${0}

        [Serializable]
        public class MyItem
        {
            #region Serialized Fields

            public string a;

            public int b;

            [HideInTables]
            public int hidden;

            #endregion
        }

        #endregion
    }
}
