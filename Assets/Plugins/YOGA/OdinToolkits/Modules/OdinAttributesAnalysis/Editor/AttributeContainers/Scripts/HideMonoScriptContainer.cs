using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class HideMonoScriptContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "HideMonoScript";
        }

        protected override string SetBrief()
        {
            return "隐藏灰色显示的脚本文件的 Property";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "可以在 Odin 的全局设置中关闭"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>();
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(HideMonoScriptExample));
        }
    }
}