namespace Yuumix.OdinToolkits.Modules.Singleton
{
    /// <summary>
    /// 定义了一个单例模式的接口。
    /// </summary>
    /// <remarks>
    /// 单例模式是一种设计模式，用于确保一个类只有一个实例，并提供一个全局访问点。
    /// </remarks>
    public interface ISingleton
    {
        /// <summary>
        /// 单例助手进行初始化时自动调用的方法，不要在外部手动调用。如果可以继承抽象基类，则不需要接口
        /// </summary>
        void OnSingletonInit();
    }
}