using System;
using System.Reflection;

using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.QoderTypeAnalyzer
{
    /// <summary>
    /// 字段信息数据类
    /// </summary>
    [Serializable]
    public class FieldData : MemberData
    {
        #region 公共属性
        
        /// <summary>
        /// 是否为只读字段
        /// </summary>
        public bool IsReadOnly { get; set; }
        
        /// <summary>
        /// 是否为静态字段
        /// </summary>
        public bool IsStatic { get; set; }
        
        /// <summary>
        /// 是否为常量字段
        /// </summary>
        public bool IsConstant { get; set; }
        
        /// <summary>
        /// 常量值（如果是常量）
        /// </summary>
        public object ConstantValue { get; set; }
        
        /// <summary>
        /// 字段类型
        /// </summary>
        public Type FieldType { get; set; }
        
        #endregion
        
        #region 构造函数
        
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FieldData() : base()
        {
        }
        
        /// <summary>
        /// 从 FieldInfo 创建 FieldData
        /// </summary>
        /// <param name="fieldInfo">字段信息</param>
        public FieldData(FieldInfo fieldInfo) : base(fieldInfo)
        {
        }
        
        #endregion
        
        #region 受保护方法
        
        /// <summary>
        /// 初始化字段特有数据
        /// </summary>
        protected override void InitializeData()
        {
            if (OriginalMemberInfo is FieldInfo fieldInfo)
            {
                IsReadOnly = fieldInfo.IsInitOnly;
                IsStatic = fieldInfo.IsStatic;
                IsConstant = fieldInfo.IsLiteral;
                FieldType = fieldInfo.FieldType;
                
                // 获取常量值
                if (IsConstant)
                {
                    try
                    {
                        ConstantValue = fieldInfo.GetRawConstantValue();
                    }
                    catch
                    {
                        ConstantValue = null;
                    }
                }
                
                // 设置访问修饰符
                AccessModifier = GetFieldAccessModifier(fieldInfo);
                
                // 设置类型名称
                TypeName = FieldType?.Name ?? "object";
                
                // 生成签名
                Signature = GenerateSignature();
                
                // 生成声明
                Declaration = GenerateDeclaration();
            }
        }
        
        /// <summary>
        /// 获取字段访问修饰符
        /// </summary>
        /// <param name="fieldInfo">字段信息</param>
        /// <returns>访问修饰符类型</returns>
        private AccessModifierType GetFieldAccessModifier(FieldInfo fieldInfo)
        {
            if (fieldInfo.IsPublic)
                return AccessModifierType.Public;
            if (fieldInfo.IsPrivate)
                return AccessModifierType.Private;
            if (fieldInfo.IsFamily)
                return AccessModifierType.Protected;
            if (fieldInfo.IsAssembly)
                return AccessModifierType.Internal;
            if (fieldInfo.IsFamilyOrAssembly)
                return AccessModifierType.ProtectedInternal;
            if (fieldInfo.IsFamilyAndAssembly)
                return AccessModifierType.PrivateProtected;
                
            return AccessModifierType.None;
        }
        
        /// <summary>
        /// 生成字段签名
        /// </summary>
        /// <returns>字段签名</returns>
        private string GenerateSignature()
        {
            var result = GetAccessModifierString();
            
            if (IsStatic)
                result += " static";
                
            if (IsConstant)
                result += " const";
            else if (IsReadOnly)
                result += " readonly";
                
            result += $" {TypeName} {Name}";
            
            return result.Trim();
        }
        
        /// <summary>
        /// 生成字段声明
        /// </summary>
        /// <returns>字段声明</returns>
        private string GenerateDeclaration()
        {
            var result = GenerateSignature();
            
            // 如果是常量，添加常量值
            if (IsConstant && ConstantValue != null)
            {
                result += " = ";
                if (ConstantValue is string)
                    result += $"\"{ConstantValue}\"";
                else
                    result += ConstantValue.ToString();
            }
            
            result += ";";
            
            return result;
        }
        
        #endregion
        
        #region 公共方法
        
        /// <summary>
        /// 生成格式化的字符串
        /// </summary>
        /// <returns>格式化的字符串</returns>
        public override string ToFormattedString()
        {
            return GenerateSignature();
        }
        
        /// <summary>
        /// 生成详细的描述字符串
        /// </summary>
        /// <returns>详细的描述字符串</returns>
        public override string ToDetailedString()
        {
            var result = ToFormattedString();
            
            // 添加常量值信息
            if (IsConstant && ConstantValue != null)
            {
                result += $" = {ConstantValue}";
            }
            
            // 添加描述
            if (!string.IsNullOrEmpty(ChineseSummary))
                result += $" : {ChineseSummary}";
            else if (!string.IsNullOrEmpty(EnglishSummary))
                result += $" : {EnglishSummary}";
                
            return result;
        }
        
        /// <summary>
        /// 转换为 UML 格式：[可见性][/静态][名称]:[类型]
        /// 示例：+ publicField: string
        ///      - _privateField: int
        ///      # protectedField: bool
        ///      ~ internalField: object
        /// </summary>
        /// <returns>UML 格式的字段表示</returns>
        public override string ToUMLString()
        {
            var visibility = GetUMLVisibility(AccessModifier);
            var staticModifier = IsStatic ? "/" : "";
            var constModifier = IsConstant ? " {readOnly}" : (IsReadOnly ? " {readOnly}" : "");
            
            return $"{visibility}{staticModifier}{Name}: {TypeName}{constModifier}";
        }
        
        #endregion
    }
}