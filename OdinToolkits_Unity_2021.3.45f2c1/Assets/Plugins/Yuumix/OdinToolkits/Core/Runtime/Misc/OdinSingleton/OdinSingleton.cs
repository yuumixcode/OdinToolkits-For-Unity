using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core
{
    public abstract class OdinSingleton<T> : SerializedMonoBehaviour where T : OdinSingleton<T>
    {
        static T _instance;

        #region Serialized Fields

        bool _isInitialized;

        #endregion

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

        public static void CreateNewInstance()
        {
            DestroyCurrentInstance();
            _instance = Instance;
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

        #region Event Functions

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

        #endregion
    }
}
