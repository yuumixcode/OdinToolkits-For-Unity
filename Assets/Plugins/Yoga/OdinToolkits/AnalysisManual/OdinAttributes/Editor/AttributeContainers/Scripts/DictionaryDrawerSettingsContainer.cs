using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class DictionaryDrawerSettingsContainer : AbsContainer
    {
        protected override string SetHeader() => "DictionaryDrawerSettings";

        protected override string SetBrief() => "设置字典绘制样式";

        protected override List<string> SetTip() => new List<string>()
        {
            "Unity 无法序列化字典，需要 Odin 序列化，需要继承特殊的序列化基类或者自己实现序列化"
        };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>()
        {
            new ParamValue()
            {
                returnType = "string",
                paramName = "KeyLabel",
                paramDescription = "修改 Key 键的名称"
            },
            new ParamValue()
            {
                returnType = "string",
                paramName = "ValueLabel",
                paramDescription = "修改 Value 值的名称"
            },
            new ParamValue()
            {
                returnType = "int",
                paramName = "KeyColumnWidth",
                paramDescription = "修改 Key 键的宽度，但是没有效果"
            },
            new ParamValue()
            {
                returnType = "DictionaryDisplayOptions",
                paramName = "DisplayMode",
                paramDescription = "修改字典的显示模式，默认为 OneLine 一行"
            },
            new ParamValue()
            {
                returnType = "bool",
                paramName = "IsReadOnly",
                paramDescription = "是否只读，不能在面板上修改字典的元素数量和字典结构，可以通过代码修改，同时可以在面板上修改具体元素内部的 Property"
            },
        };

        protected override string SetOriginalCode() =>
            ReadCodeWithoutNamespace(typeof(DictionaryDrawerSettingsExample));
    }
}