using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core.Runtime.Editor
{
    [DrawerPriority(DrawerPriorityLevel.SuperPriority)]
    public class BilingualTextAttributeDrawer : OdinAttributeDrawer<BilingualTextAttribute>
    {
        ValueResolver<string> _textProvider;
        ValueResolver<Color> _iconColorResolver;
        GUIContent _tempLabel;

        protected override void Initialize()
        {
            _textProvider = ValueResolver.GetForString(Property, GetAttributeText());
            _iconColorResolver =
                ValueResolver.Get(Property, Attribute.IconColor, EditorStyles.label.normal.textColor);
            _tempLabel = new GUIContent();
            BilingualSetting.OnLanguageChange -= ReloadResolver;
            BilingualSetting.OnLanguageChange += ReloadResolver;
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (Property.GetAttribute<HideLabelAttribute>() != null)
            {
                CallNextDrawer(null);
            }

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
                string str = _textProvider.GetValue();
                GUIContent nextLabel;
                if (str == null && Attribute.Icon == SdfIconType.None)
                {
                    nextLabel = label;
                }
                else
                {
                    string name = str ?? label.text;
                    if (Attribute.NicifyEnglishText)
                    {
                        name = ObjectNames.NicifyVariableName(name);
                    }

                    _tempLabel.text = name;
                    nextLabel = _tempLabel;
                    if (Attribute.Icon != SdfIconType.None)
                    {
                        Color color = _iconColorResolver.GetValue();
                        Debug.Log("Color: " + color);
                        nextLabel.image =
                            SdfIcons.CreateTransparentIconTexture(Attribute.Icon, color, 16 /*0x10*/, 16 /*0x10*/,
                                0);
                    }
                }

                CallNextDrawer(nextLabel);
            }
        }

        void ReloadResolver()
        {
            _textProvider = ValueResolver.GetForString(Property, GetAttributeText());
        }

        string GetAttributeText() => Attribute.BilingualData.GetCurrentOrFallback();
    }
}
