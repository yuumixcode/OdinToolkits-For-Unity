using System;
using System.Diagnostics;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Core.Runtime
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class BilingualBoxGroupAttribute : PropertyGroupAttribute
    {
        public BilingualData LanguageData;
        public bool ShowLabel { get; private set; }
        public bool CenterLabel { get; private set; }
        bool _hasCombineValues;

        public BilingualBoxGroupAttribute(string groupId,
            string chinese, string english = null,
            bool showLabel = true,
            bool centerLabel = false,
            float order = 0.0f) : base(groupId, order)
        {
            LanguageData = new BilingualData(chinese, english);
            ShowLabel = showLabel;
            CenterLabel = centerLabel;
        }

        public BilingualBoxGroupAttribute(string groupId, string chinese, string english = null) : base(groupId) =>
            LanguageData = new BilingualData(chinese, english);

        public BilingualBoxGroupAttribute()
            : this("_DefaultMultiLanguageBoxGroup", "Null", showLabel: false) { }

        /// <summary>
        /// 如果为 <c>true</c>，则表示在使用此特性实例的成员所属的类中，存在相同 <c>Type</c> 的，<c>GroupId</c> 相同的特性实例。
        /// </summary>
        public bool GetHasCombineValues() => _hasCombineValues;

        protected override void CombineValuesWith(PropertyGroupAttribute other)
        {
            if (other is not BilingualBoxGroupAttribute multiLanguageBoxGroupAttribute)
            {
                return;
            }

            if (!ShowLabel || !multiLanguageBoxGroupAttribute.ShowLabel)
            {
                ShowLabel = false;
                multiLanguageBoxGroupAttribute.ShowLabel = false;
            }

            CenterLabel |= multiLanguageBoxGroupAttribute.CenterLabel;
            _hasCombineValues = true;
        }
    }
}
