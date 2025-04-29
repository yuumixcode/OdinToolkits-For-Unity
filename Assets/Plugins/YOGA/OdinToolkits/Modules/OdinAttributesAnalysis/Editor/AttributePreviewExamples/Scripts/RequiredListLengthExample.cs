using Plugins.YOGA.Common.Utility.YuumiEditor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using YOGA.OdinToolkits.Common.Runtime;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class RequiredListLengthExample : ExampleScriptableObject
    {
        [FoldoutGroup("固定长度")] [RequiredListLength(3)]
        public List<int> list = new();

        [FoldoutGroup("设置长度范围")] [RequiredListLength(3, 5)]
        public List<int> list2 = new();

        [TitleGroup("设置长度范围/支持解析字符串")] [RequiredListLength(nameof(SetMinLength), nameof(SetMaxLength))]
        public List<int> list3 = new();

        private int SetMinLength => 3;
        private int SetMaxLength => 5;

        private static string Path => ProjectEditorUtility
            .GetTargetFolderPath("RuntimeExamples",
                "OdinToolkits") + "/RequiredListLength";

        [DisplayAsString(fontSize: 12, overflow: false)]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string Tip => "提示: " + "RequiredListLengthAttribute 的 PrefabKind 参数针对预制体使用，需要到具体情况中才能生效";

        [DisplayAsString(fontSize: 12, overflow: false)]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string PathTip => "文件夹路径为: " + Path;

        [Button("跳转到 Example 文件夹", ButtonSizes.Large)]
        public void SelectionFolder()
        {
            OdinLog.Log("RequiredListLength Runtime Example 文件夹路径为: " + Path);
            ProjectEditorUtility.PingAndSelectAsset(Path);
        }
    }
}