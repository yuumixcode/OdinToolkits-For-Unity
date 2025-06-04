using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Common.EditorLocalization;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Attributes;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Classes.InspectorGUI;

namespace Yuumix.OdinToolkits.Examples.CustomExtensions.CustomAttributes
{
    public class LanguageTextAttributeExample : MonoBehaviour
    {
        // 使用一行代码创建语言切换按钮
        public SwitchEditorLanguageButton button = new SwitchEditorLanguageButton();

        public EnumLanguageButton enumButton = new EnumLanguageButton();

        [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        public EditorLocalizationManagerSO languageLocalizationManager;

        [LocalizedText("$Text", "Assets", true)]
        public string asset;

        [LabelText("$Text")]
        public string label;

        public string Text() => "资源";
    }
}
