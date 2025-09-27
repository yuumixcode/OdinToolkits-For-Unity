using System;
using System.Collections.Generic;
using System.Reflection;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.QoderTypeAnalyzer
{
    /// <summary>
    /// 成员数据工厂类
    /// </summary>
    public static class MemberDataFactory
    {
        #region 静态字段

        /// <summary>
        /// 创建器映射字典
        /// </summary>
        static readonly Dictionary<Type, Func<MemberInfo, MemberData>> CreatorMap =
            new Dictionary<Type, Func<MemberInfo, MemberData>>
            {
                { typeof(FieldInfo), info => new FieldData((FieldInfo)info) },
                { typeof(PropertyInfo), info => new PropertyData((PropertyInfo)info) },
                { typeof(MethodInfo), info => new MethodData((MethodInfo)info) },
                { typeof(ConstructorInfo), info => new MethodData((ConstructorInfo)info) },
                { typeof(EventInfo), info => CreateBaseMemberData(info) }
            };

        #endregion

        #region 私有方法

        /// <summary>
        /// 创建基础成员数据
        /// </summary>
        /// <param name="memberInfo">成员信息</param>
        /// <returns>基础成员数据</returns>
        static MemberData CreateBaseMemberData(MemberInfo memberInfo) => new BasicMemberData(memberInfo);

        #endregion

        #region 私有类

        /// <summary>
        /// 基础成员数据实现（用于不支持的成员类型）
        /// </summary>
        class BasicMemberData : MemberData
        {
            public BasicMemberData(MemberInfo memberInfo) : base(memberInfo) { }

            protected override void InitializeData()
            {
                // 设置基本的访问修饰符
                AccessModifier = AccessModifierType.Public; // 默认为 public

                // 设置类型名称
                TypeName = OriginalMemberInfo?.GetType().Name ?? "Unknown";

                // 生成简单的签名
                Signature = $"{GetAccessModifierString()} {TypeName} {Name}";

                // 生成声明
                Declaration = Signature;
            }

            public override string ToUMLString() => throw new NotImplementedException();
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 创建成员数据
        /// </summary>
        /// <param name="memberInfo">成员信息</param>
        /// <returns>成员数据</returns>
        /// <exception cref="ArgumentNullException">memberInfo 为 null</exception>
        public static MemberData CreateData(MemberInfo memberInfo)
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException(nameof(memberInfo));
            }

            Type memberType = memberInfo.GetType();

            // 查找精确匹配的创建器
            if (CreatorMap.TryGetValue(memberType, out Func<MemberInfo, MemberData> exactCreator))
            {
                return exactCreator(memberInfo);
            }

            // 查找兼容的创建器
            foreach (KeyValuePair<Type, Func<MemberInfo, MemberData>> kvp in CreatorMap)
            {
                if (kvp.Key.IsAssignableFrom(memberType))
                {
                    return kvp.Value(memberInfo);
                }
            }

            // 如果没有找到合适的创建器，创建基础成员数据
            return CreateBaseMemberData(memberInfo);
        }

        /// <summary>
        /// 创建指定类型的成员数据
        /// </summary>
        /// <typeparam name="T">成员数据类型</typeparam>
        /// <param name="memberInfo">成员信息</param>
        /// <returns>指定类型的成员数据</returns>
        /// <exception cref="ArgumentNullException">memberInfo 为 null</exception>
        /// <exception cref="InvalidOperationException">无法创建指定类型的成员数据</exception>
        public static T CreateData<T>(MemberInfo memberInfo) where T : MemberData
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException(nameof(memberInfo));
            }

            MemberData result = CreateData(memberInfo);

            if (result is T typedResult)
            {
                return typedResult;
            }

            throw new InvalidOperationException(
                $"无法将 {memberInfo.GetType().Name} 转换为 {typeof(T).Name}");
        }

        /// <summary>
        /// 批量创建成员数据
        /// </summary>
        /// <param name="memberInfos">成员信息数组</param>
        /// <returns>成员数据数组</returns>
        /// <exception cref="ArgumentNullException">memberInfos 为 null</exception>
        public static MemberData[] CreateDataArray(MemberInfo[] memberInfos)
        {
            if (memberInfos == null)
            {
                throw new ArgumentNullException(nameof(memberInfos));
            }

            var result = new MemberData[memberInfos.Length];

            for (var i = 0; i < memberInfos.Length; i++)
            {
                result[i] = CreateData(memberInfos[i]);
            }

            return result;
        }

        /// <summary>
        /// 注册自定义创建器
        /// </summary>
        /// <param name="memberType">成员类型</param>
        /// <param name="creator">创建器函数</param>
        /// <exception cref="ArgumentNullException">参数为 null</exception>
        public static void RegisterCreator(Type memberType, Func<MemberInfo, MemberData> creator)
        {
            if (memberType == null)
            {
                throw new ArgumentNullException(nameof(memberType));
            }

            if (creator == null)
            {
                throw new ArgumentNullException(nameof(creator));
            }

            CreatorMap[memberType] = creator;
        }

        /// <summary>
        /// 移除创建器
        /// </summary>
        /// <param name="memberType">成员类型</param>
        /// <returns>是否成功移除</returns>
        public static bool RemoveCreator(Type memberType)
        {
            if (memberType == null)
            {
                return false;
            }

            return CreatorMap.Remove(memberType);
        }

        /// <summary>
        /// 检查是否支持指定类型
        /// </summary>
        /// <param name="memberType">成员类型</param>
        /// <returns>是否支持</returns>
        public static bool IsSupported(Type memberType)
        {
            if (memberType == null)
            {
                return false;
            }

            // 检查精确匹配
            if (CreatorMap.ContainsKey(memberType))
            {
                return true;
            }

            // 检查兼容匹配
            foreach (Type key in CreatorMap.Keys)
            {
                if (key.IsAssignableFrom(memberType))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
