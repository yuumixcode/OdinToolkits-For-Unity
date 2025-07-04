using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Common.Logger;

namespace Yuumix.OdinToolkits.Common
{
    public sealed class Yuumix :
#if UNITY_EDITOR && (ODIN_INSPECTOR_3 || ODIN_INSPECTOR_3_1 || ODIN_INSPECTOR_3_2 || ODIN_INSPECTOR_3_3)
        SerializedMonoBehaviour
#else
        MonoBehaviour
#endif
    {
        static Yuumix _instance;

        public static Yuumix Instance
        {
            get
            {
                if (_instance)
                {
                    return _instance;
                }

                _instance = FindAnyObjectByType<Yuumix>();
                if (_instance)
                {
                    return _instance;
                }

                _instance = new GameObject("[Yuumix]")
                    .AddComponent<Yuumix>();
                return _instance;
            }
        }

        public OdinToolkitsRuntimeConfig OdinToolkitsRuntimeConfig
        {
            get
            {
                if (!_odinToolkitsRuntimeConfig)
                {
                    _odinToolkitsRuntimeConfig =
                        Resources.Load<OdinToolkitsRuntimeConfig>("Runtime_OdinToolkitsRuntimeConfig");
                }

                return _odinToolkitsRuntimeConfig;
            }
        }

        OdinToolkitsRuntimeConfig _odinToolkitsRuntimeConfig;

        void Awake()
        {
            if (!_instance)
            {
                _instance = this;
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

            // 初始加载一次
            _odinToolkitsRuntimeConfig = Resources.Load<OdinToolkitsRuntimeConfig>("Runtime_OdinToolkitsRuntimeConfig");
            if (!_odinToolkitsRuntimeConfig)
            {
                YuumixLogger.EditorLogError("OdinToolkitsRuntimeConfig 配置资源加载失败，需要检查 Resources 路径！",
                    prefix: "[Yuumix Error]");
            }
        }

        void OnDestroy()
        {
            if (_instance && _instance == this)
            {
                _instance = null;
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void LaunchYuumix()
        {
            _ = Instance;
        }
    }
}
