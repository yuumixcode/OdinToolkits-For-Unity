using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    public class OnCollectionChangedExample : ExampleOdinSO
    {
        public void Before(CollectionChangeInfo info, object value)
        {
            Debug.Log("改变前-触发方法，读取 CollectionChangeInfo: " + info +
                      ", 触发改变的集合为: " + value);
        }

        public void After(CollectionChangeInfo info, object value)
        {
            Debug.Log("改变后-触发方法，读取 CollectionChangeInfo: " + info +
                      ", 触发改变的集合为: " + value);
        }

        #region Serialized Fields

        [InfoBox("改变集合时触发函数")]
        [OnCollectionChanged("Before", "After")]
        public List<string> list = new List<string> { "str1", "str2", "str3" };

        [OnCollectionChanged("Before", "After")]
        public HashSet<string> Hashset = new HashSet<string> { "str1", "str2", "str3" };

        [OnCollectionChanged("Before", "After")]
        public Stack<int> Stack = new Stack<int>();

        [OnCollectionChanged("Before", "After")]
        public Dictionary<string, string> Dictionary = new Dictionary<string, string>
            { { "key1", "str1" }, { "key2", "str2" }, { "key3", "str3" } };

        #endregion
    }
}
