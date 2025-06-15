using Sirenix.OdinInspector;
using System;
using System.Reflection;
using Yuumix.OdinToolkits.Common.InspectorLocalization.Attributes;
using Yuumix.OdinToolkits.Modules.Utilities.Runtime;

namespace Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Runtime
{
    [Serializable]
    public class FieldData
    {
        public string belongToType;
        public MemberTypes memberType;
        public AccessModifierType accessModifierType;
        public string declaringType;
        public string accessModifier;
        public string returnType;
        public bool isStatic;
        public bool isObsolete;
        public bool isConst;
        public bool isReadonly;
        public string name;
        public string fullSignature;
        public string chineseComment;
        public string englishComment;

        [ShowInInspector] public bool NoFromInherit => belongToType == declaringType;

        public static FieldData FromFieldInfo(FieldInfo fieldInfo, Type type)
        {
            var fieldData = new FieldData
            {
                belongToType = type.GetReadableTypeName(true),
                memberType = fieldInfo.MemberType,
                declaringType = fieldInfo.DeclaringType?.GetReadableTypeName(true),
                accessModifierType = fieldInfo.GetFieldAccessModifierType(),
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

            fieldData.fullSignature = fieldData.accessModifier.TrimEnd(' ') + " " + keyword +
                                      fieldInfo.FieldType.GetReadableTypeName() + " " + fieldData.name;
            if (fieldInfo.GetCustomAttribute<LocalizedCommentAttribute>() == null)
            {
                fieldData.chineseComment = "无";
                fieldData.englishComment = "No Comment";
                return fieldData;
            }

            var commentAttr = fieldInfo.GetCustomAttribute<LocalizedCommentAttribute>();
            fieldData.chineseComment = commentAttr.ChineseComment;
            fieldData.englishComment = commentAttr.EnglishComment;

            return fieldData;
        }
    }
}
