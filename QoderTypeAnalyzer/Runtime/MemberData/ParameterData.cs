using System;
using System.Reflection;

namespace Yuumix.OdinToolkits.Modules.QoderTypeAnalyzer
{
    /// <summary>
    /// 参数信息数据类
    /// </summary>
    [Serializable]
    public class ParameterData
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 参数类型
        /// </summary>
        public Type ParameterType { get; set; }
        
        /// <summary>
        /// 是否有默认值
        /// </summary>
        public bool HasDefaultValue { get; set; }
        
        /// <summary>
        /// 默认值
        /// </summary>
        public object DefaultValue { get; set; }
        
        /// <summary>
        /// 参数方向（in/out/ref）
        /// </summary>
        public ParameterDirection Direction { get; set; }
        
        /// <summary>
        /// 是否为 params 参数
        /// </summary>
        public bool IsParams { get; set; }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public ParameterData()
        {
            Name = string.Empty;
            Direction = ParameterDirection.In;
        }
        
        /// <summary>
        /// 从 ParameterInfo 创建 ParameterData
        /// </summary>
        /// <param name="parameterInfo">参数信息</param>
        /// <returns>参数数据</returns>
        public static ParameterData FromParameterInfo(ParameterInfo parameterInfo)
        {
            if (parameterInfo == null)
                throw new ArgumentNullException(nameof(parameterInfo));
                
            var direction = ParameterDirection.In;
            if (parameterInfo.IsOut)
                direction = ParameterDirection.Out;
            else if (parameterInfo.ParameterType.IsByRef)
                direction = ParameterDirection.Ref;
            else if (parameterInfo.IsRetval)
                direction = ParameterDirection.RetVal;
                
            return new ParameterData
            {
                Name = parameterInfo.Name ?? string.Empty,
                ParameterType = parameterInfo.ParameterType,
                HasDefaultValue = parameterInfo.HasDefaultValue,
                DefaultValue = parameterInfo.HasDefaultValue ? parameterInfo.DefaultValue : null,
                Direction = direction,
                IsParams = parameterInfo.IsDefined(typeof(ParamArrayAttribute), false)
            };
        }
        
        /// <summary>
        /// 生成格式化的参数字符串
        /// </summary>
        /// <returns>格式化的参数字符串</returns>
        public string ToFormattedString()
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
                result += "params ";
                
            // 添加类型名称
            var typeName = ParameterType?.Name ?? "object";
            if (ParameterType?.IsByRef == true)
                typeName = typeName.TrimEnd('&');
                
            result += typeName;
            
            // 添加参数名称
            if (!string.IsNullOrEmpty(Name))
                result += " " + Name;
                
            // 添加默认值
            if (HasDefaultValue)
            {
                result += " = ";
                if (DefaultValue == null)
                    result += "null";
                else if (DefaultValue is string)
                    result += "\"" + DefaultValue + "\"";
                else
                    result += DefaultValue.ToString();
            }
            
            return result;
        }
        
        /// <summary>
        /// 转换为 UML 参数格式：[方向][名称]:[类型][=默认值]
        /// 示例：param1: string
        ///      out result: int
        ///      ref value: object = null
        /// </summary>
        /// <returns>UML 格式的参数表示</returns>
        public string ToUMLString()
        {
            var direction = Direction switch
            {
                ParameterDirection.Out => "out ",
                ParameterDirection.Ref => "ref ",
                _ => ""
            };
            
            var defaultValue = HasDefaultValue ? $" = {(DefaultValue?.ToString() ?? "null")}" : "";
            var typeName = ParameterType?.Name ?? "object";
            
            // 处理引用类型
            if (ParameterType?.IsByRef == true)
                typeName = typeName.TrimEnd('&');
                
            return $"{direction}{Name}: {typeName}{defaultValue}";
        }
    }
}