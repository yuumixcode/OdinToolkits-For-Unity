using System;
using System.Reflection;
using System.Text;
using Yuumix.OdinToolkits.AdvancedTypeAnalyzer;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules
{
    public class ConstructorAnalysisDataFactory : IConstructorAnalysisDataFactory
    {
        #region IConstructorAnalysisDataFactory Members

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
                parametersString = constructorInfo.GetParametersNameWithDefaultValue()
            };

            consData.fullSignature = consData.AccessModifier + " [Ext] " +
                                     constructorInfo.GetPartMethodSignatureContainsNameAndParameters();

            var declarationStringBuilder = new StringBuilder();
            var attributesObj = constructorInfo.GetCustomAttributes(false);
            foreach (var attr in attributesObj)
            {
                var attributeName = attr.GetType().Name;
                if (attributeName.EndsWith("Attribute"))
                {
                    attributeName = attributeName[..^"Attribute".Length];
                }

                declarationStringBuilder.AppendLine($"[{attributeName}]");
            }

            declarationStringBuilder.Append(consData.fullSignature);
            consData.fullDeclaration = declarationStringBuilder.ToString();

            // Summary
            consData.chineseSummary = SummaryAttribute.GetSummary(constructorInfo) ?? "无";
            return consData;
        }

        #endregion
    }
}
