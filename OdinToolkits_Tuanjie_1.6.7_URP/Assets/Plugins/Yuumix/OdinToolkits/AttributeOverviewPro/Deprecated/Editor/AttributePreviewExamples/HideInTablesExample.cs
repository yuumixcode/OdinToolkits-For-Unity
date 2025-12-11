using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    public class HideInTablesExample : ExampleSO
    {
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
    }
}
