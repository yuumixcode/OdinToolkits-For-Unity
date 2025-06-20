using System;
using System.Reflection;
using Yuumix.OdinToolkits.Common.InspectorLocalization;
using Yuumix.OdinToolkits.Modules.Utilities.Runtime;

namespace Yuumix.OdinToolkits.Modules.Tools.MemberInfoBrowseExportTool.Editor
{
    [Serializable]
    public class MemberRawData
    {
        #region Common

        // === Data 所属类型
        public Type BelongToType;

        // === 声明此成员的 Type 类名
        public string declaringTypeName;
        public MemberTypes memberType;
        public AccessModifierType modifierType;
        public string rawName;
        public string fullSignature;
        public bool isStatic;
        public bool isVirtual;
        public bool isAbstract;
        public bool isObsolete;
        public bool IsPublic => modifierType == AccessModifierType.Public;
        public bool IsPrivate => modifierType == AccessModifierType.Private;
        public bool IsProtected => modifierType == AccessModifierType.Protected;
        public bool IsInternal => modifierType == AccessModifierType.Internal;

        #endregion

        public string eventReturnTypeName;
        public string propertyReturnTypeName;
        public LocalizedCommentAttribute Comment = new LocalizedCommentAttribute("无", "null");
    }
}
