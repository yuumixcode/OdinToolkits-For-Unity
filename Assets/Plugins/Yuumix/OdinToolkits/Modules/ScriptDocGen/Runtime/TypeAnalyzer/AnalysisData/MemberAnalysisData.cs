using System;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules
{
    [Serializable]
    public class MemberAnalysisData
    {
        public string declaringType;
        public string belongToType;
        public bool isObsolete;
        public MemberTypes memberType;
        public AccessModifierType memberAccessModifierType;
        public string returnType;
        public string name;
        public string fullSignature;

        [TextArea]
        public string fullDeclaration;

        [TextArea]
        public string chineseSummary;

        [TextArea]
        public string englishSummary;

        public string AccessModifier => memberAccessModifierType.ConvertToString();

        public bool IsFromInheritMember() =>
            !string.IsNullOrEmpty(belongToType) && !string.IsNullOrEmpty(declaringType) &&
            !string.Equals(belongToType, declaringType);
    }
}
