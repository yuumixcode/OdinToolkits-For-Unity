using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorLocalization;
using Yuumix.OdinToolkits.Modules.CustomExtensions;

namespace Yuumix.OdinToolkits.Examples.CustomExtensions.CustomAttributes
{
    public class LanguageTextAttributeExample : MonoBehaviour
    {
        // 使用一行代码创建语言切换按钮
        public SwitchInspectorLanguageWidget widget = new SwitchInspectorLanguageWidget();

        public EnumLanguageWidget enumWidget = new EnumLanguageWidget();

        [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        public InspectorLocalizationManagerSO languageLocalizationManager;

        [LocalizedText("$Text", "Assets", true)]
        public string asset;

        [LabelText("$Text")]
        public string label;

        public string Text() => "资源";
    }
}
