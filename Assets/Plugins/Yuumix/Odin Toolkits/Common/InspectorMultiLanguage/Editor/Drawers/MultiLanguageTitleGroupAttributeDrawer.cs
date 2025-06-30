using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Common.InspectorMultiLanguage.Editor
{
    public class MultiLanguageTitleGroupAttributeDrawer : OdinGroupDrawer<MultiLanguageTitleGroupAttribute>
    {
        public ValueResolver<string> TitleHelper;
        public ValueResolver<string> SubtitleHelper;

        protected override void Initialize()
        {
            ReloadResolver();
            InspectorMultiLanguageManagerSO.OnLanguageChange -= ReloadResolver;
            InspectorMultiLanguageManagerSO.OnLanguageChange += ReloadResolver;
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            var property = Property;
            var attribute = Attribute;
            if (property != property.Tree.GetRootProperty(0))
            {
                EditorGUILayout.Space();
            }

            SirenixEditorGUI.Title(this.TitleHelper.GetValue(), this.SubtitleHelper.GetValue(),
                (TextAlignment)attribute.TitleAlignment, attribute.HorizontalLine, attribute.BoldTitle);
            GUIHelper.PushIndentLevel(EditorGUI.indentLevel + (attribute.Indent ? 1 : 0));
            for (var index = 0; index < property.Children.Count; ++index)
            {
                InspectorProperty child = property.Children[index];
                child.Draw(child.Label);
            }

            GUIHelper.PopIndentLevel();
        }

        void ReloadResolver()
        {
            TitleHelper = ValueResolver.GetForString(this.Property, Attribute.TitleData);
            SubtitleHelper = ValueResolver.GetForString(this.Property, this.Attribute.SubtitleData);
        }
    }
}
