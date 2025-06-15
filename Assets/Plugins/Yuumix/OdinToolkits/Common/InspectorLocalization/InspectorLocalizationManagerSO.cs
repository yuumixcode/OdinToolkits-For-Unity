using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorLocalization.Attributes;
using Yuumix.OdinToolkits.Common.Runtime;
using Yuumix.OdinToolkits.Common.Runtime.ResetTool;
#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEditor.Callbacks;
#endif

namespace Yuumix.OdinToolkits.Common.InspectorLocalization
{
    [Serializable]
    public enum LanguageType
    {
        [InspectorName("简体中文")]
        Chinese,
        English
    }

    /// <summary>
    /// Inspector 窗口的本地化管理器，用于多语言显示 Inspector 窗口
    /// <remarks>
    /// <c>2025/06/15 Yuumix Zeus 确认注释</c>
    /// </remarks>
    /// </summary>
    [LocalizedComment("Inspector 面板的本地化管理器，用于多语言显示 Inspector 面板",
        "Inspector windows localization manager, used to display Inspector windows in multiple languages")]
    public class InspectorLocalizationManagerSO : OdinScriptableSingleton<InspectorLocalizationManagerSO>, IPluginReset
    {
        LanguageType _currentLanguage;

        [ShowInInspector]
        [EnumToggleButtons]
        [LocalizedText("当前语言: ", "Current Language: ")]
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
                RefreshEditor();
            }
        }

        public bool IsChinese => CurrentLanguage == LanguageType.Chinese;
        public bool IsEnglish => CurrentLanguage == LanguageType.English;

        public void PluginReset()
        {
            CurrentLanguage = LanguageType.Chinese;
        }

        public static event Action OnLanguageChange;

        // Todo: 怎么样在语言改变后刷新所有窗口？或者是刷新当前窗口？以及 InspectorWindow 的当前显示的类
        static void RefreshEditor()
        {
#if UNITY_EDITOR
            var inspectorType = typeof(Editor).Assembly.GetType("UnityEditor.InspectorWindow");
            foreach (var inspector in (EditorWindow[])Resources.FindObjectsOfTypeAll(inspectorType))
            {
                inspector?.Repaint();
            }

            if (EditorWindow.focusedWindow)
            {
                EditorWindow.focusedWindow.Repaint();
            }

            var curr = GUIHelper.CurrentWindow;
            if (curr)
            {
                curr.Repaint();
            }
#endif
        }

        #region 处理事件订阅

#if UNITY_EDITOR
        [DidReloadScripts]
        static void ClearListener()
        {
            OnLanguageChange = null;
        }

        [InitializeOnLoadMethod]
        static void Initialize()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredEditMode)
            {
                ClearListener();
            }
        }
#endif

        #endregion
    }
}
