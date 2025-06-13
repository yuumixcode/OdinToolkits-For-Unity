using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using System;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorLocalization.Attributes;
using Yuumix.OdinToolkits.Common.Runtime;
using Yuumix.OdinToolkits.Common.Runtime.ResetTool;

namespace Yuumix.OdinToolkits.Common.InspectorLocalization
{
    [Serializable]
    public enum LanguageType
    {
        [InspectorName("简体中文")]
        Chinese,
        English
    }

    // [CreateAssetMenu(menuName = "Create EditorLanguageManagerSO", fileName = "EditorLanguageManagerSO", order = 0)]
    public class InspectorLocalizationManagerSO : OdinScriptableSingleton<InspectorLocalizationManagerSO>, IPluginReset
    {
        LanguageType _currentLanguage;

        [HideInInspector]
        public Action OnLanguageChange;

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

        // Todo: 怎么样在语言改变后刷新所有窗口？或者是刷新当前窗口？以及 InspectorWindow 的当前显示的类
        static void RefreshEditor()
        {
#if UNITY_EDITOR
            var inspectorType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.InspectorWindow");
            foreach (var inspector in (EditorWindow[])Resources.FindObjectsOfTypeAll(inspectorType))
            {
                inspector?.Repaint();
            }

            if (EditorWindow.focusedWindow != null)
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

        public void PluginReset()
        {
            CurrentLanguage = LanguageType.Chinese;
        }
    }
}
