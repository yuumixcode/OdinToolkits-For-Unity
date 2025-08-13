using System;

namespace Yuumix.Universal
{
    /// <summary>
    /// 单例助手类，用于无法继承单例抽象类的类实现单例模式。
    /// T 必须继承 ISingleton 接口，且声明私有的无参数构造函数。
    /// 使用Lazy&lt;T&gt;确保单例实例在第一次使用时且只在第一次使用时被创建，实现了线程安全的懒加载。
    /// </summary>
    /// <typeparam name="T">约束T为类类型，确保单例模式应用于类实例。</typeparam>
    [BilingualComment("单例助手类，用于无法继承单例抽象类的类实现单例模式。",
        "Singleton assistant class, used for classes that cannot inherit from the singleton abstract class to implement the singleton pattern.")]
    public sealed class SingletonAssistant<T> where T : class, ISingleton
    {
        static readonly Lazy<T> Instance = new Lazy<T>(SingletonCreator.AssistantCreateSingleton<T>());

        SingletonAssistant() { }

        [BilingualComment("获取单例实例。", "Gets the singleton instance.")]
        public static T GetInstance() => Instance.Value;
    }
}
