using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core.Runtime.Editor
{
    /// <summary>
    /// 获取 Odin 内部的语法高亮处理器并封装，通过此 Presenter 可以借助 Odin 的工具实现语法高亮
    /// </summary>
    public class OdinSyntaxHighlighterToolSO : OdinEditorScriptableSingleton<OdinSyntaxHighlighterToolSO>
    {
        [Title("Odin 语法高亮的默认颜色配置")]
        [ReadOnly]
        [ShowInInspector]
        public static Color BackgroundColor = new Color(0.118f, 0.118f, 0.118f, 1f);

        [ReadOnly]
        [ShowInInspector]
        public static Color TextColor = new Color(0.863f, 0.863f, 0.863f, 1f);

        [ReadOnly]
        [ShowInInspector]
        public static Color KeywordColor = new Color(0.337f, 0.612f, 0.839f, 1f);

        [ReadOnly]
        [ShowInInspector]
        public static Color IdentifierColor = new Color(0.306f, 0.788f, 0.69f, 1f);

        [ReadOnly]
        [ShowInInspector]
        public static Color CommentColor = new Color(0.341f, 0.651f, 0.29f, 1f);

        [ReadOnly]
        [ShowInInspector]
        public static Color LiteralColor = new Color(0.71f, 0.808f, 0.659f, 1f);

        [ReadOnly]
        [ShowInInspector]
        public static Color StringLiteralColor = new Color(0.839f, 0.616f, 0.522f, 1f);

        public static Type SyntaxHighlighterType = Type.GetType(
            "Sirenix.OdinInspector.Editor.Examples.SyntaxHighlighter," +
            "Sirenix.OdinInspector.Editor," +
            "Version=1.0.0.0," +
            "Culture=neutral," +
            "PublicKeyToken=null");

        public static readonly MethodInfo ParseMethod =
            SyntaxHighlighterType.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public);

        [PropertyOrder(-10)]
        [Title("使用须知", "Odin 的语法高亮处理有一定局限性，目前仅发现以下要点", TitleAlignments.Centered)]
        [OnInspectorGUI]
        void OnGUI1() { }

        [PropertyOrder(-5)]
        [DisplayAsString(TextAlignment.Left, FontSize = 14)]
        [HideLabel]
        public string one = "1.进行处理的源代码中，不能包含有命名空间";

        [PropertyOrder(-5)]
        [DisplayAsString(TextAlignment.Left, FontSize = 14)]
        [HideLabel]
        public string two = "2.进行处理的源代码中，不能包含有 $ 内插字符串";

        [PropertyOrder(-5)]
        [DisplayAsString(TextAlignment.Left, FontSize = 14)]
        [HideLabel]
        public string three = "3.进行处理的源代码需要提前格式化，保证合理的空格。";

        [PropertyOrder(-5)]
        [DisplayAsString(TextAlignment.Left, FontSize = 14)]
        [HideLabel]
        public string four = "4.进行处理的源代码要注意富文本标签的使用，失效时检查是否有此类原因";

        [HideInInspector]
        public List<CustomSyntaxHighlighterColorGroup>
            customColorGroups = new List<CustomSyntaxHighlighterColorGroup>();

        /// <summary>
        /// 使用富文本标记进行脚本语法高亮
        /// </summary>
        /// <param name="code">包含换行符的代码文本</param>
        /// <returns>包含了富文本的代码文本</returns>
        public static string ApplyCodeHighlighting(string code)
        {
            if (ParseMethod != null)
            {
                return ParseMethod.Invoke(null, new object[] { code }) as string;
            }

            YuumixLogger.EditorLogError("无法获取 SyntaxHighlighter.Parse 方法");
            return string.Empty;
        }

        [PropertySpace(10)]
        [Button(ButtonSizes.Large)]
        public void TestSyntaxHighlight()
        {
            const string code = @"
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : ScriptableObject
{
    public int ExampleInt;
    public float ExampleFloat;
    public string ExampleString;
    public bool ExampleBool;
    public Vector3 ExampleVector3;
    public Color ExampleColor;
    public GameObject ExampleGameObject;
    public List<int> ExampleList;
    public Dictionary<string, int> ExampleDictionary;       
}";
            YuumixLogger.OdinToolkitsLog(ApplyCodeHighlighting(code));
        }
    }
}
