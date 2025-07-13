using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits
{
    [MultiLanguageComment("持久化的 MonoBehavior 单例抽象类，在场景切换时不会被销毁。",
        "Abstract class for a persistent MonoBehaviour singleton that is not destroyed when the scene changes.")]
    public abstract class PersistentMonoSingleton<T> : MonoBehaviour where T : PersistentMonoSingleton<T>
    {
        bool _isInitialized;
        static T _instance;

        [MultiLanguageComment("获取单例实例，如果实例不存在则创建一个新的实例。",
            "Gets the singleton instance. If the instance does not exist, a new instance is created.")]
        public static T Instance
        {
            get
            {
                if (_instance)
                {
                    return _instance;
                }

                _instance = FindAnyObjectByType<T>();
                if (_instance)
                {
                    return _instance;
                }

                _instance = new GameObject(typeof(T).Name + " [Auto - Singleton]")
                    .AddComponent<T>();
                return _instance;
            }
        }

        [MultiLanguageComment("Awake 方法，用于初始化单例实例并设置为不销毁。",
            "Awake method, used to initialize the singleton instance and set it to not be destroyed.")]
        protected virtual void Awake()
        {
            if (!_instance)
            {
                _instance = this as T;
                if (_isInitialized)
                {
                    return;
                }

                _isInitialized = true;
                OnSingletonInit();
                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (Application.isPlaying)
                {
                    Destroy(gameObject);
                }
                else
                {
                    DestroyImmediate(gameObject);
                }
            }
        }

        [MultiLanguageComment("OnDestroy 方法，当单例实例被销毁时调用。",
            "OnDestroy method, called when the singleton instance is destroyed.")]
        protected virtual void OnDestroy()
        {
            if (_instance && _instance == this)
            {
                _instance = null;
            }
        }

        [MultiLanguageComment("创建一个新的单例实例。", "Creates a new singleton instance.")]
        public static void CreateNewInstance()
        {
            DestroyCurrentInstance();
            _instance = Instance;
        }

        [MultiLanguageComment("销毁当前的单例实例。", "Destroys the current singleton instance.")]
        static void DestroyCurrentInstance()
        {
            if (Application.isPlaying)
            {
                Destroy(_instance.gameObject);
            }
            else
            {
                DestroyImmediate(_instance.gameObject);
            }

            _instance = null;
        }

        [MultiLanguageComment("实例化单例对象时执行的初始化方法。",
            "Initialization method executed when instantiating the singleton object.")]
        protected virtual void OnSingletonInit() { }
    }
}
