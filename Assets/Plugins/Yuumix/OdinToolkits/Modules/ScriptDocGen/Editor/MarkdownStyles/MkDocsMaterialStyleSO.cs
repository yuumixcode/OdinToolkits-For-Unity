using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Modules.Editor;

namespace Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor
{
    public class MkDocsMaterialStyleSO : MarkdownStyleSO
    {
        protected override StringBuilder CreateHeaderIntroduction(TypeData data)
        {
            var sb = new StringBuilder();
            if (data.IsStatic)
            {
                sb.AppendLine(
                    $"# `{data.TypeName} static {data.TypeCategory.ToString().ToLower(CultureInfo.CurrentCulture)}`");
            }
            else if (data.IsAbstract)
            {
                sb.AppendLine(
                    $"# `{data.TypeName} abstract {data.TypeCategory.ToString().ToLower(CultureInfo.CurrentCulture)}`");
            }
            else
            {
                sb.AppendLine(
                    $"# `{data.TypeName} {data.TypeCategory.ToString().ToLower(CultureInfo.CurrentCulture)}`");
            }

            sb.AppendLine();
            sb.AppendLine("## Introduction");
            sb.AppendLine();
            if (!data.NamespaceName.IsNullOrWhiteSpace())
            {
                sb.AppendLine($"- Namespace: `{data.NamespaceName}`");
            }

            sb.AppendLine($"- Assembly: `{data.AssemblyName}`");
            if (data.SeeAlsoLinks.Length >= 1)
            {
                for (var i = 0; i < data.SeeAlsoLinks.Length; i++)
                {
                    sb.AppendLine($"- See Also [{i + 1}] : {data.SeeAlsoLinks[i]}");
                }
            }

            sb.AppendLine();
            sb.AppendLine("``` csharp");
            sb.AppendLine(data.TypeDeclaration);
            sb.AppendLine("```");
            if (string.IsNullOrEmpty(data.ChineseComment) && string.IsNullOrEmpty(data.EnglishComment))
            {
                sb.AppendLine();
                return sb;
            }

            sb.AppendLine("### Description");
            sb.AppendLine();
            if (!string.IsNullOrEmpty(data.ChineseComment))
            {
                sb.AppendLine("- " + data.ChineseComment);
            }

            if (!string.IsNullOrEmpty(data.EnglishComment))
            {
                sb.AppendLine("- " + data.EnglishComment);
            }

            sb.AppendLine();
            return sb;
        }

        protected override StringBuilder CreateConstructorsContent(TypeData data, DocCategory docCategory)
        {
            var sb = new StringBuilder();
            if (data.IsStatic || data.Constructors.Length <= 0)
            {
                return sb;
            }

            if (docCategory == DocCategory.ScriptingAPI)
            {
                if (!data.Constructors.Any(x => x.IsAPI))
                {
                    return sb;
                }

                AppendHeader();
                AppendConstructorLine(sb, data.Constructors.Where(x => !x.isObsolete && x.IsAPI));
            }
            else
            {
                AppendHeader();
                AppendConstructorLine(sb, data.Constructors.Where(x => !x.isObsolete));
            }

            if (!data.Constructors.Any(x => x.isObsolete))
            {
                sb.AppendLine();
                return sb;
            }

            if (docCategory == DocCategory.ScriptingAPI)
            {
                AppendConstructorLine(sb, data.Constructors.Where(x => x.isObsolete && x.IsAPI), true);
            }
            else
            {
                AppendConstructorLine(sb, data.Constructors.Where(x => x.isObsolete), true);
            }

            sb.AppendLine();
            return sb;

            void AppendHeader()
            {
                sb.AppendLine("## Constructors");
                sb.AppendLine();
                sb.AppendLine("| 构造函数签名 |");
                sb.AppendLine("| :--- |");
            }

            void AppendConstructorLine(StringBuilder constructorSb, IEnumerable<MemberData> items,
                bool addObsoleteSign = false)
            {
                foreach (MemberData item in items)
                {
                    string fullSignature = item.fullSignature;
                    if (addObsoleteSign)
                    {
                        fullSignature = $"[Obsolete] {fullSignature}";
                    }

                    constructorSb.AppendLine("| " + $"`{fullSignature}`" + " |");
                }
            }
        }

