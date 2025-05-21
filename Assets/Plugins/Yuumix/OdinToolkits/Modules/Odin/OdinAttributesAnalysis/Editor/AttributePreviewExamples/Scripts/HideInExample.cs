using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Common.Editor;
using Yuumix.OdinToolkits.Common.Editor.Locator;
using Yuumix.OdinToolkits.Common.Runtime;
using Yuumix.OdinToolkits.Modules.Utilities.YuumiEditor;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class HideInExample : ExampleScriptableObject
    {
        private static string Path =>
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
            ProjectEditorUtility.PingAndSelectAsset(Path);
        }
    }
}