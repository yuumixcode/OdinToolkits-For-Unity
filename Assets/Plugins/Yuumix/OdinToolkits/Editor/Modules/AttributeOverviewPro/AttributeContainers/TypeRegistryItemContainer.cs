using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Editor
{
    public class TypeRegistryItemContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "TypeRegistryItem";

        protected override string GetIntroduction() => "自定义类型在 Odin 的类型选择器中的样式";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "主要是用于修改类型选择器中的样式，属于编辑器美化类型"
            };

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "string",
                    paramName = "name",
                    paramDescription = "类型名称，用于在类型选择器中显示"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "categoryPath",
                    paramDescription = "类型在类型选择器中的路径，如 GameObject/UI "
                },
                new ParamValue
                {
                    returnType = "SdfIconType 枚举",
                    paramName = "Icon",
                    paramDescription = "图标类型，默认为 SdfIconType.None"
                },
                new ParamValue
                {
                    returnType = "Color",
                    paramName = "LightIconColor",
                    paramDescription = "Light 皮肤下的颜色"
                },
                new ParamValue
                {
                    returnType = "Color",
                    paramName = "DarkIconColor",
                    paramDescription = "Dark 皮肤下的颜色"
                },
                new ParamValue
                {
                    returnType = "int",
                    paramName = "Priority",
                    paramDescription = "类型在类型选择器中的优先级，默认为 0"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(TypeRegistryItemExample));
    }
}
