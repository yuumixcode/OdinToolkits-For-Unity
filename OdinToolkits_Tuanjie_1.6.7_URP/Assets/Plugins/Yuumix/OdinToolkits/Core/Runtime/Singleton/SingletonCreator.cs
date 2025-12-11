using System;
using System.Reflection;

namespace Yuumix.OdinToolkits.Core
{
    public static class SingletonCreator
    {
        /// <summary>
        /// 以私有方式创建指定类型的实例。
        /// 这个方法用于确保单例类的唯一性，通过访问私有无参构造函数来创建实例。
        /// </summary>
        /// <typeparam name="T">要创建实例的类型，必须是class</typeparam>
        /// <returns>返回指定类型的实例。</returns>
        /// <exception cref="InvalidOperationException">如果类型没有私有无参构造函数，则抛出异常。</exception>
        public static T PrivateCreateInstance<T>() where T : class
        {
            // 获取指定类型的实例
            var type = typeof(T);
            // 获取私有无参构造函数
            var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null,
                Type.EmptyTypes, null);
            // 如果没有找到私有无参构造函数，则抛出异常
            if (constructor == null)
            {
                throw new InvalidOperationException($" {typeof(T)} class 没有 private 无参构造函数，请声明准确的构造函数");
            }

            // 调用构造函数创建实例，并将其强制转换为T类型
            return constructor.Invoke(null) as T;
        }

        public static T AssistantCreateSingleton<T>() where T : class, ISingleton
        {
            var instance = PrivateCreateInstance<T>();
            instance.OnSingletonInit();
            return instance;
        }
    }
}
