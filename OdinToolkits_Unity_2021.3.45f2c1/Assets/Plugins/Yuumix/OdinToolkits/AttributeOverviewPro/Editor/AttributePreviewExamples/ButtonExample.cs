using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class ButtonExample : ExampleSO
    {
        #region Serialized Fields

        [PropertyOrder(1)]
        [FoldoutGroup("Button 基础使用")]
        public bool toggle;

        [PropertyOrder(10)]
        [FoldoutGroup("Button 基础使用")]
        public string buttonName = "Dynamic button name";

        #endregion

        [PropertyOrder(1)]
        [FoldoutGroup("Button 基础使用")]
        [InfoBox("基础使用，将特性直接置于方法上，用字符串命名按钮，即可快速创建可以执行方法的按钮")]
        [Button("Name of button")]
        void NamedButton()
        {
            SetToggle();
        }

        [PropertyOrder(10)]
        [FoldoutGroup("Button 基础使用")]
        [InfoBox("动态按钮名称，用成员变量赋值按钮名，$ 符号在 Rider 中可以自动补全")]
        [Button("$buttonName")]
        void DefaultSizedButton()
        {
            SetToggle();
        }

        [PropertyOrder(20)]
        [FoldoutGroup("Button 基础使用")]
        [InfoBox("动态按钮名称，使用表达式的返回值赋值")]
        [Button("@\"Expression label: \" + DateTime.Now.ToString(\"HH:mm:ss\")")]
        public void ExpressionLabel()
        {
            SetToggle();
        }

        [PropertyOrder(30)]
        [FoldoutGroup("Button 基础使用")]
        [InfoBox("设置按钮大小，默认可以使用 ButtonSizes 枚举")]
        [Button(ButtonSizes.Small)]
        void SmallButton()
        {
            SetToggle();
        }

        [PropertyOrder(40)]
        [FoldoutGroup("Button 基础使用")]
        [Button(ButtonSizes.Medium)]
        void MediumSizedButton()
        {
            SetToggle();
        }

        [PropertyOrder(50)]
        [FoldoutGroup("Button 基础使用")]
        [Button(ButtonSizes.Gigantic)]
        void GiganticButton()
        {
            SetToggle();
        }

        [PropertyOrder(50)]
        [FoldoutGroup("Button 基础使用")]
        [InfoBox("设置按钮大小，90 表示的是高度")]
        [Button(90)]
        void CustomSizedButton()
        {
            SetToggle();
        }

        void SetToggle()
        {
            toggle = !toggle;
            Debug.Log("toggle 的值改变了，当前值为: " + toggle);
        }

        [PropertyOrder(60)]
        [FoldoutGroup("Button 进阶使用 - 图标")]
        [InfoBox("SdfIconType 枚举控制图标样式，IconAlignment 枚举控制图标对齐方式")]
        [Button(SdfIconType.Dice1Fill, IconAlignment.LeftOfText)]
        void IconButtonLeftOfText() { }

        [PropertyOrder(70)]
        [FoldoutGroup("Button 进阶使用 - 图标")]
        [Button(SdfIconType.Dice2Fill, IconAlignment.RightOfText)]
        void IconButtonRightOfText() { }

        [PropertyOrder(80)]
        [FoldoutGroup("Button 进阶使用 - 图标")]
        [Button(SdfIconType.Dice3Fill, IconAlignment.LeftEdge)]
        void IconButtonLeftEdge() { }

        [PropertyOrder(90)]
        [FoldoutGroup("Button 进阶使用 - 图标")]
        [Button(SdfIconType.Dice4Fill, IconAlignment.RightEdge)]
        void IconButtonRightEdge() { }

        [PropertyOrder(100)]
        [FoldoutGroup("Button 进阶使用 - 图标")]
        [InfoBox("Stretch 参数控制宽度是否拉伸，默认拉伸")]
        [Button(SdfIconType.Dice5Fill, IconAlignment.RightEdge, Stretch = false)]
        void DontStretch() { }

        [PropertyOrder(110)]
        [FoldoutGroup("Button 进阶使用 - 图标")]
        [InfoBox("当 Stretch == false 时，ButtonAlignment 控制按钮的位置，" +
                 "范围 0 - 1，0 表示最左侧，1 表示最右侧")]
        [Button(SdfIconType.Dice5Fill, IconAlignment.RightEdge, Stretch = false, ButtonAlignment = 1f)]
        void DontStretchAndAlign() { }

        [PropertyOrder(120)]
        [FoldoutGroup("Button 进阶使用 - 有参数的方法")]
        [InfoBox("设置要输出的 int 类型参数，使用 ButtonStyle.CompactBox 的按钮样式，默认样式")]
        [Button("输出 Int 值", ButtonSizes.Medium, ButtonStyle.CompactBox)]
        void HaveArgFunction(int arg1)
        {
            Debug.Log("参数为: " + arg1);
        }

        [PropertyOrder(130)]
        [FoldoutGroup("Button 进阶使用 - 有参数的方法")]
        [InfoBox("设置要输出的 string 类型参数，使用 ButtonStyle.FoldoutButton 的按钮样式")]
        [Button("输出 string 值", ButtonSizes.Medium, ButtonStyle.FoldoutButton)]
        void HaveStringArgFunction(string strArg)
        {
            Debug.Log("参数为: " + strArg);
        }

        [PropertyOrder(140)]
        [FoldoutGroup("Button 进阶使用 - 有参数的方法")]
        [InfoBox("设置要输出的 float 类型参数，使用 ButtonStyle.Box 的按钮样式")]
        [Button("输出 float 值", ButtonSizes.Medium, ButtonStyle.Box)]
        void HaveFloatArgFunction(float floatArg)
        {
            Debug.Log("参数为: " + floatArg);
        }
    }
}
