using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core.Editor
{
    [DrawerPriority(1)]
    public class BilingualTitleAttributeDrawer : OdinAttributeDrawer<BilingualTitleAttribute>
    {
        ValueResolver<string> _subTitleResolver;
        ValueResolver<string> _titleResolver;

        protected override void Initialize()
        {
            _titleResolver = ValueResolver.GetForString(Property, GetAttributeTitle());
            _subTitleResolver = ValueResolver.GetForString(Property, GetAttributeSubTitle());
            InspectorBilingualismConfigSO.OnLanguageChange -= ReloadResolver;
            InspectorBilingualismConfigSO.OnLanguageChange += ReloadResolver;
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (Attribute.BeforeSpace)
            {
                if (Property != Property.Tree.GetRootProperty(0))
                {
                    EditorGUILayout.Space();
                }
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

        string GetAttributeTitle() => Attribute.TitleData.GetCurrentOrFallback();

        string GetAttributeSubTitle() => Attribute.SubtitleData.GetCurrentOrFallback();
    }
}
