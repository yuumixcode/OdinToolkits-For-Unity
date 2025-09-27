using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.QoderTypeAnalyzer
{
    /// <summary>
    /// 成员信息数据抽象基类
    /// </summary>
    [Serializable]
    public abstract class MemberData : IMemberData
    {
        #region 私有字段
        
        /// <summary>
        /// 原始成员信息
        /// </summary>
        public MemberInfo OriginalMemberInfo { get; set; }
        
        #endregion
        
        #region 公共属性
        
        /// <summary>
        /// 成员名称
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 成员类型名称
        /// </summary>
        public string TypeName { get; set; }
        
        /// <summary>
        /// 成员签名
        /// </summary>
        public string Signature { get; set; }
        
        /// <summary>
        /// 成员完整声明
        /// </summary>
        public string Declaration { get; set; }
        
        /// <summary>
        /// 访问修饰符类型
        /// </summary>
        public AccessModifierType AccessModifier { get; set; }
        
        /// <summary>
        /// 成员类型枚举
        /// </summary>
        public MemberTypes MemberType { get; set; }
        
        /// <summary>
        /// 是否已过时
        /// </summary>
        public bool IsObsolete { get; set; }
        
        /// <summary>
        /// 中文描述
        /// </summary>
        public string ChineseSummary { get; set; }
        
        /// <summary>
        /// 英文描述
        /// </summary>
        public string EnglishSummary { get; set; }
        
        /// <summary>
        /// 特性声明字符串
        /// </summary>
        public string AttributesDeclaration { get; set; }
        
        /// <summary>
        /// 完整声明字符串（包含特性）
        /// </summary>
        public string FullDeclarationWithAttributes { get; set; }
        
        #endregion
        
        #region 构造函数
        
        /// <summary>
        /// 构造函数
        /// </summary>
        protected MemberData()
        {
            Name = string.Empty;
            TypeName = string.Empty;
            Signature = string.Empty;
            Declaration = string.Empty;
            ChineseSummary = string.Empty;
            EnglishSummary = string.Empty;
            AttributesDeclaration = string.Empty;
            FullDeclarationWithAttributes = string.Empty;
        }
        
        /// <summary>
        /// 从 MemberInfo 创建数据
        /// </summary>
        /// <param name="memberInfo">成员信息</param>
        protected MemberData(MemberInfo memberInfo) : this()
        {
            if (memberInfo == null)
                throw new ArgumentNullException(nameof(memberInfo));
                
            OriginalMemberInfo = memberInfo;
            InitializeCommonData(memberInfo);
            InitializeAttributes(memberInfo);
            InitializeData();
        }
        
        #endregion
        
        #region 受保护方法
        
        /// <summary>
        /// 初始化通用数据
        /// </summary>
        /// <param name="memberInfo">成员信息</param>
        protected virtual void InitializeCommonData(MemberInfo memberInfo)
        {
            Name = memberInfo.Name;
            MemberType = memberInfo.MemberType;
            IsObsolete = memberInfo.IsDefined(typeof(ObsoleteAttribute), false);
            
            // 获取中文描述
            ChineseSummary = ChineseSummaryAttribute.GetChineseSummary(memberInfo) ?? string.Empty;
            
            // 获取英文描述 - 这里可以根据项目需要实现
            EnglishSummary = GetEnglishSummary(memberInfo);
        }
        
        /// <summary>
        /// 获取英文描述
        /// </summary>
        /// <param name="memberInfo">成员信息</param>
        /// <returns>英文描述</returns>
        protected virtual string GetEnglishSummary(MemberInfo memberInfo)
        {
            // 这里可以根据项目需要实现英文描述的获取逻辑
            // 目前返回空字符串
            return string.Empty;
        }
        
        /// <summary>
        /// 初始化特有类型的数据（由子类实现）
        /// </summary>
        protected abstract void InitializeData();
                
        /// <summary>
        /// 初始化特性信息
        /// </summary>
        /// <param name="memberInfo">成员信息</param>
        protected virtual void InitializeAttributes(MemberInfo memberInfo)
        {
            var attributes = memberInfo.GetCustomAttributes(false);
            AttributesDeclaration = GenerateAttributesDeclaration(attributes);
            FullDeclarationWithAttributes = CombineAttributesWithDeclaration();
        }
                
        /// <summary>
        /// 生成特性声明字符串
        /// </summary>
        /// <param name="attributes">特性数组</param>
        /// <param name="filter">可选的特性过滤器</param>
        /// <returns>特性声明字符串</returns>
        protected virtual string GenerateAttributesDeclaration(object[] attributes, IAttributeFilter filter = null)
        {
            if (attributes == null || attributes.Length == 0)
                return string.Empty;
                        
            var attributeStrings = new List<string>();
                    
            foreach (var attr in attributes)
            {
                if (attr is Attribute attribute)
                {
                    var attributeType = attribute.GetType();
                            
                    // 如果有过滤器，检查是否应该包含
                    if (filter != null && !filter.ShouldInclude(attributeType))
                        continue;
                                
                    var attrString = FormatAttribute(attribute);
                    if (!string.IsNullOrEmpty(attrString))
                        attributeStrings.Add(attrString);
                }
            }
                    
            return attributeStrings.Count > 0 
                ? string.Join("\n", attributeStrings) 
                : string.Empty;
        }
                
        /// <summary>
        /// 格式化单个特性
        /// </summary>
        /// <param name="attribute">特性对象</param>
        /// <returns>格式化的特性字符串</returns>
        protected virtual string FormatAttribute(Attribute attribute)
        {
            if (attribute == null)
                return string.Empty;
                        
            var attributeType = attribute.GetType();
            var typeName = attributeType.Name;
                    
            // 移除 "Attribute" 后缀
            if (typeName.EndsWith("Attribute"))
                typeName = typeName.Substring(0, typeName.Length - 9);
                        
            return $"[{typeName}]";
        }
                
        /// <summary>
        /// 合并特性和声明
        /// </summary>
        /// <returns>完整的声明字符串</returns>
        protected virtual string CombineAttributesWithDeclaration()
        {
            if (string.IsNullOrEmpty(AttributesDeclaration))
                return Declaration;
                        
            return $"{AttributesDeclaration}\n{Declaration}";
        }
        
        #endregion
        
        #region 公共方法
        
        /// <summary>
        /// 生成格式化的字符串
        /// </summary>
        /// <returns>格式化的字符串</returns>
        public virtual string ToFormattedString()
        {
            return $"{GetAccessModifierString()} {TypeName} {Name}";
        }
        
        /// <summary>
        /// 生成详细的描述字符串
        /// </summary>
        /// <returns>详细的描述字符串</returns>
        public virtual string ToDetailedString()
        {
            var result = ToFormattedString();
            
            if (!string.IsNullOrEmpty(ChineseSummary))
                result += $" : {ChineseSummary}";
            else if (!string.IsNullOrEmpty(EnglishSummary))
                result += $" : {EnglishSummary}";
                
            return result;
        }
        
        /// <summary>
        /// 获取访问修饰符字符串
        /// </summary>
        /// <returns>访问修饰符字符串</returns>
        protected virtual string GetAccessModifierString()
        {
            return AccessModifier switch
            {
                AccessModifierType.Public => "public",
                AccessModifierType.Protected => "protected",
                AccessModifierType.Internal => "internal",
                AccessModifierType.Private => "private",
                AccessModifierType.ProtectedInternal => "protected internal",
                AccessModifierType.PrivateProtected => "private protected",
                _ => string.Empty
            };
        }
        
        #endregion
        
        #region 公共方法
        
        /// <summary>
        /// 获取过滤后的特性声明字符串
        /// </summary>
        /// <param name="filter">特性过滤器</param>
        /// <returns>过滤后的特性声明字符串</returns>
        public virtual string GetFilteredAttributesDeclaration(IAttributeFilter filter)
        {
            if (OriginalMemberInfo == null || filter == null)
                return AttributesDeclaration;
                
            var attributes = OriginalMemberInfo.GetCustomAttributes(false);
            return GenerateAttributesDeclaration(attributes, filter);
        }
        
        /// <summary>
        /// 转换为 UML 图中的一行信息（由子类实现）
        /// </summary>
        /// <returns>UML 格式字符串</returns>
        public abstract string ToUMLString();
        
        /// <summary>
        /// 获取 UML 可见性符号
        /// </summary>
        /// <param name="accessModifier">访问修饰符</param>
        /// <returns>UML 可见性符号</returns>
        protected virtual string GetUMLVisibility(AccessModifierType accessModifier)
        {
            return accessModifier switch
            {
                AccessModifierType.Public => "+",
                AccessModifierType.Protected => "#",
                AccessModifierType.Private => "-",
                AccessModifierType.Internal => "~",
                AccessModifierType.ProtectedInternal => "#~",
                AccessModifierType.PrivateProtected => "-#",
                _ => "+"
            };
        }
    }

    public interface IAttributeFilter
    {
        bool ShouldInclude(Type attributeType);
    }
}