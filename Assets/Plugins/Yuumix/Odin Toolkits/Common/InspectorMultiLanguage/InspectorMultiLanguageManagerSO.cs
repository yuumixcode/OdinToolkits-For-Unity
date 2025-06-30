using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Yuumix.OdinToolkits.Common.ResetTool;
#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEditor.Callbacks;
#endif

namespace Yuumix.OdinToolkits.Common.InspectorMultiLanguage
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
    [MultiLanguageComment("Inspector 面板的多语言管理器，用于设置当前语言",
        "Inspector panel's multilingual manager for setting the current language")]
    public class InspectorMultiLanguageManagerSO : OdinScriptableSingleton<InspectorMultiLanguageManagerSO>,
        IOdinToolkitsReset
    {
        [PropertyOrder(5)]
        [ShowInInspector]
        [EnumToggleButtons]
        [HideLabel]
        [MultiLanguageTitle("Inspector 当前语言", "Inspector Language", beforeSpace: false)]
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

        LanguageType _currentLanguage;

        public static bool IsChinese => Instance.CurrentLanguage == LanguageType.Chinese;
        public static bool IsEnglish => Instance.CurrentLanguage == LanguageType.English;

        public void OdinToolkitsReset()
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

        #region Handle Event Subscribe

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
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                ClearListener();
            }
        }
#endif

        #endregion
    }
}
