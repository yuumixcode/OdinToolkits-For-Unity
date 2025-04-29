using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class TypeSelectorSettingsContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "TypeSelectorSettings";
        }

        protected override string SetBrief()
        {
            return "为使用 Odin 绘制的类型选择器提供选项";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "Odin 对于 TypeSelectorSettings 有全局设置，如果另外设置覆盖，则将使用全局设置"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "bool",
                    paramName = "ShowCategories",
                    paramDescription = "是否显示类型分组"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "PreferNamespaces",
                    paramDescription = "指定是否优先使用命名空间而不是程序集类别名称"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "ShowNoneItem",
                    paramDescription = "指定是否显示‘<none>’项，多一行空的，无法选择的项"
                },
                new()
                {
                    returnType = "string",
                    paramName = "FilterTypesFunction",
                    paramDescription = "自定义类型过滤函数，Func<Type, bool>，参数为类型，返回值为 bool，表示是否显示该类型。" +
                                       DescriptionConfigs.SupportAllResolver
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(TypeSelectorSettingsExample));
        }
    }
}