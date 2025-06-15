using System;
using System.Reflection;
using Yuumix.OdinToolkits.Modules.Utilities.Runtime;

namespace Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Runtime
{
    [Serializable]
    public class MethodData
    {
        public string belongToType;
        public MemberTypes memberType;
        public AccessModifierType accessModifierType;
        public string declaringType;
        public string accessModifier;
        public string returnType;
        public bool isStatic;
        public bool isObsolete;
        public bool isAbstract;
        public bool isVirtual;
        public bool isOverride;
        public string name;
        public string fullSignature;
        public string chineseComment;
        public string englishComment;

        public static MethodData FromMethodInfo(MethodInfo methodInfo, Type type)
        {
            var methodData = new MethodData
            {
                belongToType = type.GetReadableTypeName(true),
                memberType = methodInfo.MemberType,
                accessModifierType = methodInfo.GetMethodAccessModifierType(),
            };
            methodData.accessModifier = methodData.accessModifierType.GetAccessModifierString();
            return methodData;
        }
    }
}
