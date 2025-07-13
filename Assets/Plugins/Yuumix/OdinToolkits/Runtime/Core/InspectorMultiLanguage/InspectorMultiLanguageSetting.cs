using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core
{
    [Serializable]
    public enum LanguageType
    {
        [InspectorName("简体中文")]
        Chinese,
        English
    }

    /// <summary>
    /// Inspector 面板的多语言管理器，用于设置当前语言
    /// </summary>
    [Serializable]
    [MultiLanguageComment("Inspector 面板的多语言管理器，用于设置当前语言",
        "Inspector panel's multilingual manager for setting the current language")]
    public class InspectorMultiLanguageSetting : IOdinToolkitsReset
    {
        [PropertyOrder(5)]
        [ShowInInspector]
        [EnumToggleButtons]
        [HideLabel]
        [MultiLanguageTitle("Odin Toolkits 语言", "Odin Toolkits Language", beforeSpace: false)]
        public LanguageType CurrentLanguage
        {
            get => _currentLanguage;
            set
            {
                if (value == _currentLanguage)
                {
                    return;
                }

                _currentLanguage = value;
                OnLanguageChange?.Invoke();
            }
        }

        LanguageType _currentLanguage;

        public static bool IsChinese =>
            OdinToolkitsPreferencesSO.Instance.inspectorMultiLanguageSetting.CurrentLanguage == LanguageType.Chinese;

        public static bool IsEnglish =>
            OdinToolkitsPreferencesSO.Instance.inspectorMultiLanguageSetting.CurrentLanguage == LanguageType.English;

        public void OdinToolkitsReset()
        {
            CurrentLanguage = LanguageType.Chinese;
        }

        public static event Action OnLanguageChange;
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void Initialize()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                OnLanguageChange = null;
            }
        }
#endif
    }
}
