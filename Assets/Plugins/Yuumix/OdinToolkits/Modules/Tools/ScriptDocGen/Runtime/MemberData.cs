using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorLocalization.Attributes;

/// <summary>
/// 成员类型枚举，用于表示成员的基本类型
/// </summary>
public enum MemberKind
{
    Field,       // 字段
    Property,    // 属性
    Method,      // 方法
    Constructor, // 构造函数
    Event,       // 事件
    Delegate,    // 委托
    Enum,        // 枚举
    Class,       // 类
    Interface,   // 接口
    Struct,      // 结构体
    Operator,    // 运算符重载
    Indexer,     // 索引器
    Unknown      // 未知类型
}

/// <summary>
/// 表示类型成员信息的可读数据结构，用于生成脚本文档
/// </summary>
[Serializable]
public class MemberData
{
    public string MemberType;       // 成员类型 (Field, Property, Method, Event 等)
    public string Name;             // 成员名称
    public string DeclaringType;    // 声明该成员的类型
    public string ReturnType;       // 返回类型 (如果适用)
    public string AccessModifier;   // 访问修饰符 (public, private 等)
    public bool IsStatic;           // 是否为静态成员
    public bool IsConst;            // 是否为常量成员
    public bool IsReadonly;         // 是否为只读成员
    public List<string> Parameters; // 参数列表 (如果是方法)
    public string ChineseComment;   // 中文注释
    public string EnglishComment;   // 英文注释
    public bool IsEvent;            // 是否为事件成员
    public MemberKind Kind;         // 成员类型枚举

    // 工厂方法：从 MemberInfo 创建 MemberData
    public static MemberData FromMemberInfo(MemberInfo memberInfo)
    {
        if (memberInfo == null)
            throw new ArgumentNullException(nameof(memberInfo));

        var data = new MemberData
        {
            MemberType = memberInfo.MemberType.ToString(),
            Name = memberInfo.Name,
            DeclaringType = memberInfo.DeclaringType?.FullName ?? "Unknown",
            Parameters = new List<string>(),
            ChineseComment = "无注释",
            EnglishComment = "No comment",
            IsEvent = memberInfo.MemberType == MemberTypes.Event,
            Kind = GetMemberKind(memberInfo)
        };

        // 获取访问修饰符
        if (memberInfo is MethodInfo method)
        {
            data.AccessModifier = GetAccessModifier(method);
            data.IsStatic = method.IsStatic;
            data.ReturnType = GetFriendlyTypeName(method.ReturnType);

            // 获取方法参数
            foreach (var param in method.GetParameters())
            {
                data.Parameters.Add($"{GetFriendlyTypeName(param.ParameterType)} {param.Name}");
            }
        }
        else if (memberInfo is FieldInfo field)
        {
            data.AccessModifier = GetAccessModifier(field);
            data.IsStatic = field.IsStatic;
            data.IsConst = field.IsLiteral && !field.IsInitOnly;
            data.IsReadonly = field.IsInitOnly;
            data.ReturnType = GetFriendlyTypeName(field.FieldType);
        }
        else if (memberInfo is PropertyInfo property)
        {
            data.AccessModifier = GetAccessModifier(property);
            data.IsStatic = property.GetGetMethod(true)?.IsStatic ?? false;
            data.ReturnType = GetFriendlyTypeName(property.PropertyType);
        }
        else if (memberInfo is ConstructorInfo constructor)
        {
            data.AccessModifier = GetAccessModifier(constructor);
            data.IsStatic = false;
            data.ReturnType = "void";

            // 获取构造函数参数
            foreach (var param in constructor.GetParameters())
            {
                data.Parameters.Add($"{GetFriendlyTypeName(param.ParameterType)} {param.Name}");
            }
        }
        else if (memberInfo is EventInfo eventInfo)
        {
            data.AccessModifier = GetAccessModifier(eventInfo);
            data.IsStatic = eventInfo.GetAddMethod(true)?.IsStatic ?? false;
            data.ReturnType = GetFriendlyTypeName(eventInfo.EventHandlerType);
        }

        // 获取本地化注释
        var commentAttribute = memberInfo.GetCustomAttribute<LocalizedCommentAttribute>();
        if (commentAttribute != null)
        {
            data.ChineseComment = commentAttribute.ChineseComment;
            data.EnglishComment = commentAttribute.EnglishComment;
        }

        return data;
    }

