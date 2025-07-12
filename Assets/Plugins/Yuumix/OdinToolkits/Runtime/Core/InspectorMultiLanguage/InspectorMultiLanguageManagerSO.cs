using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Shared;

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
#if UNITY_EDITOR
                CollectActiveInspectors();
                OnDelayCallAction();
#endif
            }
        }

        LanguageType _currentLanguage;

        public static bool IsChinese => Instance.CurrentLanguage == LanguageType.Chinese;
        public static bool IsEnglish => Instance.CurrentLanguage == LanguageType.English;
#if UNITY_EDITOR

        static Type _inspectorWindowType = typeof(Editor).Assembly.GetType("UnityEditor.InspectorWindow");

        static FieldInfo _allInspectorsFieldInfo =
            _inspectorWindowType.GetField("m_AllInspectors", BindingFlags.NonPublic | BindingFlags.Static);

        static readonly List<EditorWindow> ActiveInspectors = new List<EditorWindow>();
#endif
        public void OdinToolkitsReset()
        {
            CurrentLanguage = LanguageType.Chinese;
        }

        public static event Action OnLanguageChange;

        #region Handle Event Subscribe

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void Initialize()
        {
            // 监听编辑器更新，处理延迟重绘
            EditorApplication.delayCall -= OnDelayCallAction;
            EditorApplication.delayCall += OnDelayCallAction;
            // 添加窗口焦点变更回调
            Application.focusChanged -= EditorApplicationOnfocusChanged();
            Application.focusChanged += EditorApplicationOnfocusChanged();
            Selection.selectionChanged -= CollectActiveInspectors;
            Selection.selectionChanged += CollectActiveInspectors;
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        static Action<bool> EditorApplicationOnfocusChanged()
        {
            return hasFocus =>
            {
                if (hasFocus)
                {
                    CollectActiveInspectors();
                }
            };
        }

        static void CollectActiveInspectors()
        {
            if (_allInspectorsFieldInfo == null)
            {
                return;
            }

            var windows = (IEnumerable)_allInspectorsFieldInfo.GetValue(_inspectorWindowType);
            foreach (EditorWindow window in windows)
            {
                ActiveInspectors.Add(window);
            }
        }

        static void OnDelayCallAction()
        {
            foreach (EditorWindow window in ActiveInspectors)
            {
                window.Repaint();
            }
        }

        static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                OnLanguageChange = null;
            }
        }
#endif

        #endregion
    }
}
