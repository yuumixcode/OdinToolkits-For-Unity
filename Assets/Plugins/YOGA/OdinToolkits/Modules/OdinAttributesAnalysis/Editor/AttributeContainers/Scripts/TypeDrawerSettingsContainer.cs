using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class TypeDrawerSettingsContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "TypeDrawerSettings";
        }

        protected override string SetBrief()
        {
            return "设置 Type 类型的绘制样式";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "Type 类型 Unity 无法直接序列化，可以使用 Odin 序列化，" +
                "通常 Type 类型需要显示在 Inspector 面板上时，是用于编辑器工具的，如果只是运行时检查，使用 [ShowInInspector]",
                "该 Example 采用了 Odin 序列化"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "Type",
                    paramName = "BaseType",
                    paramDescription = "基类类型"
                },
                new()
                {
                    returnType = "TypeInclusionFilter 枚举 [Flag]",
                    paramName = "Filter",
                    paramDescription = "过滤器，默认为 TypeInclusionFilter.IncludeAll"
                },
                new()
                {
                    returnType = "TypeInclusionFilter 枚举 [Flag]",
                    paramName = "TypeInclusionFilter.IncludeConcreteTypes",
                    paramDescription = "只包含具体实例类型，不能是接口，抽象类，或者泛型类"
                },
                new()
                {
                    returnType = "TypeInclusionFilter 枚举 [Flag]",
                    paramName = "TypeInclusionFilter.IncludeGenerics",
                    paramDescription = "只包含泛型类型"
                },
                new()
                {
                    returnType = "TypeInclusionFilter 枚举 [Flag]",
                    paramName = "TypeInclusionFilter.IncludeInterfaces",
                    paramDescription = "只包含接口类型"
                },
                new()
                {
                    returnType = "TypeInclusionFilter 枚举 [Flag]",
                    paramName = "TypeInclusionFilter.IncludeAbstracts",
                    paramDescription = "只包含抽象类"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(TypeDrawerSettingsExample));
        }
    }
}