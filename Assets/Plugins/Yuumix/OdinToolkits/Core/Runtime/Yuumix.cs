using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core
{
    public class Yuumix :
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

        OdinToolkitsPreferencesSO _odinToolkitsPreferences;

        public OdinToolkitsPreferencesSO OdinToolkitsPreferences
        {
            get
            {
                if (!_odinToolkitsPreferences)
                {
                    _odinToolkitsPreferences = LoadPreferences();
                }

                return _odinToolkitsPreferences;
            }
        }

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
            _odinToolkitsPreferences = LoadPreferences();
        }

        static OdinToolkitsPreferencesSO LoadPreferences()
        {
            var asset = Resources.Load<OdinToolkitsPreferencesSO>("OdinToolkitsPreferences");
            if (!asset)
            {
                YuumixLogger.LogError(nameof(OdinToolkitsPreferencesSO) + " 配置资源加载失败，需要检查 Resources 路径！",
                    prefix: "[Yuumix Error]");
            }

            return asset;
        }

        void OnDestroy()
        {
            if (_instance && _instance == this)
            {
                _instance = null;
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void YuumixGameEntry()
        {
            _instance = new GameObject("[Yuumix]").AddComponent<Yuumix>();
        }
    }
}
