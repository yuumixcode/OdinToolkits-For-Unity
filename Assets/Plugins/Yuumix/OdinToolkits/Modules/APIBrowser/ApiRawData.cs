using System;
using System.Reflection;
using Yuumix.OdinToolkits.Modules.Utilities.Runtime;

namespace Yuumix.OdinToolkits.Modules.APIBrowser
{
    [Serializable]
    public class ApiRawData
    {
        public Type BelongToType;
        public string FullName => BelongToType.FullName;
        public string TypeName => BelongToType.Name;
        public string AssemblyQualifiedName => BelongToType.AssemblyQualifiedName;
        public string declaringTypeName;
        public MemberTypes memberType;
        public AccessModifierType modifierType;
        public bool isStatic;
        public string rawName;
        public string fullSignature;
        public bool isVirtual;
        public bool isAbstract;
        public bool isObsolete;
        public bool IsPublic => modifierType == AccessModifierType.Public;
        public bool IsPrivate => modifierType == AccessModifierType.Private;
        public bool IsProtected => modifierType == AccessModifierType.Protected;
        public bool IsInternal => modifierType == AccessModifierType.Internal;
        public string eventReturnTypeName;
        public string propertyReturnTypeName;
    }
}
