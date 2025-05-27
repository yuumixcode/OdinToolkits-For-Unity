using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Common.Localization;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Attributes;

namespace Yuumix.OdinToolkits.Examples.CustomExtensions.CustomAttributes
{
    public class LanguageTextAttributeExample : MonoBehaviour
    {
        // 使用一行代码创建语言切换按钮
        public LanguageButton button = new LanguageButton();

        public EnumLanguageButton enumButton = new EnumLanguageButton();

        [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        public InspectorLanguageManagerSO languageManager;

        [LanguageText("$Text", "Assets", true)]
        public string asset;

        [LabelText("$Text")]
        public string label;

        public string Text() => "资源";
    }
}
