using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class GUIColorExample : ExampleSO
    {
        [FoldoutGroup("构造函数 (R,G,B,A)")]
        [InfoBox("可以使用 RGBA 和 RGB，Rider 中可以预览这个颜色，默认的 Alpha == 1 ")]
        [GUIColor(1, 1, 0.5f)]
        public int guiColor0;

        [FoldoutGroup("GetColor 参数 支持多种解析字符串")]
        [LabelWidth(200)]
        public Color color = Color.green;

        [FoldoutGroup("GetColor 参数 支持多种解析字符串")]
        [LabelWidth(200)]
        public bool useRed;

        [FoldoutGroup("GetColor 参数 支持多种解析字符串")]
        [GUIColor("@useRed ? UnityEngine.Color.red : UnityEngine.Color.green")]
        [LabelWidth(200)]
        public string attributeExpressionExample;

        [FoldoutGroup("GetColor 参数 支持多种解析字符串")]
        [GUIColor("$color")]
        [LabelWidth(200)]
        public string fieldNameExample;

        [FoldoutGroup("GetColor 参数 支持多种解析字符串")]
        [GUIColor("$GetColor")]
        [LabelWidth(200)]
        public string methodNameExample;

        [FoldoutGroup("GetColor 参数 支持多种解析字符串")]
        [GUIColor("$ColorProperty")]
        [LabelWidth(200)]
        public string propertyNameExample;

        [FoldoutGroup("GUIColor 扩展")]
        [InfoBox("直接使用 Odin 内置的特殊字符")]
        [GUIColor("red")]
        public int guiColor1;

        [FoldoutGroup("GUIColor 扩展")]
        [InfoBox("使用 Hex Code，在 Rider 中可以预览这个颜色")]
        [GUIColor("#FF5512")]
        public int guiColor2;

        [FoldoutGroup("GUIColor 扩展")]
        [InfoBox("动态变换颜色")]
        [GUIColor("$GetDynamicColor")]
        public int guiColor4;

        public Color ColorProperty => color;

        Color GetColor() => useRed ? Color.red : Color.green;

        public override void SetDefaultValue()
        {
            guiColor0 = 0;
            guiColor1 = 0;
            guiColor2 = 0;
            attributeExpressionExample = "";
            fieldNameExample = "";
            methodNameExample = "";
            propertyNameExample = "";
        }

        static Color GetDynamicColor()
        {
            // 需要一直改变的元素，需要调用重绘
            GUIHelper.RequestRepaint();
            // 使用余弦函数和 timeSinceStartup 计算色调值
            // 通过添加和乘以常数来调整颜色变化的频率和范围
            return Color.HSVToRGB(
                Mathf.Cos((float)EditorApplication.timeSinceStartup + 1f) * 0.225f + 0.325f, 1, 1);
        }
    }
}
