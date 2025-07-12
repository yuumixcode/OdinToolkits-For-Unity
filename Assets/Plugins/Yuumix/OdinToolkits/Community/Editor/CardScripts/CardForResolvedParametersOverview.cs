using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Shared;
using Yuumix.OdinToolkits.ThirdParty.ResolvedParametersOverview.Schwapo.Editor.Window;

namespace Yuumix.OdinToolkits.Community.Editor
{
    public class CardForResolvedParametersOverview : CommunityCardSO<CardForResolvedParametersOverview>
    {
        protected override MultiLanguageDisplayAsStringWidget GetCardHeader() =>
            new MultiLanguageDisplayAsStringWidget(
                "被解析的参数总览",
                "Resolved Parameters Overview");

        protected override MultiLanguageDisplayAsStringWidget GetIntroduction() =>
            new MultiLanguageDisplayAsStringWidget(
                "可以查看 Odin Inspector 所有自带的特性中能够进行字符串解析的参数以及用法示例。",
                "You can view all the parameters that can perform string parsing among the built-in attributes of Odin Inspector, " +
                "as well as their usage examples.");
        

        protected override void OpenWindowOrPingFolder()
        {
            ResolvedParametersOverviewWindow.Open();
        }
        
    }
}
