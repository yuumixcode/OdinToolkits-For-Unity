using System;

namespace Yuumix.OdinToolkits.Core
{
    [Summary("双语数据结构体，存放中文和英文两个字段")]
    public readonly struct BilingualData : IEquatable<BilingualData>
    {
        [Summary("空的 BilingualData 实例，中文和英文均为空字符串，类似于 string.Empty")]
        public static BilingualData Empty => new BilingualData(string.Empty, string.Empty);

        readonly string _chinese;
        readonly string _english;

        public BilingualData(string chinese, string english)
        {
            _chinese = chinese;
            _english = english;
        }

        public string GetChinese() => _chinese;
        public string GetEnglish() => _english;

        public bool Equals(BilingualData other) => _chinese == other._chinese && _english == other._english;

        [Summary("返回当前编辑器语言的文本或者回退到中文")]
        public string GetCurrentOrFallback()
        {
            if (InspectorBilingualismConfigSO.IsChinese)
            {
                return _chinese;
            }

            if (InspectorBilingualismConfigSO.IsEnglish && !string.IsNullOrEmpty(_english) &&
                !string.IsNullOrWhiteSpace(_english))
            {
                return _english;
            }

            return _chinese;
        }

        public override string ToString() => GetCurrentOrFallback();

        [Summary("隐式类型转换，BilingualData 可以直接转换为 String")]
        public static implicit operator string(BilingualData data) => data.GetCurrentOrFallback();
    }
}
