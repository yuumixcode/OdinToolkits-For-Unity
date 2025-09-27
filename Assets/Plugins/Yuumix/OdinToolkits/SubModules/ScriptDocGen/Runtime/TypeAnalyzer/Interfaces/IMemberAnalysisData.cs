using System;
using System.Reflection;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules
{
    public interface IMemberAnalysisData
    {
        string DeclaringType { get; }
        string BelongToType { get; }
        bool IsObsolete { get; }
        MemberTypes MemberType { get; }
        AccessModifierType MemberAccessModifierType { get; }
        string ReturnType { get; }
        string Name { get; }
        string PartSignature { get; }
        string FullSignature { get; }
        string FullDeclaration { get; }
        string ChineseSummary { get; }
        string EnglishSummary { get; }
        string AccessModifier { get; }
        
        /// <summary>
        /// 是否由基类声明
        /// </summary>
        bool IsFromInheritMember();
        
        /// <summary>
        /// 是否为 API 成员
        /// </summary>
        bool IsAPI();
    }
}