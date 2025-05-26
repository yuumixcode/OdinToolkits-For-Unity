using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using System;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.YuumiEditor.Localization;
using Yuumix.OdinToolkits.Modules.Odin.Customs.Runtime.Attributes;

namespace Yuumix.OdinToolkits.Modules.Odin.Customs.Editor.Drawers
{
    [DrawerPriority(DrawerPriorityLevel.SuperPriority)]
    public partial class LanguageTextAttributeDrawer : OdinAttributeDrawer<LanguageTextAttribute>
    {
        ValueResolver<Color> _iconColorResolver;
        InspectorLanguageManagerSO _languageManager;
        GUIContent _tempLabel;
        ValueResolver<string> _textProvider;

        protected override void Initialize()
        {
            // 每次查看 Inspector 面板时执行一次
            // Debug.Log("LanguageText Initialize");
            _languageManager = InspectorLanguageManagerSO.Instance;
            _textProvider = ValueResolver.GetForString(Property, GetTextToValueResolver());
            _iconColorResolver =
                ValueResolver.Get(Property, Attribute.IconColor, EditorStyles.label.normal.textColor);
            _tempLabel = new GUIContent();
            _languageManager.OnLanguageChange -= ReloadResolver;
            _languageManager.OnLanguageChange += ReloadResolver;
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
                if (str == string.Empty
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
                    if (_languageManager.CurrentLanguage == InspectorLanguageType.English &&
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
            _textProvider = ValueResolver.GetForString(Property, GetTextToValueResolver());
        }

        string GetTextToValueResolver()
        {
            return _languageManager.CurrentLanguage switch
            {
                InspectorLanguageType.SimplifiedChinese => Attribute.SimplifiedChineseText,
                InspectorLanguageType.English => Attribute.EnglishText,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public partial class LanguageTextAttributeDrawer
    {
        // 推荐使用 partial 扩展映射
    }
}
