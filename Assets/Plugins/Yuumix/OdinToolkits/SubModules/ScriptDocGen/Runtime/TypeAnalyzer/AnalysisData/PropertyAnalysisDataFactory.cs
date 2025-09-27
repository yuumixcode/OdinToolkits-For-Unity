using System;
using System.Reflection;
using System.Text;
using Sirenix.Utilities;
using Yuumix.OdinToolkits.AdvancedTypeAnalyzer;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules
{
    public class PropertyAnalysisDataFactory : IPropertyAnalysisDataFactory
    {
        public PropertyAnalysisData CreateFromPropertyInfo(PropertyInfo propertyInfo, Type type)
        {
            var propertyData = new PropertyAnalysisData
            {
                memberType = propertyInfo.MemberType,
                belongToType = type.FullName,
                declaringType = propertyInfo.DeclaringType?.FullName,
                memberAccessModifierType = TypeAnalyzerUtility.GetPropertyAccessModifierType(propertyInfo),
                returnType = propertyInfo.GetGetMethod(true).ReturnType.GetReadableTypeName(),
                isStatic = propertyInfo.IsStatic(),
                isObsolete = propertyInfo.IsDefined(typeof(ObsoleteAttribute)),
                name = propertyInfo.Name,
                getAccessModifierType = propertyInfo.GetGetMethod(true) != null
                    ? propertyInfo.GetGetMethod(true).GetMethodAccessModifierType()
                    : AccessModifierType.None,
                setAccessModifierType = propertyInfo.GetSetMethod(true) != null
                    ? propertyInfo.GetSetMethod(true).GetMethodAccessModifierType()
                    : AccessModifierType.None
            };
            
            var keyword = "";
            if (propertyData.isStatic)
            {
                keyword = "static ";
            }

            propertyData.partSignature =
                propertyData.AccessModifier + " " + keyword + propertyData.returnType + " " + propertyData.name;
            var methodsString = "";
            if (propertyData.GetAccessModifier.IsNullOrWhiteSpace() &&
                propertyData.SetAccessModifier.IsNullOrWhiteSpace())
            {
                methodsString = "";
            }

            if (propertyData.GetAccessModifier.IsNullOrWhiteSpace() &&
                !propertyData.SetAccessModifier.IsNullOrWhiteSpace())
            {
                methodsString = " { " + propertyData.SetAccessModifier + " set; }";
            }
            else if (propertyData.SetAccessModifier.IsNullOrWhiteSpace() &&
                     !propertyData.GetAccessModifier.IsNullOrWhiteSpace())
            {
                methodsString = " { " + propertyData.GetAccessModifier + " get; }";
            }
            else if (!propertyData.GetAccessModifier.IsNullOrWhiteSpace() &&
                     !propertyData.SetAccessModifier.IsNullOrWhiteSpace())
            {
                methodsString = " { " + propertyData.GetAccessModifier + " get; " +
                                propertyData.SetAccessModifier + " set; }";
            }

            propertyData.fullSignature += propertyData.partSignature + methodsString;
            var declarationStringBuilder = new StringBuilder();
            object[] attributesObj = propertyInfo.GetCustomAttributes(false);
            foreach (object attr in attributesObj)
            {
                string attributeName = attr.GetType().Name;
                if (attributeName.EndsWith("Attribute"))
                {
                    attributeName = attributeName[..^"Attribute".Length];
                }

                declarationStringBuilder.AppendLine($"[{attributeName}]");
            }

            declarationStringBuilder.Append(propertyData.fullSignature);
            propertyData.fullDeclaration = declarationStringBuilder.ToString();
            
            // Summary
            propertyData.chineseSummary = SummaryAttribute.GetSummary(propertyInfo) ?? "无";
            return propertyData;
        }
    }
}