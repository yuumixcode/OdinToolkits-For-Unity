using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorLocalization.Attributes;

namespace Yuumix.OdinToolkits.Common.InspectorLocalization.Editor.Drawers
{
    [DrawerPriority(1)]
    public class LocalizedTitleAttributeDrawer : OdinAttributeDrawer<LocalizedTitleAttribute>
    {
        ValueResolver<string> _subTitleResolver;
        ValueResolver<string> _titleResolver;

        protected override void Initialize()
        {
            _titleResolver = ValueResolver.GetForString(Property, GetAttributeTitle());
            _subTitleResolver = ValueResolver.GetForString(Property, GetAttributeSubTitle());
            InspectorLocalizationManagerSO.OnLanguageChange -= ReloadResolver;
            InspectorLocalizationManagerSO.OnLanguageChange += ReloadResolver;
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (Property != Property.Tree.GetRootProperty(0))
            {
                EditorGUILayout.Space();
            }

            var flag = true;
            if (_titleResolver.HasError)
            {
                SirenixEditorGUI.ErrorMessageBox(_titleResolver.ErrorMessage);
                flag = false;
            }

            if (_subTitleResolver.HasError)
            {
                SirenixEditorGUI.ErrorMessageBox(_subTitleResolver.ErrorMessage);
                flag = false;
            }

            if (flag)
            {
                SirenixEditorGUI.Title(_titleResolver.GetValue(), _subTitleResolver.GetValue(),
                    (TextAlignment)Attribute.TitleAlignment, Attribute.HorizontalLine, Attribute.Bold);
            }

            CallNextDrawer(label);
        }

        void ReloadResolver()
        {
            _titleResolver = ValueResolver.GetForString(Property, GetAttributeTitle());
            _subTitleResolver = ValueResolver.GetForString(Property, GetAttributeSubTitle());
        }

        string GetAttributeTitle() => Attribute.MultiLanguageData.GetCurrentTitleOrFallback();

        string GetAttributeSubTitle() => Attribute.MultiLanguageData.GetCurrentSubTitleOrFallback();
    }
}
