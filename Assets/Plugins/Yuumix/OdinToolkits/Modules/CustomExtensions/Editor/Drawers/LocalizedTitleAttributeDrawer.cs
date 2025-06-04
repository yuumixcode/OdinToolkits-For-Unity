using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.EditorLocalization;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Attributes;

namespace Yuumix.OdinToolkits.Modules.CustomExtensions.Editor.Drawers
{
    [DrawerPriority(1.0)]
    public class LocalizedTitleAttributeDrawer : OdinAttributeDrawer<LocalizedTitleAttribute>
    {
        EditorLocalizationManagerSO _editorLocalizationLanguageManager;
        ValueResolver<string> _chineseSubTitleResolver;
        ValueResolver<string> _chineseTitleResolver;
        ValueResolver<string> _englishSubTitleResolver;
        ValueResolver<string> _englishTitleResolver;

        protected override void Initialize()
        {
            _editorLocalizationLanguageManager = EditorLocalizationManagerSO.Instance;
            _chineseTitleResolver = ValueResolver.GetForString(Property, Attribute.ChineseTitle);
            _englishTitleResolver = ValueResolver.GetForString(Property, Attribute.EnglishTitle);
            _chineseSubTitleResolver = ValueResolver.GetForString(Property, Attribute.ChineseSubTitle);
            _englishSubTitleResolver = ValueResolver.GetForString(Property, Attribute.EnglishSubTitle);
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (Property != Property.Tree.GetRootProperty(0))
            {
                EditorGUILayout.Space();
            }

            var flag = true;
            if (_chineseTitleResolver.HasError)
            {
                SirenixEditorGUI.ErrorMessageBox(_chineseTitleResolver.ErrorMessage);
                flag = false;
            }

            if (_englishTitleResolver.HasError)
            {
                SirenixEditorGUI.ErrorMessageBox(_englishTitleResolver.ErrorMessage);
                flag = false;
            }

            if (_chineseSubTitleResolver.HasError)
            {
                SirenixEditorGUI.ErrorMessageBox(_chineseSubTitleResolver.ErrorMessage);
                flag = false;
            }

            if (_englishSubTitleResolver.HasError)
            {
                SirenixEditorGUI.ErrorMessageBox(_englishSubTitleResolver.ErrorMessage);
                flag = false;
            }

            if (flag)
            {
                if (_editorLocalizationLanguageManager.IsSimplifiedChinese)
                {
                    SirenixEditorGUI.Title(_chineseTitleResolver.GetValue(), _chineseSubTitleResolver.GetValue(),
                        (TextAlignment)Attribute.TitleAlignment, Attribute.HorizontalLine, Attribute.Bold);
                }
                else
                {
                    SirenixEditorGUI.Title(_englishTitleResolver.GetValue(), _englishSubTitleResolver.GetValue(),
                        (TextAlignment)Attribute.TitleAlignment, Attribute.HorizontalLine, Attribute.Bold);
                }
            }

            CallNextDrawer(label);
        }
    }
}
