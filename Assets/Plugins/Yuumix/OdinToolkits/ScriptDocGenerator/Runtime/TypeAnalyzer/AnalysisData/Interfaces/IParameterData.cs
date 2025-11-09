using System;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    /// <summary>
    /// 参数信息解析数据接口
    /// </summary>
    [Summary("参数信息解析数据接口")]
    public interface IParameterData
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        [Summary("参数名称")]
        string Name { get; }

        /// <summary>
        /// 参数类型
        /// </summary>
        [Summary("参数类型")]
        Type ParameterType { get; }

        /// <summary>
        /// 是否有默认值
        /// </summary>
        [Summary("是否有默认值")]
        bool HasDefaultValue { get; }

        /// <summary>
        /// 默认值
        /// </summary>
        [Summary("默认值")]
        object DefaultValue { get; }

        /// <summary>
        /// 参数方向（in/out/ref）
        /// </summary>
        [Summary("参数方向（in/out/ref）")]
        ParameterDirection Direction { get; }

        /// <summary>
        /// 是否为 params 参数
        /// </summary>
        [Summary("是否为 params 参数")]
        bool IsParams { get; }

        /// <summary>
        /// 生成格式化的参数字符串
        /// </summary>
        /// <returns></returns>
        [Summary("生成格式化的参数字符串")]
        string GetFormattedString();
    }
}
