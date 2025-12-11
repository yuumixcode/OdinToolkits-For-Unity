using Yuumix.OdinToolkits.Core.SafeEditor;
using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;
using Yuumix.OdinToolkits.Core;
using YuumixEditor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    public class EnableInExample : ExampleSO
    {
        static string Path => PathSafeEditorUtility.GetTargetFolderPath("RuntimeExamples",
            "OdinToolkits") + "/EnableIn";

        [DisplayAsString(fontSize: 12, overflow: false)]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string Tip => "提示: " + "EnableInAttribute 针对预制体使用，需要到具体情况中才能生效";

        [DisplayAsString(fontSize: 12, overflow: false)]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string PathTip => "文件夹路径为: " + Path;

        [Button("跳转到 Example 文件夹", ButtonSizes.Large)]
        public void SelectionFolder()
        {
            YuumixLogger.OdinToolkitsLog("EnableIn Runtime Example 文件夹路径为: " + Path);
            ProjectSafeEditorUtility.PingAndSelectAsset(Path);
        }
    }
}
