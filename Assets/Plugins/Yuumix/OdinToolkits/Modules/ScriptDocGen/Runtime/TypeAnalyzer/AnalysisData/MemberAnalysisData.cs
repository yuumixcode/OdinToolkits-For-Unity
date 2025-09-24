using System;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules
{
    [Serializable]
    public class MemberAnalysisData : IMemberAnalysisData
    {
        public string declaringType;
        public string belongToType;
        public bool isObsolete;
        public MemberTypes memberType;
        public AccessModifierType memberAccessModifierType;
        public string returnType;
        public string name;
        public string partSignature;
        public string fullSignature;

        [TextArea]
        public string fullDeclaration;

        [TextArea]
        public string chineseSummary;

        [TextArea]
        public string englishSummary;

        public string DeclaringType => declaringType;
        public string BelongToType => belongToType;
        public bool IsObsolete => isObsolete;
        public MemberTypes MemberType => memberType;
        public AccessModifierType MemberAccessModifierType => memberAccessModifierType;
        public string ReturnType => returnType;
        public string Name => name;
        public string PartSignature => partSignature;
        public string FullSignature => fullSignature;
        public string FullDeclaration => fullDeclaration;
        public string ChineseSummary => chineseSummary;
        public string EnglishSummary => englishSummary;

        public string AccessModifier => memberAccessModifierType.ConvertToString();

        /// <summary>
        /// 是否由基类声明
        /// </summary>
        public bool IsFromInheritMember() =>
            !string.IsNullOrEmpty(belongToType) && !string.IsNullOrEmpty(declaringType) &&
            !string.Equals(belongToType, declaringType);

        /// <summary>
        /// 是否为 API 成员
        /// </summary>
        public bool IsAPI() => memberAccessModifierType is AccessModifierType.Public or AccessModifierType.Protected
            or AccessModifierType.ProtectedInternal;
    }
}