    // 获取成员类型枚举
    private static MemberKind GetMemberKind(MemberInfo memberInfo)
    {
        if (memberInfo == null)
            return MemberKind.Unknown;

        switch (memberInfo.MemberType)
        {
            case MemberTypes.Field:
                return MemberKind.Field;

            case MemberTypes.Property:
                return MemberKind.Property;

            case MemberTypes.Method:
                if (memberInfo.Name.StartsWith("op_"))
                    return MemberKind.Operator;
                if (memberInfo.Name.StartsWith("get_") || memberInfo.Name.StartsWith("set_"))
                    return MemberKind.Property;
                return MemberKind.Method;

            case MemberTypes.Constructor:
                return MemberKind.Constructor;

            case MemberTypes.Event:
                return MemberKind.Event;

            case MemberTypes.TypeInfo:
            case MemberTypes.NestedType:
                if (memberInfo is Type type)
                {
                    if (type.IsEnum)
                        return MemberKind.Enum;
                    if (type.IsInterface)
                        return MemberKind.Interface;
                    if (type.IsValueType)
                        return MemberKind.Struct;
                    if (type.IsSubclassOf(typeof(MulticastDelegate)))
                        return MemberKind.Delegate;
                    return MemberKind.Class;
                }

                return MemberKind.Class;

            default:
                return MemberKind.Unknown;
        }
    }

    // 常见类型映射表 - 移除了多余的泛型类型映射
    private static readonly Dictionary<Type, string> TypeNameMap = new Dictionary<Type, string>
    {
        { typeof(void), "void" },
        { typeof(bool), "bool" },
        { typeof(byte), "byte" },
        { typeof(sbyte), "sbyte" },
        { typeof(char), "char" },
        { typeof(decimal), "decimal" },
        { typeof(double), "double" },
        { typeof(float), "float" },
        { typeof(int), "int" },
        { typeof(uint), "uint" },
        { typeof(long), "long" },
        { typeof(ulong), "ulong" },
        { typeof(object), "object" },
        { typeof(short), "short" },
        { typeof(ushort), "ushort" },
        { typeof(string), "string" },

        // Unity 特定类型
        { typeof(Vector2), "Vector2" },
        { typeof(Vector3), "Vector3" },
        { typeof(Vector4), "Vector4" },
        { typeof(Quaternion), "Quaternion" },
        { typeof(Color), "Color" },
        { typeof(Rect), "Rect" },

        // 委托类型 - 保留常用的委托类型映射
        { typeof(System.Action), "Action" },
        { typeof(System.Func<>), "Func<>" },

        // 其他常用类型
        { typeof(System.Collections.Generic.KeyValuePair<,>), "KeyValuePair<,>" },
        { typeof(System.Collections.IEnumerable), "IEnumerable" },
        { typeof(System.Collections.Generic.IEnumerable<>), "IEnumerable<>" },
    };

    // 获取友好的类型名称，支持泛型类型显示和类型映射
    private static string GetFriendlyTypeName(Type type)
    {
        if (type == null)
            return "void";

        // 检查类型映射表
        if (TypeNameMap.TryGetValue(type, out string friendlyName))
            return friendlyName;

        // 处理数组类型
        if (type.IsArray)
        {
            Type elementType = type.GetElementType();
            return $"{GetFriendlyTypeName(elementType)}[]";
        }

        // 处理泛型类型
        if (type.IsGenericType)
        {
            StringBuilder sb = new StringBuilder();
            string genericTypeName = type.Name;
            int backtickIndex = genericTypeName.IndexOf('`');
            if (backtickIndex > 0)
                genericTypeName = genericTypeName.Substring(0, backtickIndex);

            sb.Append(genericTypeName);
            sb.Append("<");

            Type[] genericArguments = type.GetGenericArguments();
            for (int i = 0; i < genericArguments.Length; i++)
            {
                string typeName = GetFriendlyTypeName(genericArguments[i]);
                sb.Append(typeName);

                if (i < genericArguments.Length - 1)
                    sb.Append(", ");
            }

            sb.Append(">");
            return sb.ToString();
        }

        // 处理 Nullable<T> 类型
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            Type underlyingType = Nullable.GetUnderlyingType(type);
            return $"{GetFriendlyTypeName(underlyingType)}?";
        }

        // 处理 Unity 类型 - 直接使用 Name，不需要映射
        if (type.Namespace != null && type.Namespace.StartsWith("UnityEngine"))
        {
            return type.Name;
        }

