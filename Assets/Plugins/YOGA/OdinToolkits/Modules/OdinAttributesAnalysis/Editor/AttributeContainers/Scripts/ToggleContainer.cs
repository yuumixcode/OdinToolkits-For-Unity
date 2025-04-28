using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class ToggleContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "Toggle";
        }

        protected override string SetBrief()
        {
            return "将一个自定义类的 Property 通过一个 bool 类型的值控制是否可以获取焦点";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "通常是一个自定义类内部有一个 bool 类型的字段，然后引用该字段",
                "也可以直接作用与一个类上，让该类默认变为 Toggle 样式"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "string",
                    paramName = "ToggleMemberName",
                    paramDescription = "成员名引用"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "CollapseOthersOnExpand",
                    paramDescription = "展开时是否折叠其他 Toggle，默认为 true"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(ToggleExample));
        }
    }
}