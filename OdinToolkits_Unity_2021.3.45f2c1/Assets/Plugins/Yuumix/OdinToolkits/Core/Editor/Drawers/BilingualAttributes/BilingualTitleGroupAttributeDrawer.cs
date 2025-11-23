using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core.Editor
{
    public class BilingualTitleGroupAttributeDrawer : OdinGroupDrawer<BilingualTitleGroupAttribute>
    {
        public ValueResolver<string> SubtitleHelper;
        public ValueResolver<string> TitleHelper;

        protected override void Initialize()
        {
            ReloadResolver();
            InspectorBilingualismConfigSO.OnLanguageChanged -= ReloadResolver;
            InspectorBilingualismConfigSO.OnLanguageChanged += ReloadResolver;
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            var property = Property;
            var attribute = Attribute;
            if (property != property.Tree.GetRootProperty(0))
            {
                EditorGUILayout.Space();
            }

            SirenixEditorGUI.Title(TitleHelper.GetValue(), SubtitleHelper.GetValue(),
                (TextAlignment)attribute.TitleAlignment, attribute.HorizontalLine, attribute.BoldTitle);
            GUIHelper.PushIndentLevel(EditorGUI.indentLevel + (attribute.Indent ? 1 : 0));
            for (var index = 0; index < property.Children.Count; ++index)
            {
                var child = property.Children[index];
                child.Draw(child.Label);
            }

            GUIHelper.PopIndentLevel();
        }

        void ReloadResolver()
        {
            TitleHelper = ValueResolver.GetForString(Property, Attribute.TitleData);
            SubtitleHelper = ValueResolver.GetForString(Property, Attribute.SubtitleData);
        }
    }
}
