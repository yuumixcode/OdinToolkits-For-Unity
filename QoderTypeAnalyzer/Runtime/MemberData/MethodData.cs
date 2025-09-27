using System;
using System.Linq;
using System.Reflection;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.QoderTypeAnalyzer
{
    /// <summary>
    /// 方法信息数据类
    /// </summary>
    [Serializable]
    public class MethodData : MemberData
    {
        #region 公共属性
        
        /// <summary>
        /// 是否为静态方法
        /// </summary>
        public bool IsStatic { get; set; }
        
        /// <summary>
        /// 是否为虚方法
        /// </summary>
        public bool IsVirtual { get; set; }
        
        /// <summary>
        /// 是否为重写方法
        /// </summary>
        public bool IsOverride { get; set; }
        
        /// <summary>
        /// 是否为抽象方法
        /// </summary>
        public bool IsAbstract { get; set; }
        
        /// <summary>
        /// 返回类型
        /// </summary>
        public Type ReturnType { get; set; }
        
        /// <summary>
        /// 方法参数
        /// </summary>
        public ParameterData[] Parameters { get; set; }
        
        /// <summary>
        /// 泛型参数
        /// </summary>
        public Type[] GenericArguments { get; set; }
        
        /// <summary>
        /// 是否为泛型方法
        /// </summary>
        public bool IsGeneric { get; set; }
        
        /// <summary>
        /// 是否为扩展方法
        /// </summary>
        public bool IsExtensionMethod { get; set; }
        
        /// <summary>
        /// 是否为特殊名称方法（运算符、转换方法等，经过特殊处理）
        /// </summary>
        public bool IsSpecialName { get; set; }
        
        /// <summary>
        /// 是否为接口实现方法（不应该显示 override 关键字）
        /// </summary>
        public bool IsInterfaceImplementation { get; set; }
        
        /// <summary>
        /// 方法的特殊标识（[Extension]、[Operator]、[Conversion] 之一，或为空）
        /// </summary>
        public string SpecialTag { get; set; }
        
        #endregion
        
        #region 构造函数
        
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MethodData() : base()
        {
            Parameters = new ParameterData[0];
            GenericArguments = new Type[0];
        }
        
        /// <summary>
        /// 从 MethodInfo 创建 MethodData
        /// </summary>
        /// <param name="methodInfo">方法信息</param>
        public MethodData(MethodInfo methodInfo) : base(methodInfo)
        {
        }
        
        /// <summary>
        /// 从 ConstructorInfo 创建 MethodData
        /// </summary>
        /// <param name="constructorInfo">构造函数信息</param>
        public MethodData(ConstructorInfo constructorInfo) : base(constructorInfo)
        {
        }
        
        #endregion
        
        #region 受保护方法
        
        /// <summary>
        /// 初始化方法特有数据
        /// </summary>
        protected override void InitializeData()
        {
            if (OriginalMemberInfo is MethodInfo methodInfo)
            {
                InitializeFromMethodInfo(methodInfo);
            }
            else if (OriginalMemberInfo is ConstructorInfo constructorInfo)
            {
                InitializeFromConstructorInfo(constructorInfo);
            }
        }
        
        /// <summary>
        /// 从 MethodInfo 初始化数据
        /// </summary>
        /// <param name="methodInfo">方法信息</param>
        private void InitializeFromMethodInfo(MethodInfo methodInfo)
        {
                IsStatic = methodInfo.IsStatic;
                IsVirtual = methodInfo.IsVirtual && !methodInfo.IsAbstract;
                IsOverride = methodInfo.GetBaseDefinition() != methodInfo;
                IsAbstract = methodInfo.IsAbstract;
                ReturnType = methodInfo.ReturnType;
                IsGeneric = methodInfo.IsGenericMethod;
                
                // 获取泛型参数
                if (IsGeneric)
                {
                    GenericArguments = methodInfo.GetGenericArguments();
                }
                else
                {
                    GenericArguments = new Type[0];
                }
                
                // 获取方法参数
                var parameters = methodInfo.GetParameters();
                Parameters = parameters.Select(ParameterData.FromParameterInfo).ToArray();
                
                // 检查是否为扩展方法
                IsExtensionMethod = methodInfo.IsDefined(typeof(System.Runtime.CompilerServices.ExtensionAttribute), false);
                
                // 检查是否为特殊名称方法（运算符、转换方法等）
                IsSpecialName = methodInfo.IsSpecialName;
                
                // 检查是否为接口实现方法
                IsInterfaceImplementation = IsInterfaceImplementationMethod(methodInfo);
                
                // 设置特殊标识（按优先级检查）
                SpecialTag = GetSpecialTag(methodInfo);
                
                // 设置访问修饰符
                AccessModifier = GetMethodAccessModifier(methodInfo);
                
                // 设置类型名称（返回类型）
                TypeName = ReturnType?.Name ?? "void";
                
                // 生成签名
                Signature = GenerateSignature();
                
                // 生成声明
                Declaration = GenerateDeclaration();
        }
        
        /// <summary>
        /// 从 ConstructorInfo 初始化数据
        /// </summary>
        /// <param name="constructorInfo">构造函数信息</param>
        private void InitializeFromConstructorInfo(ConstructorInfo constructorInfo)
        {
            IsStatic = constructorInfo.IsStatic;
            IsVirtual = false; // 构造函数不能是虚的
            IsOverride = false; // 构造函数不能被重写
            IsAbstract = false; // 构造函数不能是抽象的
            ReturnType = constructorInfo.DeclaringType; // 构造函数的返回类型是声明类型
            IsGeneric = false; // 构造函数不能是泛型的
            
            GenericArguments = new Type[0];
            
            // 获取方法参数
            var parameters = constructorInfo.GetParameters();
            Parameters = parameters.Select(ParameterData.FromParameterInfo).ToArray();
            
            IsExtensionMethod = false; // 构造函数不能是扩展方法
            
            // 检查是否为特殊名称方法（构造函数通常不是特殊名称）
            IsSpecialName = constructorInfo.IsSpecialName;
            
            // 构造函数不是接口实现
            IsInterfaceImplementation = false;
            
            // 构造函数不是特殊方法
            SpecialTag = null;
            
            // 设置访问修饰符
            AccessModifier = GetConstructorAccessModifier(constructorInfo);
            
            // 设置类型名称（构造函数没有返回类型）
            TypeName = "void";
            
            // 生成签名
            Signature = GenerateConstructorSignature();
            
            // 生成声明
            Declaration = GenerateConstructorDeclaration();
            }
        
        
        /// <summary>
        /// 获取构造函数访问修饰符
        /// </summary>
        /// <param name="constructorInfo">构造函数信息</param>
        /// <returns>访问修饰符类型</returns>
        private AccessModifierType GetConstructorAccessModifier(ConstructorInfo constructorInfo)
        {
            if (constructorInfo.IsPublic)
                return AccessModifierType.Public;
            if (constructorInfo.IsPrivate)
                return AccessModifierType.Private;
            if (constructorInfo.IsFamily)
                return AccessModifierType.Protected;
            if (constructorInfo.IsAssembly)
                return AccessModifierType.Internal;
            if (constructorInfo.IsFamilyOrAssembly)
                return AccessModifierType.ProtectedInternal;
            if (constructorInfo.IsFamilyAndAssembly)
                return AccessModifierType.PrivateProtected;
                
            return AccessModifierType.None;
        }
        
        /// <summary>
        /// 生成构造函数签名
        /// </summary>
        /// <returns>构造函数签名</returns>
        private string GenerateConstructorSignature()
        {
            var result = GetAccessModifierString();
            
            if (IsStatic)
                result += " static";
                
            // 构造函数名称是类名
            var className = OriginalMemberInfo.DeclaringType?.Name ?? "Unknown";
            result += $" {className}";
            
            // 添加参数列表
            result += "(";
            result += string.Join(", ", Parameters.Select(p => p.ToFormattedString()));
            result += ")";
            
            return result.Trim();
        }
        
        /// <summary>
        /// 生成构造函数声明
        /// </summary>
        /// <returns>构造函数声明</returns>
        private string GenerateConstructorDeclaration()
        {
            return GenerateConstructorSignature() + " { }";
        }
        private AccessModifierType GetMethodAccessModifier(MethodInfo methodInfo)
        {
            if (methodInfo.IsPublic)
                return AccessModifierType.Public;
            if (methodInfo.IsPrivate)
                return AccessModifierType.Private;
            if (methodInfo.IsFamily)
                return AccessModifierType.Protected;
            if (methodInfo.IsAssembly)
                return AccessModifierType.Internal;
            if (methodInfo.IsFamilyOrAssembly)
                return AccessModifierType.ProtectedInternal;
            if (methodInfo.IsFamilyAndAssembly)
                return AccessModifierType.PrivateProtected;
                
            return AccessModifierType.None;
        }
        
        /// <summary>
        /// 检查方法是否为接口实现方法
        /// </summary>
        /// <param name="methodInfo">方法信息</param>
        /// <returns>是否为接口实现</returns>
        private bool IsInterfaceImplementationMethod(MethodInfo methodInfo)
        {
            if (methodInfo == null || methodInfo.DeclaringType == null)
                return false;
                
            // 检查是否为显式接口实现（私有方法，名称包含接口名）
            if (methodInfo.IsPrivate && methodInfo.IsFinal && methodInfo.Name.Contains("."))
            {
                return true;
            }
            
            // 检查隐式接口实现：遍历类型实现的所有接口
            var implementedInterfaces = methodInfo.DeclaringType.GetInterfaces();
            
            foreach (var interfaceType in implementedInterfaces)
            {
                // 获取接口中的所有方法
                var interfaceMethods = interfaceType.GetMethods();
                
                foreach (var interfaceMethod in interfaceMethods)
                {
                    // 检查方法名、参数签名和返回类型是否匹配
                    if (IsMethodSignatureMatch(methodInfo, interfaceMethod))
                    {
                        // 进一步检查：如果该方法也重写了基类虚方法，则优先认为是重写
                        if (methodInfo.IsVirtual && methodInfo.GetBaseDefinition() != methodInfo)
                        {
                            // 这是重写基类虚方法，不是接口实现
                            return false;
                        }
                        
                        return true;
                    }
                }
            }
            
            return false;
        }
        
        /// <summary>
        /// 检查两个方法的签名是否匹配
        /// </summary>
        /// <param name="method1">方法1</param>
        /// <param name="method2">方法2</param>
        /// <returns>签名是否匹配</returns>
        private bool IsMethodSignatureMatch(MethodInfo method1, MethodInfo method2)
        {
            // 检查方法名
            if (method1.Name != method2.Name)
                return false;
                
            // 检查返回类型
            if (method1.ReturnType != method2.ReturnType)
                return false;
                
            // 检查参数
            var params1 = method1.GetParameters();
            var params2 = method2.GetParameters();
            
            if (params1.Length != params2.Length)
                return false;
                
            for (int i = 0; i < params1.Length; i++)
            {
                // 对于泛型参数，需要使用更宽松的比较方式
                if (params1[i].ParameterType != params2[i].ParameterType && 
                    params1[i].ParameterType.FullName != params2[i].ParameterType.FullName)
                    return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// 获取方法的特殊标识
        /// </summary>
        /// <param name="methodInfo">方法信息</param>
        /// <returns>特殊标识字符串，或为 null</returns>
        private string GetSpecialTag(MethodInfo methodInfo)
        {
            if (methodInfo == null)
                return null;
                
            // 1. 检查是否为运算符方法（最高优先级）
            if (MemberGenerationDetector.IsOperatorMethod(methodInfo))
            {
                return "[Operator]";
            }
            
            // 2. 检查是否为转换方法
            if (MemberGenerationDetector.IsConversionMethod(methodInfo))
            {
                return "[Conversion]";
            }
            
            // 3. 检查是否为扩展方法（最低优先级）
            if (IsExtensionMethod)
            {
                return "[Extension]";
            }
            
            // 不是特殊方法
            return null;
        }
        
        /// <summary>
        /// 生成方法签名
        /// </summary>
        /// <returns>方法签名</returns>
        private string GenerateSignature()
        {
            // 检查是否为运算符方法或转换方法
            if (OriginalMemberInfo is MethodInfo methodInfo)
            {
                if (MemberGenerationDetector.IsOperatorMethod(methodInfo))
                {
                    return GenerateOperatorSignature(methodInfo);
                }
                if (MemberGenerationDetector.IsConversionMethod(methodInfo))
                {
                    return GenerateConversionSignature(methodInfo);
                }
            }
            
            // 普通方法的签名生成
            var result = GetAccessModifierString();
            
            // 添加修饰符
            if (IsStatic)
                result += " static";
            if (IsAbstract)
                result += " abstract";
            else if (IsVirtual)
                result += " virtual";
            else if (IsOverride && !IsInterfaceImplementation) // 接口实现不显示 override
                result += " override";
                
            // 添加返回类型
            result += $" {TypeName}";
            
            // 添加方法名
            result += $" {Name}";
            
            // 添加泛型参数
            if (IsGeneric && GenericArguments.Length > 0)
            {
                result += "<";
                result += string.Join(", ", GenericArguments.Select(t => t.Name));
                result += ">";
            }
            
            // 添加参数列表
            result += "(";
            result += string.Join(", ", Parameters.Select(p => p.ToFormattedString()));
            result += ")";
            
            return result.Trim();
        }
        
        /// <summary>
        /// 生成方法声明
        /// </summary>
        /// <returns>方法声明</returns>
        private string GenerateDeclaration()
        {
            var result = GenerateSignature();
            
            // 如果是抽象方法，添加分号
            if (IsAbstract)
                result += ";";
            else
                result += " { }";
                
            return result;
        }
        
        /// <summary>
        /// 生成运算符方法签名
        /// 格式：public static ReturnType operator +([params])
        /// </summary>
        /// <param name="methodInfo">方法信息</param>
        /// <returns>运算符方法签名</returns>
        private string GenerateOperatorSignature(MethodInfo methodInfo)
        {
            var result = GetAccessModifierString();
            
            // 运算符方法必须是 static
            result += " static";
            
            // 添加返回类型
            result += $" {TypeName}";
            
            // 添加 operator 关键字和运算符符号
            result += " operator ";
            
            // 根据方法名获取运算符符号
            var operatorSymbol = GetOperatorSymbol(methodInfo.Name);
            result += operatorSymbol;
            
            // 添加参数列表
            result += "(";
            result += string.Join(", ", Parameters.Select(p => p.ToFormattedString()));
            result += ")";
            
            return result.Trim();
        }
        
        /// <summary>
        /// 生成转换方法签名
        /// 格式：public static implicit/explicit operator TargetType([params])
        /// </summary>
        /// <param name="methodInfo">方法信息</param>
        /// <returns>转换方法签名</returns>
        private string GenerateConversionSignature(MethodInfo methodInfo)
        {
            var result = GetAccessModifierString();
            
            // 转换方法必须是 static
            result += " static";
            
            // 添加 implicit 或 explicit 关键字
            if (methodInfo.Name == "op_Implicit")
                result += " implicit";
            else if (methodInfo.Name == "op_Explicit")
                result += " explicit";
            
            // 添加 operator 关键字和目标类型
            result += $" operator {TypeName}";
            
            // 添加参数列表
            result += "(";
            result += string.Join(", ", Parameters.Select(p => p.ToFormattedString()));
            result += ")";
            
            return result.Trim();
        }
        
        /// <summary>
        /// 根据方法名获取运算符符号
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <returns>运算符符号</returns>
        private string GetOperatorSymbol(string methodName)
        {
            return methodName switch
            {
                "op_Addition" => "+",
                "op_Subtraction" => "-",
                "op_Multiply" => "*",
                "op_Division" => "/",
                "op_Modulus" => "%",
                "op_Equality" => "==",
                "op_Inequality" => "!=",
                "op_LessThan" => "<",
                "op_LessThanOrEqual" => "<=",
                "op_GreaterThan" => ">",
                "op_GreaterThanOrEqual" => ">=",
                "op_LogicalAnd" => "&&",
                "op_LogicalOr" => "||",
                "op_BitwiseAnd" => "&",
                "op_BitwiseOr" => "|",
                "op_ExclusiveOr" => "^",
                "op_LeftShift" => "<<",
                "op_RightShift" => ">>",
                "op_UnaryPlus" => "+",
                "op_UnaryNegation" => "-",
                "op_LogicalNot" => "!",
                "op_OnesComplement" => "~",
                "op_Increment" => "++",
                "op_Decrement" => "--",
                "op_True" => "true",
                "op_False" => "false",
                _ => methodName.StartsWith("op_") ? methodName.Substring(3) : methodName
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
            if (OriginalMemberInfo is ConstructorInfo)
            {
                return GenerateConstructorSignature();
            }
            else
            {
                return GenerateSignature();
            }
        }
        
        /// <summary>
        /// 生成详细的描述字符串
        /// </summary>
        /// <returns>详细的描述字符串</returns>
        public override string ToDetailedString()
        {
            var result = ToFormattedString();
            
            // 添加特殊方法信息
            if (IsSpecialName)
            {
                if (OriginalMemberInfo is MethodInfo methodInfo)
                {
                    if (MemberGenerationDetector.IsOperatorMethod(methodInfo))
                        result += " [运算符方法-特殊处理]";
                    else if (MemberGenerationDetector.IsConversionMethod(methodInfo))
                        result += " [转换方法-特殊处理]";
                    else
                        result += " [特殊名称方法-特殊处理]";
                }
            }
            
            // 添加接口实现信息
            if (IsInterfaceImplementation)
                result += " [接口实现-不显示override]";
            
            // 添加扩展方法信息
            if (IsExtensionMethod)
                result += " [扩展方法]";
                
            // 添加描述
            if (!string.IsNullOrEmpty(ChineseSummary))
                result += $" : {ChineseSummary}";
            else if (!string.IsNullOrEmpty(EnglishSummary))
                result += $" : {EnglishSummary}";
                
            return result;
        }
        
        /// <summary>
        /// 转换为 UML 格式：[可见性][/静态][名称]([参数列表]):[返回类型]
        /// 示例：+ PublicMethod(param1: string, param2: int): void
        ///      - PrivateMethod(): string
        ///      # ProtectedMethod<T>(input: T): T
        ///      + operator +(a: MyClass, b: MyClass): MyClass
        ///      + implicit operator string(obj: MyClass): string
        /// </summary>
        /// <returns>UML 格式的方法表示</returns>
        public override string ToUMLString()
        {
            var visibility = GetUMLVisibility(AccessModifier);
            var staticModifier = IsStatic ? "/" : "";
            var parameters = string.Join(", ", Parameters.Select(p => p.ToUMLString()));
            var genericPart = IsGeneric ? $"<{string.Join(", ", GenericArguments.Select(g => g.Name))}>" : "";
            
            if (OriginalMemberInfo is ConstructorInfo)
            {
                // 构造函数使用类名
                var className = OriginalMemberInfo.DeclaringType?.Name ?? "Unknown";
                return $"{visibility}{staticModifier}{className}({parameters})";
            }
            else if (OriginalMemberInfo is MethodInfo methodInfo)
            {
                // 检查是否为运算符方法
                if (MemberGenerationDetector.IsOperatorMethod(methodInfo))
                {
                    var operatorSymbol = GetOperatorSymbol(methodInfo.Name);
                    return $"{visibility}{staticModifier}operator {operatorSymbol}({parameters}): {TypeName}";
                }
                // 检查是否为转换方法
                else if (MemberGenerationDetector.IsConversionMethod(methodInfo))
                {
                    var conversionType = methodInfo.Name == "op_Implicit" ? "implicit" : "explicit";
                    return $"{visibility}{staticModifier}{conversionType} operator {TypeName}({parameters}): {TypeName}";
                }
            }
            
            // 普通方法
            return $"{visibility}{staticModifier}{Name}{genericPart}({parameters}): {TypeName}";
        }
        
        #endregion
    }
}