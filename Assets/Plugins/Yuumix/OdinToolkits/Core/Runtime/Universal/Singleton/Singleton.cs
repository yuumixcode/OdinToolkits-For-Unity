using System;

namespace Yuumix.OdinToolkits.Core.Runtime
{
    /// <summary>
    /// 一个通用的 C# 类对象的单例抽象类，使用 Lazy &lt;T&gt; 提供线程安全的单例实例获取方法。
    /// </summary>
    /// <typeparam name="T">继承单例的类型，单纯的 C# 对象，不继承 Mono</typeparam>
    [BilingualComment("一个通用的 C# 类对象的单例抽象类，使用 Lazy<T> 提供线程安全的单例实例获取方法。",
        "A generic singleton abstract class for C# class objects, using Lazy<T> to provide a thread - safe method for obtaining singleton instances.")]
    public abstract class Singleton<T> where T : Singleton<T>
    {
        /// <summary>
        /// 单例实例的静态存储字段，使用 Lazy&lt;T&gt; 来实现延迟初始化。
        /// </summary>
        static readonly Lazy<T> SelfIns = new Lazy<T>(SingletonCreator.PrivateCreateInstance<T>());

        /// <summary>
        /// 表示实例是否已初始化的私有字段。
        /// </summary>
        bool _isInitialized;

        /// <summary>
        /// 以线程安全的方式提供单例实例的访问。
        /// </summary>
        /// <returns>返回类型 T 的单例实例。</returns>
        [BilingualComment("以线程安全的方式提供单例实例的访问。", "Provides thread - safe access to the singleton instance.")]
        public static T Instance
        {
            get
            {
                if (SelfIns.Value._isInitialized)
                {
                    return SelfIns.Value;
                }

                SelfIns.Value.OnSingletonInit();
                SelfIns.Value._isInitialized = true;

                return SelfIns.Value;
            }
        }

        /// <summary>
        /// 实例化单例对象时执行的初始化方法。
        /// </summary>
        [BilingualComment("实例化单例对象时执行的初始化方法。",
            "Initialization method executed when instantiating the singleton object.")]
        protected virtual void OnSingletonInit() { }
    }
}
