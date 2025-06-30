using Sirenix.OdinInspector;
using System;
using System.Reflection;
using Yuumix.OdinToolkits.Modules.Utilities;

namespace Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Runtime
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

        public bool IsAPI => memberAccessModifierType is AccessModifierType.Public or AccessModifierType.Protected or AccessModifierType.ProtectedInternal;
    }
}
