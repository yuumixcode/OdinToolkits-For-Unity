using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Yuumix.OdinToolkits.AdvancedTypeAnalyzer;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules
{
    public class MethodAnalysisDataFactory : IMethodAnalysisDataFactory
    {
        public static Dictionary<string, string> OperatorStringMap = new Dictionary<string, string>
        {
            // 算术运算符
            { "op_Addition", "operator +" },
            { "op_Subtraction", "operator -" },
            { "op_Multiply", "operator *" },
            { "op_Division", "operator /" },
            { "op_Modulus", "operator %" },
            { "op_Increment", "operator ++" },
            { "op_Decrement", "operator --" },

            // 位运算符
            { "op_BitwiseAnd", "operator &" },
            { "op_BitwiseOr", "operator |" },
            { "op_ExclusiveOr", "operator ^" },
            { "op_LeftShift", "operator <<" },
            { "op_RightShift", "operator >>" },
            { "op_OnesComplement", "operator ~" },

            // 逻辑运算符
            { "op_LogicalNot", "operator !" },
            { "op_True", "operator true" },
            { "op_False", "operator false" },

            // 比较运算符
            { "op_Equality", "operator ==" },
            { "op_Inequality", "operator !=" },
            { "op_LessThan", "operator <" },
            { "op_GreaterThan", "operator >" },
            { "op_LessThanOrEqual", "operator <=" },
            { "op_GreaterThanOrEqual", "operator >=" },

            // 类型转换
            { "op_Implicit", "implicit operator" },
            { "op_Explicit", "explicit operator" },

            // 赋值运算符 (C# 7.3+)
            { "op_Assign", "operator =" },
            { "op_MultiplyAssign", "operator *=" },
            { "op_DivideAssign", "operator /=" },
            { "op_ModulusAssign", "operator %=" },
            { "op_AdditionAssign", "operator +=" },
            { "op_SubtractionAssign", "operator -=" },
            { "op_LeftShiftAssign", "operator <<=" },
            { "op_RightShiftAssign", "operator >>=" },
            { "op_BitwiseAndAssign", "operator &=" },
            { "op_BitwiseOrAssign", "operator |=" },
            { "op_ExclusiveOrAssign", "operator ^=" },

            // 补充遗漏的标准运算符
            { "op_Coalesce", "operator ??" },
            { "op_MemberAccess", "operator ->" },
            { "op_Index", "operator []" },
            { "op_AddressOf", "operator &" },
            { "op_PointerDereference", "operator * " }
        };

        public MethodAnalysisData CreateFromMethodInfo(MethodInfo methodInfo, Type type)
        {
            var methodData = new MethodAnalysisData
            {
                belongToType = type.GetReadableTypeName(true),
                memberType = methodInfo.MemberType,
                memberAccessModifierType = methodInfo.GetMethodAccessModifierType(),
                declaringType = methodInfo.DeclaringType?.GetReadableTypeName(true),
                isStaticMethod = methodInfo.IsStatic,
                isObsolete = methodInfo.IsDefined(typeof(ObsoleteAttribute)),
                isAbstract = methodInfo.IsAbstract,
                isVirtual = methodInfo.IsVirtual,
                name = methodInfo.Name,
                returnType = methodInfo.ReturnType.GetReadableTypeName(),
                parametersString = methodInfo.GetParamsNamesWithDefaultValue(),
                isFromInterfaceImplement = methodInfo.IsFromInterfaceMethod(),
                isFromAncestor = methodInfo.IsInheritedOverrideFromAncestor(type)
            };

            var keyword = "";
            if (methodInfo.IsStatic)
            {
                keyword = "static ";
            }
            else if (methodInfo.IsAbstract)
            {
                keyword = "abstract ";
            }
            else if (methodInfo.IsVirtual && methodInfo.DeclaringType != methodInfo.GetBaseDefinition().DeclaringType)
            {
                methodData.isOverride = true;
                keyword = "override ";
            }
            else if (methodInfo.DeclaringType == methodInfo.GetBaseDefinition().DeclaringType &&
                     methodInfo.IsVirtual && methodInfo.IsFromInterfaceMethod())
            {
                // 这是实现接口的方法
                methodData.isOverride = true;
                keyword = "";
            }
            else if (methodInfo.IsVirtual)
            {
                keyword = "virtual ";
            }

            // 异步处理
            if (methodInfo.GetCustomAttribute<AsyncStateMachineAttribute>() != null)
            {
                keyword += "async ";
            }

            var methodName = TypeAnalyzerHelper.GetFullMethodName(methodInfo, "");
            if (methodInfo.IsSpecialName && methodInfo.Name.StartsWith("op"))
            {
                methodData.isOperator = true;
                foreach (var variable in OperatorStringMap
                             .Where(variable => methodInfo.Name.Contains(variable.Key)))
                {
                    methodName = methodName.Replace(variable.Key, variable.Value);
                    break;
                }
            }

            methodData.partSignature =
                methodData.AccessModifier + " " + keyword + methodData.returnType + " " + methodData.name;
            methodData.fullSignature =
                methodData.AccessModifier + " " + keyword + methodData.returnType + " " + methodName;

            // 转换运算符要特殊处理
            if (methodInfo.Name.Contains("op_Implicit") || methodInfo.Name.Contains("op_Explicit"))
            {
                methodData.fullSignature =
                    methodData.AccessModifier + " " + keyword + methodName;
            }

            methodData.fullSignature += ";";
            if (methodInfo.IsExtensionMethod())
            {
                const string extensionMethodPrefix = "[Extension] ";
                methodData.partSignature = extensionMethodPrefix + methodData.partSignature;
                methodData.fullSignature = extensionMethodPrefix + methodData.fullSignature;
            }

            var declarationStringBuilder = new StringBuilder();
            var attributesObj = methodInfo.GetCustomAttributes(false);
            foreach (var attr in attributesObj)
            {
                var attributeName = attr.GetType().Name;
                if (attributeName.EndsWith("Attribute"))
                {
                    attributeName = attributeName[..^"Attribute".Length];
                }

                declarationStringBuilder.AppendLine($"[{attributeName}]");
            }

            declarationStringBuilder.Append(methodData.fullSignature);
            methodData.fullDeclaration = declarationStringBuilder.ToString();
            // Summary
            methodData.chineseSummary = SummaryAttribute.GetSummary(methodInfo) ?? "无";
            return methodData;
        }
    }
}
