using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core
{
    public abstract class PersistentOdinSingleton<T> : SerializedMonoBehaviour where T : PersistentOdinSingleton<T>
    {
        static T _instance;
        bool _isInitialized;

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

        protected virtual void OnDestroy()
        {
            if (_instance && _instance == this)
            {
                _instance = null;
            }
        }

        public static void CreateNewInstance()
        {
            DestroyCurrentInstance();
            _ = Instance;
        }

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

        protected virtual void OnSingletonInit() { }
    }
}
