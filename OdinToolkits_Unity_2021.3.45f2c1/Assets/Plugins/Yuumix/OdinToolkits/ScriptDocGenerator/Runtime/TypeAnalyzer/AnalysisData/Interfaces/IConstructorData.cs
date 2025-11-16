using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    /// <summary>
    /// 构造方法数据接口，继承自 IDerivedMemberData
    /// </summary>
    [Summary("构造方法数据接口，继承自 IDerivedMemberData")]
    public interface IConstructorData : IDerivedMemberData
    {
        /// <summary>
        /// 方法的参数声明字符串，包含参数名称和类型
        /// </summary>
        [Summary("方法的参数声明字符串，包含参数名称和类型")]
        string ParametersDeclaration { get; }

        /// <summary>
        /// 不包含参数的简单方法签名
        /// </summary>
        [Summary("不包含参数的简单方法签名")]
        string SignatureWithoutParameters { get; }
    }
}
