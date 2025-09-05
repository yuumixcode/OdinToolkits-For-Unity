using System;

namespace Yuumix.OdinToolkits.Core.Runtime
{
    [BilingualComment("双语数据结构体", "Bilingual String Data Structure")]
    [Serializable]
    public readonly struct BilingualData : IEquatable<BilingualData>
    {
        readonly string _chinese;
        readonly string _english;

        public BilingualData(string chinese, string english = null)
        {
            _chinese = chinese;
            _english = english ?? string.Empty;
        }

        public string GetCurrentOrFallback()
        {
            if (BilingualSetting.IsChinese)
            {
                return _chinese;
            }

            if (BilingualSetting.IsEnglish && !_english.IsNullOrWhiteSpace())
            {
                return _english;
            }

            return _chinese;
        }

        public string GetChinese() => _chinese;
        public string GetEnglish() => _english;
        public override string ToString() => GetCurrentOrFallback();
        public bool Equals(BilingualData other) => _chinese == other._chinese && _english == other._english;
        public override bool Equals(object obj) => obj is BilingualData other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(_chinese, _english);

        /// <summary>
        /// 隐式类型转换，BilingualData -> String
        /// </summary>
        public static implicit operator string(BilingualData data) => data.GetCurrentOrFallback();
    }
}
