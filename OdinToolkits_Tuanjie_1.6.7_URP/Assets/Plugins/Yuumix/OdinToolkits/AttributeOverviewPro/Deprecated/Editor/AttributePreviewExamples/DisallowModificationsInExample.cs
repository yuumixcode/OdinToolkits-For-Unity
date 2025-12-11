using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.SafeEditor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    public class DisallowModificationsInExample : ExampleSO
    {
        static string Path =>
            PathSafeEditorUtility.GetTargetFolderPath("RuntimeExamples", "OdinToolkits") +
            "/DisallowModificationsIn";

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
            YuumixLogger.OdinToolkitsLog("DisallowModificationsIn Runtime Example 文件夹路径为: " + Path);
            ProjectSafeEditorUtility.PingAndSelectAsset(Path);
        }
    }
}
