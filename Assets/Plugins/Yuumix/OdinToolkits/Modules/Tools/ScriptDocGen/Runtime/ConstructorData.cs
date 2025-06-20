using Sirenix.Utilities;
using System;
using System.Reflection;
using Yuumix.OdinToolkits.Common.InspectorLocalization;
using Yuumix.OdinToolkits.Modules.Utilities.Runtime;
using MethodInfoExtensions = Yuumix.OdinToolkits.Modules.Utilities.Runtime.MethodInfoExtensions;

namespace Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Runtime
{
    /// <summary>
    /// 构造函数数据
    /// </summary>
    [Serializable]
    public class ConstructorData : MemberData
    {
        public bool isVirtual;
        public string parameters;

        public static ConstructorData FromConstructorInfo(ConstructorInfo constructorInfo, Type type)
        {
            var consData = new ConstructorData
            {
                belongToType = type.GetReadableTypeName(true),
                memberType = constructorInfo.MemberType,
                memberAccessModifierType = constructorInfo.GetMethodAccessModifierType(),
                isStatic = constructorInfo.IsStatic,
                isObsolete = constructorInfo.IsDefined(typeof(ObsoleteAttribute)),
                isVirtual = constructorInfo.IsVirtual,
                name = constructorInfo.DeclaringType?.Name,
                parameters = MethodInfoExtensions.GetParamsNames(constructorInfo),
            };
            consData.accessModifier = consData.memberAccessModifierType.GetAccessModifierString();
            consData.fullSignature = consData.accessModifier + " " + constructorInfo.GetFullMethodName();
            if (constructorInfo.GetCustomAttribute<LocalizedCommentAttribute>() == null)
            {
                consData.chineseComment = "无";
                consData.englishComment = "No Comment";
                return consData;
            }

            var commentAttr = constructorInfo.GetCustomAttribute<LocalizedCommentAttribute>();
            consData.chineseComment = commentAttr.ChineseComment;
            consData.englishComment = commentAttr.EnglishComment;

            return consData;
        }
    }
}
