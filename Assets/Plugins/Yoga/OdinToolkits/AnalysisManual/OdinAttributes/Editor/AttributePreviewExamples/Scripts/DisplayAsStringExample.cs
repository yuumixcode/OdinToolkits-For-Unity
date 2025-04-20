using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class DisplayAsStringExample : ExampleScriptableObject
    {
        [FoldoutGroup("无参数使用")]
        [DisplayAsString]
        public string string1 = "测试部分";

        [FoldoutGroup("fontSize")]
        [DisplayAsString(12)]
        public string string2 = "测试部分";

        [FoldoutGroup("overflow")]
        [DisplayAsString(fontSize: 14, overflow: true)]
        public string string3 = "测试部分非常非常非常非常非常非常非常非常非常非常非常非常非常非常非常非常非常非常长";

        [FoldoutGroup("enableRichText")]
        [DisplayAsString(14, true)]
        public string string4 = "<color = red>" + "测试部分" + "</color>";

        [FoldoutGroup("alignment")]
        [DisplayAsString(alignment: TextAlignment.Center)]
        public string string5 = "测试部分";

        // Format 中的字符串是实际提供的字符串，而不是标记字段的值
        // 被标记的字段，用于格式化字符串，这个类要实现 IFormattable 接口
        // 在这个类的 ToString 方法中对提供的字符串进行个性化处理
        [FoldoutGroup("Format")]
        [DisplayAsString(Format = "自定义格式化字符串 AAA")]
        public SpecialString string6;

        [Serializable]
        public class SpecialString : IFormattable
        {
            public string ToString(string format, IFormatProvider formatProvider)
            {
                var result = format.ToLower();
                return result;
            }
        }
    }
}