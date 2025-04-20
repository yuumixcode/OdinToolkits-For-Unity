using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class TypeFilterContainer : AbsContainer
    {
        protected override string SetHeader() => "TypeFilter";

        protected override string SetBrief() => "可以根据列表中的类型直接在面板中创建一个对象实例，需要 Odin 序列化";

        protected override List<string> SetTip() => new List<string>()
        {
            "TypeFilter 需要 Odin 序列化才能工作，而且此时的序列化不仅仅是编辑器阶段，需要打包进入成品，无法在 EditorOnly 状态下使用",
            "可以用作测试时使用，不必代码中修改，在 Inspector 面板上修改本次运行使用哪一个子类"
        };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>()
        {
            new ParamValue()
            {
                returnType = "string",
                paramName = "filterGetter",
                paramDescription = "填写返回值是类型集合(列表或者数组)的方法名，返回的类型数组就是可选的类型，" +
                                   DescriptionConfigs.SupportAllResolver
            },
            new ParamValue()
            {
                returnType = "string",
                paramName = "DropdownTitle",
                paramDescription = "选择框的标题"
            },
            new ParamValue()
            {
                returnType = "bool",
                paramName = "DrawValueNormally",
                paramDescription = "是否额外多绘制一个正常的类。默认为 false"
            },
        };

        public override List<ResolvedParam> SetResolvedParams() =>
            new List<ResolvedParam>()
            {
                new ResolvedParam()
                {
                    ParamName = "FilterGetter",
                    ReturnType = "IEnumerable<Type>",
                    ResolverType = ResolverType.ValueResolver,
                    ParamValues = new List<ParamValue>()
                    {
                        new ParamValue()
                        {
                            returnType = typeof(InspectorProperty).FullName,
                            paramName = "$property",
                            paramDescription = DescriptionConfigs.InspectorPropertyDesc
                        },
                        new ParamValue()
                        {
                            returnType = "T 泛型",
                            paramName = "$value",
                            paramDescription = DescriptionConfigs.ValueDesc
                        }
                    }
                }
            };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(TypeFilterExample));
    }
}
