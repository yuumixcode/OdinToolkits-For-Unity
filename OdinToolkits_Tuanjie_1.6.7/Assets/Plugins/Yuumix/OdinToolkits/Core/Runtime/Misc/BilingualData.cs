using System;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 双语数据，存放中文和英文两个字段，实现双语特性的核心类
    /// </summary>
    [Serializable]
    public readonly struct BilingualData : IEquatable<BilingualData>
    {
        readonly string _chinese;
        readonly string _english;

        public BilingualData(string chinese, string english)
        {
            _chinese = chinese;
            _english = english;
        }

        public string GetChinese() => _chinese;
        public string GetEnglish() => _english;

        public bool Equals(BilingualData other)
            => _chinese == other._chinese && _english == other._english;

        /// <summary>
        /// 返回当前语言的文本
        /// </summary>
        public string GetCurrentOrFallback()
        {
            if (InspectorBilingualismConfigSO.IsChinese)
            {
                return _chinese;
            }

            if (InspectorBilingualismConfigSO.IsEnglish && !_english.IsNullOrWhiteSpace())
            {
                return _english;
            }

            return _chinese;
        }

        public override string ToString() => GetCurrentOrFallback();

        /// <summary>
        /// 隐式类型转换，BilingualData 可以直接转换为 String
        /// </summary>
        [Summary("隐式类型转换，BilingualData 可以直接转换为 String")]
        public static implicit operator string(BilingualData data) => data.GetCurrentOrFallback();
    }
}
