using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class OnCollectionChangedExample : ExampleOdinScriptableObject
    {
        [InfoBox("改变集合时触发函数")] [OnCollectionChanged("Before", "After")]
        public List<string> list = new() { "str1", "str2", "str3" };

        [OnCollectionChanged("Before", "After")]
        public HashSet<string> Hashset = new() { "str1", "str2", "str3" };

        [OnCollectionChanged("Before", "After")]
        public Stack<int> Stack = new();

        [OnCollectionChanged("Before", "After")]
        public Dictionary<string, string> Dictionary = new()
            { { "key1", "str1" }, { "key2", "str2" }, { "key3", "str3" } };

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
    }
}