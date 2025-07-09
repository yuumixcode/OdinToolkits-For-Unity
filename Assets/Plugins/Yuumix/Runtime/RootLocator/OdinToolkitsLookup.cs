using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core.RootLocator
{
    public sealed class OdinToolkitsLookup : ScriptableObject
    {
        [DisplayAsString(FontSize = 22, Overflow = false)]
        [TempComposite]
        public string WarningCn1
            => "不要删除这个资源文件！";

        [DisplayAsString(FontSize = 15, Overflow = false)]
        [TempComposite]
        public string WarningCn2
            => "该资源文件用于定位 OdinToolkits 文件夹路径，防止整体移动 OdinToolkits 文件夹时出现错误。";

        [DisplayAsString(FontSize = 22, Overflow = false)]
        [TempComposite]
        public string WarningEn1
            => "Don't Delete this file !";

        [DisplayAsString(FontSize = 15, Overflow = false)]
        [TempComposite]
        public string WarningEn2
            => "This resource file is used to locate the path of the OdinToolkits folder to prevent errors " +
               "when moving the OdinToolkits folder as a whole.";

        [IncludeMyAttributes]
        [PropertySpace(10)]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public class TempCompositeAttribute : Attribute { }
    }
}
