using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    /// <summary>
    /// 类型分析器静态扩展类，统一管理类型分析器有关的静态扩展方法
    /// </summary>
    [Summary("类型分析器静态扩展类，统一管理类型分析器有关的静态扩展方法")]
    public static class TypeAnalyzerStaticExtensions
    {
        static readonly Dictionary<string, string> OperatorStringMap = new Dictionary<string, string>
        {
            // 算术运算符
            { "op_Addition", "operator +" },
            { "op_Subtraction", "operator -" },
            { "op_Multiply", "operator *" },
            { "op_Division", "operator /" },
            { "op_Modulus", "operator %" },
            { "op_Increment", "operator ++" },
            { "op_Decrement", "operator --" },
            // 类型转换
            { "op_Implicit", "implicit operator" },
            { "op_Explicit", "explicit operator" },
            // 位运算符
            { "op_BitwiseAnd", "operator &" },
            { "op_BitwiseOr", "operator |" },
            { "op_ExclusiveOr", "operator ^" },
            { "op_LeftShift", "operator <<" },
            { "op_RightShift", "operator >>" },
            { "op_OnesComplement", "operator ~" },
            // 逻辑运算符
            { "op_LogicalNot", "operator !" },
            { "op_True", "operator true" },
            { "op_False", "operator false" },
            // 比较运算符
            { "op_Equality", "operator ==" },
            { "op_Inequality", "operator !=" },
            { "op_LessThan", "operator <" },
            { "op_GreaterThan", "operator >" },
            { "op_LessThanOrEqual", "operator <=" },
            { "op_GreaterThanOrEqual", "operator >=" },
            // 赋值运算符 (C# 7.3+)
            { "op_Assign", "operator =" },
            { "op_MultiplyAssign", "operator *=" },
            { "op_DivideAssign", "operator /=" },
            { "op_ModulusAssign", "operator %=" },
            { "op_AdditionAssign", "operator +=" },
            { "op_SubtractionAssign", "operator -=" },
            // ---
            { "op_LeftShiftAssign", "operator <<=" },
            { "op_RightShiftAssign", "operator >>=" },
            { "op_BitwiseAndAssign", "operator &=" },
            { "op_BitwiseOrAssign", "operator |=" },
            { "op_ExclusiveOrAssign", "operator ^=" },
            { "op_Coalesce", "operator ??" },
            { "op_MemberAccess", "operator ->" },
            { "op_Index", "operator []" },
            { "op_AddressOf", "operator &" },
            { "op_PointerDereference", "operator * " }
        };

        /// <summary>
        /// 获取事件的访问修饰符类型
        /// </summary>
        [Summary("获取事件的访问修饰符类型")]
        public static AccessModifierType GetEventAccessModifierType(this EventInfo eventInfo)
        {
            var addMethod = eventInfo.GetAddMethod(true);
            var removeMethod = eventInfo.GetRemoveMethod(true);
            var invokeMethod = eventInfo.GetRaiseMethod(true);
            if (addMethod != null && removeMethod != null)
            {
                return ((int)addMethod.GetMethodAccessModifierType() <= (int)removeMethod.GetMethodAccessModifierType()
                    ? addMethod
                    : removeMethod).GetMethodAccessModifierType();
            }

            if (addMethod != null)
            {
                return addMethod.GetMethodAccessModifierType();
            }

            if (removeMethod != null)
            {
                return removeMethod.GetMethodAccessModifierType();
            }

            return invokeMethod != null ? invokeMethod.GetMethodAccessModifierType() : AccessModifierType.None;
        }

        /// <summary>
        /// 判断是否为 API 成员，返回 true 表示是 API 成员，返回 false 表示不是。API 成员指的是公共成员或受保护成员。
        /// </summary>
        [Summary("判断是否为 API 成员，返回 true 表示是 API 成员，返回 false 表示不是。API 成员指的是公共成员或受保护成员。")]
        public static bool IsApiMember(this IDerivedMemberData derivedMemberData) =>
            derivedMemberData.AccessModifier is AccessModifierType.Public or AccessModifierType.Protected;

        /// <summary>
        /// 将 IDerivedMemberData 转换为 IMemberData，转换成功返回 true，转换失败返回 false
        /// </summary>
        [Summary("将 IDerivedMemberData 转换为 IMemberData，转换成功返回 true，转换失败返回 false")]
        public static bool TryAsIMemberData(this IDerivedMemberData derivedMemberData, out IMemberData memberData)
        {
            if (derivedMemberData is not MemberData)
            {
                Debug.LogError(derivedMemberData.GetType().Name + " 没有实现 IMemberData 接口");
            }

            memberData = derivedMemberData as IMemberData;
            return memberData != null;
        }

        /// <summary>
        /// 获取类型的种类
        /// </summary>
        [Summary("获取类型的种类")]
        public static TypeCategory GetTypeCategory(this Type type)
        {
            if (type.IsDelegate())
            {
                return TypeCategory.Delegate;
            }

            if (type.IsClass)
            {
                return type.IsRecord() ? TypeCategory.Record : TypeCategory.Class;
            }

            if (type.IsInterface)
            {
                return TypeCategory.Interface;
            }

            if (type.IsValueType && !type.IsEnum)
            {
                return type.IsRecord() ? TypeCategory.Record : TypeCategory.Struct;
            }

            if (type.IsEnum)
            {
                return TypeCategory.Enum;
            }

            Debug.LogError("出现不存在的 TypeCategory，需要补充枚举！");
            return TypeCategory.Unknown;
        }

        /// <summary>
        /// 判断指定类型是否为委托类型
        /// </summary>
        [Summary("判断指定类型是否为委托类型")]
        public static bool IsDelegate(this Type type) =>
            type.IsSubclassOf(typeof(Delegate)) || type.IsSubclassOf(typeof(MulticastDelegate));

        /// <summary>
        /// 判断类型是否为 record struct（值类型 record）
        /// </summary>
        [Summary("判断类型是否为 record struct（值类型 record）")]
        public static bool IsRecordStruct(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            // 条件：是值类型、不是枚举、且是 record
            return type.IsValueType && !type.IsEnum && IsRecord(type);
        }

        /// <summary>
        /// 判断指定类型是否为 record（包括 record class 和 record struct）
        /// </summary>
        [Summary("判断指定类型是否为 record（包括 record class 和 record struct）")]
        public static bool IsRecord(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            // 核心判断：检查是否存在名为 "<Clone>$" 的方法
            // （record 类型编译后会自动生成该方法用于复制实例）
            var cloneMethod = type.GetMethod("<Clone>$",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            return cloneMethod != null;
        }

        /// <summary>
        /// 获取类型声明字符串
        /// </summary>
        /// <returns>
        /// 返回字符串，形如：<br />
        /// <c>
        /// [Serializable]
        /// public class ForTestTypeUtilGenericNestedClass&lt;TCollection, TItem&gt;: System.Object
        /// where TCollection : System.Collections.Generic.IEnumerable&lt;Item&gt;
        /// </c>
        /// </returns>
        [Summary("获取类型声明字符串")]
        public static string GetTypeDeclaration(this Type type)
        {
            var sb = new StringBuilder();
            var attributes = type.GetCustomAttributes(false);
            // 1. 添加特性部分，只包含特性名称，不包含特性参数
            foreach (var attr in attributes)
            {
                var attributeName = attr.GetType().Name;
                if (attributeName.EndsWith("Attribute"))
                {
                    attributeName = attributeName[..^"Attribute".Length];
                }

                sb.AppendLine($"[{attributeName}]");
            }

            // 2. 添加访问修饰符
            sb.Append(GetTypeAccessModifier(type));

            // 3. 添加类修饰符
            // type.IsSealed && type.IsAbstract == static
            if (type.IsStatic())
            {
                sb.Append("static ");
            }
            else if (type.IsAbstract && !type.IsInterface)
            {
                sb.Append("abstract ");
            }
            else if (type.IsSealed && !type.IsEnum && !type.IsDelegate())
            {
                sb.Append("sealed ");
            }

            // 4. 添加类型关键字
            var category = type.GetTypeCategory();
            switch (category)
            {
                case TypeCategory.Class:
                    sb.Append("class ");
                    break;
                case TypeCategory.Interface:
                    sb.Append("interface ");
                    break;
                case TypeCategory.Struct:
                    sb.Append("struct ");
                    break;
                case TypeCategory.Enum:
                    sb.Append("enum ");
                    break;
                case TypeCategory.Delegate:
                    sb.Append("delegate ");
                    break;
                case TypeCategory.Record:
                    sb.Append("record ");
                    break;
            }

            // 特殊情况，委托的声明需要单独处理
            if (category == TypeCategory.Delegate)
            {
                var invokeMethod = type.GetMethod("Invoke");
                if (invokeMethod == null)
                {
                    Debug.LogError("无法获取委托的 Invoke 方法");
                }

                sb.Append(invokeMethod.GetReturnType().GetReadableTypeName());
                sb.Append(" ");
                sb.Append(type.GetReadableTypeName());
                sb.Append("(");
                sb.Append(invokeMethod.GetParametersNameWithDefaultValue());
                sb.Append(");");
            }
            else
            {
                // 单独处理 Record Struct
                if (type.IsRecordStruct())
                {
                    sb.Append("struct ");
                }

                // 5. 添加类名（处理泛型）
                sb.Append(type.GetReadableTypeName());

                // 6. 添加基类和接口
                var inheritTypes = new List<string>();

                // 添加直接基类
                if (type.BaseType != null)
                {
                    inheritTypes.Add(type.BaseType.GetReadableTypeName(true));
                }

                // 添加实现的接口，忽略编译时生成的接口，包含所有从基类继承的接口
                var interfaces = type.GetInterfaces()
                    .Where(i => !i.IsDefined(typeof(CompilerGeneratedAttribute), false));

                inheritTypes.AddRange(interfaces.Select(x => x.GetReadableTypeName(true)));

                if (inheritTypes.Count > 0)
                {
                    sb.Append(" : ");
                    foreach (var inheritType in inheritTypes)
                    {
                        sb.AppendLine(inheritType + ";");
                    }
                }

                // 7. 添加泛型约束
                if (type.IsGenericType)
                {
                    sb.AppendLine(" " + type.GetGenericConstraintsString(true));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获取类型的访问修饰符
        /// </summary>
        [Summary("获取类型的访问修饰符")]
        public static AccessModifierType GetTypeAccessModifier(this Type type)
        {
            if (type.IsNested)
            {
                if (type.IsNestedPublic)
                {
                    return AccessModifierType.Public;
                }

                if (type.IsNestedPrivate)
                {
                    return AccessModifierType.Private;
                }

                if (type.IsNestedFamily)
                {
                    return AccessModifierType.Protected;
                }

                if (type.IsNestedAssembly)
                {
                    return AccessModifierType.Internal;
                }

                if (type.IsNestedFamORAssem)
                {
                    return AccessModifierType.ProtectedInternal;
                }
            }
            else
            {
                return type.IsPublic ? AccessModifierType.Public : AccessModifierType.Internal;
            }

            return AccessModifierType.Public;
        }

        /// <summary>
        /// 将反射获取到的系统类型名称转换为人类可读的 C# 风格类型名称
        /// </summary>
        /// <param name="type">想要获取可读性强的类型定义名称字符串的 Type 对象</param>
        /// <param name="useFullName">是否获取包含命名空间的类名</param>
        /// <returns>
        /// 可读性高的类型名称
        /// </returns>
        /// <example>
        ///     <c>Single => float </c><br />
        ///     <c>Enumerable`1 => Enumerable&lt;T&gt;</c>
        /// </example>
        [Summary("将反射获取到的系统类型名称转换为人类可读的 C# 风格类型名称")]
        public static string GetReadableTypeName(this Type type, bool useFullName = false)
        {
            var targetTypeName = type.GetNiceName();
            if (useFullName)
            {
                targetTypeName = type.GetNiceFullName();
            }

            // 移除 obj 的后缀，针对 Unity 的特殊处理
            if (targetTypeName.EndsWith("obj") && targetTypeName.Length > 3)
            {
                targetTypeName = targetTypeName[..^3];
            }

            if (TypeAnalyzerUtility.TypeAliasMap.TryGetValue(type, out var alias))
            {
                targetTypeName = alias;
            }

            return targetTypeName;
        }

        /// <summary>
        /// 获取开发者声明的字段，剔除自动属性生成的字段
        /// </summary>
        [Summary("获取开发者声明的字段，剔除自动属性生成的字段")]
        public static FieldInfo[] GetUserDefinedFields(this Type type)
        {
            return type.GetRuntimeFields()
                .Where(f => !f.IsSpecialName && !IsAutoPropertyBackingField(f))
                .ToArray();

            static bool IsAutoPropertyBackingField(FieldInfo field)
            {
                return field.Name.Contains("k__BackingField") ||
                       field.Name.Contains("__BackingField");
            }
        }

        /// <summary>
        /// 获取一个数组，内容是所有的 ReferenceLinkURL 特性中的网页链接字符串
        /// </summary>
        [Summary("获取一个数组，内容是所有的 ReferenceLinkURL 特性中的网页链接字符串")]
        public static string[] GetReferenceLinks(this Type type)
        {
            var links = type.GetAttributes<ReferenceLinkURLAttribute>();
            return links.Select(x => x.WebLink).ToArray();
        }

        /// <summary>
        /// 获取一个类型的继承链，不包括接口
        /// </summary>
        [Summary("获取一个类型的继承链，不包括接口")]
        public static string[] GetInheritanceChain(this Type type)
        {
            return type.GetBaseTypes()
                .Where(t => !t.IsInterface)
                .Select(baseType => baseType.GetReadableTypeName(true).Replace("object", "System.Object"))
                .ToArray();
        }

        /// <summary>
        /// 获取一个类型继承的所有接口
        /// </summary>
        [Summary("获取一个类型继承的所有接口")]
        public static string[] GetInterfaceArray(this Type type)
        {
            return type.GetInterfaces().Select(i => i.GetReadableTypeName(true)).ToArray();
        }

        /// <summary>
        /// 判断一个类型是否为非字符串的引用类型（非值类型）
        /// </summary>
        [Summary("判断一个类型是否为非字符串的引用类型（非值类型）")]
        public static bool IsReferenceTypeExcludeString(this Type type) =>
            !type.IsPrimitive && !type.IsValueType && type != typeof(string);

        /// <summary>
        /// 判断一个类型是否为抽象类或接口
        /// </summary>
        [Summary("判断一个类型是否为抽象类或接口")]
        public static bool IsAbstractOrInterface(this Type type) => type.IsAbstract || type.IsInterface;

        /// <summary>
        /// 判断成员是否从继承中获取，这里的成员不包括 Type 类型
        /// </summary>
        [Summary("判断成员是否从继承中获取，这里的成员不包括 Type 类型")]
        public static bool IsFromInheritance(this MemberInfo member)
        {
            if (member.DeclaringType == null || member.ReflectedType == null)
            {
                return false;
            }

            if (member is MethodInfo methodInfo && methodInfo.IsOverrideMethod())
            {
                var baseMethod = methodInfo.GetBaseDefinition();
                return baseMethod.DeclaringType != methodInfo.DeclaringType;
            }

            return member.DeclaringType != member.ReflectedType;
        }

        /// <summary>
        /// 获取特性声明字符串，多行显示
        /// </summary>
        [Summary("获取特性声明字符串，多行显示")]
        public static string GetAttributesDeclarationWithMultiLine(this MemberInfo member,
            IAttributeFilter filter = null)
        {
            var attributes = member.GetCustomAttributes(false);
            if (attributes.Length == 0)
            {
                return string.Empty;
            }

            var attributesStringBuilder = new StringBuilder();
            foreach (var attr in attributes)
            {
                var attributeType = attr.GetType();
                if (filter != null && filter.ShouldFilterOut(attributeType))
                {
                    continue;
                }

                if (TypeAnalyzerUtility.TryGetFormatedAttributeWithFullParameter(attr, out var attributeSignature))
                {
                    attributesStringBuilder.AppendLine(attributeSignature);
                    continue;
                }

                var attributeName = TypeAnalyzerUtility.GetAttributeNameWithoutSuffix(attributeType.Name);
                attributesStringBuilder.AppendLine($"[{attributeName}]");
            }

            return attributesStringBuilder.ToString();
        }

        /// <summary>
        /// 获取方法的关键字片段，用于生成方法签名，如：static、virtual、async 等
        /// </summary>
        [Summary("获取方法的关键字片段，用于生成方法签名，如：static、virtual、async 等")]
        public static string GetKeywordSnippetInSignature(this MethodInfo methodInfo)
        {
            var keyword = "";
            if (methodInfo.IsStatic)
            {
                keyword = "static ";
            }
            else if (methodInfo.IsAbstract)
            {
                keyword = "abstract ";
            }
            else if (methodInfo.IsVirtual && methodInfo.DeclaringType != methodInfo.GetBaseDefinition().DeclaringType)
            {
                keyword = "override ";
            }
            else if (methodInfo.DeclaringType == methodInfo.GetBaseDefinition().DeclaringType &&
                     methodInfo.IsVirtual && methodInfo.IsFromInterfaceImplementMethod())
            {
                // 这是实现接口的方法
                keyword = "";
            }
            else if (methodInfo.IsVirtual)
            {
                keyword = "virtual ";
            }

            if (methodInfo.GetCustomAttribute<AsyncStateMachineAttribute>() != null)
            {
                keyword += "async ";
            }

            return keyword;
        }

        /// <summary>
        /// 判断方法是否为从祖先类继承的重写方法，重写声明不是在当前类中
        /// </summary>
        [Summary("判断方法是否为从祖先类继承的重写方法，重写声明不是在当前类中")]
        public static bool IsInheritedOverrideFromAncestor(this MethodInfo method, Type currentType)
        {
            // 方法不是在当前类中声明的
            if (method.DeclaringType == currentType)
            {
                return false;
            }

            if (method.IsVirtual && method.GetBaseDefinition() != method)
            {
                var baseDefinitionDeclaringType = method.GetBaseDefinition().DeclaringType;
                var directBaseType = currentType.BaseType;
                if (baseDefinitionDeclaringType != directBaseType)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 方法是否具有 override 的特性
        /// </summary>
        [Summary("方法是否具有 override 的特性")]
        public static bool IsOverrideMethod(this MethodInfo methodInfo) =>
            (methodInfo.IsVirtual &&
             methodInfo.DeclaringType != methodInfo.GetBaseDefinition().DeclaringType)
            || (methodInfo.DeclaringType == methodInfo.GetBaseDefinition().DeclaringType &&
                methodInfo.IsVirtual && methodInfo.IsFromInterfaceImplementMethod());

        /// <summary>
        /// 判断方法是否是异步方法
        /// </summary>
        [Summary("判断方法是否是异步方法")]
        public static bool IsAsyncMethod(this MethodBase methodBase) =>
            methodBase.GetCustomAttribute<AsyncStateMachineAttribute>() != null;

        /// <summary>
        /// 判断方法是否是运算符方法
        /// </summary>
        [Summary("判断方法是否是运算符方法")]
        public static bool IsOperatorMethod(this MethodBase methodInfo) =>
            methodInfo.IsSpecialName && methodInfo.Name.StartsWith("op_");

        /// <summary>
        /// 获取方法的访问修饰符类型
        /// </summary>
        [Summary("获取方法的访问修饰符类型")]
        public static AccessModifierType GetMethodAccessModifierType(this MethodBase method)
        {
            if (method.IsPublic)
            {
                return AccessModifierType.Public;
            }

            if (method.IsPrivate)
            {
                return AccessModifierType.Private;
            }

            if (method.IsFamily)
            {
                return AccessModifierType.Protected;
            }

            if (method.IsAssembly)
            {
                return AccessModifierType.Internal;
            }

            if (method.IsFamilyOrAssembly)
            {
                return AccessModifierType.ProtectedInternal;
            }

            return method.IsFamilyAndAssembly ? AccessModifierType.PrivateProtected : AccessModifierType.None;
        }

        /// <summary>
        /// 获取方法的部分签名，包含名称和参数列表，不包含返回值和修饰符
        /// </summary>
        [Summary("获取方法的部分签名，包含名称和参数列表，不包含返回值和修饰符")]
        public static string GetPartMethodSignatureContainsNameAndParameters(this MethodBase method)
        {
            var stringBuilder = new StringBuilder();
            if (method.IsConstructor || (method.IsStatic && method.Name == ".cctor"))
            {
                stringBuilder.Append(method.DeclaringType?.GetReadableTypeName());
            }
            else
            {
                if (method.IsSpecialName && OperatorStringMap.TryGetValue(method.Name, out var value))
                {
                    stringBuilder.Append(value);
                }
                else
                {
                    stringBuilder.Append(method.Name);
                }
            }

            if (method.IsGenericMethod)
            {
                var genericArguments = method.GetGenericArguments();
                stringBuilder.Append("<");
                for (var index = 0; index < genericArguments.Length; ++index)
                {
                    if (index != 0)
                    {
                        stringBuilder.Append(", ");
                    }

                    stringBuilder.Append(genericArguments[index].GetNiceName());
                }

                stringBuilder.Append(">");
            }

            if (method.Name.Contains("op_Implicit") || method.Name.Contains("op_Explicit"))
            {
                stringBuilder.Append(" " + method.GetReturnType().GetReadableTypeName());
            }

            stringBuilder.Append("(");
            stringBuilder.Append(method.GetParametersNameWithDefaultValue());
            stringBuilder.Append(")");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 获取方法的参数签名，包含默认值
        /// </summary>
        [Summary("获取方法的参数签名，包含默认值")]
        public static string GetParametersNameWithDefaultValue(this MethodBase method)
        {
            var parameterInfoArray = method.GetParameters();
            var stringBuilder = new StringBuilder();
            var index = 0;
            for (var length = parameterInfoArray.Length; index < length; ++index)
            {
                var parameterInfo = parameterInfoArray[index];
                var parameterData = new ParameterData(parameterInfo);
                if (index == 0 && method.IsExtensionMethod())
                {
                    stringBuilder.Append("this ");
                }

                stringBuilder.Append(parameterData.GetFormattedString());

                if (index < length - 1)
                {
                    stringBuilder.Append(", ");
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 判断是否为接口的实现方法
        /// </summary>
        [Summary("判断是否为接口的实现方法")]
        public static bool IsFromInterfaceImplementMethod(this MethodBase method)
        {
            var declaringType = method.DeclaringType;
            if (declaringType == null)
            {
                return false;
            }

            // 如果声明类型本身就是接口，则该方法不是接口的实现方法
            if (declaringType.IsInterface)
            {
                return false;
            }

            var interfaceTypeList = declaringType.GetInterfaces();
            return interfaceTypeList.Select(interfaceType => declaringType.GetInterfaceMap(interfaceType))
                .Select(interfaceMapping => interfaceMapping.TargetMethods)
                .Any(targetMethods => targetMethods.Contains(method));
        }

        /// <summary>
        /// 判断是否为静态属性
        /// </summary>
        [Summary("判断是否为静态属性")]
        public static bool IsStaticProperty(this PropertyInfo propertyInfo)
            => propertyInfo.GetGetMethod(true)?.IsStatic
               ?? propertyInfo.GetSetMethod(true)?.IsStatic
               ?? false;

        /// <summary>
        /// 获取属性的访问修饰符类型
        /// </summary>
        [Summary("获取属性的访问修饰符类型")]
        public static AccessModifierType GetPropertyAccessModifierType(this PropertyInfo propertyInfo)
        {
            var getMethod = propertyInfo.GetGetMethod(true);
            var setMethod = propertyInfo.GetSetMethod(true);

            AccessModifierType? getAccess = getMethod != null ? getMethod.GetMethodAccessModifierType() : null;
            AccessModifierType? setAccess = setMethod != null ? setMethod.GetMethodAccessModifierType() : null;

            if (!getAccess.HasValue && !setAccess.HasValue)
            {
                return AccessModifierType.None;
            }

            if (!setAccess.HasValue)
            {
                return getAccess.Value;
            }

            if (!getAccess.HasValue)
            {
                return setAccess.Value;
            }

            return (int)getAccess.Value <= (int)setAccess.Value
                ? getAccess.Value
                : setAccess.Value;
        }

        /// <summary>
        /// 获取属性的自定义默认值，不能获取到值则返回 null，只获取静态属性的默认值。
        /// </summary>
        [Summary("获取属性的自定义默认值，不能获取到值则返回 null，只获取静态属性的默认值。")]
        public static bool TryGetPropertyCustomDefaultValue(this PropertyInfo propertyInfo, out object defaultValue)
        {
            var propertyType = propertyInfo.PropertyType;
            if (propertyType.IsReferenceTypeExcludeString() || propertyType.IsAbstractOrInterface())
            {
                defaultValue = null;
                return false;
            }

            var isStatic = propertyInfo.IsStaticProperty();
            if (isStatic)
            {
                var staticValue = propertyInfo.GetMemberValue(null);
                if (staticValue == null ||
                    TypeAnalyzerUtility.TreatedAsTypeDefaultValue(staticValue, propertyInfo.PropertyType))
                {
                    defaultValue = null;
                    return false;
                }

                defaultValue = staticValue;
                return true;
            }

            defaultValue = null;
            return false;
        }

        /// <summary>
        /// 获取字段访问修饰符
        /// </summary>
        [Summary("获取字段访问修饰符")]
        public static AccessModifierType GetFieldAccessModifier(this FieldInfo fieldInfo)
        {
            if (fieldInfo == null)
            {
                throw new ArgumentNullException(nameof(fieldInfo));
            }

            // 按照访问修饰符的优先级进行检查
            if (fieldInfo.IsPublic)
            {
                return AccessModifierType.Public;
            }

            if (fieldInfo.IsPrivate)
            {
                return AccessModifierType.Private;
            }

            if (fieldInfo.IsFamily)
            {
                return AccessModifierType.Protected;
            }

            if (fieldInfo.IsAssembly)
            {
                return AccessModifierType.Internal;
            }

            if (fieldInfo.IsFamilyOrAssembly)
            {
                return AccessModifierType.ProtectedInternal;
            }

            return fieldInfo.IsFamilyAndAssembly ? AccessModifierType.PrivateProtected : AccessModifierType.None;
        }

        /// <summary>
        /// 判断是否为动态字段
        /// </summary>
        [Summary("判断是否为动态字段")]
        public static bool IsDynamicField(this FieldInfo fieldInfo)
            => fieldInfo.FieldType == typeof(object) &&
               fieldInfo.GetCustomAttributes(typeof(DynamicAttribute), false).Length > 0;

        /// <summary>
        /// 获取字段的自定义默认值，不能获取到值则返回 null。只获取静态字段和常量字段的默认值。
        /// </summary>
        [Summary("获取字段的自定义默认值，不能获取到值则返回 null。只获取静态字段和常量字段的默认值。")]
        public static bool TryGetFieldCustomDefaultValue(this FieldInfo fieldInfo, out object defaultValue)
        {
            var fieldType = fieldInfo.FieldType;
            if (fieldType.IsReferenceTypeExcludeString() || fieldType.IsAbstractOrInterface())
            {
                defaultValue = null;
                return false;
            }

            var isConst = fieldInfo.IsLiteral && !fieldInfo.IsInitOnly;
            var isStatic = fieldInfo.IsStatic;
            if (isConst)
            {
                var constValue = fieldInfo.GetRawConstantValue();
                if (TypeAnalyzerUtility.TreatedAsTypeDefaultValue(constValue, fieldInfo.FieldType))
                {
                    defaultValue = null;
                    return false;
                }

                defaultValue = constValue;
                return true;
            }

            if (isStatic)
            {
                var staticValue = fieldInfo.GetValue(null);
                if (staticValue == null ||
                    TypeAnalyzerUtility.TreatedAsTypeDefaultValue(staticValue, fieldInfo.FieldType))
                {
                    defaultValue = null;
                    return false;
                }

                defaultValue = staticValue;
                return true;
            }

            defaultValue = null;
            return false;
        }
    }
}
