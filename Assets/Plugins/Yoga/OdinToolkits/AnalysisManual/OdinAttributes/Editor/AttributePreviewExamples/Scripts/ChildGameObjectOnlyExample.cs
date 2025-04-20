using Sirenix.OdinInspector;
using YOGA.Modules.Utilities;
using YOGA.OdinToolkits.Common.Runtime;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class ChildGameObjectOnlyExample : ExampleScriptableObject
    {
        static string Path => ProjectEditorUtility
            .GetTargetFolderPath("RuntimeExamples",
                "OdinToolkits") + "/ChildGameObjectOnly";

        [DisplayAsString(fontSize: 12, overflow: false)]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string Tip => "提示: " + "ChildGameObjectOnly 和场景中物体有关，需要运行时场景验证";

        [DisplayAsString(fontSize: 12, overflow: false)]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string PathTip => "文件夹路径为: " + Path;

        [Button("跳转到 Example 文件夹", ButtonSizes.Large)]
        public void SelectionFolder()
        {
            OdinLog.Log("ChildGameObjectOnly Runtime Example 文件夹路径为: " + Path);
            ProjectEditorUtility.PingAndSelectAsset(Path);
        }
    }
}
