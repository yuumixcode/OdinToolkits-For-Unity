using System.Collections.Generic;
using YuumixEditor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    internal class AssetsOnlyAttributeModel : AbstractAttributeModel
    {
        public override void Initialize()
        {
            HeaderWidget = new BilingualHeaderWidget("AssetsOnly", "AssetsOnly", "选择一个资产", "Select an asset");
            UsageTips = new List<string>
            {
                "只能选择一个资产aaaaaaaaaaaaaaadjiwjaidjiawjdiwajdiowjaidjwaiodjiowajdiowajiodjawiojdioajdwjasijgkncjbieri",
                "只能选择多个资产",
                "资产列表不能为空",
                "资产列表中不能包含重复资产",
                "资产列表中不能包含空资产",
                "资产列表中不能包含重复资产"
            };
            AttributeParameters = null;
        }
    }
}
