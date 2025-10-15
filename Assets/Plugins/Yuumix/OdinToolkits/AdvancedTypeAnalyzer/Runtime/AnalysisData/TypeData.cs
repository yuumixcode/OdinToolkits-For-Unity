using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.AdvancedTypeAnalyzer
{
    public interface ITypeData : IDerivedMemberData
    {
        TypeCategory TypeCategory { get; }
        Assembly Assembly { get; }
        string AssemblyName { get; }
        string NamespaceName { get; }
        bool IsGenericType { get; }
        public bool IsSealed { get; }
        public bool IsAbstract { get; }
        public string[] ReferenceWebLinkArray { get; }
        public string[] InheritanceChain { get; }
        public string[] InterfaceArray { get; }
        IAnalysisDataFactory DataFactory { get; }
        IConstructorData[] RuntimeReflectedConstructorsData { get; }
        IMethodData[] RuntimeReflectedMethodsData { get; }
        IEventData[] RuntimeReflectedEventsData { get; }
        IPropertyData[] RuntimeReflectedPropertiesData { get; }
        IFieldData[] RuntimeReflectedFieldsData { get; }
        string GetTypeFullSignature(Type type, string accessModifierName, TypeCategory category);
    }

    [Serializable]
    public class TypeData : MemberData, ITypeData
    {
        public TypeData(Type type, IAttributeFilter filter = null, IAnalysisDataFactory factory = null)
            : base(type, filter)
        {
            IsStatic = type.IsStatic();
            MemberType = type.MemberType;
            MemberTypeName = MemberType.ToString();
            AccessModifier = type.GetTypeAccessModifier();
            AccessModifierName = AccessModifier.ConvertToString();
            // ---
            DataFactory = factory ?? new YuumixDefaultAnalysisDataFactory();
            TypeInfo = type.GetTypeInfo();
            TypeCategory = type.GetTypeCategory();
            Assembly = type.Assembly;
            AssemblyName = Assembly.GetName().Name;
            NamespaceName = type.Namespace;
            IsGenericType = type.IsGenericType;
            IsSealed = type.IsSealed;
            IsAbstract = type.IsAbstract;
            ReferenceWebLinkArray = type.GetReferenceLinks();
            InheritanceChain = type.GetInheritanceChain();
            InterfaceArray = type.GetInterfaceArray();
            Signature = GetTypeFullSignature(type, AccessModifierName, TypeCategory);
            FullDeclarationWithAttributes = AttributesDeclaration + Signature;
            // ---
            RuntimeReflectedConstructorsData = type.GetConstructors()
                .Select(c => DataFactory.CreateConstructorData(c))
                .OrderBy(data => data, new DerivedMemberDataComparer())
                .ToArray();
            RuntimeReflectedMethodsData = type.GetRuntimeMethods()
                .Where(x => x != null
                            && !x.Name.Contains("add_") && !x.Name.Contains("remove_")
                            && !x.Name.Contains("get_") && !x.Name.Contains("set_"))
                .Select(m => DataFactory.CreateMethodData(m))
                .OrderBy(data => data, new DerivedMemberDataComparer())
                .ToArray();
            RuntimeReflectedEventsData = type.GetRuntimeEvents()
                .Select(e => DataFactory.CreateEventData(e))
                .OrderBy(data => data, new DerivedMemberDataComparer())
                .ToArray();
            RuntimeReflectedPropertiesData = type.GetRuntimeProperties()
                .Select(p => DataFactory.CreatePropertyData(p))
                .OrderBy(data => data, new DerivedMemberDataComparer())
                .ToArray();
            RuntimeReflectedFieldsData = type.GetUserDefinedFields()
                .Where(f => f != null
                            && !f.IsSpecialName
                            && !f.Name.Contains("k__BackingField") && !f.Name.Contains("__BackingField"))
                .Select(f => DataFactory.CreateFieldData(f))
                .OrderBy(data => data, new DerivedMemberDataComparer())
                .ToArray();
            // --- Post Process
            MarkOverloadMethod(RuntimeReflectedMethodsData);
        }

        TypeInfo TypeInfo { get; }

        #region ITypeData

        [PropertyOrder(-5)]
        [ShowEnableProperty]
        [BilingualText("Type 种类", nameof(TypeCategory))]
        public TypeCategory TypeCategory { get; }

        public Assembly Assembly { get; }

        [PropertyOrder(-5)]
        [ShowEnableProperty]
        [BilingualText("程序集名称", nameof(AssemblyName))]
        public string AssemblyName { get; }

        [PropertyOrder(-5)]
        [ShowEnableProperty]
        [BilingualText("命名空间名称", nameof(NamespaceName))]
        public string NamespaceName { get; }

        public bool IsGenericType { get; }
        public bool IsSealed { get; }
        public bool IsAbstract { get; }

        [PropertyOrder(150)]
        [ShowEnableProperty]
        [BilingualText("引用链接", nameof(ReferenceWebLinkArray))]
        [HideDuplicateReferenceBox]
        public string[] ReferenceWebLinkArray { get; }

        [PropertyOrder(150)]
        [ShowEnableProperty]
        [BilingualText("继承链", nameof(InheritanceChain))]
        [HideDuplicateReferenceBox]
        public string[] InheritanceChain { get; }

        [PropertyOrder(150)]
        [ShowEnableProperty]
        [BilingualText("接口列表", nameof(InterfaceArray))]
        [HideDuplicateReferenceBox]
        public string[] InterfaceArray { get; }

        public IAnalysisDataFactory DataFactory { get; }

        [PropertyOrder(200)]
        [ShowEnableProperty]
        [BilingualText("声明的构造方法解析数据", nameof(RuntimeReflectedConstructorsData))]
        public IConstructorData[] RuntimeReflectedConstructorsData { get; }

        [PropertyOrder(200)]
        [ShowEnableProperty]
        [BilingualText("声明的方法解析数据", nameof(RuntimeReflectedMethodsData))]
        public IMethodData[] RuntimeReflectedMethodsData { get; }

        [PropertyOrder(200)]
        [ShowEnableProperty]
        [BilingualText("声明的事件解析数据", nameof(RuntimeReflectedEventsData))]
        public IEventData[] RuntimeReflectedEventsData { get; }

        [PropertyOrder(200)]
        [ShowEnableProperty]
        [BilingualText("声明的属性解析数据", nameof(RuntimeReflectedPropertiesData))]
        public IPropertyData[] RuntimeReflectedPropertiesData { get; }

        [PropertyOrder(200)]
        [ShowEnableProperty]
        [BilingualText("声明的字段解析数据", nameof(RuntimeReflectedFieldsData))]
        public IFieldData[] RuntimeReflectedFieldsData { get; }

        public string GetTypeFullSignature(Type type, string accessModifierName, TypeCategory category)
        {
            var sb = new StringBuilder();
            sb.Append(accessModifierName).Append(" ");
            if (type.IsStatic())
            {
                sb.Append("static ");
            }
            else if (type.IsAbstract && !type.IsInterface)
            {
                sb.Append("abstract ");
            }
            else if (type.IsSealed && !type.IsEnum && !type.IsDelegate() && !type.IsInterface &&
                     !type.IsRecordStruct() && category != TypeCategory.Struct)
            {
                sb.Append("sealed ");
            }

            sb.Append(category.ToString().ToLower()).Append(" ");
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
                sb.Append(")");
            }
            else
            {
                if (type.IsRecordStruct())
                {
                    sb.Append("struct ");
                }

                // 5. 添加类名（处理泛型）
                sb.Append(type.GetReadableTypeName());

                // 6. 添加基类和接口，忽略默认继承的 object 类型
                var inheritTypes = new List<string>();
                if (type.BaseType != null && type.BaseType != typeof(object))
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
                    for (var i = 0; i < inheritTypes.Count; i++)
                    {
                        sb.Append(inheritTypes[i]);
                        if (i < inheritTypes.Count - 1)
                        {
                            sb.AppendLine(", ");
                        }
                    }
                }

                // 7. 添加泛型约束
                if (type.IsGenericType)
                {
                    sb.Append(" " + type.GetGenericConstraintsString(true));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 标记类型中存在的重载方法，加上 [Overload] 前缀
        /// </summary>
        public static IMethodData[] MarkOverloadMethod(IMethodData[] methodAnalysisDataArray)
        {
            for (var i = 0; i < methodAnalysisDataArray.Length; i++)
            {
                for (var j = 0; j < methodAnalysisDataArray.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if (methodAnalysisDataArray[i].SignatureWithoutParameters ==
                        methodAnalysisDataArray[j].SignatureWithoutParameters)
                    {
                        methodAnalysisDataArray[i].IsOverloadMethodInDeclaringType = true;
                        methodAnalysisDataArray[j].IsOverloadMethodInDeclaringType = true;
                        methodAnalysisDataArray[i].AddOverloadPrefix();
                        methodAnalysisDataArray[j].AddOverloadPrefix();
                    }
                }
            }

            return methodAnalysisDataArray;
        }

        #endregion

        #region IDerivedMemberData

        public bool IsStatic { get; }
        public MemberTypes MemberType { get; }
        public string MemberTypeName { get; }
        public AccessModifierType AccessModifier { get; }
        public string AccessModifierName { get; }
        public string Signature { get; }

        [PropertyOrder(90)]
        [ShowEnableProperty]
        [BilingualTitle("完整类型声明 - 包含特性和签名 - 默认剔除 [Summary] 特性",
            nameof(FullDeclarationWithAttributes) + " - Include Attributes and Signature - Default Exclude [Summary]")]
        [HideLabel]
        [MultiLineProperty]
        public string FullDeclarationWithAttributes { get; }

        #endregion
    }
}
