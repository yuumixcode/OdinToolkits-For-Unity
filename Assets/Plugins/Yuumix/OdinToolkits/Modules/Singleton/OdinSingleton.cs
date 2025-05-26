using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Singleton
{
    /// <summary>
    /// 继承了 SerializedMonoBehaviour 的单例抽象类，非持久化单例，默认删除后生成的实例
    /// </summary>
    /// <typeparam name="T">泛型参数，指定单例的类型</typeparam>
    public abstract class OdinSingleton<T> : SerializedMonoBehaviour where T : OdinSingleton<T>
    {
        /// <summary>
        /// 单例实例的静态存储字段。
        /// </summary>
        static T _instance;

        /// <summary>
        /// 获取当前单例实例的方法。如果实例不存在，则尝试自动创建。
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<T>();
                    if (_instance == null)
                    {
                        var go = new GameObject("[Auto-Generated] " + typeof(T).Name);
                        go.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// 默认删除后生成的实例，维持单例
        /// </summary>
        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this as T;
                // 设置自定义名称
                if (SetCustomName() != string.Empty)
                {
                    gameObject.name = SetCustomName();
                }

                CustomBehaviour();
            }
        }

        /// <summary>
        /// 对象销毁时的处理逻辑，确保单例引用被正确置空，保留 base.OnDestroy()，且将其置于最后执行
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (_instance != null && _instance == this)
            {
                _instance = null;
            }
        }

        /// <summary>
        /// 重写该方法可以设置自定义名称，不需要保留 base.SetCustomName()
        /// </summary>
        /// <returns> </returns>
        protected virtual string SetCustomName() => string.Empty;

        /// <summary>
        /// Awake 最后调用的方法，可以在这里添加自定义行为
        /// </summary>
        protected virtual void CustomBehaviour() { }
    }
}
