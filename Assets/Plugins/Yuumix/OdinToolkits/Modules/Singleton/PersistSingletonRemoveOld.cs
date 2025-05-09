using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Singleton
{
    /// <summary>
    /// 继承了 Mono 的单例抽象类，持久化单例，强制生成新的实例可以销毁旧的实例
    /// </summary>
    /// <typeparam name="T">泛型参数，指定单例的类型</typeparam>
    public abstract class PersistSingletonRemoveOld<T> : MonoBehaviour
        where T : PersistSingletonRemoveOld<T>
    {
        /// <summary>
        /// 单例实例的静态存储字段。
        /// </summary>
        private static T _instance;

        /// <summary>
        /// 单例的访问器，确保全局唯一实例的获取。
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                _instance = FindAnyObjectByType<T>();
                if (_instance == null)
                {
                    var go = new GameObject("[Auto-Generated] " + typeof(T).Name);
                    go.AddComponent<T>();
                }

                return _instance;
            }
        }

        /// <summary>
        /// 当脚本实例化时调用的方法，用于处理单例逻辑。
        /// </summary>
        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(_instance.gameObject);
                _instance = null;
                _instance = this as T;
                // 设置自定义名称
                if (SetCustomName() != string.Empty)
                {
                    gameObject.name = SetCustomName();
                }

                DontDestroyOnLoad(gameObject);
                CustomBehaviour();
                return;
            }

            if (_instance == null)
            {
                _instance = this as T;
                // 设置自定义名称
                if (SetCustomName() != string.Empty)
                {
                    gameObject.name = SetCustomName();
                }

                DontDestroyOnLoad(gameObject);
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
        /// 重写该方法可以设置自定义名称，不需要 base.SetCustomName() 调用
        /// </summary>
        protected virtual string SetCustomName()
        {
            return string.Empty;
        }

        /// <summary>
        /// Awake 最后调用的方法，可以在这里添加自定义行为
        /// </summary>
        protected virtual void CustomBehaviour()
        {
        }

        /// <summary>
        /// 强制生成新的实例
        /// </summary>
        public static void ForceCreateNewInstance()
        {
            var go = new GameObject("[Auto-Generated] " + typeof(T).Name);
            go.AddComponent<T>();
        }
    }
}