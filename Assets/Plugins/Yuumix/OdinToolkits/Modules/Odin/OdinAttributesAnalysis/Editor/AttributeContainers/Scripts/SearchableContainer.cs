using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class SearchableContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "Searchable";
        }

        protected override string SetBrief()
        {
            return "让集合，类，复合类字段开启搜索框，不适用于字典";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "通常使用挂载即可，不需要设置任何参数，默认为 SearchFilterOptions.All，可以满足一般情况",
                "自定义类推荐实现接口 ISearchFilterable，可以实现满足特殊条件的精准搜索，列表开启 SearchFilterOptions.ISearchFilterableInterface 模式",
                "可以直接挂载到类名上，用于搜索这个类包含的字段或者值",
                "搜索要使用在代码中的字符，而不是显示在 Inspector 上的，如果希望中文搜索，代码酌情使用中文，比如一个类中引用了非常多的音频资源，超过 100 个，使用搜索可以快速获取"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "bool",
                    paramName = "FuzzySearch",
                    paramDescription = "开启模糊搜索，不区分大小写，默认为 true"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "Recursive",
                    paramDescription = "开启递归搜索，默认为 true"
                },
                new()
                {
                    returnType = "SearchFilterOptions 枚举类型",
                    paramName = "FilterOptions",
                    paramDescription = "搜索过滤选项，控制搜索方式，默认为 SearchFilterOptions.All"
                },
                new()
                {
                    returnType = ">>> SearchFilterOptions",
                    paramName = "SearchFilterOptions.PropertyName",
                    paramDescription = "可以匹配成员名称"
                },
                new()
                {
                    returnType = ">>> SearchFilterOptions",
                    paramName = "SearchFilterOptions.PropertyNiceName",
                    paramDescription = "可以匹配成员的 NiceName，指大写字母开头，单词分开"
                },
                new()
                {
                    returnType = ">>> SearchFilterOptions",
                    paramName = "SearchFilterOptions.TypeOfValue",
                    paramDescription = "可以匹配值的类型"
                },
                new()
                {
                    returnType = ">>> SearchFilterOptions",
                    paramName = "SearchFilterOptions.ValueToString",
                    paramDescription = "可以匹配任意值转化为 string 类型的结果"
                },
                new()
                {
                    returnType = ">>> SearchFilterOptions",
                    paramName = "SearchFilterOptions.ISearchFilterableInterface",
                    paramDescription = "自定义实现搜索过滤规则，在需要被搜索的元素（自定义类）上实现 ISearchFilterable 接口"
                },
                new()
                {
                    returnType = ">>> SearchFilterOptions",
                    paramName = "SearchFilterOptions.All",
                    paramDescription = "属于以上所有的合集，任何方式均可匹配"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(SearchableExample));
        }
    }
}