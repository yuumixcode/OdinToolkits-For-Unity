using System.Reflection;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.AdvancedTypeAnalyzer
{
    /// <summary>
    /// 派生成员数据接口，不同的派生类有不同的表现形式，MemberData 无法直接准确获取的信息，需要派生类自己实现
    /// </summary>
    public interface IDerivedMemberData
    {
        /// <summary>
        /// 是否为静态
        /// </summary>
        public bool IsStatic { get; }

        /// <summary>
        /// 成员类别
        /// </summary>
        MemberTypes MemberType { get; }

        /// <summary>
        /// MemberType 类型的字符串表示形式
        /// </summary>
        string MemberTypeName { get; }

        /// <summary>
        /// 访问修饰符类型，表示成员的访问级别（public、private、protected等）
        /// </summary>
        AccessModifierType AccessModifier { get; }

        /// <summary>
        /// 访问修饰符的字符串表示形式
        /// </summary>
        string AccessModifierName { get; }

        /// <summary>
        /// 成员签名字符串，包含访问修饰符、返回类型和名称以及参数，不包含特性
        /// </summary>
        string Signature { get; }

        /// <summary>
        /// 包含特性的完整声明字符串
        /// </summary>
        string FullDeclarationWithAttributes { get; }

        /// <summary>
        /// 生成包含特性的完整声明字符串
        /// </summary>
        /// <param name="attributesDeclaration">特性声明字符串</param>
        /// <param name="signature">字段签名字符串</param>
        string GetFullDeclarationWithAttributes(string attributesDeclaration, string signature);
    }
}
