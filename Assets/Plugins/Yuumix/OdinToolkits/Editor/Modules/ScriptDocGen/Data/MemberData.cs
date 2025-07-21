using System;
using System.Reflection;
using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Shared;

namespace Yuumix.OdinToolkits.Editor
{
    [Serializable]
    public abstract class MemberData
    {
        public string belongToType;
        public MemberTypes memberType;
        public AccessModifierType memberAccessModifierType;
        public string declaringType;
        public string accessModifier;
        public string returnType;
        public bool isStatic;
        public bool isObsolete;
        public string name;
        public string fullSignature;
        public string chineseComment;
        public string englishComment;

        [ShowInInspector] public bool NoFromInherit => belongToType == declaringType;

        [ShowInInspector]
        public bool IsAPI => memberAccessModifierType is AccessModifierType.Public or AccessModifierType.Protected
            or AccessModifierType.ProtectedInternal;
    }
}
