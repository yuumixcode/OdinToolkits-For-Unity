using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class TypeDrawerSettingsContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "TypeDrawerSettings";

        protected override string GetIntroduction() => "设置 Type 类型的绘制样式";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "Type 类型 Unity 无法直接序列化，可以使用 Odin 序列化，" +
                "通常 Type 类型需要显示在 Inspector 面板上时，是用于编辑器工具的，如果只是运行时检查，使用 [ShowInInspector]",
                "该 Example 采用了 Odin 序列化"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "Type",
                    ParameterName = "BaseType",
                    ParameterDescription = "基类类型"
                },
                new ParameterValue
                {
                    ReturnType = "TypeInclusionFilter 枚举 [Flag]",
                    ParameterName = "Filter",
                    ParameterDescription = "过滤器，默认为 TypeInclusionFilter.IncludeAll"
                },
                new ParameterValue
                {
                    ReturnType = "TypeInclusionFilter 枚举 [Flag]",
                    ParameterName = "TypeInclusionFilter.IncludeConcreteTypes",
                    ParameterDescription = "只包含具体实例类型，不能是接口，抽象类，或者泛型类"
                },
                new ParameterValue
                {
                    ReturnType = "TypeInclusionFilter 枚举 [Flag]",
                    ParameterName = "TypeInclusionFilter.IncludeGenerics",
                    ParameterDescription = "只包含泛型类型"
                },
                new ParameterValue
                {
                    ReturnType = "TypeInclusionFilter 枚举 [Flag]",
                    ParameterName = "TypeInclusionFilter.IncludeInterfaces",
                    ParameterDescription = "只包含接口类型"
                },
                new ParameterValue
                {
                    ReturnType = "TypeInclusionFilter 枚举 [Flag]",
                    ParameterName = "TypeInclusionFilter.IncludeAbstracts",
                    ParameterDescription = "只包含抽象类"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(TypeDrawerSettingsExample));
    }
}
