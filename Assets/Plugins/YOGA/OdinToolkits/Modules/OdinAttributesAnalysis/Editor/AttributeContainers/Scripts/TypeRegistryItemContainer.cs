using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class TypeRegistryItemContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "TypeRegistryItem";
        }

        protected override string SetBrief()
        {
            return "自定义类型在 Odin 的类型选择器中的样式";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "主要是用于修改类型选择器中的样式，属于编辑器美化类型"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "string",
                    paramName = "name",
                    paramDescription = "类型名称，用于在类型选择器中显示"
                },
                new()
                {
                    returnType = "string",
                    paramName = "categoryPath",
                    paramDescription = "类型在类型选择器中的路径，如 GameObject/UI "
                },
                new()
                {
                    returnType = "SdfIconType 枚举",
                    paramName = "Icon",
                    paramDescription = "图标类型，默认为 SdfIconType.None"
                },
                new()
                {
                    returnType = "Color",
                    paramName = "LightIconColor",
                    paramDescription = "Light 皮肤下的颜色"
                },
                new()
                {
                    returnType = "Color",
                    paramName = "DarkIconColor",
                    paramDescription = "Dark 皮肤下的颜色"
                },
                new()
                {
                    returnType = "int",
                    paramName = "Priority",
                    paramDescription = "类型在类型选择器中的优先级，默认为 0"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(TypeRegistryItemExample));
        }
    }
}