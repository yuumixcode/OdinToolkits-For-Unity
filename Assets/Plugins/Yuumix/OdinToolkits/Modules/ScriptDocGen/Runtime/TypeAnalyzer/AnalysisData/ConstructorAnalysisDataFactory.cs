using System;
using System.Reflection;
using System.Text;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules
{
    public class ConstructorAnalysisDataFactory : IConstructorAnalysisDataFactory
    {
        public ConstructorAnalysisData CreateFromConstructorInfo(ConstructorInfo constructorInfo, Type type)
        {
            var consData = new ConstructorAnalysisData
            {
                belongToType = type.GetReadableTypeName(true),
                declaringType = constructorInfo.DeclaringType.GetReadableTypeName(true),
                memberType = constructorInfo.MemberType,
                memberAccessModifierType = constructorInfo.GetMethodAccessModifierType(),
                isObsolete = constructorInfo.IsDefined(typeof(ObsoleteAttribute)),
                returnType = constructorInfo.DeclaringType.GetReadableTypeName(true),
                name = constructorInfo.DeclaringType.GetReadableTypeName(),
                parametersString = TypeAnalyzerUtility.GetParamsNamesWithDefaultValue(constructorInfo)
            };
            
            consData.fullSignature = consData.AccessModifier + " " +
                                     TypeAnalyzerUtility.GetFullMethodName(constructorInfo, "[Ext]");
            
            var declarationStringBuilder = new StringBuilder();
            object[] attributesObj = constructorInfo.GetCustomAttributes(false);
            foreach (object attr in attributesObj)
            {
                string attributeName = attr.GetType().Name;
                if (attributeName.EndsWith("Attribute"))
                {
                    attributeName = attributeName[..^"Attribute".Length];
                }

                declarationStringBuilder.AppendLine($"[{attributeName}]");
            }

            declarationStringBuilder.Append(consData.fullSignature);
            consData.fullDeclaration = declarationStringBuilder.ToString();
            
            // Summary
            consData.chineseSummary = ChineseSummaryAttribute.GetChineseSummary(constructorInfo) ?? "无";
            return consData;
        }
    }
}