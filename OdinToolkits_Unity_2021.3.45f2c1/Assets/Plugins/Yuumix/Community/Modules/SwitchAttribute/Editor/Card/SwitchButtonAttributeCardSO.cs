using UnityEngine;
using Yuumix.Community.Editor;
using YuumixEditor;

namespace Yuumix.Community.SwitchAttribute.Editor
{
    public class SwitchButtonAttributeCardSO : CommunityCardSO<SwitchButtonAttributeCardSO>
    {
        protected override BilingualDisplayAsStringWidget GetCardHeader()
            => new BilingualDisplayAsStringWidget(
                "SwitchButton 特性",
                "SwitchButtonAttribute");

        protected override BilingualDisplayAsStringWidget GetIntroduction()
            => new BilingualDisplayAsStringWidget(
                "对 bool 类型的变量使用，绘制一个开关样式，而非使用 Unity 的默认 bool 样式",
                "Use the switch style to draw for bool type variables instead of using the default bool style provided by Unity");

        protected override void OpenWindowOrPingFolder()
        {
            ProjectEditorUtility.PingAndSelectAsset(OdinToolkitsEditorPaths.GetYuumixRootPath() +
                                                    "/Community/Modules/SwitchAttribute");
        }

        protected override Author GetAuthor() => new Author("Schwapo", "https://github.com/schwapo");

        protected override void OpenModuleLink()
        {
            Application.OpenURL("https://github.com/schwapo/Switch");
        }
    }
}
