using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.Editor
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
                memberAccessModifierType = TypeAnalyzerUtility.GetPropertyAccessModifierType(propertyInfo),
                returnType = propertyInfo.GetGetMethod(true).ReturnType.GetReadableTypeName(),
                isStatic = propertyInfo.GetMethod.IsStatic,
                isObsolete = propertyInfo.IsDefined(typeof(ObsoleteAttribute)),
                name = propertyInfo.Name,
                getMethodAccessModifierType = propertyInfo.GetGetMethod(true) != null
                    ? propertyInfo.GetGetMethod(true).GetMethodAccessModifierType()
                    : AccessModifierType.None,
                setMethodAccessModifierType = propertyInfo.GetSetMethod(true) != null
                    ? propertyInfo.GetSetMethod(true).GetMethodAccessModifierType()
                    : AccessModifierType.None
            };
            propertyData.accessModifier = propertyData.memberAccessModifierType.ConvertToString();
            propertyData.getMethodAccessModifier = propertyData.getMethodAccessModifierType.ConvertToString();
            propertyData.setMethodAccessModifier = propertyData.setMethodAccessModifierType.ConvertToString();
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
            // Summary
            propertyData.chineseSummary = ChineseSummaryAttribute.GetChineseSummary(propertyInfo) ?? "无";
            return propertyData;
        }
    }
}
