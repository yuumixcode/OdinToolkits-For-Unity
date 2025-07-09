using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.RootLocator;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using YuumixEditor;
using Yuumix.OdinToolkits;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class HideInExample : ExampleScriptableObject
    {
        static string Path =>
            OdinToolkitsPaths.GetRootPath() +
            "/ChineseManual/ChineseAttributesOverview/RuntimeExamples/HideIn";

        [DisplayAsString(fontSize: 12, overflow: false)]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string Tip => "提示: " + "HideInAttribute 针对预制体使用，需要到具体情况中才能生效";

        [DisplayAsString(fontSize: 12, overflow: false)]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string PathTip => "文件夹路径为: " + Path;

        [Button("跳转到 Example 文件夹", ButtonSizes.Large)]
        public void SelectionFolder()
        {
            OdinEditorLog.Log("HideIn Runtime Example 文件夹路径为: " + Path);
            ProjectEditorUtil.PingAndSelectAsset(Path);
        }
    }
}
