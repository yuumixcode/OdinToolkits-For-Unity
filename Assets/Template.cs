// 这是引入的命名空间
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Yuumix.OdinToolkits.Common.Runtime.Localization;
using Yuumix.OdinToolkits.Modules.Odin.Customs.Runtime.Attributes;
#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
#endif

// 需要 TemplateNameSpace 占位符
namespace TemplateNameSpace
{
    // 需要自行处理缩进，最好脚本编写完成后，直接改后缀名
    // 需要 Template 占位符
    public class Template : MonoBehaviour
    {
#if UNITY_EDITOR
        [Button("切换语言", ButtonSizes.Large)]
        public void SwitchLanguage(InspectorLanguageType type)
        {
            InspectorLanguageManagerSO.Instance.CurrentLanguage = type;
        }
#endif
        [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        public InspectorLanguageManagerSO languageManager;

        [LanguageText("$Text", "Assets", true)]
        public string asset;

        [LabelText("$Text")]
        public string label;

        public string Text() => "资源";
    }
}
