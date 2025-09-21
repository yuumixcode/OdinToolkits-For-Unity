using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// OdinToolkits 的运行时配置
    /// </summary>
    public class OdinToolkitsRuntimeConfigSO : ScriptableObject, IOdinToolkitsRuntimeReset
    {
        static OdinToolkitsRuntimeConfigSO _instance;

        public static OdinToolkitsRuntimeConfigSO Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = Resources.Load<OdinToolkitsRuntimeConfigSO>("OdinToolkitsRuntimeConfig");
                }

                return _instance;
            }
        }

        void OnEnable()
        {
            if (yuumixLoggerConfig == null)
            {
                yuumixLoggerConfig = new YuumixLoggerConfig();
                yuumixLoggerConfig.RuntimeReset();
            }
        }

        [HideLabel]
        public YuumixLoggerConfig yuumixLoggerConfig;

        public void RuntimeReset()
        {
            yuumixLoggerConfig.RuntimeReset();
        }
    }
}
