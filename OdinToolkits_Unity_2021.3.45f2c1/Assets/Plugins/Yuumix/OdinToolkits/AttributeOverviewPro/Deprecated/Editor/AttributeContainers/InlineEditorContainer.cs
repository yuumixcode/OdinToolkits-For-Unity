using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class InlineEditorContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "InlineEditor";

        protected override string GetIntroduction() => "立刻绘制另外一个类的 Editor 样式";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "InlineEditorModes",
                    ParameterName = "inlineEditorMode",
                    ParameterDescription = "显示模式，默认为 InlineEditorModes.GUIOnly"
                },
                new ParameterValue
                {
                    ReturnType = ">>> InlineEditorModes 枚举",
                    ParameterName = "InlineEditorModes.GUIAndHeader",
                    ParameterDescription = "显示 GUI 以及 Inspector 界面的 Header 部分"
                },
                new ParameterValue
                {
                    ReturnType = ">>> InlineEditorModes 枚举",
                    ParameterName = "InlineEditorModes.GUIAndPreview",
                    ParameterDescription = "显示 GUI 以及 Inspector 界面的 Preview 预览部分"
                },
                new ParameterValue
                {
                    ReturnType = ">>> InlineEditorModes 枚举",
                    ParameterName = "InlineEditorModes.SmallPreview",
                    ParameterDescription = "显示 SmallPreview 小型预览窗口"
                },
                new ParameterValue
                {
                    ReturnType = ">>> InlineEditorModes 枚举",
                    ParameterName = "InlineEditorModes.LargePreview",
                    ParameterDescription = "显示 LargePreview 大型预览窗口"
                },
                new ParameterValue
                {
                    ReturnType = ">>> InlineEditorModes 枚举",
                    ParameterName = "InlineEditorModes.FullEditor",
                    ParameterDescription = "完全显示"
                },
                new ParameterValue
                {
                    ReturnType = "InlineEditorObjectFieldModes",
                    ParameterName = "objectFieldMode",
                    ParameterDescription = "绘制模式，默认为 InlineEditorObjectFieldModes.Boxed"
                },
                new ParameterValue
                {
                    ReturnType = ">>> InlineEditorObjectFieldModes 枚举",
                    ParameterName = "InlineEditorObjectFieldModes.Foldout",
                    ParameterDescription = "显示在 Foldout 模式下"
                },
                new ParameterValue
                {
                    ReturnType = ">>> InlineEditorObjectFieldModes 枚举",
                    ParameterName = "InlineEditorObjectFieldModes.Hidden",
                    ParameterDescription = "不为空时隐藏 ObjectField"
                },
                new ParameterValue
                {
                    ReturnType = ">>> InlineEditorObjectFieldModes 枚举",
                    ParameterName = "InlineEditorObjectFieldModes.CompletelyHidden",
                    ParameterDescription = "彻底隐藏，必须代码中赋值，否则一旦为 null，面板中无法赋值"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(InlineEditorExample));
    }
}
