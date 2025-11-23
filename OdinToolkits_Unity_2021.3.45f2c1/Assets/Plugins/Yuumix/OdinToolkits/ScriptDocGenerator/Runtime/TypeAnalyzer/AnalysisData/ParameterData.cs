using System;
using System.Reflection;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    /// <summary>
    /// 参数信息解析数据
    /// </summary>
    [Summary("参数信息解析数据")]
    [Serializable]
    public class ParameterData : IParameterData
    {
        public ParameterData(ParameterInfo parameterInfo)
        {
            Name = parameterInfo.Name ?? string.Empty;
            ParameterType = parameterInfo.ParameterType;
            HasDefaultValue = parameterInfo.HasDefaultValue;
            DefaultValue = parameterInfo.HasDefaultValue ? parameterInfo.DefaultValue : null;
            Direction = ParameterDirection.In;
            if (parameterInfo.IsOut)
            {
                Direction = ParameterDirection.Out;
            }
            else if (parameterInfo.ParameterType.IsByRef)
            {
                Direction = ParameterDirection.Ref;
            }
            else if (parameterInfo.IsRetval)
            {
                Direction = ParameterDirection.RetVal;
            }

            IsParams = parameterInfo.IsDefined(typeof(ParamArrayAttribute), false);
        }

        static string GetDefaultValueString(Type parameterType, object value)
        {
            if (value != null && parameterType.IsEnum)
            {
                var enumTypeName = parameterType.Name;
                var enumName = Enum.GetName(parameterType, value);
                return $"{enumTypeName}.{enumName}";
            }

            return value switch
            {
                null => "null",
                string str => $"\"{str}\"",
                bool b => b ? "true" : "false",
                float f => $"{f}f",
                char c => $"'{c}'",
                double d => $"{d}d",
                decimal m => $"{m}m",
                uint u => $"{u}u",
                long l => $"{l}L",
                ulong ul => $"{ul}ul",
                short s => $"{s}",
                ushort us => $"{us}",
                byte b2 => $"{b2}",
                sbyte sb => $"{sb}",
                _ => value.ToString()
            };
        }

        #region IParameterData Members

        /// <summary>
        /// 参数名称
        /// </summary>
        [Summary("参数名称")]
        public string Name { get; }

        /// <summary>
        /// 参数类型
        /// </summary>
        [Summary("参数类型")]
        public Type ParameterType { get; }

        /// <summary>
        /// 是否有默认值
        /// </summary>
        [Summary("是否有默认值")]
        public bool HasDefaultValue { get; }

        /// <summary>
        /// 默认值
        /// </summary>
        [Summary("默认值")]
        public object DefaultValue { get; }

        /// <summary>
        /// 参数方向（in/out/ref）
        /// </summary>
        [Summary("参数方向（in/out/ref）")]
        public ParameterDirection Direction { get; }

        /// <summary>
        /// 是否为 params 参数
        /// </summary>
        [Summary("是否为 params 参数")]
        public bool IsParams { get; }

        /// <summary>
        /// 生成格式化的参数字符串
        /// </summary>
        /// <returns></returns>
        [Summary("生成格式化的参数字符串")]
        public string GetFormattedString()
        {
            var result = string.Empty;

            // 添加方向修饰符
            switch (Direction)
            {
                case ParameterDirection.Out:
                    result += "out ";
                    break;
                case ParameterDirection.Ref:
                    result += "ref ";
                    break;
            }

            // 添加 params 修饰符
            if (IsParams)
            {
                result += "params ";
            }

            // 添加类型名称
            var typeName = ParameterType?.GetReadableTypeName() ?? "object";
            if (ParameterType?.IsByRef == true)
            {
                typeName = typeName.TrimEnd('&');
            }

            result += typeName;

            // 添加参数名称
            if (!string.IsNullOrEmpty(Name))
            {
                result += " " + Name;
            }

            // 添加默认值
            if (HasDefaultValue)
            {
                result += " = ";
                result += GetDefaultValueString(ParameterType, DefaultValue);
            }

            return result;
        }

        #endregion
    }
}
