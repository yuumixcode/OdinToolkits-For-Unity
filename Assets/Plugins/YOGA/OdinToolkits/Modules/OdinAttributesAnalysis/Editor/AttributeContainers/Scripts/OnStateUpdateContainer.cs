using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class OnStateUpdateContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "OnStateUpdate";
        }

        protected override string SetBrief()
        {
            return "当 Property 更新时触发的方法，Property 的更新基于 Unity 设置的 Inspector 的更新时机";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "至少执行一次，当 Property 的状态改变，比如隐藏了某个字段，那么 Inspector 面板就会更新，此时将会触发",
                "被标记的 Property 可以控制其他 Property，也可以控制自身依赖其他 Property"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "string",
                    paramName = "action",
                    paramDescription = "触发函数名，方法可选参数 (InspectorProperty property, T value)，" +
                                       DescriptionConfigs.SupportAllResolver
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(OnStateUpdateExample));
        }
    }
}