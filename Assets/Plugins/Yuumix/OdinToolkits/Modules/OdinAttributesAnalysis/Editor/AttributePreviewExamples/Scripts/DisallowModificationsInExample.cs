using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Common.Runtime;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.YuumiEditor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class DisallowModificationsInExample : ExampleScriptableObject
    {
        static string Path =>
            PathEditorUtil.GetTargetFolderPath("RuntimeExamples",
                "OdinToolkits") + "/DisallowModificationsIn";

        [DisplayAsString(fontSize: 12, overflow: false)]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string Tip => "提示: " + "DisallowModificationsInAttribute 针对预制体使用，需要到具体情况中才能生效";

        [DisplayAsString(fontSize: 12, overflow: false)]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string PathTip => "文件夹路径为: " + Path;

        [Button("跳转到 Example 文件夹", ButtonSizes.Large)]
        public void SelectionFolder()
        {
            OdinEditorLog.Log("DisallowModificationsIn Runtime Example 文件夹路径为: " + Path);
            ProjectEditorUtil.PingAndSelectAsset(Path);
        }
    }
}
