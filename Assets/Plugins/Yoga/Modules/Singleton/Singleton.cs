using System;

namespace YOGA.Modules.Singleton
{
    /// <summary>
    /// 一个通用的 C# 类对象的单例抽象类，使用 Lazy&lt;T&gt; 提供线程安全的单例实例获取方法。
    /// </summary>
    /// <typeparam name="T">继承单例的类型，单纯的 C# 对象，不继承 Mono</typeparam>
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
        /// 保护构造函数，防止外部直接实例化。
        /// </summary>
        protected Singleton() { }

        /// <summary>
        /// 以线程安全的方式提供单例实例的访问。
        /// </summary>
        /// <returns>返回类型 T 的单例实例。</returns>
        public static T Instance
        {
            get
            {
                if (!SelfIns.Value._isInitialized)
                {
                    SelfIns.Value.OnSingletonInit();
                    SelfIns.Value._isInitialized = true;
                }

                return SelfIns.Value;
            }
        }

        /// <summary>
        /// 实例化单例对象时执行的初始化方法。
        /// </summary>
        protected virtual void OnSingletonInit() { }
    }
}
