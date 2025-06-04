using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using System;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.EditorLocator;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Attributes;
#if UNITY_EDITOR
using Yuumix.OdinToolkits.Common.YuumixEditor;
#endif

namespace Yuumix.OdinToolkits.Common.EditorLocalization
{
    [Serializable]
    public enum LanguageType
    {
        [InspectorName("简体中文")]
        SimplifiedChinese,
        English
    }

    // [CreateAssetMenu(menuName = "Create EditorLanguageManagerSO", fileName = "EditorLanguageManagerSO", order = 0)]
    public class EditorLocalizationManagerSO : ScriptableObject
    {
        static EditorLocalizationManagerSO _instance;

        LanguageType _currentLanguage;

        public Action OnLanguageChange;

        static string RelativeFilePath => OdinToolkitsPaths.GetRootPath() +
                                          "/Common/EditorLocalization/Editor/EditorLocalizationManagerSO.asset";

        public static EditorLocalizationManagerSO Instance
        {
            get
            {
                if (_instance)
                {
                    return _instance;
                }
#if UNITY_EDITOR
                _instance =
                    ScriptableObjectEditorUtil.GetAssetDeleteExtra<EditorLocalizationManagerSO>(RelativeFilePath);
#endif
                return _instance;
            }
        }

        [ShowInInspector]
        [EnumToggleButtons]
        [LocalizedText("当前语言: ", "Current Language: ", true)]
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

        public bool IsSimplifiedChinese => CurrentLanguage == LanguageType.SimplifiedChinese;
        public bool IsEnglish => CurrentLanguage == LanguageType.English;

        static void RefreshEditor()
        {
            // 1. 刷新所有标准Inspector窗口
            var inspectorType = typeof(Editor).Assembly.GetType("UnityEditor.InspectorWindow");
            foreach (var inspector in (EditorWindow[])Resources.FindObjectsOfTypeAll(inspectorType))
            {
                inspector?.Repaint();
            }

            if (EditorWindow.focusedWindow != null)
            {
                EditorWindow.focusedWindow.Repaint();
            }

            EditorWindow curr = GUIHelper.CurrentWindow;
            if (curr != null)
            {
                curr.Repaint();
            }
        }
    }
}
