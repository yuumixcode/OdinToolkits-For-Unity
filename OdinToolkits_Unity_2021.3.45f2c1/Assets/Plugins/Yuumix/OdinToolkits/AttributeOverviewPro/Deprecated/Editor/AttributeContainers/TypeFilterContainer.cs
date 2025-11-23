using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class TypeFilterContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "TypeFilter";

        protected override string GetIntroduction() => "可以根据列表中的类型直接在面板中创建一个对象实例，需要 Odin 序列化";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "TypeFilter 需要 Odin 序列化才能工作，而且此时的序列化不仅仅是编辑器阶段，需要打包进入成品，无法在 EditorOnly 状态下使用",
                "可以用作测试时使用，不必代码中修改，在 Inspector 面板上修改本次运行使用哪一个子类"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "filterGetter",
                    ParameterDescription = "填写返回值是类型集合(列表或者数组)的方法名，返回的类型数组就是可选的类型，" +
                                           DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "DropdownTitle",
                    ParameterDescription = "选择框的标题"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "DrawValueNormally",
                    ParameterDescription = "是否额外多绘制一个正常的类。默认为 false"
                }
            };

        public override List<ResolvedParam> GetResolvedParams() =>
            new List<ResolvedParam>
            {
                new ResolvedParam
                {
                    ParamName = "FilterGetter",
                    ReturnType = "IEnumerable<Type>",
                    ResolverType = ResolverType.ValueResolver,
                    ParamValues = new List<ParameterValue>
                    {
                        new ParameterValue
                        {
                            ReturnType = typeof(InspectorProperty).FullName,
                            ParameterName = "$property",
                            ParameterDescription = DescriptionConfigs.InspectorPropertyDesc
                        },
                        new ParameterValue
                        {
                            ReturnType = "T 泛型",
                            ParameterName = "$value",
                            ParameterDescription = DescriptionConfigs.ValueDesc
                        }
                    }
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(TypeFilterExample));
    }
}
