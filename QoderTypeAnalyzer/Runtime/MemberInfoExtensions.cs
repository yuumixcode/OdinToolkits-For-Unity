using System;
using System.Reflection;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.QoderTypeAnalyzer
{
    /// <summary>
    /// MemberInfo 扩展方法类
    /// </summary>
    public static class MemberInfoExtensions
    {
        #region AsData 系列方法
        
        /// <summary>
        /// 将 MemberInfo 转换为 MemberData
        /// </summary>
        /// <param name="memberInfo">成员信息</param>
        /// <returns>成员数据</returns>
        /// <exception cref="ArgumentNullException">memberInfo 为 null</exception>
        public static MemberData AsData(this MemberInfo memberInfo)
        {
            if (memberInfo == null)
                throw new ArgumentNullException(nameof(memberInfo));
                
            return MemberDataFactory.CreateData(memberInfo);
        }
        
        /// <summary>
        /// 将 FieldInfo 转换为 FieldData
        /// </summary>
        /// <param name="fieldInfo">字段信息</param>
        /// <returns>字段数据</returns>
        /// <exception cref="ArgumentNullException">fieldInfo 为 null</exception>
        public static FieldData AsFieldData(this FieldInfo fieldInfo)
        {
            if (fieldInfo == null)
                throw new ArgumentNullException(nameof(fieldInfo));
                
            return new FieldData(fieldInfo);
        }
        
        /// <summary>
        /// 将 PropertyInfo 转换为 PropertyData
        /// </summary>
        /// <param name="propertyInfo">属性信息</param>
        /// <returns>属性数据</returns>
        /// <exception cref="ArgumentNullException">propertyInfo 为 null</exception>
        public static PropertyData AsPropertyData(this PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException(nameof(propertyInfo));
                
            return new PropertyData(propertyInfo);
        }
        
        /// <summary>
        /// 将 MethodInfo 转换为 MethodData
        /// </summary>
        /// <param name="methodInfo">方法信息</param>
        /// <returns>方法数据</returns>
        /// <exception cref="ArgumentNullException">methodInfo 为 null</exception>
        public static MethodData AsMethodData(this MethodInfo methodInfo)
        {
            if (methodInfo == null)
                throw new ArgumentNullException(nameof(methodInfo));
                
            return new MethodData(methodInfo);
        }
        
        /// <summary>
        /// 将 ConstructorInfo 转换为 MethodData
        /// </summary>
        /// <param name="constructorInfo">构造函数信息</param>
        /// <returns>方法数据</returns>
        /// <exception cref="ArgumentNullException">constructorInfo 为 null</exception>
        public static MethodData AsMethodData(this ConstructorInfo constructorInfo)
        {
            if (constructorInfo == null)
                throw new ArgumentNullException(nameof(constructorInfo));
                
            return new MethodData(constructorInfo);
        }
        
        #endregion
        
        #region 便捷格式化方法
        
        /// <summary>
        /// 生成可读的简洁字符串
        /// </summary>
        /// <param name="memberInfo">成员信息</param>
        /// <returns>可读的简洁字符串</returns>
        /// <exception cref="ArgumentNullException">memberInfo 为 null</exception>
        public static string ToReadableString(this MemberInfo memberInfo)
        {
            if (memberInfo == null)
                throw new ArgumentNullException(nameof(memberInfo));
                
            try
            {
                var memberData = memberInfo.AsData();
                return memberData.ToFormattedString();
            }
            catch
            {
                // 如果转换失败，返回基本信息
                return $"{memberInfo.MemberType} {memberInfo.Name}";
            }
        }
        
        /// <summary>
        /// 生成详细的描述字符串
        /// </summary>
        /// <param name="memberInfo">成员信息</param>
        /// <returns>详细的描述字符串</returns>
        /// <exception cref="ArgumentNullException">memberInfo 为 null</exception>
        public static string ToDetailedDescription(this MemberInfo memberInfo)
        {
            if (memberInfo == null)
                throw new ArgumentNullException(nameof(memberInfo));
                
            try
            {
                var memberData = memberInfo.AsData();
                return memberData.ToDetailedString();
            }
            catch
            {
                // 如果转换失败，返回基本信息
                var result = $"{memberInfo.MemberType} {memberInfo.Name}";
                
                // 尝试获取中文描述
                var chineseDesc = ChineseSummaryAttribute.GetChineseSummary(memberInfo);
                if (!string.IsNullOrEmpty(chineseDesc))
                    result += $" : {chineseDesc}";
                    
                return result;
            }
        }
        
        #endregion
        
        #region 批量转换方法
        
        /// <summary>
        /// 批量转换成员信息为成员数据
        /// </summary>
        /// <param name="memberInfos">成员信息数组</param>
        /// <returns>成员数据数组</returns>
        /// <exception cref="ArgumentNullException">memberInfos 为 null</exception>
        public static MemberData[] AsDataArray(this MemberInfo[] memberInfos)
        {
            if (memberInfos == null)
                throw new ArgumentNullException(nameof(memberInfos));
                
            return MemberDataFactory.CreateDataArray(memberInfos);
        }
        
        /// <summary>
        /// 批量转换字段信息为字段数据
        /// </summary>
        /// <param name="fieldInfos">字段信息数组</param>
        /// <returns>字段数据数组</returns>
        /// <exception cref="ArgumentNullException">fieldInfos 为 null</exception>
        public static FieldData[] AsFieldDataArray(this FieldInfo[] fieldInfos)
        {
            if (fieldInfos == null)
                throw new ArgumentNullException(nameof(fieldInfos));
                
            var result = new FieldData[fieldInfos.Length];
            for (int i = 0; i < fieldInfos.Length; i++)
            {
                result[i] = fieldInfos[i].AsFieldData();
            }
            return result;
        }
        
        /// <summary>
        /// 批量转换属性信息为属性数据
        /// </summary>
        /// <param name="propertyInfos">属性信息数组</param>
        /// <returns>属性数据数组</returns>
        /// <exception cref="ArgumentNullException">propertyInfos 为 null</exception>
        public static PropertyData[] AsPropertyDataArray(this PropertyInfo[] propertyInfos)
        {
            if (propertyInfos == null)
                throw new ArgumentNullException(nameof(propertyInfos));
                
            var result = new PropertyData[propertyInfos.Length];
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                result[i] = propertyInfos[i].AsPropertyData();
            }
            return result;
        }
        
        /// <summary>
        /// 批量转换方法信息为方法数据
        /// </summary>
        /// <param name="methodInfos">方法信息数组</param>
        /// <returns>方法数据数组</returns>
        /// <exception cref="ArgumentNullException">methodInfos 为 null</exception>
        public static MethodData[] AsMethodDataArray(this MethodInfo[] methodInfos)
        {
            if (methodInfos == null)
                throw new ArgumentNullException(nameof(methodInfos));
                
            var result = new MethodData[methodInfos.Length];
            for (int i = 0; i < methodInfos.Length; i++)
            {
                result[i] = methodInfos[i].AsMethodData();
            }
            return result;
        }
        
        #endregion
        
        #region 辅助方法
        
        /// <summary>
        /// 检查成员是否支持转换为指定的数据类型
        /// </summary>
        /// <typeparam name="T">目标数据类型</typeparam>
        /// <param name="memberInfo">成员信息</param>
        /// <returns>是否支持转换</returns>
        public static bool CanConvertTo<T>(this MemberInfo memberInfo) where T : MemberData
        {
            if (memberInfo == null)
                return false;
                
            try
            {
                var memberData = memberInfo.AsData();
                return memberData is T;
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// 安全转换为指定的数据类型
        /// </summary>
        /// <typeparam name="T">目标数据类型</typeparam>
        /// <param name="memberInfo">成员信息</param>
        /// <returns>转换后的数据，如果转换失败则返回 null</returns>
        public static T TryAsData<T>(this MemberInfo memberInfo) where T : MemberData
        {
            if (memberInfo == null)
                return null;
                
            try
            {
                var memberData = memberInfo.AsData();
                return memberData as T;
            }
            catch
            {
                return null;
            }
        }
        
        #endregion
    }
}