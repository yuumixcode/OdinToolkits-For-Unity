using System;
using System.Reflection;

using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.QoderTypeAnalyzer
{
    /// <summary>
    /// 成员信息数据接口
    /// </summary>
    public interface IMemberData
    {
        /// <summary>
        /// 成员名称
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// 成员类型名称
        /// </summary>
        string TypeName { get; }
        
        /// <summary>
        /// 成员签名
        /// </summary>
        string Signature { get; }
        
        /// <summary>
        /// 成员完整声明
        /// </summary>
        string Declaration { get; }
        
        /// <summary>
        /// 访问修饰符类型
        /// </summary>
        AccessModifierType AccessModifier { get; }
        
        /// <summary>
        /// 成员类型枚举
        /// </summary>
        MemberTypes MemberType { get; }
        
        /// <summary>
        /// 是否已过时
        /// </summary>
        bool IsObsolete { get; }
        
        /// <summary>
        /// 中文描述
        /// </summary>
        string ChineseSummary { get; }
        
        /// <summary>
        /// 英文描述
        /// </summary>
        string EnglishSummary { get; }
        
        /// <summary>
        /// 特性声明字符串（包含该成员的所有特性）
        /// </summary>
        string AttributesDeclaration { get; }
        
        /// <summary>
        /// 完整声明字符串（包含特性的完整声明）
        /// </summary>
        string FullDeclarationWithAttributes { get; }
        
        /// <summary>
        /// 获取过滤后的特性声明字符串
        /// </summary>
        /// <param name="filter">特性过滤器</param>
        /// <returns>过滤后的特性声明字符串</returns>
        string GetFilteredAttributesDeclaration(IAttributeFilter filter);
        
        /// <summary>
        /// 生成格式化的字符串
        /// </summary>
        /// <returns>格式化的字符串</returns>
        string ToFormattedString();
        
        /// <summary>
        /// 生成详细的描述字符串
        /// </summary>
        /// <returns>详细的描述字符串</returns>
        string ToDetailedString();
        
        /// <summary>
        /// 转换为 UML 图中的一行信息
        /// </summary>
        /// <returns>UML 格式字符串</returns>
        string ToUMLString();
    }
}