using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Sirenix.Utilities;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules
{
    /// <summary>
    /// TypeAnalyzer 中使用的 Type 静态扩展方法
    /// </summary>
    public static class TypeAnalyzerExtensions
    {
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
        public static bool IsDelegate(this Type type) =>
            type.IsSubclassOf(typeof(Delegate)) || type.IsSubclassOf(typeof(MulticastDelegate));

        /// <summary>
        /// 判断类型是否为 record struct（值类型 record）
        /// </summary>
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
        /// <param name="type">要检查的类型</param>
        /// <returns>如果是 record 则返回 true，否则返回 false</returns>
        public static bool IsRecord(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            // 核心判断：检查是否存在名为 "<Clone>$" 的方法
            // （record 类型编译后会自动生成该方法用于复制实例）
            MethodInfo cloneMethod = type.GetMethod("<Clone>$",
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
        public static string GetTypeDeclaration(this Type type)
        {
            var sb = new StringBuilder();
            object[] attributes = type.GetCustomAttributes(false);
            // 1. 添加特性部分，只包含特性名称，不包含特性参数
            foreach (object attr in attributes)
            {
                string attributeName = attr.GetType().Name;
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
            TypeCategory category = type.GetTypeCategory();
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
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // 特殊情况，委托的声明需要单独处理
            if (category == TypeCategory.Delegate)
            {
                MethodInfo invokeMethod = type.GetMethod("Invoke");
                if (invokeMethod == null)
                {
                    Debug.LogError("无法获取委托的 Invoke 方法");
                }

                sb.Append(invokeMethod.GetReturnType().GetReadableTypeName());
                sb.Append(" ");
                sb.Append(type.GetReadableTypeName());
                sb.Append("(");
                sb.Append(invokeMethod.GetParamsNames());
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
                IEnumerable<Type> interfaces = type.GetInterfaces()
                    .Where(i => !i.IsDefined(typeof(CompilerGeneratedAttribute), false));

                inheritTypes.AddRange(interfaces.Select(x => x.GetReadableTypeName(true)));

                if (inheritTypes.Count > 0)
                {
                    sb.Append(" : " + string.Join(", ", inheritTypes));
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
        /// 获取类型的访问修饰符字符串
        /// </summary>
        /// <returns>访问修饰符字符串+空格</returns>
        public static string GetTypeAccessModifier(this Type type)
        {
            if (type.IsNested)
            {
                if (type.IsNestedPublic)
                {
                    return "public ";
                }

                if (type.IsNestedPrivate)
                {
                    return "private ";
                }

                if (type.IsNestedFamily)
                {
                    return "protected "; // protected
                }

                if (type.IsNestedAssembly)
                {
                    return "internal "; // internal
                }

                if (type.IsNestedFamORAssem)
                {
                    return "protected internal ";
                }
            }
            else
            {
                return type.IsPublic ? "public " : "internal ";
            }

            Debug.LogWarning(nameof(TypeAnalyzerExtensions) + "." + nameof(GetTypeAccessModifier) + " 出现未考虑到的情况");
            return "出现未考虑到的情况";
        }

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

            if (method.IsFamilyAndAssembly)
            {
                return AccessModifierType.PrivateProtected;
            }

            return AccessModifierType.None;
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
        public static string GetReadableTypeName(this Type type, bool useFullName = false)
        {
            // TypeExtensions
            string targetTypeName = type.GetNiceName();
            if (useFullName)
            {
                targetTypeName = type.GetNiceFullName();
            }

            // 移除 obj 的后缀，针对 Unity 的特殊处理
            if (targetTypeName.EndsWith("obj") && targetTypeName.Length > 3)
            {
                targetTypeName = targetTypeName[..^3];
            }

            if (TypeAnalyzerUtility.TypeAliasMap.TryGetValue(type, out string alias))
            {
                targetTypeName = alias;
            }

            return targetTypeName;
        }

        /// <summary>
        /// 获取开发者声明的字段，剔除自动属性生成的字段
        /// </summary>
        /// <returns>符合条件的 FieldInfo 数组</returns>
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

        public static string GetChineseDescription(this Type type) => ChineseSummaryAttribute.GetChineseSummary(type);

        public static string GetEnglishDescription(this Type type)
        {
            IEnumerable<Attribute> attributes = type.GetCustomAttributes();
            return attributes.FirstOrDefault(x => typeof(ISummaryAttribute).IsAssignableFrom(x.GetType())) is not
                ISummaryAttribute description
                ? null
                : "No Description";
        }

        /// <summary>
        /// 获取一个数组，内容是所有的 ReferenceLinkURL 特性中的网页链接字符串
        /// </summary>
        public static string[] GetReferenceLinks(this Type type)
        {
            IEnumerable<ReferenceLinkURLAttribute> links = type.GetAttributes<ReferenceLinkURLAttribute>();
            return links.Select(x => x.WebLink).ToArray();
        }

        /// <summary>
        /// 获取一个类型的继承链，不包括接口
        /// </summary>
        public static string[] GetInheritanceChain(this Type type)
        {
            return type.GetBaseTypes()
                .Where(t => !t.IsInterface)
                .Select(baseType => baseType.GetReadableTypeName(true)).ToArray();
        }

        /// <summary>
        /// 获取一个类型继承的所有接口
        /// </summary>
        public static string[] GetInterfacesArray(this Type type)
        {
            return type.GetInterfaces().Select(i => i.GetReadableTypeName(true)).ToArray();
        }

        #region 创建分析数据

        /// <summary>
        /// 获取所有公共的，非静态的，构造函数的解析数据
        /// </summary>
        public static ConstructorAnalysisData[] CreateAllPublicConstructorAnalysisDataArray(this Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors();
            List<ConstructorAnalysisData> dataList = constructors
                .Select(x => ConstructorAnalysisData.CreateFromConstructorInfo(x, type))
                .ToList();
            dataList.Sort(new ConstructorAnalysisDataComparer());
            return dataList.ToArray();
        }

        public static MethodAnalysisData[] CreateRuntimeMethodAnalysisDataArray(this Type type)
        {
            IEnumerable<MethodInfo> methods = type.GetRuntimeMethods();
            // 剔除掉特殊方法，例如 Property 的 get 和 set， Event 的 add 和 remove
            methods = methods.Where(x =>
                x != null && !x.Name.Contains("add_") && !x.Name.Contains("remove_") &&
                !x.Name.Contains("get_") && !x.Name.Contains("set_"));
            List<MethodAnalysisData> dataList =
                methods.Select(x => MethodAnalysisData.CreateFromMethodInfo(x, type))
                    .ToList();
            dataList.Sort(new MethodAnalysisDataComparer());
            return dataList.ToArray();
        }

        public static EventAnalysisData[] CreateRuntimeEventAnalysisDataArray(this Type type)
        {
            IEnumerable<EventInfo> events = type.GetRuntimeEvents();
            List<EventAnalysisData> dataList = events
                .Select(x => EventAnalysisData.FromEventInfo(x, type))
                .ToList();
            dataList.Sort(new EventAnalysisDataComparer());
            return dataList.ToArray();
        }

        public static PropertyAnalysisData[] CreateRuntimePropertyAnalysisDataArray(this Type type)
        {
            IEnumerable<PropertyInfo> properties = type.GetRuntimeProperties();
            List<PropertyAnalysisData> dataList = properties
                .Select(x => PropertyAnalysisData.FromPropertyInfo(x, type))
                .ToList();
            dataList.Sort(new PropertyAnalysisDataComparer());
            return dataList.ToArray();
        }

        public static FieldAnalysisData[] CreateUserDefinedFieldAnalysisDataArray(this Type type)
        {
            IEnumerable<FieldInfo> fields = type.GetUserDefinedFields();
            List<FieldAnalysisData> dataList = fields
                .Select(x => FieldAnalysisData.FromFieldInfo(x, type))
                .ToList();
            dataList.Sort(new FieldAnalysisDataComparer());
            return dataList.ToArray();
        }

        #endregion
    }
}
