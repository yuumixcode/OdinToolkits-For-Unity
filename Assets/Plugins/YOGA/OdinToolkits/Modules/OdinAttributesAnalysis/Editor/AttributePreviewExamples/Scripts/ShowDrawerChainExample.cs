using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class ShowDrawerChainExample : ExampleScriptableObject
    {
        [PropertyOrder(1)] [TitleGroup("Unity 内置类型开启 Odin 插件绘制的 DrawerChain")] [ShowDrawerChain]
        public int intValue;

        [PropertyOrder(1)] [TitleGroup("Unity 内置类型开启 Odin 插件绘制的 DrawerChain")] [ShowDrawerChain]
        public float floatValue;

        [PropertyOrder(1)] [TitleGroup("Unity 内置类型开启 Odin 插件绘制的 DrawerChain")] [ShowDrawerChain]
        public bool boolValue;

        [PropertyOrder(1)] [TitleGroup("Unity 内置类型开启 Odin 插件绘制的 DrawerChain")] [ShowDrawerChain]
        public Vector2 vector2Value;

        [PropertyOrder(1)] [TitleGroup("Unity 内置类型开启 Odin 插件绘制的 DrawerChain")] [ShowDrawerChain]
        public string stringValue = "Unity Build-in";

        [PropertyOrder(1)] [TitleGroup("Unity 内置类型开启 Odin 插件绘制的 DrawerChain")] [ShowDrawerChain]
        public LayerMask layerMask;

        [PropertyOrder(1)] [TitleGroup("Unity 内置类型开启 Odin 插件绘制的 DrawerChain")] [ShowDrawerChain]
        public Color color;

        [PropertyOrder(-1)]
        [ShowDrawerChain]
        [DisplayAsString(TextAlignment.Left, EnableRichText = true, FontSize = 14)]
        [ShowInInspector]
        [EnableGUI]
        [HideLabel]
        public string Info
        {
            get
            {
                GUIHelper.RequestRepaint();
                return "Display";
            }
        }
    }
}