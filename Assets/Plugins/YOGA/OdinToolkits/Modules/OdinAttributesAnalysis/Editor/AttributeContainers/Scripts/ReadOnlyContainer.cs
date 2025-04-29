using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class ReadOnlyContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "ReadOnly";
        }

        protected override string SetBrief()
        {
            return "让 Property 在 Inspector 上只读";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "仅在 Inspector 上只读，代码中依旧可以修改"
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(ReadOnlyExample));
        }
    }
}