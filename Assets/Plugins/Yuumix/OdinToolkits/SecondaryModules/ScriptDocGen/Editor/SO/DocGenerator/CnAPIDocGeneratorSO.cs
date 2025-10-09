using System.Globalization;
using System.Text;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor
{
    /// <summary>
    /// 中文的 API 文档生成器
    /// </summary>
    public class CnAPIDocGeneratorSO : DocGeneratorSO
    {
        public override string GetGeneratedDoc(TypeAnalysisData data)
        {
            var sb = new StringBuilder();
            sb.Append(CreateIntroductionContent(data));
            sb.Append(CreateConstructorsContent(data));
            sb.Append(CreateMethodsContent(data));
            sb.Append(CreateEventsContent(data));
            sb.Append(CreatePropertiesContent(data));
            sb.Append(CreateFieldsContent(data));
            sb.Append(ScriptDocGenInspectorSO.UserIdentifierParagraph);
            return sb.ToString();
        }

        public static StringBuilder CreateIntroductionContent(TypeAnalysisData data)
        {
            var sb = new StringBuilder();
            sb.AppendLine("# `" + data.typeName + "`");
            sb.AppendLine();
            sb.AppendLine("## 介绍");
            sb.AppendLine();
            sb.Append("- 种类: `");
            string typeCategory = data.typeCategory.ToString().ToLower(CultureInfo.CurrentCulture);
            if (data.isStatic)
            {
                sb.Append("static " + typeCategory);
            }
            else if (data.isAbstract)
            {
                sb.Append("abstract " + typeCategory);
            }
            else
            {
                sb.Append(typeCategory);
            }

            sb.AppendLine("`");
            sb.AppendLine($"- 所在程序集: `{data.assemblyName}`");
            if (!data.namespaceName.IsNullOrWhiteSpace())
            {
                sb.AppendLine($"- 所在命名空间: `{data.namespaceName}`");
            }

            if (data.referenceWebLinkArray.Length >= 1)
            {
                for (var i = 0; i < data.referenceWebLinkArray.Length; i++)
                {
                    sb.AppendLine($"- 参考链接 [{i + 1}] : {data.referenceWebLinkArray[i]}");
                }
            }

            sb.AppendLine();
            sb.AppendLine("``` csharp");
            sb.AppendLine(data.typeDeclaration);
            sb.AppendLine("```");
            if (string.IsNullOrEmpty(data.chineseDescription))
            {
                sb.AppendLine();
                return sb;
            }

            sb.AppendLine();
            sb.AppendLine("### 注释");
            sb.AppendLine();
            if (!string.IsNullOrEmpty(data.chineseDescription))
            {
                sb.AppendLine("- " + data.chineseDescription);
            }

            sb.AppendLine();
            return sb;
        }

        public static StringBuilder CreateConstructorsContent(TypeAnalysisData data)
        {
            var sb = new StringBuilder();
            ConstructorAnalysisData[] constructors = data.constructorAnalysisDataArray;
            if (data.isStatic || constructors.Length <= 0)
            {
                return sb;
            }

            sb.AppendLine("## 构造方法");
            sb.AppendLine();
            sb.AppendLine("| 构造方法签名 [仅包含公共实例方法] | 注释 |");
            sb.AppendLine("| :--- | :--- |");

            foreach (ConstructorAnalysisData constructor in constructors)
            {
                string fullSignature = constructor.fullSignature;
                if (constructor.isObsolete)
                {
                    fullSignature = $"`[Obsolete] {fullSignature}`";
                }

                string comment = constructor.chineseSummary;
                sb.AppendLine("| " + $"`{fullSignature}`" + " | " + comment + " |");
            }

            sb.AppendLine();
            return sb;
        }

        public static StringBuilder CreateMethodsContent(TypeAnalysisData data)
        {
            var sb = new StringBuilder();
            MethodAnalysisData[] methods = data.methodAnalysisDataArray;
            if (methods.Length <= 1)
            {
                return sb;
            }

            var hasAPI = false;
            var hasOperateMethod = false;
            var hasInheritMethod = false;
            var hasNoFromInheritMethodWithoutOperator = false;
            foreach (MethodAnalysisData d in methods)
            {
                if (!hasAPI)
                {
                    if (d.IsAPI())
                    {
                        hasAPI = true;
                    }
                }

                if (!hasOperateMethod)
                {
                    if (d.isOperator)
                    {
                        hasOperateMethod = true;
                    }
                }

                if (!hasInheritMethod)
                {
                    if (d.IsFromInheritMember())
                    {
                        hasInheritMethod = true;
                    }
                }

                if (!hasNoFromInheritMethodWithoutOperator)
                {
                    if (!d.IsFromInheritMember() && !d.isOperator)
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
                foreach (MethodAnalysisData t in methods)
                {
                    if (!t.IsAPI() || t.isOperator || t.IsFromInheritMember())
                    {
                        continue;
                    }

                    sb.AppendLine($"| `{t.partSignature}` | {t.chineseSummary} |");
                }

                sb.AppendLine();
            }

            if (hasInheritMethod)
            {
                sb.AppendLine("### 继承的普通方法");
                sb.AppendLine();
                sb.AppendLine("| 普通方法名称 | 注释 | 声明方法的类 |");
                sb.AppendLine("| :--- | :--- | :--- |");
                foreach (MethodAnalysisData t in methods)
                {
                    if (!t.IsFromInheritMember() || (t.isOverride && !t.isFromAncestor))
                    {
                        continue;
                    }

                    sb.AppendLine($"| `{t.partSignature}` | {t.chineseSummary} | `{t.declaringType}` |");
                }

                sb.AppendLine();
            }

            if (hasOperateMethod)
            {
                sb.AppendLine("### 运算符特殊方法");
                sb.AppendLine();
                sb.AppendLine("| 方法签名 | 注释 | 声明方法的类 |");
                sb.AppendLine("| :--- | :--- | :--- |");
                foreach (MethodAnalysisData t in methods)
                {
                    if (!t.isOperator)
                    {
                        continue;
                    }

                    sb.AppendLine($"| `{t.fullSignature}` | {t.chineseSummary} | `{t.declaringType}` |");
                }

                sb.AppendLine();
            }

            sb.AppendLine("### 所有方法签名总览");
            sb.AppendLine();
            sb.AppendLine("| 方法签名 |");
            sb.AppendLine("| :--- | ");
            foreach (MethodAnalysisData t in methods)
            {
                if (!t.IsAPI())
                {
                    continue;
                }

                sb.AppendLine($"| `{t.fullSignature}` |");
            }

            sb.AppendLine();
            return sb;
        }

        public static StringBuilder CreateEventsContent(TypeAnalysisData data)
        {
            var sb = new StringBuilder();
            EventAnalysisData[] events = data.eventAnalysisDataArray;
            if (events.Length <= 1)
            {
                return sb;
            }

            var hasAPI = false;
            var hasInheritedEvent = false;
            var hasNoInheritedEvent = false;

            foreach (EventAnalysisData t in events)
            {
                if (!hasAPI)
                {
                    if (t.IsAPI())
                    {
                        hasAPI = true;
                    }
                }

                if (!hasInheritedEvent)
                {
                    if (t.IsFromInheritMember())
                    {
                        hasInheritedEvent = true;
                    }
                }

                if (!hasNoInheritedEvent)
                {
                    if (!t.IsFromInheritMember())
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
                foreach (EventAnalysisData t in events)
                {
                    if (!t.IsAPI() || t.IsFromInheritMember())
                    {
                        continue;
                    }

                    sb.AppendLine(
                        $"| `{t.partSignature}` | {t.chineseSummary} |");
                }

                sb.AppendLine();
            }

            if (hasInheritedEvent)
            {
                sb.AppendLine("### 继承的事件");
                sb.AppendLine();
                sb.AppendLine("| 事件签名 | 注释 | 声明事件的类 |");
                sb.AppendLine("| :--- | :--- | :--- |");
                foreach (EventAnalysisData t in events)
                {
                    if (!t.IsAPI() || !t.IsFromInheritMember())
                    {
                        continue;
                    }

                    sb.AppendLine(
                        $"| `{t.fullSignature}` | {t.chineseSummary} | `{t.declaringType}` |");
                }

                sb.AppendLine();
            }

            return sb;
        }

        public static StringBuilder CreatePropertiesContent(TypeAnalysisData data)
        {
            var sb = new StringBuilder();
            PropertyAnalysisData[] properties = data.propertyAnalysisDataArray;
            if (properties.Length <= 0)
            {
                return sb;
            }

            var hasAPI = false;
            var hasInheritedProperty = false;
            var hasNoInheritedProperty = false;

            foreach (PropertyAnalysisData property in properties)
            {
                if (!hasAPI)
                {
                    if (property.IsAPI())
                    {
                        hasAPI = true;
                    }
                }

                if (!hasInheritedProperty)
                {
                    if (property.IsFromInheritMember())
                    {
                        hasInheritedProperty = true;
                    }
                }

                if (!hasNoInheritedProperty)
                {
                    if (!property.IsFromInheritMember())
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
                sb.AppendLine("| 属性名称 | 注释 |");
                sb.AppendLine("| :--- | :--- |");
                foreach (PropertyAnalysisData property in properties)
                {
                    if (!property.IsAPI() || property.IsFromInheritMember())
                    {
                        continue;
                    }

                    sb.AppendLine($"| `{property.partSignature}` | {property.chineseSummary} |");
                }

                sb.AppendLine();
            }

            if (hasInheritedProperty)
            {
                sb.AppendLine("### 继承的属性");
                sb.AppendLine();
                sb.AppendLine("| 属性名称 | 注释 | 声明属性的类 | ");
                sb.AppendLine("| :--- | :--- | :--- |");
                foreach (PropertyAnalysisData property in properties)
                {
                    if (!property.IsAPI() || !property.IsFromInheritMember())
                    {
                        continue;
                    }

                    sb.AppendLine(
                        $"| `{property.partSignature}` | {property.chineseSummary} | `{property.declaringType}` |");
                }

                sb.AppendLine();
            }

            sb.AppendLine("### 所有属性签名总览");
            sb.AppendLine();
            sb.AppendLine("| 属性签名 |");
            sb.AppendLine("| :--- | ");
            foreach (PropertyAnalysisData property in properties)
            {
                if (!property.IsAPI())
                {
                    continue;
                }

                sb.AppendLine($"| `{property.fullSignature}` |");
            }

            return sb;
        }

        public static StringBuilder CreateFieldsContent(TypeAnalysisData data)
        {
            var sb = new StringBuilder();
            FieldAnalysisData[] fields = data.fieldAnalysisDataArray;
            if (fields.Length <= 0)
            {
                return sb;
            }

            var hasAPI = false;
            var hasConstField = false;
            var hasNoConstAndNoInheritedField = false;
            var hasNoConstAndInheritedField = false;

            foreach (FieldAnalysisData field in fields)
            {
                if (!hasAPI)
                {
                    if (field.IsAPI())
                    {
                        hasAPI = true;
                    }
                }

                if (!hasConstField)
                {
                    if (field.isConst)
                    {
                        hasConstField = true;
                    }
                }

                if (!hasNoConstAndInheritedField)
                {
                    if (field.IsFromInheritMember() && !field.isConst)
                    {
                        hasNoConstAndInheritedField = true;
                    }
                }

                if (!hasNoConstAndNoInheritedField)
                {
                    if (!field.IsFromInheritMember() && !field.isConst)
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
                foreach (FieldAnalysisData field in fields)
                {
                    if (!field.isConst || !field.IsAPI())
                    {
                        continue;
                    }

                    sb.AppendLine(
                        $"| `{field.fullSignature}` | {field.chineseSummary} |");
                }

                sb.AppendLine();
            }

            if (hasNoConstAndNoInheritedField)
            {
                sb.AppendLine("### 声明的普通字段");
                sb.AppendLine();
                sb.AppendLine("| 字段名称 | 注释 | ");
                sb.AppendLine("| :--- | :--- | ");
                foreach (FieldAnalysisData field in fields)
                {
                    if (!field.IsAPI() || field.IsFromInheritMember() || field.isConst)
                    {
                        continue;
                    }

                    sb.AppendLine($"| `{field.fullSignature}` | {field.chineseSummary} |");
                }

                sb.AppendLine();
            }

            if (hasNoConstAndInheritedField)
            {
                sb.AppendLine("### 继承的普通字段");
                sb.AppendLine();
                sb.AppendLine("| 字段名称 | 注释 | 声明字段的类 |");
                sb.AppendLine("| :--- | :--- | :--- |");
                foreach (FieldAnalysisData field in fields)
                {
                    if (!field.IsAPI() || !field.IsFromInheritMember() || field.isConst)
                    {
                        continue;
                    }

                    sb.AppendLine(
                        $"| `{field.fullSignature}` | {field.chineseSummary} | `{field.declaringType}` |");
                }

                sb.AppendLine();
            }

            return sb;
        }
    }
}