        // 处理其他类型
        return type.Name;
    }

    // 获取访问修饰符的可读表示
    public static string GetAccessModifier(MemberInfo member)
    {
        if (member is MethodInfo method)
        {
            if (method.IsPublic) return "public";
            if (method.IsPrivate) return "private";
            if (method.IsFamily) return "protected";
            if (method.IsAssembly) return "internal";
            if (method.IsFamilyOrAssembly) return "protected internal";
            if (method.IsFamilyAndAssembly) return "private protected";
        }
        else if (member is FieldInfo field)
        {
            if (field.IsPublic) return "public";
            if (field.IsPrivate) return "private";
            if (field.IsFamily) return "protected";
            if (field.IsAssembly) return "internal";
            if (field.IsFamilyOrAssembly) return "protected internal";
            if (field.IsFamilyAndAssembly) return "private protected";
        }
        else if (member is PropertyInfo property)
        {
            // 对于属性，我们检查 getter 和 setter 中访问权限最严格的一个
            string accessModifier = "public"; // 默认最宽松

            var getMethod = property.GetGetMethod(true);
            var setMethod = property.GetSetMethod(true);

            if (getMethod != null)
            {
                if (getMethod.IsPrivate) return "private";
                if (getMethod.IsFamilyAndAssembly) return "private protected";
                if (getMethod.IsFamily) return "protected";
                if (getMethod.IsAssembly) return "internal";
                if (getMethod.IsFamilyOrAssembly) return "protected internal";
            }

            if (setMethod != null)
            {
                if (setMethod.IsPrivate) return "private";
                if (setMethod.IsFamilyAndAssembly) return "private protected";
                if (setMethod.IsFamily) return "protected";
                if (setMethod.IsAssembly) return "internal";
                if (setMethod.IsFamilyOrAssembly) return "protected internal";
            }

            return accessModifier;
        }
        else if (member is ConstructorInfo constructor)
        {
            if (constructor.IsPublic) return "public";
            if (constructor.IsPrivate) return "private";
            if (constructor.IsFamily) return "protected";
            if (constructor.IsAssembly) return "internal";
            if (constructor.IsFamilyOrAssembly) return "protected internal";
            if (constructor.IsFamilyAndAssembly) return "private protected";
        }
        else if (member is EventInfo eventInfo)
        {
            var addMethod = eventInfo.GetAddMethod(true);
            if (addMethod != null)
            {
                if (addMethod.IsPublic) return "public";
                if (addMethod.IsPrivate) return "private";
                if (addMethod.IsFamily) return "protected";
                if (addMethod.IsAssembly) return "internal";
                if (addMethod.IsFamilyOrAssembly) return "protected internal";
                if (addMethod.IsFamilyAndAssembly) return "private protected";
            }
        }

        return "unknown";
    }

    // 重写ToString方法以便于调试
    public override string ToString()
    {
        string kindStr = "";

        if (IsConst)
            kindStr = "const ";
        else if (IsReadonly)
            kindStr = "readonly ";
        else if (IsStatic)
            kindStr = "static ";

        // 格式化访问修饰符，移除前后空格
        string accessStr = AccessModifier.Trim();
        if (!string.IsNullOrEmpty(accessStr))
            accessStr += " ";

        // 特殊处理属性方法名（get_XXX, set_XXX）
        string displayName = Name;
        if (Kind == MemberKind.Property && (Name.StartsWith("get_") || Name.StartsWith("set_")))
        {
            displayName = Name.Substring(4);
        }

        if (IsEvent)
        {
            return $"{accessStr}{kindStr}event {ReturnType} {displayName};";
        }

        string paramList = Parameters.Count > 0 ? $"({string.Join(", ", Parameters)})" : "";

        // 特殊处理构造函数
        if (Kind == MemberKind.Constructor)
        {
            return $"{accessStr}{displayName}{paramList}";
        }

        // 特殊处理属性
        if (Kind == MemberKind.Property)
        {
            return $"{accessStr}{kindStr}{ReturnType} {displayName} {{ get; set; }}";
        }

        // 特殊处理字段
        if (Kind == MemberKind.Field)
        {
            return $"{accessStr}{kindStr}{ReturnType} {displayName};";
        }

        return $"{accessStr}{kindStr}{MemberType}: {displayName}{paramList} : {ReturnType}";
    }
}

/// <summary>
/// 用于获取类型成员信息的工具类
/// </summary>
public static class ReflectionUtility
{
    /// <summary>
    /// 获取类型的所有公共成员数据（包括实例和静态）
    /// </summary>
    public static List<MemberData> GetTypeMemberData(Type type)
    {
        return GetTypeMemberData(type, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
    }

    /// <summary>
    /// 获取类型的所有成员数据，包括非公共成员
    /// </summary>
    public static List<MemberData> GetTypeMemberDataWithNonPublic(Type type)
    {
        return GetTypeMemberData(type,
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
    }

    /// <summary>
    /// 获取类型的所有成员数据
    /// </summary>
    public static List<MemberData> GetTypeMemberData(Type type, BindingFlags flags)
    {
        if (type == null)
            throw new ArgumentNullException(nameof(type));

        var memberDatas = new List<MemberData>();
        var members = type.GetMembers(flags);

        foreach (var member in members)
        {
            // 只过滤掉不需要的成员类型
            if (member.MemberType == MemberTypes.NestedType ||
                member.MemberType == MemberTypes.TypeInfo)
                continue;

            memberDatas.Add(MemberData.FromMemberInfo(member));
        }

        return memberDatas;
    }
}
