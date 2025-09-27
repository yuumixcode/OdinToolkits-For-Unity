using System;
using System.Linq;
using System.Reflection;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.QoderTypeAnalyzer
{
    /// <summary>
    /// 类型信息数据类
    /// </summary>
    [Serializable]
    public class TypeData : MemberData
    {
        #region 公共属性
        
        /// <summary>
        /// 类型对象
        /// </summary>
        public Type TargetType { get; set; }
        
        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace { get; set; }
        
        /// <summary>
        /// 是否为泛型类型
        /// </summary>
        public bool IsGeneric { get; set; }
        
        /// <summary>
        /// 是否为抽象类
        /// </summary>
        public bool IsAbstract { get; set; }
        
        /// <summary>
        /// 是否为密封类
        /// </summary>
        public bool IsSealed { get; set; }
        
        /// <summary>
        /// 是否为静态类
        /// </summary>
        public bool IsStatic { get; set; }
        
        /// <summary>
        /// 是否为接口
        /// </summary>
        public bool IsInterface { get; set; }
        
        /// <summary>
        /// 是否为枚举
        /// </summary>
        public bool IsEnum { get; set; }
        
        /// <summary>
        /// 是否为值类型
        /// </summary>
        public bool IsValueType { get; set; }
        
        /// <summary>
        /// 构造函数数据数组（通过 GetConstructors 获取）
        /// </summary>
        public MethodData[] Constructors { get; set; }
        
        /// <summary>
        /// 所有方法数据数组（通过 GetRuntimeMethods 获取，包括继承的、非公共的、实例的和静态的方法）
        /// </summary>
        public MethodData[] AllMethods { get; set; }
        
        /// <summary>
        /// 所有事件数据数组（通过 GetRuntimeEvents 获取，包括继承的、非公共的、实例的和静态的事件）
        /// </summary>
        public EventData[] AllEvents { get; set; }
        
        /// <summary>
        /// 所有字段数据数组（通过 GetRuntimeFields 获取，包括继承的、非公共的、实例的和静态的字段）
        /// </summary>
        public FieldData[] AllFields { get; set; }
        
        /// <summary>
        /// 所有属性数据数组（通过 GetRuntimeProperties 获取，包括继承的、非公共的、实例的和静态的属性）
        /// </summary>
        public PropertyData[] AllProperties { get; set; }
        
        /// <summary>
        /// 继承链（从基类到当前类的类型数组）
        /// </summary>
        public Type[] InheritanceChain { get; set; }
        
        /// <summary>
        /// 实现的接口类型数组
        /// </summary>
        public Type[] ImplementedInterfaces { get; set; }
        
        /// <summary>
        /// 泛型参数
        /// </summary>
        public Type[] GenericArguments { get; set; }
        
        /// <summary>
        /// 类型种类（类、结构体、接口、枚举、委托、记录等）
        /// </summary>
        public TypeCategory TypeCategory { get; set; }
        
        /// <summary>
        /// 程序集名称
        /// </summary>
        public string AssemblyName { get; set; }
        
        /// <summary>
        /// 完整类型声明字符串（包含特性、继承关系等，支持特性过滤）
        /// </summary>
        public string TypeDeclaration { get; set; }
        
        #endregion
        
        #region 构造函数
        
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public TypeData() : base()
        {
            Constructors = new MethodData[0];
            AllMethods = new MethodData[0];
            AllEvents = new EventData[0];
            AllFields = new FieldData[0];
            AllProperties = new PropertyData[0];
            InheritanceChain = new Type[0];
            ImplementedInterfaces = new Type[0];
            GenericArguments = new Type[0];
        }
        
        /// <summary>
        /// 从 Type 创建 TypeData
        /// </summary>
        /// <param name="type">类型</param>
        public TypeData(Type type) : base(type)
        {
            TargetType = type;
        }
        
        #endregion
        
        #region 受保护方法
        
        /// <summary>
        /// 初始化类型特有数据
        /// </summary>
        protected override void InitializeData()
        {
            if (OriginalMemberInfo is Type type)
            {
                TargetType = type;
                Namespace = type.Namespace ?? string.Empty;
                IsGeneric = type.IsGenericType;
                IsAbstract = type.IsAbstract;
                IsSealed = type.IsSealed;
                IsStatic = type.IsAbstract && type.IsSealed && !type.IsInterface;
                IsInterface = type.IsInterface;
                IsEnum = type.IsEnum;
                IsValueType = type.IsValueType;
                
                // 新增的属性
                TypeCategory = type.GetTypeCategory();
                AssemblyName = type.Assembly.GetName().Name;
                TypeDeclaration = type.GetTypeDeclaration();
                
                // 获取泛型参数
                if (IsGeneric)
                {
                    GenericArguments = type.GetGenericArguments();
                }
                else
                {
                    GenericArguments = new Type[0];
                }
                
                // 设置访问修饰符
                AccessModifier = GetTypeAccessModifier(type);
                
                // 设置类型名称
                TypeName = GetFormattedTypeName(type);
                
                // 生成签名
                Signature = GenerateSignature();
                
                // 生成声明
                Declaration = GenerateDeclaration();
                
                // 初始化数组（这些通常由 TypeDataFactory 设置）
                if (Constructors == null) Constructors = new MethodData[0];
                if (AllMethods == null) AllMethods = new MethodData[0];
                if (AllEvents == null) AllEvents = new EventData[0];
                if (AllFields == null) AllFields = new FieldData[0];
                if (AllProperties == null) AllProperties = new PropertyData[0];
                if (InheritanceChain == null) InheritanceChain = new Type[0];
                if (ImplementedInterfaces == null) ImplementedInterfaces = new Type[0];
            }
        }
        
        /// <summary>
        /// 获取类型访问修饰符
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>访问修饰符类型</returns>
        private AccessModifierType GetTypeAccessModifier(Type type)
        {
            if (type.IsPublic)
                return AccessModifierType.Public;
            if (type.IsNotPublic)
                return AccessModifierType.Internal;
            if (type.IsNestedPublic)
                return AccessModifierType.Public;
            if (type.IsNestedPrivate)
                return AccessModifierType.Private;
            if (type.IsNestedFamily)
                return AccessModifierType.Protected;
            if (type.IsNestedAssembly)
                return AccessModifierType.Internal;
            if (type.IsNestedFamORAssem)
                return AccessModifierType.ProtectedInternal;
            if (type.IsNestedFamANDAssem)
                return AccessModifierType.PrivateProtected;
                
            return AccessModifierType.Public;
        }
        
        /// <summary>
        /// 获取格式化的类型名称
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>格式化的类型名称</returns>
        private string GetFormattedTypeName(Type type)
        {
            if (!type.IsGenericType)
                return type.Name;
                
            var genericTypeName = type.Name.Split('`')[0];
            var genericArgs = string.Join(", ", type.GetGenericArguments().Select(t => t.Name));
            return $"{genericTypeName}<{genericArgs}>";
        }
        
        /// <summary>
        /// 生成类型签名
        /// </summary>
        /// <returns>类型签名</returns>
        private string GenerateSignature()
        {
            var result = GetAccessModifierString();
            
            if (IsStatic)
                result += " static";
            else if (IsAbstract && !IsInterface)
                result += " abstract";
            else if (IsSealed && !IsEnum)
                result += " sealed";
                
            if (IsInterface)
                result += " interface";
            else if (IsEnum)
                result += " enum";
            else
                result += " class";
                
            result += $" {TypeName}";
            
            return result.Trim();
        }
        
        /// <summary>
        /// 生成类型声明
        /// </summary>
        /// <returns>类型声明</returns>
        private string GenerateDeclaration()
        {
            var result = GenerateSignature();
            
            // 添加继承信息
            var baseTypes = new System.Collections.Generic.List<string>();
            
            if (TargetType.BaseType != null && TargetType.BaseType != typeof(object))
            {
                baseTypes.Add(TargetType.BaseType.Name);
            }
            
            var interfaces = TargetType.GetInterfaces();
            foreach (var iface in interfaces)
            {
                baseTypes.Add(iface.Name);
            }
            
            if (baseTypes.Count > 0)
            {
                result += " : " + string.Join(", ", baseTypes);
            }
            
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
            
            // 添加命名空间信息
            if (!string.IsNullOrEmpty(Namespace))
                result = $"{Namespace}.{result}";
                
            // 添加描述
            if (!string.IsNullOrEmpty(ChineseSummary))
                result += $" : {ChineseSummary}";
            else if (!string.IsNullOrEmpty(EnglishSummary))
                result += $" : {EnglishSummary}";
                
            return result;
        }
        
        /// <summary>
        /// 转换为 UML 类图格式
        /// </summary>
        /// <returns>UML 类图字符串</returns>
        public string ToUMLClassDiagram()
        {
            var result = ToUMLClassDeclaration();
            result += "\n" + new string('-', Math.Max(10, TypeName.Length));
            
            // 添加字段
            foreach (var field in AllFields.Where(f => !MemberGenerationDetector.IsAutoGeneratedField((FieldInfo)f.OriginalMemberInfo)))
            {
                result += "\n" + field.ToUMLString();
            }
            
            // 添加属性
            foreach (var property in AllProperties.Where(p => !MemberGenerationDetector.IsAutoGeneratedProperty((PropertyInfo)p.OriginalMemberInfo)))
            {
                result += "\n" + property.ToUMLString();
            }
            
            result += "\n" + new string('-', Math.Max(10, TypeName.Length));
            
            // 添加方法
            foreach (var method in AllMethods.Where(m => !MemberGenerationDetector.IsAutoGeneratedMethod((MethodInfo)m.OriginalMemberInfo)))
            {
                result += "\n" + method.ToUMLString();
            }
            
            return result;
        }
        
        /// <summary>
        /// 获取 UML 类声明行
        /// </summary>
        /// <returns>类声明的 UML 格式</returns>
        public string ToUMLClassDeclaration()
        {
            var stereotype = "";
            if (IsInterface)
                stereotype = "<<interface>>";
            else if (IsAbstract)
                stereotype = "<<abstract>>";
            else if (IsEnum)
                stereotype = "<<enumeration>>";
                
            return string.IsNullOrEmpty(stereotype) ? TypeName : $"{stereotype}\n{TypeName}";
        }
        
        /// <summary>
        /// 转换为 UML 字符串（类型名称）
        /// </summary>
        /// <returns>UML 格式字符串</returns>
        public override string ToUMLString()
        {
            return ToUMLClassDeclaration();
        }
        
        #endregion
    }
}