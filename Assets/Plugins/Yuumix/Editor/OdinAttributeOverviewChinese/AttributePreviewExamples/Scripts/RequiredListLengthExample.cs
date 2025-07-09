using Sirenix.OdinInspector;
using System.Collections.Generic;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using YuumixEditor;
using Yuumix.OdinToolkits;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class RequiredListLengthExample : ExampleScriptableObject
    {
        [FoldoutGroup("固定长度")]
        [RequiredListLength(3)]
        public List<int> list = new List<int>();

        [FoldoutGroup("设置长度范围")]
        [RequiredListLength(3, 5)]
        public List<int> list2 = new List<int>();

        [TitleGroup("设置长度范围/支持解析字符串")]
        [RequiredListLength(nameof(SetMinLength), nameof(SetMaxLength))]
        public List<int> list3 = new List<int>();

        int SetMinLength => 3;
        int SetMaxLength => 5;

        static string Path => PathEditorUtil.GetTargetFolderPath("RuntimeExamples",
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
            OdinEditorLog.Log("RequiredListLength Runtime Example 文件夹路径为: " + Path);
            ProjectEditorUtil.PingAndSelectAsset(Path);
        }
    }
}
