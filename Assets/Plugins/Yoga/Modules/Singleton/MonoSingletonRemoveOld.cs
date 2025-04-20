using UnityEngine;

namespace YOGA.Modules.Singleton
{
    /// <summary>
    /// 继承了 MonoBehaviour 的单例抽象类，提供非持久化单例模式。提供一个强制生成新的实例的方法
    /// </summary>
    /// <typeparam name="T">泛型参数，指定单例的类型</typeparam>
    public abstract class MonoSingletonRemoveOld<T> : MonoBehaviour where T : MonoSingletonRemoveOld<T>
    {
        /// <summary>
        /// 单例实例的静态存储字段。
        /// </summary>
        static T _instance;

        /// <summary>
        /// 单例的访问器，确保全局唯一实例的获取。
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
                        // 如果当前没有实例，则创建一个新的游戏对象并附加该单例组件
                        var go = new GameObject("[Auto-Generated] " + typeof(T).Name);
                        go.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// 当脚本实例化时调用的方法，用于处理单例逻辑。
        /// </summary>
        protected virtual void Awake()
        {
            // 确保仅存在一个实例，如果发现已有实例且不是当前实例，则销毁它并设置当前实例
            if (_instance != null && _instance != this)
            {
                Destroy(_instance.gameObject);
                _instance = null;
                _instance = this as T;
                // 设置自定义名称，如果有必要
                if (SetCustomName() != string.Empty)
                {
                    gameObject.name = SetCustomName();
                }

                CustomBehaviour();
                return;
            }

            // 如果当前没有实例，则将当前实例设置为单例实例，并执行自定义逻辑
            if (_instance == null)
            {
                _instance = this as T;
                // 设置自定义名称，如果有必要
                if (SetCustomName() != string.Empty)
                {
                    gameObject.name = SetCustomName();
                }

                CustomBehaviour();
            }
        }

        /// <summary>
        /// 当对象销毁时调用的方法，用于清理单例引用。
        /// </summary>
        protected virtual void OnDestroy()
        {
            // 确保在对象销毁时单例引用被正确置空
            if (_instance != null && _instance == this)
            {
                _instance = null;
            }
        }

        /// <summary>
        /// 重写该方法以设置自定义名称。
        /// </summary>
        /// <returns>返回设置的自定义名称，如果不需要则返回空字符串。</returns>
        protected virtual string SetCustomName()
        {
            return string.Empty;
        }

        /// <summary>
        /// Awake 函数最后调用的方法，可用于添加自定义行为。
        /// </summary>
        protected virtual void CustomBehaviour() { }

        /// <summary>
        /// 强制创建新的实例的方法，可用于销毁当前实例并生成新的实例。
        /// </summary>
        public static void ForceCreateNewInstance()
        {
            // 创建新的游戏对象并附加该单例组件
            var go = new GameObject("[Auto-Generated] " + typeof(T).Name);
            go.AddComponent<T>();
        }
    }
}
