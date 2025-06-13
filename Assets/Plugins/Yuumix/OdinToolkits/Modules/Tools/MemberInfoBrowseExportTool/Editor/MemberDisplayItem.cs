using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Tools.MemberInfoBrowseExportTool.Editor
{
    [Serializable]
    public class MemberDisplayItem
    {
        [HideInInspector]
        public MemberRawData rawData;

        [ShowInInspector]
        [DisplayAsString]
        [EnableGUI]
        public string memberType;

        [ShowInInspector]
        [DisplayAsString]
        [EnableGUI]
        public string MemberName => rawData.rawName;

        [ShowInInspector]
        [DisplayAsString]
        [EnableGUI]
        public string FullMemberSignature => rawData.fullSignature;

        [ShowInInspector]
        [DisplayAsString]
        [EnableGUI]
        public string DeclaringTypeName => rawData.declaringTypeName;

        [ShowInInspector]
        [DisplayAsString]
        [EnableGUI]
        public bool IsObsolete => rawData.isObsolete;

        public string ChineseComment => rawData.Comment.ChineseComment;
        public string EnglishComment => rawData.Comment.EnglishComment;
    }
}
