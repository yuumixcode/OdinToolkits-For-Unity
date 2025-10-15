using System;
using System.Reflection;
using System.Text;
using Yuumix.OdinToolkits.AdvancedTypeAnalyzer;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules
{
    public class FieldAnalysisDataFactory : IFieldAnalysisDataFactory
    {
        #region IFieldAnalysisDataFactory Members

        public FieldAnalysisData CreateFromFieldInfo(FieldInfo fieldInfo, Type type)
        {
            var fieldData = new FieldAnalysisData
            {
                belongToType = type.GetReadableTypeName(true),
                memberType = fieldInfo.MemberType,
                declaringType = fieldInfo.DeclaringType?.GetReadableTypeName(true),
                memberAccessModifierType = TypeAnalyzerUtility.GetFieldAccessModifierType(fieldInfo),
                returnType = fieldInfo.FieldType.GetReadableTypeName(true),
                isStatic = fieldInfo.IsStatic,
                isObsolete = fieldInfo.IsDefined(typeof(ObsoleteAttribute)),
                isConst = TypeAnalyzerUtility.IsConstantField(fieldInfo),
                isReadonly = fieldInfo.IsInitOnly,
                name = fieldInfo.Name
            };

            fieldData.partSignature = fieldData.fullSignature;
            if (fieldData.isConst)
            {
                fieldData.fullSignature += " = " + fieldInfo.GetRawConstantValue();
                fieldData.fullSignature += ";";
            }

            if (fieldData.isStatic && fieldData.isReadonly && !fieldInfo.FieldType.IsGenericType)
            {
                fieldData.fullSignature += " = " + fieldInfo.GetValue(null);
                fieldData.fullSignature += ";";
            }

            if (!fieldData.fullSignature.TrimEnd().EndsWith(';'))
            {
                fieldData.fullSignature += ";";
            }

            var declarationStringBuilder = new StringBuilder();
            var attributesObj = fieldInfo.GetCustomAttributes(false);
            foreach (var attr in attributesObj)
            {
                var attributeName = attr.GetType().Name;
                if (attributeName.EndsWith("Attribute"))
                {
                    attributeName = attributeName[..^"Attribute".Length];
                }

                declarationStringBuilder.AppendLine($"[{attributeName}]");
            }

            declarationStringBuilder.Append(fieldData.fullSignature);
            fieldData.fullDeclaration = declarationStringBuilder.ToString();

            // Summary
            fieldData.chineseSummary = SummaryAttribute.GetSummary(fieldInfo) ?? "无";

            return fieldData;
        }

        #endregion
    }
}
