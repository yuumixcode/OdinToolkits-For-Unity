using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Modules;

namespace Yuumix.OdinToolkits.AdvancedTypeAnalyzer
{
    /// <summary>
    /// 类型分析器静态扩展类，统一管理类型分析器有关的静态扩展方法
    /// </summary>
    public static class TypeAnalyzerStaticExtensions
    {
        #region MemberInfo

        /// <summary>
        /// 获取特性声明字符串，多行显示
        /// </summary>
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

        #endregion

        #region EventInfo

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

        #endregion

        #region Type

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

            return "出现未考虑到的情况";
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

            if (TypeAnalyzerHelper.TypeAliasMap.TryGetValue(type, out var alias))
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

        public static string GetSummary(this Type type)
        {
            var attributes = type.GetCustomAttributes(typeof(SummaryAttribute), false);
            return attributes.Length > 0 ? ((SummaryAttribute)attributes[0]).GetSummary() : null;
        }

        /// <summary>
        /// 获取一个数组，内容是所有的 ReferenceLinkURL 特性中的网页链接字符串
        /// </summary>
        public static string[] GetReferenceLinks(this Type type)
        {
            var links = type.GetAttributes<ReferenceLinkURLAttribute>();
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

        /// <summary>
        /// 判断一个类型是否为非字符串的引用类型（非值类型）
        /// </summary>
        public static bool IsReferenceTypeExcludeString(this Type type) =>
            !type.IsPrimitive && !type.IsValueType && type != typeof(string);

        public static bool IsAbstractOrInterface(this Type type) => type.IsAbstract || type.IsInterface;

        #endregion

        #region FieldInfo

        /// <summary>
        /// 获取字段访问修饰符
        /// </summary>
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
        public static bool IsDynamicField(this FieldInfo fieldInfo)
            => fieldInfo.FieldType == typeof(object) &&
               fieldInfo.GetCustomAttributes(typeof(DynamicAttribute), false).Length > 0;

        /// <summary>
        /// 获取字段的自定义默认值，不能获取到值则返回 null
        /// </summary>
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

            var fieldDeclaringType = fieldInfo.DeclaringType;
            if (fieldDeclaringType == null || fieldDeclaringType.IsAbstractOrInterface())
            {
                defaultValue = null;
                return false;
            }

            var instance = Activator.CreateInstance(fieldDeclaringType);
            if (instance == null)
            {
                defaultValue = null;
                return false;
            }

            var instanceFieldValue = fieldInfo.GetValue(instance);
            if (instanceFieldValue == null ||
                TypeAnalyzerUtility.TreatedAsTypeDefaultValue(instanceFieldValue, fieldInfo.FieldType))
            {
                defaultValue = null;
                return false;
            }

            defaultValue = instanceFieldValue;
            return true;
        }

        #endregion

        #region PropertyInfo

        /// <summary>
        /// 判断是否为静态属性
        /// </summary>
        public static bool IsStaticProperty(this PropertyInfo propertyInfo)
            => propertyInfo.GetGetMethod(true)?.IsStatic
               ?? propertyInfo.GetSetMethod(true)?.IsStatic
               ?? false;

        /// <summary>
        /// 获取属性的访问修饰符类型
        /// </summary>
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
        /// 获取属性的自定义默认值，不能获取到值则返回 null
        /// </summary>
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

            var propertyDeclaringType = propertyInfo.DeclaringType;
            if (propertyDeclaringType == null || propertyDeclaringType.IsAbstractOrInterface())
            {
                defaultValue = null;
                return false;
            }

            var instance = Activator.CreateInstance(propertyDeclaringType);
            if (instance == null)
            {
                defaultValue = null;
                return false;
            }

            var instancePropertyValue = propertyInfo.GetMemberValue(instance);
            if (instancePropertyValue == null ||
                TypeAnalyzerUtility.TreatedAsTypeDefaultValue(instancePropertyValue, propertyInfo.PropertyType))
            {
                defaultValue = null;
                return false;
            }

            defaultValue = instancePropertyValue;
            return true;
        }

        #endregion
        
        #region MethodInfo
        
        public static string GetParamsNamesWithDefaultValue(this MethodBase method)
        {
            var parameterInfoArray = method.IsExtensionMethod()
                ? method.GetParameters().Skip(1).ToArray()
                : method.GetParameters();
            var stringBuilder = new StringBuilder();
            var index = 0;
            for (var length = parameterInfoArray.Length; index < length; ++index)
            {
                var parameterInfo = parameterInfoArray[index];
                var parameterData = new ParameterData(parameterInfo);
                stringBuilder.Append(parameterData.GetFormattedString());

                if (index < length - 1)
                {
                    stringBuilder.Append(", ");
                }
            }

            return stringBuilder.ToString();
        }

       

        /// <summary>
        /// 判断一个 MethodInfo 是否为声明它的类的接口的方法的实现。
        /// </summary>
        public static bool IsFromInterfaceMethod(this MethodInfo method)
        {
            var declaringType = method.DeclaringType;
            if (declaringType == null)
            {
                return false;
            }

            var interfaceTypeList = declaringType.GetInterfaces();
            return interfaceTypeList.Select(interfaceType => declaringType.GetInterfaceMap(interfaceType))
                .Select(interfaceMapping => interfaceMapping.TargetMethods)
                .Any(targetMethods => targetMethods.Contains(method));
        }

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
                // 如果最初定义的类不是当前类的直接父类，则说明是 override 了从更上层继承的方法
                if (baseDefinitionDeclaringType != directBaseType)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
    }
}
