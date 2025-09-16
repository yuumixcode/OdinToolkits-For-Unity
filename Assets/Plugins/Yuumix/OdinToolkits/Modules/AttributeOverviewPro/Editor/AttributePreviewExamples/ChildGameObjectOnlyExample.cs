using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Core;
using YuumixEditor;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class ChildGameObjectOnlyExample : ExampleSO
    {
        static string Path => PathEditorUtility.GetTargetFolderPath("RuntimeExamples",
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
            YuumixLogger.OdinToolkitsLog("ChildGameObjectOnly Runtime Example 文件夹路径为: " + Path);
            ProjectEditorUtility.PingAndSelectAsset(Path);
        }
    }
}
