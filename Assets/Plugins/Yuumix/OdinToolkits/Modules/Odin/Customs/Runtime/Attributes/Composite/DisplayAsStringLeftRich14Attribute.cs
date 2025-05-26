using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Odin.Customs.Runtime.Attributes.Composite
{
    /// <summary>
    /// 由 [PropertySpace] [HideLabel] [ShowInInspector] [EnableGUI]
    /// [DisplayAsString(false, TextAlignment.Left, EnableRichText = true, FontSize = 14)] 组合而成。
    /// </summary>
    [IncludeMyAttributes]
    [HideLabel]
    [ShowInInspector]
    [EnableGUI]
    [DisplayAsString(false, TextAlignment.Left, EnableRichText = true, FontSize = 14)]
    public class DisplayAsStringLeftRich14Attribute : Attribute { }

    [IncludeMyAttributes]
    [HideLabel]
    [ShowInInspector]
    [EnableGUI]
    [DisplayAsString(false, TextAlignment.Left, EnableRichText = true, FontSize = 28)]
    public class DisplayAsStringLeftRich28Attribute : Attribute { }

    [IncludeMyAttributes]
    [HideLabel]
    [ShowInInspector]
    [EnableGUI]
    [DisplayAsString(false, TextAlignment.Left, EnableRichText = true, FontSize = 36)]
    public class DisplayAsStringLeftRich36Attribute : Attribute { }

    [IncludeMyAttributes]
    [HideLabel]
    [ShowInInspector]
    [EnableGUI]
    [DisplayAsString(false, TextAlignment.Left, EnableRichText = true, FontSize = 50)]
    public class DisplayAsStringLeftRich50Attribute : Attribute { }
}
