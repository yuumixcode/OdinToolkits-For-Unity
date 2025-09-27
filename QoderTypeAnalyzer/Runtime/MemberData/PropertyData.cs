using System;
using System.Linq;
using System.Reflection;

using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.QoderTypeAnalyzer
{
    /// <summary>
    /// 属性信息数据类
    /// </summary>
    [Serializable]
    public class PropertyData : MemberData
    {
        #region 公共属性
        
        /// <summary>
        /// 是否有 getter
        /// </summary>
        public bool HasGetter { get; set; }
        
        /// <summary>
        /// 是否有 setter
        /// </summary>
        public bool HasSetter { get; set; }
        
        /// <summary>
        /// 是否为静态属性
        /// </summary>
        public bool IsStatic { get; set; }
        
        /// <summary>
        /// 是否为索引器
        /// </summary>
        public bool IsIndexer { get; set; }
        
        /// <summary>
        /// 属性类型
        /// </summary>
        public Type PropertyType { get; set; }
        
        /// <summary>
        /// 索引器参数（如果是索引器）
        /// </summary>
        public ParameterData[] IndexerParameters { get; set; }
        
        /// <summary>
        /// Getter 访问修饰符
        /// </summary>
        public AccessModifierType GetterAccessModifier { get; set; }
        
        /// <summary>
        /// Setter 访问修饰符
        /// </summary>
        public AccessModifierType SetterAccessModifier { get; set; }
        
        #endregion
        
        #region 构造函数
        
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PropertyData() : base()
        {
            IndexerParameters = new ParameterData[0];
        }
        
        /// <summary>
        /// 从 PropertyInfo 创建 PropertyData
        /// </summary>
        /// <param name="propertyInfo">属性信息</param>
        public PropertyData(PropertyInfo propertyInfo) : base(propertyInfo)
        {
        }
        
        #endregion
        
        #region 受保护方法
        
        /// <summary>
        /// 初始化属性特有数据
        /// </summary>
        protected override void InitializeData()
        {
            if (OriginalMemberInfo is PropertyInfo propertyInfo)
            {
                PropertyType = propertyInfo.PropertyType;
                
                // 检查 getter 和 setter
                var getMethod = propertyInfo.GetGetMethod(true);
                var setMethod = propertyInfo.GetSetMethod(true);
                
                HasGetter = getMethod != null;
                HasSetter = setMethod != null;
                
                // 检查是否为静态属性
                IsStatic = (getMethod?.IsStatic ?? setMethod?.IsStatic) == true;
                
                // 检查是否为索引器
                var indexParameters = propertyInfo.GetIndexParameters();
                IsIndexer = indexParameters.Length > 0;
                
                // 如果是索引器，获取参数信息
                if (IsIndexer)
                {
                    IndexerParameters = indexParameters
                        .Select(ParameterData.FromParameterInfo)
                        .ToArray();
                }
                else
                {
                    IndexerParameters = new ParameterData[0];
                }
                
                // 获取访问修饰符
                GetterAccessModifier = getMethod != null ? GetMethodAccessModifier(getMethod) : AccessModifierType.None;
                SetterAccessModifier = setMethod != null ? GetMethodAccessModifier(setMethod) : AccessModifierType.None;
                
                // 设置主要访问修饰符（取较严格的一个）
                AccessModifier = GetPropertyAccessModifier(GetterAccessModifier, SetterAccessModifier);
                
                // 设置类型名称
                TypeName = PropertyType?.Name ?? "object";
                
                // 生成签名
                Signature = GenerateSignature();
                
                // 生成声明
                Declaration = GenerateDeclaration();
            }
        }
        
        /// <summary>
        /// 获取方法访问修饰符
        /// </summary>
        /// <param name="method">方法信息</param>
        /// <returns>访问修饰符类型</returns>
        private AccessModifierType GetMethodAccessModifier(MethodInfo method)
        {
            if (method.IsPublic)
                return AccessModifierType.Public;
            if (method.IsPrivate)
                return AccessModifierType.Private;
            if (method.IsFamily)
                return AccessModifierType.Protected;
            if (method.IsAssembly)
                return AccessModifierType.Internal;
            if (method.IsFamilyOrAssembly)
                return AccessModifierType.ProtectedInternal;
            if (method.IsFamilyAndAssembly)
                return AccessModifierType.PrivateProtected;
                
            return AccessModifierType.None;
        }
        
        /// <summary>
        /// 获取属性访问修饰符
        /// </summary>
        /// <param name="getterAccess">Getter 访问修饰符</param>
        /// <param name="setterAccess">Setter 访问修饰符</param>
        /// <returns>属性访问修饰符</returns>
        private AccessModifierType GetPropertyAccessModifier(AccessModifierType getterAccess, AccessModifierType setterAccess)
        {
            if (getterAccess == AccessModifierType.None && setterAccess == AccessModifierType.None)
                return AccessModifierType.None;
                
            if (getterAccess == AccessModifierType.None)
                return setterAccess;
                
            if (setterAccess == AccessModifierType.None)
                return getterAccess;
                
            // 返回较严格的访问级别
            return (int)getterAccess <= (int)setterAccess ? getterAccess : setterAccess;
        }
        
        /// <summary>
        /// 生成属性签名
        /// </summary>
        /// <returns>属性签名</returns>
        private string GenerateSignature()
        {
            var result = GetAccessModifierString();
            
            if (IsStatic)
                result += " static";
                
            result += $" {TypeName}";
            
            // 处理索引器
            if (IsIndexer)
            {
                result += " this[";
                result += string.Join(", ", IndexerParameters.Select(p => p.ToFormattedString()));
                result += "]";
            }
            else
            {
                result += $" {Name}";
            }
            
            return result.Trim();
        }
        
        /// <summary>
        /// 生成属性声明
        /// </summary>
        /// <returns>属性声明</returns>
        private string GenerateDeclaration()
        {
            var result = GenerateSignature();
            
            result += " { ";
            
            // 添加 getter
            if (HasGetter)
            {
                if (GetterAccessModifier != AccessModifier && GetterAccessModifier != AccessModifierType.None)
                    result += GetAccessModifierString(GetterAccessModifier) + " ";
                result += "get; ";
            }
            
            // 添加 setter
            if (HasSetter)
            {
                if (SetterAccessModifier != AccessModifier && SetterAccessModifier != AccessModifierType.None)
                    result += GetAccessModifierString(SetterAccessModifier) + " ";
                result += "set; ";
            }
            
            result += "}";
            
            return result;
        }
        
        /// <summary>
        /// 获取指定访问修饰符的字符串
        /// </summary>
        /// <param name="accessModifier">访问修饰符</param>
        /// <returns>访问修饰符字符串</returns>
        private string GetAccessModifierString(AccessModifierType accessModifier)
        {
            return accessModifier switch
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
        /// 生成格式化的字符串
        /// </summary>
        /// <returns>格式化的字符串</returns>
        public override string ToFormattedString()
        {
            return GenerateSignature() + " { " +
                   (HasGetter ? "get; " : "") +
                   (HasSetter ? "set; " : "") +
                   "}";
        }
        
        /// <summary>
        /// 生成详细的描述字符串
        /// </summary>
        /// <returns>详细的描述字符串</returns>
        public override string ToDetailedString()
        {
            var result = ToFormattedString();
            
            // 添加描述
            if (!string.IsNullOrEmpty(ChineseSummary))
                result += $" : {ChineseSummary}";
            else if (!string.IsNullOrEmpty(EnglishSummary))
                result += $" : {EnglishSummary}";
                
            return result;
        }
        
        /// <summary>
        /// 转换为 UML 格式：[可见性][/静态][名称]:[类型] {get/set}
        /// 示例：+ PropertyName: string {get; set;}
        ///      + ReadOnlyProp: int {get;}
        /// </summary>
        /// <returns>UML 格式的属性表示</returns>
        public override string ToUMLString()
        {
            var visibility = GetUMLVisibility(AccessModifier);
            var staticModifier = IsStatic ? "/" : "";
            var accessors = GetUMLAccessors();
            
            if (IsIndexer)
            {
                var indexerParams = string.Join(", ", IndexerParameters.Select(p => p.ToUMLString()));
                return $"{visibility}{staticModifier}this[{indexerParams}]: {TypeName} {accessors}";
            }
            else
            {
                return $"{visibility}{staticModifier}{Name}: {TypeName} {accessors}";
            }
        }
        
        /// <summary>
        /// 获取 UML 访问器格式
        /// </summary>
        /// <returns>UML 访问器字符串</returns>
        private string GetUMLAccessors()
        {
            var accessors = new System.Collections.Generic.List<string>();
            
            if (HasGetter)
                accessors.Add("get;");
            if (HasSetter)
                accessors.Add("set;");
                
            return accessors.Count > 0 ? $"{{ {string.Join(" ", accessors)} }}" : "";
        }
        
        #endregion
    }
}