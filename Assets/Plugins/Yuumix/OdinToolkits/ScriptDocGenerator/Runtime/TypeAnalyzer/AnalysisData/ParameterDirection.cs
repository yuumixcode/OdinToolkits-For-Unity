using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    /// <summary>
    /// 参数方向枚举
    /// </summary>
    [Summary("参数方向枚举")]
    public enum ParameterDirection
    {
        /// <summary>
        /// 输入参数
        /// </summary>
        [Summary("输入参数")]
        In = 0,

        /// <summary>
        /// 输出参数
        /// </summary>
        [Summary("输出参数")]
        Out = 1,

        /// <summary>
        /// 引用参数
        /// </summary>
        [Summary("引用参数")]
        Ref = 2,

        /// <summary>
        /// 返回值参数
        /// </summary>
        [Summary("返回值参数")]
        RetVal = 3
    }
}
