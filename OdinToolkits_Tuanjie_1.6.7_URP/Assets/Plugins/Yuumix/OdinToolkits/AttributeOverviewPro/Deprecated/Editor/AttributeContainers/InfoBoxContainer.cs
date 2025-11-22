using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class InfoBoxContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "InfoBox";

        protected override string GetIntroduction() => "在标记的 Property 上方绘制一个消息盒，InfoBox";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "可以更换提示 InfoMessageType 级别图标和 SdfIconType 图标，还可以根据一个 bool 值选择是否显示 InfoBox"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "message",
                    ParameterDescription = "提示信息"
                },
                new ParameterValue
                {
                    ReturnType = "InfoMessageType 枚举",
                    ParameterName = "messageType",
                    ParameterDescription = "提示信息级别，有 Info,Warning,Error,None"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "visibleIfMemberName",
                    ParameterDescription = "填写成员名，该成员类型或者返回值类型为 bool，" + DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "GUIAlwaysEnabled",
                    ParameterDescription = "是否在 GUI 模式下强制显示 InfoBox，InfoBox 不会被灰色显示"
                },
                new ParameterValue
                {
                    ReturnType = "SdfIconType 枚举",
                    ParameterName = "icon",
                    ParameterDescription = "Odin 提供的图标类型，非常多"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "IconColor",
                    ParameterDescription = DescriptionConfigs.ColorDescription
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(InfoBoxExample));
    }
}
