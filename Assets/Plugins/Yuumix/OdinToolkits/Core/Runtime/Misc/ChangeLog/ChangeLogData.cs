using System;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Core
{
    [Serializable]
    public class ChangeLogData
    {
        ChangeLogCategory _category;
        string _chineseLog;
        string _englishLog;

        public ChangeLogData(ChangeLogCategory category, string chineseLog, string englishLog = null)
        {
            _category = category;
            _chineseLog = chineseLog;
            _englishLog = englishLog;
        }

        string Prefix
        {
            get
            {
                switch (_category)
                {
                    case ChangeLogCategory.Added:
                        return ChangePrefix.Added;
                    case ChangeLogCategory.Changed:
                        return ChangePrefix.Changed;
                    case ChangeLogCategory.Deprecated:
                        return ChangePrefix.Deprecated;
                    case ChangeLogCategory.Removed:
                        return ChangePrefix.Removed;
                    case ChangeLogCategory.Fixed:
                        return ChangePrefix.Fixed;
                    case ChangeLogCategory.Security:
                        return ChangePrefix.Security;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        [ShowIf("IsShowChinese")]
        [DisplayAsString(EnableRichText = true, Overflow = false, FontSize = 13)]
        [ShowInInspector]
        [EnableGUI]
        [HideLabel]
        public string Chinese => $"{Prefix} {_chineseLog}";

        [ShowIf("IsShowEnglish")]
        [DisplayAsString(EnableRichText = true, Overflow = false, FontSize = 13)]
        [ShowInInspector]
        [EnableGUI]
        [HideLabel]
        public string English => $"{Prefix} {_englishLog}";

        bool IsShowChinese() => InspectorBilingualismConfigSO.IsChinese ||
                                (InspectorBilingualismConfigSO.IsEnglish && _englishLog == null);

        bool IsShowEnglish() => InspectorBilingualismConfigSO.IsEnglish && _englishLog != null;
    }
}
