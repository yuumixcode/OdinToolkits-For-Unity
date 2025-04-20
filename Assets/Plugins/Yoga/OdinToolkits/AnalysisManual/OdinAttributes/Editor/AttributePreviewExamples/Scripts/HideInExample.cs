using Sirenix.OdinInspector;
using YOGA.Modules.Utilities;
using YOGA.OdinToolkits.Common.Runtime;
using YOGA.OdinToolkits.Config.Editor;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class HideInExample : ExampleScriptableObject
    {
        static string Path
        {
            get => OdinToolkitsPaths.GetRootPath() +
                   "/ChineseManual/ChineseAttributesOverview/RuntimeExamples/HideIn";
        }

        [DisplayAsString(fontSize: 12, overflow: false)]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string Tip
        {
            get => "提示: " + "HideInAttribute 针对预制体使用，需要到具体情况中才能生效";
        }

        [DisplayAsString(fontSize: 12, overflow: false)]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string PathTip
        {
            get => "文件夹路径为: " + Path;
        }

        [Button("跳转到 Example 文件夹", ButtonSizes.Large)]
        public void SelectionFolder()
        {
            OdinLog.Log("HideIn Runtime Example 文件夹路径为: " + Path);
            ProjectEditorUtility.PingAndSelectAsset(Path);
        }
    }
}
