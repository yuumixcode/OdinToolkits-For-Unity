using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core
{
    public class Yuumix :
#if UNITY_EDITOR
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

        OdinToolkitsRuntimeConfigSO _odinToolkitsRuntimeConfig;

        public OdinToolkitsRuntimeConfigSO OdinToolkitsRuntimeConfig
        {
            get
            {
                if (!_odinToolkitsRuntimeConfig)
                {
                    _odinToolkitsRuntimeConfig = LoadRuntimeConfigSO();
                }

                return _odinToolkitsRuntimeConfig;
            }
        }

        void Awake()
        {
            if (!_instance)
            {
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

            _odinToolkitsRuntimeConfig = LoadRuntimeConfigSO();
        }

        static OdinToolkitsRuntimeConfigSO LoadRuntimeConfigSO()
        {
            var asset = Resources.Load<OdinToolkitsRuntimeConfigSO>("OdinToolkitsRuntimeConfig");
            if (!asset)
            {
                YuumixLogger.LogError(nameof(OdinToolkitsRuntimeConfigSO) + " 配置资源加载失败，需要检查 Resources 路径！",
                    prefix: "[Yuumix Error]");
            }

            return asset;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void YuumixGameEntry()
        {
            _instance = new GameObject("[Yuumix]").AddComponent<Yuumix>();
        }
    }
}
