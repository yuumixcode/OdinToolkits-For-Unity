using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class RequiredInExample : ExampleSO
    {
        // static string Path => PathEditorUtil.GetTargetFolderPath("RuntimeExamples",
        //     "Odin Toolkits") + "/RequiredIn";

        [DisplayAsString(fontSize: 12, overflow: false)]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string Tip => "提示: " + "RequiredInAttribute 针对预制体使用，需要到具体情况中才能生效";

        // [DisplayAsString(fontSize: 12, overflow: false)]
        // [HideLabel]
        // [ShowInInspector]
        // [EnableGUI]
        // public string PathTip => "文件夹路径为: " + Path;
        //
        // [Button("跳转到 Example 文件夹", ButtonSizes.Large)]
        // public void SelectionFolder()
        // {
        //     OdinEditorLog.Log("RequiredIn Runtime Example 文件夹路径为: " + Path);
        //     ProjectEditorUtil.PingAndSelectAsset(Path);
        // }
    }
}
