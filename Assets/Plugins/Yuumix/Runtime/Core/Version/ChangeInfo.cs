using System;

namespace Yuumix.OdinToolkits.Core.Version
{
    [Serializable]
    public class ChangeInfo
    {
        public string Time { get; }
        public string Version1 { get; }
        public string Author { get; }
        public string[] AddedInfo { get; }
        public string[] ChangedInfo { get; }
        public string[] DeprecatedInfo { get; }
        public string[] RemoveInfo { get; }
        public string[] FixInfo { get; }
        public string[] SecurityInfo { get; }

        public ChangeInfo(string time, string version, string author, string[] addedInfo, string[] changedInfo,
            string[] deprecatedInfo, string[] removeInfo, string[] fixInfo, string[] securityInfo)
        {
            Time = time;
            Version1 = version;
            Author = author;
            AddedInfo = addedInfo;
            ChangedInfo = changedInfo;
            DeprecatedInfo = deprecatedInfo;
            RemoveInfo = removeInfo;
            FixInfo = fixInfo;
            SecurityInfo = securityInfo;
        }
    }
}
