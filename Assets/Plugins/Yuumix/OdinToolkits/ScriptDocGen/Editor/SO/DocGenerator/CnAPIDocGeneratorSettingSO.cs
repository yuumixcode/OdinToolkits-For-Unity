using System.Globalization;
using System.Text;
using UnityEngine;
using Yuumix.OdinToolkits.AdvancedTypeAnalyzer;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGen.Editor
{
    /// <summary>
    /// 中文的 API 文档生成器
    /// </summary>
    public class CnAPIDocGeneratorSettingSO : DocGeneratorSettingSO
    {
        public override string GetGeneratedDoc(ITypeData data)
        {
            var sb = new StringBuilder();
            sb.Append(CreateIntroductionContent(data));
            sb.Append(CreateConstructorsContent(data.RuntimeReflectedConstructorsData));
            sb.Append(CreateEventsContent(data.RuntimeReflectedEventsData));
            sb.Append(CreateMethodsContent(data.RuntimeReflectedMethodsData));
            sb.Append(CreatePropertiesContent(data.RuntimeReflectedPropertiesData));
            sb.Append(CreateFieldsContent(data.RuntimeReflectedFieldsData));
            return sb.ToString();
        }

        public static StringBuilder CreateIntroductionContent(ITypeData typeData)
        {
            typeData.TryAsIMemberData(out var memberData);
            var sb = new StringBuilder();
            sb.AppendLine("# `" + memberData.Name + "`");
            sb.AppendLine();
            sb.AppendLine("## 介绍");
            sb.AppendLine();
            sb.Append("- 种类: `");
            var typeCategory = typeData.TypeCategory.ToString().ToLower(CultureInfo.CurrentCulture);
            if (typeData.IsStatic)
            {
                sb.Append("static " + typeCategory);
            }
            else if (typeData.IsAbstract)
            {
                sb.Append("abstract " + typeCategory);
            }
            else
            {
                sb.Append(typeCategory);
            }

            sb.AppendLine("`");
            sb.AppendLine($"- 所在程序集: `{typeData.AssemblyName}`");
            if (!typeData.NamespaceName.IsNullOrWhiteSpace())
            {
                sb.AppendLine($"- 所在命名空间: `{typeData.NamespaceName}`");
            }

            if (typeData.ReferenceWebLinkArray.Length >= 1)
            {
                for (var i = 0; i < typeData.ReferenceWebLinkArray.Length; i++)
                {
                    sb.AppendLine($"- 参考链接 [{i + 1}] : {typeData.ReferenceWebLinkArray[i]}");
                }
            }

            sb.AppendLine();
            sb.AppendLine("``` csharp");
            sb.AppendLine(typeData.FullDeclarationWithAttributes);
            sb.AppendLine("```");
            if (string.IsNullOrEmpty(memberData.SummaryAttributeValue))
            {
                sb.AppendLine();
                return sb;
            }

            sb.AppendLine();
            sb.AppendLine("### 注释");
            sb.AppendLine();
            if (!string.IsNullOrEmpty(memberData.SummaryAttributeValue))
            {
                sb.AppendLine("- " + memberData.SummaryAttributeValue);
            }

            sb.AppendLine();
            return sb;
        }

        public static StringBuilder CreateConstructorsContent(IConstructorData[] constructorDataArray)
        {
            var sb = new StringBuilder();
            if (constructorDataArray.Length <= 0)
            {
                return sb;
            }

            sb.AppendLine("## 构造方法");
            sb.AppendLine();
            sb.AppendLine("| 构造方法签名 [仅包含公共实例方法] | 注释 |");
            sb.AppendLine("| :--- | :--- |");

            foreach (var constructorData in constructorDataArray)
            {
                var fullSignature = constructorData.Signature;
                constructorData.TryAsIMemberData(out var memberData);
                if (memberData.IsObsolete)
                {
                    fullSignature = $"`[Obsolete] {fullSignature}`";
                }

                var comment = memberData.SummaryAttributeValue;
                sb.AppendLine("| " + $"`{fullSignature}`" + " | " + comment + " |");
            }

            sb.AppendLine();
            return sb;
        }

        public static StringBuilder CreateEventsContent(IEventData[] eventDataArray)
        {
            var sb = new StringBuilder();

            if (eventDataArray.Length <= 1)
            {
                return sb;
            }

            var hasAPI = false;
            var hasInheritedEvent = false;
            var hasNoInheritedEvent = false;

            foreach (var eventData in eventDataArray)
            {
                eventData.TryAsIMemberData(out var memberData);
                if (!hasAPI && eventData.IsApiMember())
                {
                    hasAPI = true;
                }

                if (!hasInheritedEvent)
                {
                    if (memberData.IsFromInheritance)
                    {
                        hasInheritedEvent = true;
                    }
                }

                if (!hasNoInheritedEvent)
                {
                    if (!memberData.IsFromInheritance)
                    {
                        hasNoInheritedEvent = true;
                    }
                }

                if (hasAPI && hasNoInheritedEvent && hasInheritedEvent)
                {
                    break;
                }
            }

            if (!hasAPI)
            {
                return sb;
            }

            sb.AppendLine("## 事件");
            sb.AppendLine();
            if (hasNoInheritedEvent)
            {
                sb.AppendLine("### 声明的事件");
                sb.AppendLine();
                sb.AppendLine("| 事件名称 | 注释 |");
                sb.AppendLine("| :--- | :--- | ");
                foreach (var eventData in eventDataArray)
                {
                    eventData.TryAsIMemberData(out var memberData);
                    if (!eventData.IsApiMember() || memberData.IsFromInheritance)
                    {
                        continue;
                    }

                    sb.AppendLine(
                        $"| `{eventData.Signature}` | {memberData.SummaryAttributeValue} |");
                }

                sb.AppendLine();
            }

            if (hasInheritedEvent)
            {
                sb.AppendLine("### 继承的事件");
                sb.AppendLine();
                sb.AppendLine("| 事件签名 | 注释 | 声明事件的类 |");
                sb.AppendLine("| :--- | :--- | :--- |");
                foreach (var eventData in eventDataArray)
                {
                    eventData.TryAsIMemberData(out var memberData);
                    if (!eventData.IsApiMember() || !memberData.IsFromInheritance)
                    {
                        continue;
                    }

                    sb.AppendLine(
                        $"| `{eventData.Signature}` | {memberData.SummaryAttributeValue} | `{memberData.DeclaringType}` |");
                }

                sb.AppendLine();
            }

            return sb;
        }

        public static StringBuilder CreateMethodsContent(IMethodData[] methodDataArray)
        {
            var sb = new StringBuilder();
            if (methodDataArray.Length <= 1)
            {
                return sb;
            }

            var hasAPI = false;
            var hasOperateMethod = false;
            var hasInheritMethod = false;
            var hasNoFromInheritMethodWithoutOperator = false;
            foreach (var methodData in methodDataArray)
            {
                if (!hasAPI)
                {
                    if (methodData.IsApiMember())
                    {
                        hasAPI = true;
                    }
                }

                if (!hasOperateMethod)
                {
                    if (methodData.IsOperator)
                    {
                        hasOperateMethod = true;
                    }
                }

                methodData.TryAsIMemberData(out var memberData);
                if (!hasInheritMethod)
                {
                    if (memberData.IsFromInheritance)
                    {
                        hasInheritMethod = true;
                    }
                }

                if (!hasNoFromInheritMethodWithoutOperator)
                {
                    if (!memberData.IsFromInheritance && !methodData.IsOperator)
                    {
                        hasNoFromInheritMethodWithoutOperator = true;
                    }
                }

                if (hasAPI && hasNoFromInheritMethodWithoutOperator && hasInheritMethod && hasOperateMethod)
                {
                    break;
                }
            }

            if (!hasAPI)
            {
                return sb;
            }

            sb.AppendLine("## 非构造方法");
            sb.AppendLine();
            if (hasNoFromInheritMethodWithoutOperator)
            {
                sb.AppendLine("### 声明的普通方法");
                sb.AppendLine();
                sb.AppendLine("| 普通方法名称 | 注释 |");
                sb.AppendLine("| :--- | :--- | ");
                foreach (var methodData in methodDataArray)
                {
                    methodData.TryAsIMemberData(out var memberData);
                    if (!methodData.IsApiMember() || methodData.IsOperator || memberData.IsFromInheritance)
                    {
                        continue;
                    }

                    sb.AppendLine(
                        $"| `{methodData.SignatureWithoutParameters}` | {memberData.SummaryAttributeValue} |");
                }

                sb.AppendLine();
            }

            if (hasInheritMethod)
            {
                sb.AppendLine("### 继承的普通方法");
                sb.AppendLine();
                sb.AppendLine("| 普通方法名称 | 注释 | 声明方法的类 |");
                sb.AppendLine("| :--- | :--- | :--- |");
                foreach (var methodData in methodDataArray)
                {
                    methodData.TryAsIMemberData(out var memberData);
                    if (!memberData.IsFromInheritance || (methodData.IsOperator && !methodData.IsFromAncestor))
                    {
                        continue;
                    }

                    sb.AppendLine(
                        $"| `{methodData.SignatureWithoutParameters}` | {memberData.SummaryAttributeValue} | `{memberData.DeclaringType}` |");
                }

                sb.AppendLine();
            }

            if (hasOperateMethod)
            {
                sb.AppendLine("### 运算符特殊方法");
                sb.AppendLine();
                sb.AppendLine("| 方法签名 | 注释 | 声明方法的类 |");
                sb.AppendLine("| :--- | :--- | :--- |");
                foreach (var methodData in methodDataArray)
                {
                    methodData.TryAsIMemberData(out var memberData);
                    if (!methodData.IsOperator)
                    {
                        continue;
                    }

                    sb.AppendLine(
                        $"| `{methodData.Signature}` | {memberData.SummaryAttributeValue} | `{memberData.DeclaringType}` |");
                }

                sb.AppendLine();
            }

            sb.AppendLine("### 所有方法签名总览");
            sb.AppendLine();
            sb.AppendLine("| 方法签名 |");
            sb.AppendLine("| :--- | ");
            foreach (var methodData in methodDataArray)
            {
                if (!methodData.IsApiMember())
                {
                    continue;
                }

                sb.AppendLine($"| `{methodData.Signature}` |");
            }

            sb.AppendLine();
            return sb;
        }

        public static StringBuilder CreatePropertiesContent(IPropertyData[] propertyDataArray)
        {
            var sb = new StringBuilder();
            if (propertyDataArray.Length <= 0)
            {
                return sb;
            }

            var hasAPI = false;
            var hasInheritedProperty = false;
            var hasNoInheritedProperty = false;

            foreach (var propertyData in propertyDataArray)
            {
                if (!hasAPI)
                {
                    if (propertyData.IsApiMember())
                    {
                        hasAPI = true;
                    }
                }

                propertyData.TryAsIMemberData(out var memberData);
                if (!hasInheritedProperty)
                {
                    if (memberData.IsFromInheritance)
                    {
                        hasInheritedProperty = true;
                    }
                }

                if (!hasNoInheritedProperty)
                {
                    if (!memberData.IsFromInheritance)
                    {
                        hasNoInheritedProperty = true;
                    }
                }

                if (hasAPI && hasNoInheritedProperty && hasInheritedProperty)
                {
                    break;
                }
            }

            if (!hasAPI)
            {
                return sb;
            }

            sb.AppendLine("## 属性");
            sb.AppendLine();
            if (hasNoInheritedProperty)
            {
                sb.AppendLine("### 声明的属性");
                sb.AppendLine();
                sb.AppendLine("| 属性签名 | 注释 |");
                sb.AppendLine("| :--- | :--- |");
                foreach (var propertyData in propertyDataArray)
                {
                    propertyData.TryAsIMemberData(out var memberData);
                    if (!propertyData.IsApiMember() || memberData.IsFromInheritance)
                    {
                        continue;
                    }

                    sb.AppendLine($"| `{propertyData.Signature}` | {memberData.SummaryAttributeValue} |");
                }

                sb.AppendLine();
            }

            if (hasInheritedProperty)
            {
                sb.AppendLine("### 继承的属性");
                sb.AppendLine();
                sb.AppendLine("| 属性签名 | 注释 | 声明属性的类 | ");
                sb.AppendLine("| :--- | :--- | :--- |");
                foreach (var propertyData in propertyDataArray)
                {
                    propertyData.TryAsIMemberData(out var memberData);
                    if (!propertyData.IsApiMember() || !memberData.IsFromInheritance)
                    {
                        continue;
                    }

                    sb.AppendLine(
                        $"| `{propertyData.Signature}` | {memberData.SummaryAttributeValue} | `{memberData.DeclaringType}` |");
                }

                sb.AppendLine();
            }

            return sb;
        }

        public static StringBuilder CreateFieldsContent(IFieldData[] fieldDataArray)
        {
            var sb = new StringBuilder();
            if (fieldDataArray.Length <= 0)
            {
                return sb;
            }

            var hasAPI = false;
            var hasConstField = false;
            var hasNoConstAndNoInheritedField = false;
            var hasNoConstAndInheritedField = false;

            foreach (var fieldData in fieldDataArray)
            {
                if (!hasAPI)
                {
                    if (fieldData.IsApiMember())
                    {
                        hasAPI = true;
                    }
                }

                if (!hasConstField)
                {
                    if (fieldData.IsConstant)
                    {
                        hasConstField = true;
                    }
                }

                fieldData.TryAsIMemberData(out var memberData);

                if (!hasNoConstAndInheritedField)
                {
                    if (memberData.IsFromInheritance && !fieldData.IsConstant)
                    {
                        hasNoConstAndInheritedField = true;
                    }
                }

                if (!hasNoConstAndNoInheritedField)
                {
                    if (!memberData.IsFromInheritance && !fieldData.IsConstant)
                    {
                        hasNoConstAndNoInheritedField = true;
                    }
                }

                if (hasAPI && hasConstField && hasNoConstAndNoInheritedField && hasNoConstAndInheritedField)
                {
                    break;
                }
            }

            if (!hasAPI)
            {
                return sb;
            }

            sb.AppendLine("## 字段");
            sb.AppendLine();
            if (hasConstField)
            {
                sb.AppendLine("### 常量字段");
                sb.AppendLine();
                sb.AppendLine("| 字段完整签名 | 注释 |");
                sb.AppendLine("| :--- | :--- |");
                foreach (var fieldData in fieldDataArray)
                {
                    fieldData.TryAsIMemberData(out var memberData);
                    if (!fieldData.IsConstant || !fieldData.IsApiMember())
                    {
                        continue;
                    }

                    sb.AppendLine(
                        $"| `{fieldData.Signature}` | {memberData.SummaryAttributeValue} |");
                }

                sb.AppendLine();
            }

            if (hasNoConstAndNoInheritedField)
            {
                sb.AppendLine("### 声明的普通字段");
                sb.AppendLine();
                sb.AppendLine("| 字段名称 | 注释 | ");
                sb.AppendLine("| :--- | :--- | ");
                foreach (var fieldData in fieldDataArray)
                {
                    fieldData.TryAsIMemberData(out var memberData);
                    if (!fieldData.IsApiMember() || memberData.IsFromInheritance || fieldData.IsConstant)
                    {
                        continue;
                    }

                    sb.AppendLine($"| `{fieldData.Signature}` | {memberData.SummaryAttributeValue} |");
                }

                sb.AppendLine();
            }

            if (hasNoConstAndInheritedField)
            {
                sb.AppendLine("### 继承的普通字段");
                sb.AppendLine();
                sb.AppendLine("| 字段名称 | 注释 | 声明字段的类 |");
                sb.AppendLine("| :--- | :--- | :--- |");
                foreach (var fieldData in fieldDataArray)
                {
                    fieldData.TryAsIMemberData(out var memberData);
                    if (!fieldData.IsApiMember() || !memberData.IsFromInheritance || fieldData.IsConstant)
                    {
                        continue;
                    }

                    sb.AppendLine(
                        $"| `{fieldData.Signature}` | {memberData.SummaryAttributeValue} | `{memberData.DeclaringType}` |");
                }

                sb.AppendLine();
            }

            return sb;
        }
    }
}
