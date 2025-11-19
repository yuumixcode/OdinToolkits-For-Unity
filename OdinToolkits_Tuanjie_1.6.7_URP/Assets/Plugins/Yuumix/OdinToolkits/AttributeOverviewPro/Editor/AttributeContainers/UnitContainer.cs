using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class UnitContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "Unit";

        protected override string GetIntroduction() => "对字段或者属性增加一个单位后缀";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "鼠标右键提供了更换单位的选择",
                "可以输入值+单位简写，将会自动换算成目标单位的值",
                "Odin 提供了一个完整的 Unit Overview",
                "手动提供换算比例即可自定义单位"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "Unit",
                    ParameterName = "unit",
                    ParameterDescription = "单位类型，一个参数的构造函数时，此单位对应的值就是实际值"
                },
                new ParameterValue
                {
                    ReturnType = "Unit",
                    ParameterName = "base",
                    ParameterDescription = "单位类型，表示实际单位"
                },
                new ParameterValue
                {
                    ReturnType = "Unit",
                    ParameterName = "display",
                    ParameterDescription = "单位类型，表示显示在面板上的单位，输入时，按照显示单位和实际单位进行换算，得到最终值才是变量值"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "DisplayAsString",
                    ParameterDescription = "如果为 true，显示为字符串"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "ForceDisplayUnit",
                    ParameterDescription = "强制显示单位，如果为 true，将无法改变显示单位的类型"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(UnitExample));
    }
}
