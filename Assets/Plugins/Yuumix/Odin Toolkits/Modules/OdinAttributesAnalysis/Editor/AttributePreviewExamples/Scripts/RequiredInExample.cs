using Sirenix.OdinInspector;
using Yuumix.OdinToolkits;
using Yuumix.OdinToolkits.Common;
using Yuumix.YuumixEditor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class RequiredInExample : ExampleScriptableObject
    {
        static string Path => PathEditorUtil.GetTargetFolderPath("RuntimeExamples",
            "Odin Toolkits") + "/RequiredIn";

        [DisplayAsString(fontSize: 12, overflow: false)]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string Tip => "提示: " + "RequiredInAttribute 针对预制体使用，需要到具体情况中才能生效";

        [DisplayAsString(fontSize: 12, overflow: false)]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string PathTip => "文件夹路径为: " + Path;

        [Button("跳转到 Example 文件夹", ButtonSizes.Large)]
        public void SelectionFolder()
        {
            OdinEditorLog.Log("RequiredIn Runtime Example 文件夹路径为: " + Path);
            ProjectEditorUtil.PingAndSelectAsset(Path);
        }
    }
}
