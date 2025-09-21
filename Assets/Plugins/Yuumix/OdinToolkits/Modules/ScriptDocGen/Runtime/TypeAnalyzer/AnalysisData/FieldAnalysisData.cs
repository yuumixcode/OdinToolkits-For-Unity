using System;
using System.Reflection;
using System.Text;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules
{
    [Serializable]
    public class FieldAnalysisData : MemberAnalysisData
    {
        public bool isStatic;
        public bool isConst;
        public bool isReadonly;

        public static FieldAnalysisData FromFieldInfo(FieldInfo fieldInfo, Type type)
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

            var keyword = "";
            if (fieldData.isConst)
            {
                keyword = "const ";
            }
            else if (fieldData.isReadonly)
            {
                keyword = "readonly ";
            }
            else if (fieldData.isStatic)
            {
                keyword = "static ";
            }

            fieldData.fullSignature = fieldData.AccessModifier.Trim(' ') + " " + keyword +
                                      fieldInfo.FieldType.GetReadableTypeName() + " " + fieldData.name;
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

            if (!fieldData.fullSignature.TrimEnd().EndsWith(";"))
            {
                fieldData.fullSignature += ";";
            }

            var declarationStringBuilder = new StringBuilder();
            object[] attributesObj = fieldInfo.GetCustomAttributes(false);
            foreach (object attr in attributesObj)
            {
                string attributeName = attr.GetType().Name;
                if (attributeName.EndsWith("Attribute"))
                {
                    attributeName = attributeName[..^"Attribute".Length];
                }

                declarationStringBuilder.AppendLine($"[{attributeName}]");
            }

            declarationStringBuilder.Append(fieldData.fullSignature);
            fieldData.fullDeclaration = declarationStringBuilder.ToString();
            // Summary
            fieldData.chineseSummary = ChineseSummaryAttribute.GetChineseSummary(fieldInfo) ?? "无";

            return fieldData;
        }
    }
}
