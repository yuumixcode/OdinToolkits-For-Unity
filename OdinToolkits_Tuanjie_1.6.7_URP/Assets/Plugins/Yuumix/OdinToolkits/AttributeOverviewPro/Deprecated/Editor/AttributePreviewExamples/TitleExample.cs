using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    public class TitleExample : ExampleSO
    {
        string TitleProperty => useAlternativeTitle ? alternativeTitle : title;

        [PropertyOrder(25)]
        [FoldoutGroup("Title 扩展")]
        [InfoBox("属性显示需要 ShowInInspector")]
        [ShowInInspector]
        [Title("Title on a Property")]
        public int S { get; set; }

        [PropertyOrder(30)]
        [FoldoutGroup("Title 扩展")]
        [Title("Title on a Method")]
        [Button]
        public void DoNothing() { }

        string GetTitle() => useAlternativeTitle ? alternativeTitle : title;

        public override void SetDefaultValue()
        {
            title = "SOAP, SOAP & SOAP";
            alternativeTitle = "YOGA, YOGA & YOGA";
            useAlternativeTitle = false;
            titleMain = "";
            titleAttributeExpressionExample = "";
            titleFieldNameExample = "";
            titleMethodNameExample = "";
            titlePropertyNameExample = "";
            subtitle = "";
            subtitleAttributeExpressionExample = "";
            subtitleFieldNameExample = "";
            subtitleMethodNameExample = "";
            subtitlePropertyNameExample = "";
            titleTitleAlignmentsLeft = "";
            titleTitleAlignmentsCenter = "";
            titleTitleAlignmentsRight = "";
            titleTitleAlignmentsSplit = "";
            titleHorizontalLineNone = "";
            expression = 0;
            S = 0;
        }

        #region Serialized Fields

        [Title("共用参数")]
        public string title = "SOAP, SOAP & SOAP";

        public string alternativeTitle = "YOGA, YOGA & YOGA";
        public bool useAlternativeTitle;

        [FoldoutGroup("Title 参数 支持多种解析字符串")]
        [Title("主标题")]
        [LabelWidth(200)]
        public string titleMain;

        [FoldoutGroup("Title 参数 支持多种解析字符串")]
        [Title("@useAlternativeTitle ? alternativeTitle : title")]
        [LabelWidth(200)]
        public string titleAttributeExpressionExample;

        [FoldoutGroup("Title 参数 支持多种解析字符串")]
        [Title("$title")]
        [LabelWidth(200)]
        public string titleFieldNameExample;

        [FoldoutGroup("Title 参数 支持多种解析字符串")]
        [Title("$GetTitle")]
        [LabelWidth(200)]
        public string titleMethodNameExample;

        [FoldoutGroup("Title 参数 支持多种解析字符串")]
        [Title("$TitleProperty")]
        [LabelWidth(200)]
        public string titlePropertyNameExample;

        [PropertyOrder(5)]
        [FoldoutGroup("Subtitle 参数 支持多种解析字符串")]
        [Title("主标题", "子标题")]
        [InfoBox("使用方法和 Title 参数一模一样")]
        [LabelWidth(200)]
        public string subtitle;

        [PropertyOrder(5)]
        [FoldoutGroup("Subtitle 参数 支持多种解析字符串")]
        [Title("主标题", "@useAlternativeTitle ? alternativeTitle : title")]
        [LabelWidth(200)]
        public string subtitleAttributeExpressionExample;

        [PropertyOrder(5)]
        [FoldoutGroup("Subtitle 参数 支持多种解析字符串")]
        [Title("主标题", "$title")]
        [LabelWidth(200)]
        public string subtitleFieldNameExample;

        [PropertyOrder(5)]
        [FoldoutGroup("Subtitle 参数 支持多种解析字符串")]
        [Title("主标题", "$GetTitle")]
        [LabelWidth(200)]
        public string subtitleMethodNameExample;

        [PropertyOrder(5)]
        [FoldoutGroup("Subtitle 参数 支持多种解析字符串")]
        [Title("主标题", "$TitleProperty")]
        [LabelWidth(200)]
        public string subtitlePropertyNameExample;

        [PropertyOrder(10)]
        [FoldoutGroup("TitleAlignment 参数 主标题和子标题的对齐方式")]
        [Title("主标题", "子标题，默认左对齐")]
        [LabelWidth(250)]
        public string titleTitleAlignmentsLeft;

        [PropertyOrder(11)]
        [FoldoutGroup("TitleAlignment 参数 主标题和子标题的对齐方式")]
        [Title("主标题", "子标题", TitleAlignments.Centered)]
        [LabelWidth(250)]
        public string titleTitleAlignmentsCenter;

        [PropertyOrder(12)]
        [FoldoutGroup("TitleAlignment 参数 主标题和子标题的对齐方式")]
        [Title("主标题", "子标题", TitleAlignments.Right)]
        [LabelWidth(250)]
        public string titleTitleAlignmentsRight;

        [PropertyOrder(13)]
        [FoldoutGroup("TitleAlignment 参数 主标题和子标题的对齐方式")]
        [Title("主标题", "子标题", TitleAlignments.Split)]
        [LabelWidth(250)]
        public string titleTitleAlignmentsSplit;

        [PropertyOrder(15)]
        [FoldoutGroup("HorizontalLine 参数 下划线")]
        [Title("设置无下划线", HorizontalLine = false)]
        [LabelWidth(250)]
        public string titleHorizontalLineNone;

        [PropertyOrder(20)]
        [FoldoutGroup("Title 扩展")]
        [Title("@DateTime.Now.ToString(\"dd:MM:yyyy\")", "@DateTime.Now.ToString(\"HH:mm:ss\")")]
        public int expression;

        #endregion
    }
}
