using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Shared;
using Yuumix.OdinToolkits.LowLevel;

namespace Yuumix.OdinToolkits
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
                memberAccessModifierType = fieldInfo.GetFieldAccessModifierType(),
                returnType = fieldInfo.FieldType.GetReadableTypeName(true),
                isStatic = fieldInfo.IsStatic,
                isObsolete = fieldInfo.IsDefined(typeof(ObsoleteAttribute)),
                isConst = fieldInfo.IsConstantField(),
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

            fieldData.accessModifier = fieldData.memberAccessModifierType.GetAccessModifierString();
            fieldData.fullSignature = fieldData.accessModifier.Trim(' ') + " " + keyword +
                                      fieldInfo.FieldType.GetReadableTypeName() + " " + fieldData.name;
            IEnumerable<Attribute> attributes = fieldInfo.GetCustomAttributes();
            if (attributes
                    .First(x => typeof(IMultiLanguageComment).IsAssignableFrom(x.GetType())) is not
                IMultiLanguageComment comment)
            {
                return fieldData;
            }

            fieldData.chineseComment = comment.GetChineseComment();
            fieldData.englishComment = comment.GetEnglishComment();

            return fieldData;
        }
    }
}
