using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Modules.Utilities;

namespace Yuumix.OdinToolkits
{
    [Serializable]
    public class MethodData : MemberData
    {
        public static Dictionary<string, string> OperatorStringMap = new Dictionary<string, string>()
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
            { "op_PointerDereference", "operator * " },
        };

        public bool isAbstract;
        public bool isVirtual;
        public bool isOperator;

        public static MethodData FromMethodInfo(MethodInfo methodInfo, Type type)
        {
            var methodData = new MethodData
            {
                belongToType = type.GetReadableTypeName(true),
                memberType = methodInfo.MemberType,
                memberAccessModifierType = methodInfo.GetMethodAccessModifierType(),
                declaringType = methodInfo.DeclaringType?.GetReadableTypeName(true),
                isStatic = methodInfo.IsStatic,
                isObsolete = methodInfo.IsDefined(typeof(ObsoleteAttribute)),
                isAbstract = methodInfo.IsAbstract,
                isVirtual = methodInfo.IsVirtual,
                name = methodInfo.Name,
                returnType = methodInfo.ReturnType.GetReadableTypeName(),
            };
            methodData.accessModifier = methodData.memberAccessModifierType.GetAccessModifierString();
            var keyword = "";
            if (methodInfo.IsStatic)
            {
                keyword = "static ";
            }
            else if (methodInfo.IsAbstract)
            {
                keyword = "abstract ";
            }
            else if (methodInfo.IsVirtual && methodInfo.DeclaringType == typeof(object))
            {
                keyword = "virtual ";
            }
            else if (methodInfo.IsVirtual)
            {
                keyword = "virtual(override) ";
            }

            var methodName = methodInfo.GetFullMethodName("[Ext]");
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

            methodData.fullSignature =
                methodData.accessModifier + " " + keyword + methodData.returnType + " " + methodName;

            if (methodInfo.GetCustomAttribute<MultiLanguageCommentAttribute>() == null)
            {
                methodData.chineseComment = "无";
                methodData.englishComment = "No Comment";
                return methodData;
            }

            var commentAttr = methodInfo.GetCustomAttribute<MultiLanguageCommentAttribute>();
            methodData.chineseComment = commentAttr.ChineseComment;
            methodData.englishComment = commentAttr.EnglishComment;
            return methodData;
        }
    }
}
