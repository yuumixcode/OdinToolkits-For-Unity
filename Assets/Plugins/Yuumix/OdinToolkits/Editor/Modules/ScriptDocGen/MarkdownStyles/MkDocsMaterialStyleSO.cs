using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Yuumix.OdinToolkits.Shared;

namespace Yuumix.OdinToolkits.Editor
{
    public class MkDocsMaterialStyleSO : MarkdownStyleSO
    {
        public override string GetMarkdownText(TypeData typeData, DocCategory docCategory, StringBuilder identifier)
        {
            StringBuilder headerIntroduction = CreateHeaderIntroductionMkDocs(typeData);
            StringBuilder constructorsContent = CreateConstructorsContentMkDocs(typeData, docCategory);
            StringBuilder fieldsContent = CreateCurrentFieldsContentMkDocs(typeData, docCategory);
            StringBuilder methodsContent = CreateCurrentMethodsContentMkDocs(typeData, docCategory);
            StringBuilder propertiesContent = CreateCurrentPropertiesContentMkDocs(typeData, docCategory);
            StringBuilder eventsContent = CreateCurrentEventsContentMkDocs(typeData, docCategory);
            StringBuilder inheritedContent = CreateInheritedContentMkDocs(typeData, docCategory);
            StringBuilder finalStringBuilder = headerIntroduction
                .Append(constructorsContent)
                .Append(methodsContent)
                .Append(eventsContent)
                .Append(propertiesContent)
                .Append(fieldsContent)
                .Append(inheritedContent)
                .Append(identifier);
            return finalStringBuilder.ToString();
        }

        static StringBuilder CreateHeaderIntroductionMkDocs(TypeData data)
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
            sb.AppendLine();
            if (!string.IsNullOrEmpty(data.ChineseComment) || !string.IsNullOrEmpty(data.EnglishComment))
            {
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
            }

            sb.AppendLine();
            return sb;
        }

        static StringBuilder CreateConstructorsContentMkDocs(TypeData data, DocCategory docCategory)
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
                AppendMemberLine(sb, data.Constructors.Where(x => !x.isObsolete && x.IsAPI));
            }
            else
            {
                AppendHeader();
                AppendMemberLine(sb, data.Constructors.Where(x => !x.isObsolete));
            }

            if (!data.Constructors.Any(x => x.isObsolete))
            {
                return sb;
            }

            if (docCategory == DocCategory.ScriptingAPI)
            {
                AppendMemberLine(sb, data.Constructors.Where(x => x.isObsolete && x.IsAPI), true);
            }
            else
            {
                AppendMemberLine(sb, data.Constructors.Where(x => x.isObsolete), true);
            }

            sb.AppendLine();
            return sb;

            void AppendHeader()
            {
                sb.AppendLine("## Constructors");
                sb.AppendLine();
                sb.AppendLine("| 构造函数 | 注释 | Comment |");
                sb.AppendLine("| :--- | :--- | :--- |");
            }
        }

        static StringBuilder CreateCurrentMethodsContentMkDocs(TypeData data, DocCategory docCategory)
        {
            var sb = new StringBuilder();
            if (data.CurrentMethods.Length <= 0)
            {
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    if (!data.CurrentMethods.Any(x => x.IsAPI))
                    {
                        // YuumixLogger.EditorLog("!data.CurrentMethods.Any(x => x.IsAPI)");
                        return sb;
                    }

                    AppendHeader();
                    AppendMemberLine(sb, data.CurrentMethods.Where(x => !x.isObsolete && x.IsAPI));
                    break;
                case DocCategory.CompleteReferences:
                    AppendHeader();
                    AppendMemberLine(sb, data.CurrentMethods.Where(x => !x.isObsolete));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(docCategory), docCategory, null);
            }

            if (!data.CurrentMethods.Any(x => x.isObsolete))
            {
                // YuumixLogger.EditorLog("!data.CurrentMethods.Any(x => x.isObsolete)");
                sb.AppendLine();
                return sb;
            }

            switch (docCategory)
            {
                case DocCategory.ScriptingAPI:
                    AppendMemberLine(sb, data.CurrentMethods.Where(x => x.isObsolete && x.IsAPI), true);
                    break;
                case DocCategory.CompleteReferences:
                    AppendMemberLine(sb, data.CurrentMethods.Where(x => x.isObsolete), true);
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

        static StringBuilder CreateCurrentEventsContentMkDocs(TypeData data, DocCategory docCategory)
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

        static StringBuilder CreateCurrentPropertiesContentMkDocs(TypeData data, DocCategory docCategory)
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

        static StringBuilder CreateCurrentFieldsContentMkDocs(TypeData data, DocCategory docCategory)
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
                    sb.AppendLine("| " + $"`{fieldData.name}`" + " | " + fieldData.chineseComment + " | " +
                                  $"`{fieldData.englishComment}`" + " |");
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

        static StringBuilder CreateInheritedContentMkDocs(TypeData data, DocCategory docCategory)
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
                string fullSignature = item.fullSignature;
                if (addObsoleteSign)
                {
                    fullSignature = $"[Obsolete] {fullSignature}";
                }

                sb.AppendLine("| " + $"`{fullSignature}`" + " | " + item.chineseComment + " | " +
                              $"`{item.englishComment}`" + " |");
            }
        }

        static void AppendInheritedMemberLine(StringBuilder sb, IEnumerable<MemberData> items,
            bool addObsoleteSign = false)
        {
            foreach (MemberData item in items)
            {
                string fullSignature = item.fullSignature;
                if (addObsoleteSign)
                {
                    fullSignature = $"[Obsolete] {fullSignature}";
                }

                sb.AppendLine("| " + $"`{fullSignature}`" + " | " + item.chineseComment + " | " +
                              $"`{item.declaringType}`" + " |");
            }
        }
    }
}
