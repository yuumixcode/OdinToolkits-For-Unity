using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core.Runtime
{
    /// <summary>
    /// OdinToolkits 插件的全局设置
    /// </summary>
    public class OdinToolkitsPreferencesSO : ScriptableObject, IOdinToolkitsReset
    {
        static OdinToolkitsPreferencesSO _instance;

        public static OdinToolkitsPreferencesSO Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = Resources.Load<OdinToolkitsPreferencesSO>("OdinToolkitsPreferences");
                }

                return _instance;
            }
        }

        void OnEnable()
        {
            if (bilingualSetting == null)
            {
                bilingualSetting = new BilingualSetting();
                bilingualSetting.OdinToolkitsReset();
            }

            if (yuumixLoggerSetting == null)
            {
                yuumixLoggerSetting = new YuumixLoggerSetting();
                yuumixLoggerSetting.OdinToolkitsReset();
            }
        }

        [PropertyOrder(-99)]
        [HideLabel]
        public BilingualSetting bilingualSetting;

        [HideLabel]
        public YuumixLoggerSetting yuumixLoggerSetting;

        public void OdinToolkitsReset()
        {
            bilingualSetting.OdinToolkitsReset();
            yuumixLoggerSetting.OdinToolkitsReset();
        }
    }
}
