using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class PreviewFieldContainer : AbsContainer
    {
        protected override string SetHeader() => "PreviewField";

        protected override string SetBrief() => "绘制一个正方形的 Preview 预览框，代替原有的 ObjectField，默认支持拖拽";

        protected override List<string> SetTip() =>
            new List<string>
            {
                "默认支持拖拽，可以使用全局快捷键，Ctrl + 点击 = 删除实例，直接拖拽 = 交换或移动，Ctrl + 拖拽并放下 = 覆盖"
            };

        protected override List<ParamValue> SetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "string",
                    paramName = "previewGetter",
                    paramDescription = "可以渲染一个 Object 的 Preview 预览框，主要是用于渲染 Texture"
                },
                new ParamValue
                {
                    returnType = "float",
                    paramName = "height",
                    paramDescription = "渲染框的高度"
                },
                new ParamValue
                {
                    returnType = "ObjectFieldAlignment",
                    paramName = "alignment",
                    paramDescription = "对齐样式"
                },
                new ParamValue
                {
                    returnType = "FilterMode",
                    paramName = "filterMode",
                    paramDescription = "纹理的过滤模式，有 Point，Bilinear，Trilinear"
                }
            };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(PreviewFieldExample));
    }
}
