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

        #region Serialized Fields

        [HideLabel]
        public YuumixLoggerConfig yuumixLoggerConfig;

        #endregion

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

        #region Event Functions

        void OnEnable()
        {
            if (yuumixLoggerConfig == null)
            {
                yuumixLoggerConfig = new YuumixLoggerConfig();
                yuumixLoggerConfig.RuntimeReset();
            }
        }

        #endregion

        #region IOdinToolkitsRuntimeReset Members

        public void RuntimeReset()
        {
            yuumixLoggerConfig.RuntimeReset();
        }

        #endregion
    }
}
