using System;
using System.Diagnostics;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Core
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class BilingualBoxGroupAttribute : PropertyGroupAttribute
    {
        public BilingualBoxGroupAttribute(string groupId, string chinese, string english, bool showLabel,
            bool centerLabel = false, float order = 0.0f) : base(groupId, order)
        {
            LanguageData = new BilingualData(chinese, english);
            ShowLabel = showLabel;
            CenterLabel = centerLabel;
        }

        public BilingualBoxGroupAttribute(string groupId, string chinese, string english = null) :
            base(groupId) =>
            LanguageData = new BilingualData(chinese, english);

        public BilingualBoxGroupAttribute() : this("_DefaultMultiLanguageBoxGroup", "Null", "Null", false) { }

        public BilingualData LanguageData { get; set; }
        public bool ShowLabel { get; set; }
        public bool CenterLabel { get; set; }

        /// <summary>
        /// 如果为 <c>true</c>，则表示在使用此特性实例的成员所属的类中，存在相同 <c>Type</c> 的，<c>GroupId</c> 相同的特性实例。
        /// </summary>
        public bool HasCombineValues { get; set; }

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
            HasCombineValues = true;
        }
    }
}
