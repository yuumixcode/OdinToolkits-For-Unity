using Sirenix.OdinInspector;
using Yuumix.OdinToolkits;
using Yuumix.OdinToolkits.Common;
using Yuumix.YuumixEditor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class ShowInInspectorExample : ExampleScriptableObject
    {
        static string Path => PathEditorUtil.GetTargetFolderPath("RuntimeExamples",
            "OdinToolkits") + "/ShowInInspector";

        [DisplayAsString(fontSize: 12, overflow: false)]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string Tip => "提示: " + "ShowInInspector 关系到序列化，通过场景中的实际案例验证更直观";

        [DisplayAsString(fontSize: 12, overflow: false)]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string PathTip => "文件夹路径为: " + Path;

        [Button("跳转到 Example 文件夹", ButtonSizes.Large)]
        public void SelectionFolder()
        {
            OdinEditorLog.Log("ShowInInspector Runtime Example 文件夹路径为: " + Path);
            ProjectEditorUtil.PingAndSelectAsset(Path);
        }
    }
}
