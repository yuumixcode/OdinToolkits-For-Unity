using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class PreviewFieldContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "PreviewField";

        protected override string GetIntroduction() => "绘制一个正方形的 Preview 预览框，代替原有的 ObjectField，默认支持拖拽";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "默认支持拖拽，可以使用全局快捷键，Ctrl + 点击 = 删除实例，直接拖拽 = 交换或移动，Ctrl + 拖拽并放下 = 覆盖"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "previewGetter",
                    ParameterDescription = "可以渲染一个 Object 的 Preview 预览框，主要是用于渲染 Texture"
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "height",
                    ParameterDescription = "渲染框的高度"
                },
                new ParameterValue
                {
                    ReturnType = "ObjectFieldAlignment",
                    ParameterName = "alignment",
                    ParameterDescription = "对齐样式"
                },
                new ParameterValue
                {
                    ReturnType = "FilterMode",
                    ParameterName = "filterMode",
                    ParameterDescription = "纹理的过滤模式，有 Point，Bilinear，Trilinear"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(PreviewFieldExample));
    }
}
