#region

using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using System;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.EditorLocalization;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Attributes;

#endregion

namespace Yuumix.OdinToolkits.Modules.CustomExtensions.Editor.Drawers
{
    [DrawerPriority(DrawerPriorityLevel.SuperPriority)]
    public partial class LocalizedTextAttributeDrawer : OdinAttributeDrawer<LocalizedTextAttribute>
    {
        ValueResolver<Color> _iconColorResolver;
        EditorLocalizationManagerSO _languageLocalizationManager;
        GUIContent _tempLabel;
        ValueResolver<string> _textProvider;
        string _finalResolvedString;
        event Action MultiLanguageSetResolvedString;

        protected override void Initialize()
        {
            // 每次查看 Inspector 面板时执行一次
            _languageLocalizationManager = EditorLocalizationManagerSO.Instance;
            SetFinalResolvedString();
            _textProvider = ValueResolver.GetForString(Property, _finalResolvedString);
            _iconColorResolver =
                ValueResolver.Get(Property, Attribute.IconColor, EditorStyles.label.normal.textColor);
            _tempLabel = new GUIContent();
            _languageLocalizationManager.OnLanguageChange -= ReloadResolver;
            _languageLocalizationManager.OnLanguageChange += ReloadResolver;
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (_textProvider.HasError)
            {
                SirenixEditorGUI.ErrorMessageBox(_textProvider.ErrorMessage);
                CallNextDrawer(label);
            }
            else if (_iconColorResolver.HasError)
            {
                SirenixEditorGUI.ErrorMessageBox(_iconColorResolver.ErrorMessage);
                CallNextDrawer(label);
            }
            else
            {
                var str = _textProvider.GetValue();
                GUIContent nextLabel;
                // 当英文为空，中文不为空时
                if (string.IsNullOrEmpty(str)
                    && ValueResolver.GetForString(Property, Attribute.SimplifiedChineseText)
                        .GetValue() != string.Empty)
                {
                    str = ValueResolver.GetForString(Property, Attribute.SimplifiedChineseText).GetValue();
                }

                if (str == null && Attribute.Icon == SdfIconType.None)
                {
                    nextLabel = label;
                }
                else
                {
                    var name = str ?? label.text;
                    if (_languageLocalizationManager.CurrentLanguage == LanguageType.English &&
                        Attribute.EnglishText != string.Empty &&
                        Attribute.NicifyEnglishText)
                    {
                        name = ObjectNames.NicifyVariableName(name);
                    }

                    _tempLabel.text = name;
                    if (Attribute.Icon != SdfIconType.None)
                    {
                        var color = _iconColorResolver.GetValue();
                        _tempLabel.image =
                            SdfIcons.CreateTransparentIconTexture(Attribute.Icon, color, 16 /*0x10*/, 16 /*0x10*/,
                                0);
                    }

                    nextLabel = _tempLabel;
                }

                CallNextDrawer(nextLabel);
                // Debug.Log("PropertyLayout." + nextLabel);
            }
        }

        void ReloadResolver()
        {
            SetFinalResolvedString();
            _textProvider = ValueResolver.GetForString(Property, _finalResolvedString);
        }

        public string GetFinalResolvedString() => _finalResolvedString;

        void SetFinalResolvedString()
        {
            if (_languageLocalizationManager.CurrentLanguage == LanguageType.SimplifiedChinese)
            {
                _finalResolvedString = Attribute.SimplifiedChineseText;
            }
            else if (_languageLocalizationManager.CurrentLanguage == LanguageType.English)
            {
                _finalResolvedString = Attribute.EnglishText;
            }
            else
            {
                MultiLanguageSetResolvedString?.Invoke();
            }
        }
    }

    public partial class LocalizedTextAttributeDrawer
    {
        // 推荐使用 partial 扩展映射
    }
}
