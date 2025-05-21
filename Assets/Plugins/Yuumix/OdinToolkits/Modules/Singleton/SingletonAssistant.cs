using System;

namespace Yuumix.OdinToolkits.Modules.Singleton
{
    /// <summary>
    /// 单例助手类，用于无法继承单例抽象类的类实现单例模式。
    /// T 必须继承 ISingleton 接口，且声明私有的无参数构造函数。
    /// 使用Lazy&lt;T&gt;确保单例实例在第一次使用时且只在第一次使用时被创建，实现了线程安全的懒加载。
    /// </summary>
    /// <typeparam name="T">约束T为类类型，确保单例模式应用于类实例。</typeparam>
    public sealed class SingletonAssistant<T> where T : class, ISingleton
    {
        // 使用Lazy<T>确保单例实例在第一次访问时且只在第一次访问时被创建。
        private static readonly Lazy<T> Instance = new Lazy<T>(SingletonCreator.AssistantCreateSingleton<T>());

        // 私有构造函数，防止外部实例化。
        private SingletonAssistant()
        {
        }

        /// <summary>
        /// 静态方法，用于获取T类型的单例实例。
        /// 如果实例尚未创建，则此方法将创建并返回新实例；如果实例已存在，则返回现有的实例。
        /// </summary>
        /// <returns>T类型的单例实例，并且完成初始化</returns>
        public static T GetInit()
        {
            return Instance.Value;
        }
    }
}