using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [Serializable]
    public class FieldData : MemberData
    {
        public bool isConst;
        public bool isReadonly;

        public static FieldData FromFieldInfo(FieldInfo fieldInfo, Type type)
        {
            var fieldData = new FieldData
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

            fieldData.accessModifier = fieldData.memberAccessModifierType.ConvertToString();
            fieldData.fullSignature = fieldData.accessModifier.Trim(' ') + " " + keyword +
                                      fieldInfo.FieldType.GetReadableTypeName() + " " + fieldData.name;
            // Summary
            fieldData.chineseSummary = ChineseSummaryAttribute.GetChineseSummary(fieldInfo) != null
                ? ChineseSummaryAttribute.GetChineseSummary(fieldInfo)
                : "无";

            return fieldData;
        }
    }
}
