using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Modules.Utilities;
using System;
using System.Reflection;
using Yuumix.OdinToolkits.Common.InspectorMultiLanguage;

namespace Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Runtime
{
    [Serializable]
    public class PropertyData : MemberData
    {
        public AccessModifierType getMethodAccessModifierType;
        public AccessModifierType setMethodAccessModifierType;
        public string getMethodAccessModifier;
        public string setMethodAccessModifier;

        public static PropertyData FromPropertyInfo(PropertyInfo propertyInfo, Type type)
        {
            var propertyData = new PropertyData
            {
                memberType = propertyInfo.MemberType,
                belongToType = type.FullName,
                declaringType = propertyInfo.DeclaringType?.FullName,
                memberAccessModifierType = propertyInfo.GetPropertyAccessModifierType(),
                returnType = propertyInfo.GetGetMethod(true).ReturnType.GetReadableTypeName(),
                isStatic = propertyInfo.GetMethod.IsStatic,
                isObsolete = propertyInfo.IsDefined(typeof(ObsoleteAttribute)),
                name = propertyInfo.Name,
                getMethodAccessModifierType = propertyInfo.GetGetMethod(true) != null
                    ? propertyInfo.GetGetMethod(true).GetMethodAccessModifierType()
                    : AccessModifierType.None,
                setMethodAccessModifierType = propertyInfo.GetSetMethod(true) != null
                    ? propertyInfo.GetSetMethod(true).GetMethodAccessModifierType()
                    : AccessModifierType.None,
            };
            propertyData.accessModifier = propertyData.memberAccessModifierType.GetAccessModifierString();
            propertyData.getMethodAccessModifier = propertyData.getMethodAccessModifierType.GetAccessModifierString();
            propertyData.setMethodAccessModifier = propertyData.setMethodAccessModifierType.GetAccessModifierString();
            var keyword = "";
            if (propertyData.isStatic)
            {
                keyword = "static ";
            }

            propertyData.fullSignature = propertyData.accessModifier + " " + keyword + propertyData.returnType + " " +
                                         propertyData.name;
            var methodsString = "";
            if (propertyData.getMethodAccessModifier.IsNullOrWhiteSpace() &&
                propertyData.setMethodAccessModifier.IsNullOrWhiteSpace())
            {
                methodsString = "";
            }

            if (propertyData.getMethodAccessModifier.IsNullOrWhiteSpace() &&
                !propertyData.setMethodAccessModifier.IsNullOrWhiteSpace())
            {
                methodsString = " { " + propertyData.setMethodAccessModifier + " set; }";
            }
            else if (propertyData.setMethodAccessModifier.IsNullOrWhiteSpace() &&
                     !propertyData.getMethodAccessModifier.IsNullOrWhiteSpace())
            {
                methodsString = " { " + propertyData.getMethodAccessModifier + " get; }";
            }
            else if (!propertyData.getMethodAccessModifier.IsNullOrWhiteSpace() &&
                     !propertyData.setMethodAccessModifier.IsNullOrWhiteSpace())
            {
                methodsString = " { " + propertyData.getMethodAccessModifier + " get; " +
                                propertyData.setMethodAccessModifier + " set; }";
            }

            propertyData.fullSignature += methodsString;
            if (propertyInfo.GetCustomAttribute<MultiLanguageCommentAttribute>() == null)
            {
                propertyData.chineseComment = "无";
                propertyData.englishComment = "No Comment";
                return propertyData;
            }

            var commentAttr = propertyInfo.GetCustomAttribute<MultiLanguageCommentAttribute>();
            propertyData.chineseComment = commentAttr.ChineseComment;
            propertyData.englishComment = commentAttr.EnglishComment;
            return propertyData;
        }
    }
}
