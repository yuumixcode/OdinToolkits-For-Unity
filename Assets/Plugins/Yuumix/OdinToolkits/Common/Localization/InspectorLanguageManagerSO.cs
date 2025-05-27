using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Yuumix.OdinToolkits.Common.Locator;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Attributes;
#if UNITY_EDITOR
using Yuumix.OdinToolkits.Modules.Utilities.YuumiEditor;
#endif

namespace Yuumix.OdinToolkits.Common.Localization
{
    [Serializable]
    public enum InspectorLanguageType
    {
        [InspectorName("简体中文")]
        SimplifiedChinese,
        English
    }

    // [CreateAssetMenu(menuName = "Create EditorLanguageManagerSO", fileName = "EditorLanguageManagerSO", order = 0)]
    public class InspectorLanguageManagerSO : ScriptableObject
    {
        static InspectorLanguageManagerSO _instance;

        InspectorLanguageType _currentLanguage;

        public Action OnLanguageChange;

        static string RelativeFilePath => OdinToolkitsPaths.GetRootPath() +
                                          "/Common/Localization/Editor/InspectorLanguageManagerSO.asset";

        public static InspectorLanguageManagerSO Instance
        {
            get
            {
                if (_instance)
                {
                    return _instance;
                }
#if UNITY_EDITOR
                _instance = ProjectEditorUtility.SO.GetScriptableObjectDeleteExtra<InspectorLanguageManagerSO>(RelativeFilePath);
#endif
                return _instance;
            }
        }

        [ShowInInspector]
        [EnumToggleButtons]
        [LanguageText("当前语言: ", "Current Language: ", true)]
        public InspectorLanguageType CurrentLanguage
        {
            get => _currentLanguage;
            set
            {
                _currentLanguage = value;
                OnLanguageChange?.Invoke();
            }
        }
    }
}