        protected override StringBuilder CreateCurrentMethodsContent(TypeData data, DocCategory docCategory)
        {
            var sb = new StringBuilder();
            if (data.CurrentMethods.Length <= 0)
            {
                return sb;
            }

            MethodData[] hasRemovedLocalMethods = data.CurrentMethods;
            hasRemovedLocalMethods = hasRemovedLocalMethods.Where(x => !(x.name.Contains("<") && x.name.Contains(">")))
                .ToArray();

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    if (!hasRemovedLocalMethods.Any(x => x.IsAPI))
                    {
                        return sb;
                    }

                    AppendHeader();
                    AppendMemberLine(sb, hasRemovedLocalMethods.Where(x => !x.isObsolete && x.IsAPI));
                    break;
                case DocCategory.CompleteReferences:
                    AppendHeader();
                    AppendMemberLine(sb, hasRemovedLocalMethods.Where(x => !x.isObsolete));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            if (!hasRemovedLocalMethods.Any(x => x.isObsolete))
            {
                sb.AppendLine();
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    AppendMemberLine(sb, hasRemovedLocalMethods.Where(x => x.isObsolete && x.IsAPI), true);
                    break;
                case DocCategory.CompleteReferences:
                    AppendMemberLine(sb, hasRemovedLocalMethods.Where(x => x.isObsolete), true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            sb.AppendLine();
            return sb;

            void AppendHeader()
            {
                sb.AppendLine("## Methods");
                sb.AppendLine();
                sb.AppendLine("| 方法 | 注释 | Comment |");
                sb.AppendLine("| :--- | :--- | :--- |");
            }
        }

        protected override StringBuilder CreateCurrentEventsContent(TypeData data, DocCategory docCategory)
        {
            var sb = new StringBuilder();
            if (data.CurrentEvents.Length <= 0)
            {
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    if (!data.CurrentEvents.Any(x => x.IsAPI))
                    {
                        // YuumixLogger.EditorLog("!data.CurrentEvents.Any(x => x.IsAPI)");
                        return sb;
                    }

                    AppendHeader();
                    AppendMemberLine(sb, data.CurrentEvents.Where(x => !x.isObsolete && x.IsAPI));
                    break;
                case DocCategory.CompleteReferences:
                    AppendHeader();
                    AppendMemberLine(sb, data.CurrentEvents.Where(x => !x.isObsolete));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            if (!data.CurrentEvents.Any(x => x.isObsolete))
            {
                // YuumixLogger.EditorLog("!data.CurrentEvents.Any(x => x.isObsolete)");
                sb.AppendLine();
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    AppendMemberLine(sb, data.CurrentEvents.Where(x => x.isObsolete && x.IsAPI), true);
                    break;
                case DocCategory.CompleteReferences:
                    AppendMemberLine(sb, data.CurrentEvents.Where(x => x.isObsolete), true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            sb.AppendLine();
            return sb;

            void AppendHeader()
            {
                sb.AppendLine("## Events");
                sb.AppendLine();
                sb.AppendLine("| 事件 | 注释 | Comment |");
                sb.AppendLine("| :--- | :--- | :--- |");
            }
        }

        protected override StringBuilder CreateCurrentPropertiesContent(TypeData data, DocCategory docCategory)
        {
            var sb = new StringBuilder();
            if (data.CurrentProperties.Length <= 0)
            {
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    if (!data.CurrentProperties.Any(x => x.IsAPI))
                    {
                        // YuumixLogger.EditorLog("!data.CurrentProperties.Any(x => x.IsAPI)");
                        return sb;
                    }

                    AppendHeader();
                    AppendMemberLine(sb, data.CurrentProperties.Where(x => !x.isObsolete && x.IsAPI));
                    break;
                case DocCategory.CompleteReferences:
                    AppendHeader();
                    AppendMemberLine(sb, data.CurrentProperties.Where(x => !x.isObsolete));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            if (!data.CurrentProperties.Any(x => x.isObsolete))
            {
                // YuumixLogger.EditorLog("!data.CurrentProperties.Any(x => x.isObsolete)");
                sb.AppendLine();
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    AppendMemberLine(sb, data.CurrentProperties.Where(x => x.isObsolete && x.IsAPI), true);
                    break;
                case DocCategory.CompleteReferences:
                    AppendMemberLine(sb, data.CurrentProperties.Where(x => x.isObsolete), true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            sb.AppendLine();
            return sb;

            void AppendHeader()
            {
                sb.AppendLine("## Properties");
                sb.AppendLine();
                sb.AppendLine("| 属性 | 注释 | Comment |");
                sb.AppendLine("| :--- | :--- | :--- |");
            }
        }

        protected override StringBuilder CreateCurrentFieldsContent(TypeData data, DocCategory docCategory)
        {
            var sb = new StringBuilder();
            if (data.CurrentFields.Length <= 0)
            {
                return sb;
            }

            if (data.TypeCategory == TypeCategory.Enum)
            {
                sb.AppendLine("## Enum Value");
                sb.AppendLine();
                sb.AppendLine("| 枚举值 | 注释 | Comment|");
                sb.AppendLine("| :--- | :--- | :---| ");
                foreach (FieldData fieldData in data.CurrentFields)
                {
                    sb.AppendLine("| " + $"`{fieldData.name}`" + " | " + fieldData.chineseSummary + " | " +
                                  $"`{fieldData.englishSummary}`" + " |");
                }

                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    if (!data.CurrentFields.Any(x => x.IsAPI))
                    {
                        // YuumixLogger.EditorLog("!data.CurrentFields.Any(x => x.IsAPI)");
                        return sb;
                    }

                    AppendHeader();
                    AppendMemberLine(sb, data.CurrentFields.Where(x => !x.isObsolete && x.IsAPI));
                    break;
                case DocCategory.CompleteReferences:
                    AppendHeader();
                    AppendMemberLine(sb, data.CurrentFields.Where(x => !x.isObsolete));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            if (!data.CurrentFields.Any(x => x.isObsolete))
            {
                // YuumixLogger.EditorLog("!data.CurrentFields.Any(x => x.isObsolete)");
                sb.AppendLine();
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    AppendMemberLine(sb, data.CurrentFields.Where(x => x.isObsolete && x.IsAPI), true);
                    break;
                case DocCategory.CompleteReferences:
                    AppendMemberLine(sb, data.CurrentFields.Where(x => x.isObsolete), true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            sb.AppendLine();
            return sb;

            void AppendHeader()
            {
                sb.AppendLine("## Fields");
                sb.AppendLine();
                sb.AppendLine("| 字段 | 注释 | Comment |");
                sb.AppendLine("| :--- | :--- | :--- |");
            }
        }

        protected override StringBuilder CreateInheritanceContent(TypeData data, DocCategory docCategory)
        {
            var sb = new StringBuilder();
            if (IsNonExistInheritedMember(data))
            {
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    if (IsNonExistAPIMember(data))
                    {
                        // YuumixLogger.EditorLog("IsNonExistAPIMember(data)");
                        return sb;
                    }

                    AppendInheritedHeader(sb);
                    CreateInheritedMemberString(data, sb, x => !x.isObsolete && x.IsAPI);
                    break;
                case DocCategory.CompleteReferences:
                    AppendInheritedHeader(sb);
                    CreateInheritedMemberString(data, sb, x => !x.isObsolete);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            if (IsNonExistObsoleteInheritedMember(data))
            {
                // YuumixLogger.EditorLog("IsNonExistObsoleteInheritedMember(data)");
                sb.AppendLine();
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    CreateInheritedMemberString(data, sb, x => x.isObsolete && x.IsAPI, true);
                    break;
                case DocCategory.CompleteReferences:
                    CreateInheritedMemberString(data, sb, x => x.isObsolete, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            sb.AppendLine();
            return sb;

            bool IsNonExistInheritedMember(TypeData dataArg)
            {
                return dataArg.InheritedMethods.Length <= 0 && dataArg.InheritedProperties.Length <= 0 &&
                       dataArg.InheritedEvents.Length <= 0 && dataArg.InheritedFields.Length <= 0;
            }

            bool IsNonExistObsoleteInheritedMember(TypeData dataArg)
            {
                return !dataArg.InheritedMethods.Any(x => x.isObsolete) &&
                       !dataArg.InheritedEvents.Any(x => x.isObsolete) &&
                       !dataArg.InheritedProperties.Any(x => x.isObsolete) &&
                       !dataArg.InheritedFields.Any(x => x.isObsolete);
            }

            void AppendInheritedHeader(StringBuilder stringBuilder)
            {
                stringBuilder.AppendLine("## Inherited Members");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("| 成员 | 注释 | 声明此方法的类 |");
                stringBuilder.AppendLine("| :--- | :--- | :--- |");
            }

            bool IsNonExistAPIMember(TypeData dataArg)
            {
                return !dataArg.InheritedMethods.Any(x => x.IsAPI) && !dataArg.InheritedEvents.Any(x => x.IsAPI) &&
                       !dataArg.InheritedProperties.Any(x => x.IsAPI) && !dataArg.InheritedFields.Any(x => x.IsAPI);
            }
        }

        public override string GetMarkdownText(TypeData typeData, DocCategory docCategory, StringBuilder identifier)
        {
            StringBuilder headerIntroduction = CreateHeaderIntroduction(typeData);
            StringBuilder constructorsContent = CreateConstructorsContent(typeData, docCategory);
            StringBuilder methodsContent = CreateCurrentMethodsContent(typeData, docCategory);
            StringBuilder eventsContent = CreateCurrentEventsContent(typeData, docCategory);
            StringBuilder propertiesContent = CreateCurrentPropertiesContent(typeData, docCategory);
            StringBuilder fieldsContent = CreateCurrentFieldsContent(typeData, docCategory);
            StringBuilder inheritedContent = CreateInheritanceContent(typeData, docCategory);
            StringBuilder memberSignatureOverviewContent = CreateMemberSignatureOverviewContent(typeData, docCategory);
            StringBuilder finalStringBuilder = headerIntroduction
                .Append(constructorsContent)
                .Append(methodsContent)
                .Append(eventsContent)
                .Append(propertiesContent)
                .Append(fieldsContent)
                .Append(inheritedContent)
                .Append(memberSignatureOverviewContent)
                .Append(identifier);
            return finalStringBuilder.ToString();
        }

        StringBuilder CreateMemberSignatureOverviewContent(TypeData data, DocCategory docCategory)
        {
            var sb = new StringBuilder();
            if (data.CurrentMethods.Length == 0 && data.CurrentEvents.Length == 0 &&
                data.CurrentProperties.Length == 0 && data.CurrentFields.Length == 0 &&
                data.OperatorMethods.Length == 0 && data.InheritedMethods.Length == 0 &&
                data.InheritedEvents.Length == 0 && data.InheritedProperties.Length == 0 &&
                data.InheritedFields.Length == 0)
            {
                return sb;
            }

            sb.AppendLine("## 所有成员完整签名总览(构造函数除外)");
            sb.AppendLine();
            sb.AppendLine("> 用于避免某个成员的完整签名过长导致成员分类表格拥挤的情况。");
            sb.AppendLine();
            sb.AppendLine("| 成员完整签名(构造函数除外) |");
            sb.AppendLine("| :--- |");

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    // 第一轮：非过时的API成员
                    AppendCurrentMembers(false);
                    AppendInheritedMembers(false);
                    // 第二轮：过时的API成员
                    AppendCurrentMembers(true);
                    AppendInheritedMembers(true);
                    break;

                case DocCategory.CompleteReferences:
                    // 第一轮：所有非过时成员
                    AppendAllMembers(false);
                    // 第二轮：所有过时成员
                    AppendAllMembers(true);
                    break;
            }

            return sb;

            void AppendCurrentMembers(bool isObsolete)
            {
                // 当前方法
                foreach (MethodData methodData in data.CurrentMethods.Where(x => x.IsAPI && x.isObsolete == isObsolete))
                {
                    AppendMemberSignature(methodData);
                }

                // 当前事件
                foreach (EventData eventData in data.CurrentEvents.Where(x => x.IsAPI && x.isObsolete == isObsolete))
                {
                    AppendMemberSignature(eventData);
                }

                // 当前属性
                foreach (PropertyData propertyData in data.CurrentProperties.Where(x =>
                             x.IsAPI && x.isObsolete == isObsolete))
                {
                    AppendMemberSignature(propertyData);
                }

                // 当前字段
                foreach (FieldData fieldData in data.CurrentFields.Where(x => x.IsAPI && x.isObsolete == isObsolete))
                {
                    AppendMemberSignature(fieldData);
                }

                // 当前运算符方法
                foreach (MethodData methodData in
                         data.OperatorMethods.Where(x => x.IsAPI && x.isObsolete == isObsolete))
                {
                    AppendMemberSignature(methodData);
                }
            }

            void AppendInheritedMembers(bool isObsolete)
            {
                // 继承的方法
                foreach (MethodData methodData in
                         data.InheritedMethods.Where(x => x.IsAPI && x.isObsolete == isObsolete))
                {
                    AppendMemberSignature(methodData);
                }

                // 继承的事件
                foreach (EventData eventData in data.InheritedEvents.Where(x => x.IsAPI && x.isObsolete == isObsolete))
                {
                    AppendMemberSignature(eventData);
                }

                // 继承的属性
                foreach (PropertyData propertyData in data.InheritedProperties.Where(x =>
                             x.IsAPI && x.isObsolete == isObsolete))
                {
                    AppendMemberSignature(propertyData);
                }

                // 继承的字段
                foreach (FieldData fieldData in data.InheritedFields.Where(x => x.IsAPI && x.isObsolete == isObsolete))
                {
                    AppendMemberSignature(fieldData);
                }
            }

            // 处理CompleteReferences模式下的所有成员
            void AppendAllMembers(bool isObsolete)
            {
                // 当前方法
                foreach (MethodData methodData in data.CurrentMethods.Where(x => x.isObsolete == isObsolete))
                {
                    AppendMemberSignature(methodData);
                }

                // 当前事件
                foreach (EventData eventData in data.CurrentEvents.Where(x => x.isObsolete == isObsolete))
                {
                    AppendMemberSignature(eventData);
                }

                // 当前属性
                foreach (PropertyData propertyData in data.CurrentProperties.Where(x => x.isObsolete == isObsolete))
                {
                    AppendMemberSignature(propertyData);
                }

                // 当前字段
                foreach (FieldData fieldData in data.CurrentFields.Where(x => x.isObsolete == isObsolete))
                {
                    AppendMemberSignature(fieldData);
                }

                // 当前运算符方法
                foreach (MethodData methodData in data.OperatorMethods.Where(x => x.isObsolete == isObsolete))
                {
                    AppendMemberSignature(methodData);
                }

                // 继承的方法
                foreach (MethodData methodData in data.InheritedMethods.Where(x => x.isObsolete == isObsolete))
                {
                    AppendMemberSignature(methodData);
                }

                // 继承的事件
                foreach (EventData eventData in data.InheritedEvents.Where(x => x.isObsolete == isObsolete))
                {
                    AppendMemberSignature(eventData);
                }

                // 继承的属性
                foreach (PropertyData propertyData in data.InheritedProperties.Where(x => x.isObsolete == isObsolete))
                {
                    AppendMemberSignature(propertyData);
                }

                // 继承的字段
                foreach (FieldData fieldData in data.InheritedFields.Where(x => x.isObsolete == isObsolete))
                {
                    AppendMemberSignature(fieldData);
                }
            }

            void AppendMemberSignature(MemberData memberData)
            {
                var memberDataFullSignature = memberData.fullSignature;
                if (memberData.isObsolete)
                {
                    memberDataFullSignature = "[Obsolete] " + memberDataFullSignature;
                }

                sb.AppendLine($"| `{memberDataFullSignature}` |");
            }
        }

        static void CreateInheritedMemberString(TypeData data, StringBuilder sb,
            Func<MemberData, bool> filterPredicate,
            bool addObsoleteSign = false)
        {
            AppendInheritedMemberLine(sb, data.InheritedMethods.Where(filterPredicate), addObsoleteSign);
            AppendInheritedMemberLine(sb, data.InheritedEvents.Where(filterPredicate), addObsoleteSign);
            AppendInheritedMemberLine(sb, data.InheritedProperties.Where(filterPredicate), addObsoleteSign);
            AppendInheritedMemberLine(sb, data.InheritedFields.Where(filterPredicate), addObsoleteSign);
        }

        static void AppendMemberLine(StringBuilder sb, IEnumerable<MemberData> items,
            bool addObsoleteSign = false)
        {
            foreach (MemberData item in items)
            {
                string memberName = item.name;
                if (addObsoleteSign)
                {
                    memberName = $"[Obsolete] {memberName}";
                }

                sb.AppendLine("| " + $"`{memberName}`" + " | " + item.chineseSummary + " | " +
                              $"`{item.englishSummary}`" + " |");
            }
        }

        static void AppendInheritedMemberLine(StringBuilder sb, IEnumerable<MemberData> items,
            bool addObsoleteSign = false)
        {
            foreach (MemberData item in items)
            {
                string memberName = item.name;
                if (addObsoleteSign)
                {
                    memberName = $"[Obsolete] {memberName}";
                }

                sb.AppendLine("| " + $"`{memberName}`" + " | " + item.chineseSummary + " | " +
                              $"`{item.declaringType}`" + " |");
            }
        }
    }
}
