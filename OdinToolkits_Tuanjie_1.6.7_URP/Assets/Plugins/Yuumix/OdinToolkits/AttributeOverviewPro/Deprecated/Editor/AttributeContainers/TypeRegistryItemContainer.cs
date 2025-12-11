using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
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

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "name",
                    ParameterDescription = "类型名称，用于在类型选择器中显示"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "categoryPath",
                    ParameterDescription = "类型在类型选择器中的路径，如 GameObject/UI "
                },
                new ParameterValue
                {
                    ReturnType = "SdfIconType 枚举",
                    ParameterName = "Icon",
                    ParameterDescription = "图标类型，默认为 SdfIconType.None"
                },
                new ParameterValue
                {
                    ReturnType = "Color",
                    ParameterName = "LightIconColor",
                    ParameterDescription = "Light 皮肤下的颜色"
                },
                new ParameterValue
                {
                    ReturnType = "Color",
                    ParameterName = "DarkIconColor",
                    ParameterDescription = "Dark 皮肤下的颜色"
                },
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "Priority",
                    ParameterDescription = "类型在类型选择器中的优先级，默认为 0"
                }
            };

        protected override string GetOriginalCode() =>
            ReadCodeWithoutNamespace(typeof(TypeRegistryItemExample));
    }
}
