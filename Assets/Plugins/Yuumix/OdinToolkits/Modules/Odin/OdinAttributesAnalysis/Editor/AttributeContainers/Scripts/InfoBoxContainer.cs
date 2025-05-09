using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class InfoBoxContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "InfoBox";
        }

        protected override string SetBrief()
        {
            return "在标记的 Property 上方绘制一个消息盒，InfoBox";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "可以更换提示 InfoMessageType 级别图标和 SdfIconType 图标，还可以根据一个 bool 值选择是否显示 InfoBox"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "string",
                    paramName = "message",
                    paramDescription = "提示信息"
                },
                new()
                {
                    returnType = "InfoMessageType 枚举",
                    paramName = "messageType",
                    paramDescription = "提示信息级别，有 Info,Warning,Error,None"
                },
                new()
                {
                    returnType = "string",
                    paramName = "visibleIfMemberName",
                    paramDescription = "填写成员名，该成员类型或者返回值类型为 bool，" + DescriptionConfigs.SupportAllResolver
                },
                new()
                {
                    returnType = "bool",
                    paramName = "GUIAlwaysEnabled",
                    paramDescription = "是否在 GUI 模式下强制显示 InfoBox，InfoBox 不会被灰色显示"
                },
                new()
                {
                    returnType = "SdfIconType 枚举",
                    paramName = "icon",
                    paramDescription = "Odin 提供的图标类型，非常多"
                },
                new()
                {
                    returnType = "string",
                    paramName = "IconColor",
                    paramDescription = DescriptionConfigs.ColorDescription
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(InfoBoxExample));
        }
    }
}