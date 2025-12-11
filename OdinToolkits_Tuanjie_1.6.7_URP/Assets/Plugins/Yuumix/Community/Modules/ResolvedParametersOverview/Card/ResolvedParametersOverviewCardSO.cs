using Yuumix.OdinToolkits.Core.SafeEditor;
using UnityEngine;
using Yuumix.Community.Editor;

namespace Yuumix.Community.Schwapo.Editor
{
    public class ResolvedParametersOverviewCardSO : CommunityCardSO<ResolvedParametersOverviewCardSO>
    {
        protected override BilingualDisplayAsStringWidget GetCardHeader() =>
            new BilingualDisplayAsStringWidget(
                "Odin 特性中被解析的参数总览",
                "Resolved Parameters Overview");

        protected override BilingualDisplayAsStringWidget GetIntroduction() =>
            new BilingualDisplayAsStringWidget(
                "可以查看 Odin Inspector 所有自带的特性中能够进行字符串解析的参数以及用法示例。",
                "You can view all the parameters that can perform string parsing among the built-in attributes of Odin Inspector, " +
                "as well as their usage examples.");

        protected override Author GetAuthor() => new Author("Schwapo", "https://github.com/schwapo");

        protected override void OpenWindowOrPingFolder()
        {
            ResolvedParametersOverviewWindow.Open();
        }

        protected override void OpenModuleLink()
        {
            Application.OpenURL("https://github.com/schwapo/Odin-Resolved-Parameters-Overview");
        }
    }
}
