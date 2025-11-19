using Sirenix.OdinInspector;
using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// Odin Toolkits 面板双语配置，对外提供一个语言切换事件，
    /// 主要用于设置 BilingualData 的返回语言，Runtime 和 Editor 脚本均可访问
    /// </summary>
    [Summary("Odin Toolkits 面板双语配置，对外提供一个语言切换事件， 主要用于设置 BilingualData 的返回语言，Runtime 和 Editor 脚本均可访问")]
    [Serializable]
    public class InspectorBilingualismConfigSO : ScriptableObject, IOdinToolkitsRuntimeReset
    {
        #region LanguageType enum

        /// <summary>
        /// Odin Toolkits 编辑器语言类型
        /// </summary>
        [Summary("Odin Toolkits 编辑器语言类型")]
        [Serializable]
        public enum LanguageType
        {
            [InspectorName("简体中文")]
            Chinese,
            English
        }

        #endregion

        static InspectorBilingualismConfigSO _instance;

        LanguageType _currentLanguage;

        public static InspectorBilingualismConfigSO Instance
        {
            get
            {
                if (!_instance)
                {
                    return Resources.Load<InspectorBilingualismConfigSO>("InspectorBilingualismConfig");
                }

                return _instance;
            }
        }

        [PropertyOrder(5)]
        [ShowInInspector]
        [EnumToggleButtons]
        [HideLabel]
        [BilingualTitle("Odin Toolkits 面板语言", "Odin Toolkits Inspector Language", beforeSpace: false)]
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
                OnLanguageChanged?.Invoke();
            }
        }

        public static bool IsChinese =>
            Instance.CurrentLanguage == LanguageType.Chinese;

        public static bool IsEnglish =>
            Instance.CurrentLanguage == LanguageType.English;

        #region IOdinToolkitsRuntimeReset Members

        public void RuntimeReset()
        {
            CurrentLanguage = LanguageType.Chinese;
        }

        #endregion

        public static event Action OnLanguageChanged;
#if UNITY_EDITOR

        #region 兼容 [禁用域重新加载]

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
                OnLanguageChanged = null;
            }
        }

        #endregion

#endif
    }
}
