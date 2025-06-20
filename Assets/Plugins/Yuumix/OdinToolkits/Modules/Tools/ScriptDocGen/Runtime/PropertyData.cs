using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Reflection;
using Yuumix.OdinToolkits.Common.InspectorLocalization;
using Yuumix.OdinToolkits.Modules.Utilities.Runtime;

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
            if (propertyData.getMethodAccessModifier.IsNullOrWhitespace() &&
                propertyData.setMethodAccessModifier.IsNullOrWhitespace())
            {
                methodsString = "";
            }

            if (propertyData.getMethodAccessModifier.IsNullOrWhitespace() &&
                !propertyData.setMethodAccessModifier.IsNullOrWhitespace())
            {
                methodsString = " { " + propertyData.setMethodAccessModifier + " set; }";
            }
            else if (propertyData.setMethodAccessModifier.IsNullOrWhitespace() &&
                     !propertyData.getMethodAccessModifier.IsNullOrWhitespace())
            {
                methodsString = " { " + propertyData.getMethodAccessModifier + " get; }";
            }
            else if (!propertyData.getMethodAccessModifier.IsNullOrWhitespace() &&
                     !propertyData.setMethodAccessModifier.IsNullOrWhitespace())
            {
                methodsString = " { " + propertyData.getMethodAccessModifier + " get; " +
                                propertyData.setMethodAccessModifier + " set; }";
            }

            propertyData.fullSignature += methodsString;
            if (propertyInfo.GetCustomAttribute<LocalizedCommentAttribute>() == null)
            {
                propertyData.chineseComment = "无";
                propertyData.englishComment = "No Comment";
                return propertyData;
            }

            var commentAttr = propertyInfo.GetCustomAttribute<LocalizedCommentAttribute>();
            propertyData.chineseComment = commentAttr.ChineseComment;
            propertyData.englishComment = commentAttr.EnglishComment;
            return propertyData;
        }
    }
}
