using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class ShowInInspectorContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "ShowInInspector";
        }

        protected override string SetBrief()
        {
            return "在 Inspector 面板上显示任何成员";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "ShowInInspector 不会序列化任何东西，任何更改都不会由 ** 仅用 ** ShowInInspector 特性的成员保存",
                "当没有 ShowInInspector 特性时，检查器中没有出现的任何字段或属性都不会被序列化",
                "使用 Odin 提供的 Serialization Debugger 工具可以查询类中哪些是序列化的，哪些不是序列化的",
                "ShowInInspector 显示的成员的值可以修改生效，但是实际上没有序列化保存",
                "通常用于游戏运行时的观察数据，而且可以在游戏过程中实时修改"
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(ShowInInspectorExample));
        }
    }
}