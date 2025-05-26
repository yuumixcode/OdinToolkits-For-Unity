using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Common.YuumiEditor.Localization;
using Yuumix.OdinToolkits.Modules.Odin.Customs.Runtime.Attributes;

namespace Yuumix.OdinToolkits.Examples.CustomAttributes
{
    public class LanguageTextMonoExample : MonoBehaviour
    {
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
