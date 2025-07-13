using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core
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
            if (inspectorMultiLanguageSetting == null)
            {
                inspectorMultiLanguageSetting = new InspectorMultiLanguageSetting();
                inspectorMultiLanguageSetting.OdinToolkitsReset();
            }

            if (yuumixLoggerSetting == null)
            {
                yuumixLoggerSetting = new YuumixLoggerSetting();
                yuumixLoggerSetting.OdinToolkitsReset();
            }
        }

        [PropertyOrder(-99)]
        [HideLabel]
        public InspectorMultiLanguageSetting inspectorMultiLanguageSetting;

        [HideLabel]
        public YuumixLoggerSetting yuumixLoggerSetting;

        public void OdinToolkitsReset()
        {
            inspectorMultiLanguageSetting.OdinToolkitsReset();
            yuumixLoggerSetting.OdinToolkitsReset();
        }
    }
}
