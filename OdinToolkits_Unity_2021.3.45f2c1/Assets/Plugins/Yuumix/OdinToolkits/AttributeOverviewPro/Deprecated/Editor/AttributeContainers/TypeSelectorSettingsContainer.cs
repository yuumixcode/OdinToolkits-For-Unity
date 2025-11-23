using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class TypeSelectorSettingsContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "TypeSelectorSettings";

        protected override string GetIntroduction() => "为使用 Odin 绘制的类型选择器提供选项";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "Odin 对于 TypeSelectorSettings 有全局设置，如果另外设置覆盖，则将使用全局设置"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "ShowCategories",
                    ParameterDescription = "是否显示类型分组"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "PreferNamespaces",
                    ParameterDescription = "指定是否优先使用命名空间而不是程序集类别名称"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "ShowNoneItem",
                    ParameterDescription = "指定是否显示‘<none>’项，多一行空的，无法选择的项"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "FilterTypesFunction",
                    ParameterDescription = "自定义类型过滤函数，Func<Type, bool>，参数为类型，返回值为 bool，表示是否显示该类型。" +
                                           DescriptionConfigs.SupportAllResolver
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(TypeSelectorSettingsExample));
    }
}
