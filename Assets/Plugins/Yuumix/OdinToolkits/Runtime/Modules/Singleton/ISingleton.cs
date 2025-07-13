using System;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits
{
    [MultiLanguageComment("单例初始化接口，实现该接口的类需要实现单例初始化方法。",
        "Singleton initialization interface. Classes implementing this interface need to implement the singleton initialization method.")]
    public interface ISingleton
    {
        [MultiLanguageComment("单例初始化方法，在单例实例创建后调用。",
            "Singleton initialization method, called after the singleton instance is created.")]
        void OnSingletonInit();
    }
}
