using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Tools.SyntaxHighlighter.Editor
{
    [Serializable]
    public class CustomSyntaxHighlighterColorGroup
    {
        [BoxGroup(ShowLabel = false)] [ColorPalette] [LabelText("背景颜色")] [InfoBox("绘制背景时可以设置为此颜色，此颜色默认不参与代码颜色设置")]
        public Color backgroundColor;

        [BoxGroup(ShowLabel = false)] [ColorPalette] [LabelText("普通文本颜色")]
        public Color textColor;

        [BoxGroup(ShowLabel = false)] [ColorPalette] [LabelText("关键字颜色")]
        public Color keywordColor;

        [BoxGroup(ShowLabel = false)] [ColorPalette] [LabelText("标识符颜色")]
        public Color identifierColor;

        [BoxGroup(ShowLabel = false)] [ColorPalette] [LabelText("注释颜色")]
        public Color commentColor;

        [BoxGroup(ShowLabel = false)] [ColorPalette] [LabelText("字面量颜色")]
        public Color literalColor;

        [BoxGroup(ShowLabel = false)] [ColorPalette] [LabelText("字符串颜色")]
        public Color stringLiteralColor;
    }
}