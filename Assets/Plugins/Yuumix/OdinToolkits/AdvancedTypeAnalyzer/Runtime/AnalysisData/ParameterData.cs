using System;
using System.Reflection;

namespace Yuumix.OdinToolkits.AdvancedTypeAnalyzer
{
    /// <summary>
    /// 参数方向枚举
    /// </summary>
    public enum ParameterDirection
    {
        /// <summary>
        /// 输入参数
        /// </summary>
        In = 0,

        /// <summary>
        /// 输出参数
        /// </summary>
        Out = 1,

        /// <summary>
        /// 引用参数
        /// </summary>
        Ref = 2,

        /// <summary>
        /// 返回值参数
        /// </summary>
        RetVal = 3
    }

    /// <summary>
    /// 参数信息解析数据接口
    /// </summary>
    public interface IParameterData
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 参数类型
        /// </summary>
        Type ParameterType { get; }

        /// <summary>
        /// 是否有默认值
        /// </summary>
        bool HasDefaultValue { get; }

        /// <summary>
        /// 默认值
        /// </summary>
        object DefaultValue { get; }

        /// <summary>
        /// 参数方向（in/out/ref）
        /// </summary>
        ParameterDirection Direction { get; }

        /// <summary>
        /// 是否为 params 参数
        /// </summary>
        bool IsParams { get; }

        /// <summary>
        /// 生成格式化的参数字符串
        /// </summary>
        /// <returns></returns>
        string GetFormattedString();
    }

    /// <summary>
    /// 参数信息解析数据
    /// </summary>
    /// <remarks>
    /// 对应 <see cref="ParameterInfo" />
    /// </remarks>
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

        public string Name { get; }

        public Type ParameterType { get; }

        public bool HasDefaultValue { get; }

        public object DefaultValue { get; }

        public ParameterDirection Direction { get; }

        public bool IsParams { get; }

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
    }
}
