using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class DictionaryDrawerSettingsContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "DictionaryDrawerSettings";

        protected override string GetIntroduction() => "设置字典绘制样式";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "Unity 无法序列化字典，需要 Odin 序列化，需要继承特殊的序列化基类或者自己实现序列化"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "KeyLabel",
                    ParameterDescription = "修改 Key 键的名称"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "ValueLabel",
                    ParameterDescription = "修改 Value 值的名称"
                },
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "KeyColumnWidth",
                    ParameterDescription = "修改 Key 键的宽度，但是没有效果"
                },
                new ParameterValue
                {
                    ReturnType = "DictionaryDisplayOptions",
                    ParameterName = "DisplayMode",
                    ParameterDescription = "修改字典的显示模式，默认为 OneLine 一行"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "IsReadOnly",
                    ParameterDescription = "是否只读，不能在面板上修改字典的元素数量和字典结构，可以通过代码修改，同时可以在面板上修改具体元素内部的 Property"
                }
            };

        protected override string GetOriginalCode() =>
            ReadCodeWithoutNamespace(typeof(DictionaryDrawerSettingsExample));
    }
}
