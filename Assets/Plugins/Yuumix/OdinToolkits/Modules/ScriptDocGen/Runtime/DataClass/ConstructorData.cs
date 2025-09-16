using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.Utilities;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.Editor
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
                parameters = constructorInfo.GetParamsNames()
            };
            consData.accessModifier = consData.memberAccessModifierType.ConvertToString();
            consData.fullSignature = consData.accessModifier + " " +
                                     TypeAnalyzerUtility.GetFullMethodName(constructorInfo, "[Ext]");
            // Summary
            consData.chineseSummary = ChineseSummaryAttribute.GetChineseSummary(constructorInfo) ?? "无";
            return consData;
        }
    }
}
