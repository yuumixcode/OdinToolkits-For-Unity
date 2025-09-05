using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core.Runtime
{
    [BilingualComment("持久化的 Odin SerializedMonoBehaviour 单例抽象类，在场景切换时不会被销毁。",
        "Abstract class for a persistent Odin SerializedMonoBehaviour singleton that is not destroyed when the scene changes.")]
    public abstract class PersistentOdinSingleton<T> : SerializedMonoBehaviour where T : PersistentOdinSingleton<T>
    {
        bool _isInitialized;
        static T _instance;

        [BilingualComment("获取单例实例，如果实例不存在则创建一个新的实例。",
            "Gets the singleton instance. If the instance does not exist, a new instance is created.")]
        public static T Instance
        {
            get
            {
                // YuumixLogger.Log("Instance 属性被访问，当前 _instance: " + (_instance ? "已赋值" : "null"));
                if (_instance)
                {
                    return _instance;
                }

                _instance = FindAnyObjectByType<T>();
                // YuumixLogger.Log("FindAnyObjectByType<T>() 后 _instance: " + (_instance ? "已赋值" : "null"));
                if (_instance)
                {
                    return _instance;
                }

                _instance = new GameObject(typeof(T).Name + " [Auto - Singleton]")
                    .AddComponent<T>();
                // YuumixLogger.Log("new GameObject().AddComponent 后 _instance: " + (_instance ? "已赋值" : "null"));
                return _instance;
            }
        }

        [BilingualComment("Awake 方法，用于初始化单例实例并设置为不销毁。",
            "Awake method, used to initialize the singleton instance and set it to not be destroyed.")]
        protected virtual void Awake()
        {
            if (!_instance)
            {
                _instance = this as T;
                // YuumixLogger.Log("Awake 方法内赋值 _instance");
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

        [BilingualComment("OnDestroy 方法，当单例实例被销毁时调用。",
            "OnDestroy method, called when the singleton instance is destroyed.")]
        protected virtual void OnDestroy()
        {
            if (_instance && _instance == this)
            {
                _instance = null;
            }
        }

        [BilingualComment("创建一个新的单例实例。", "Creates a new singleton instance.")]
        public static void CreateNewInstance()
        {
            DestroyCurrentInstance();
            _ = Instance;
        }

        [BilingualComment("销毁当前的单例实例。", "Destroys the current singleton instance.")]
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

        [BilingualComment("实例化单例对象时执行的初始化方法。",
            "Initialization method executed when instantiating the singleton object.")]
        protected virtual void OnSingletonInit() { }
    }
}
