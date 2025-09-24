namespace Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor.Test.TestChineseSummaryAttribute
{
    /// <summary>
    /// 用于测试 ChineseSummary 同步的不同情况的源代码字符串仓库
    /// </summary>
    public static class TestChineseSummaryCodeRepository
    {
        /// <summary>
        /// 成员级别（包括字段，属性，方法等）的 Summary，最推荐的代码样式，每个特性独立一行
        /// </summary>
        public const string MEMBER_SUMMARY_A = @"    /// <summary>
    /// 测试成员级别（包括字段，属性，方法等）的 Summary，以字段为例
    /// </summary>
    [EmptyPlaceholder]
    [TextArea]
    public string testMemberSummary;";

        #region 类级别的 Summary

        /// <summary>
        /// 其他特性和类声明在同一行的情况
        /// </summary>
        public const string CLASS_SUMMARY_A = @"    /// <summary>
    /// 测试类级别（包括结构体，接口等）的 Summary，以 class 为例
    /// </summary>
    [EmptyPlaceholder] public class TestClassSummary { }";

        /// <summary>
        /// 最佳代码样式，特性和类声明不在同一行
        /// </summary>
        public const string CLASS_SUMMARY_B = @"    /// <summary>
    /// 测试类级别（包括结构体，接口等）的 Summary，以 class 为例
    /// </summary>
    [EmptyPlaceholder]
    public class TestClassSummary { }";

        /// <summary>
        /// Summary 注释均在同一行的情况
        /// </summary>
        public const string CLASS_SUMMARY_C = @"    /// <summary> 测试类级别（包括结构体，接口等）的 Summary，以 class 为例 </summary>
    [EmptyPlaceholder]
    public class TestClassSummary { }";

        /// <summary>
        /// Summary 注释存在多行，但是 &lt;/summary&gt; 符号没有独立一行
        /// </summary>
        public const string CLASS_SUMMARY_D = @"    /// <summary> 测试类级别（包括结构体，接口等）的 Summary，
    /// 以 class 为例 </summary>
    [EmptyPlaceholder]
    public class TestClassSummary { }";

        /// <summary>
        /// Summary 特殊符号，子标签。ChineseSummary 特性多行，代码样式不规范
        /// </summary>
        public const string CLASS_SUMMARY_E = @"    /// <summary>
    /// 测试成员的 Summary 注释 “”
    /// </summary>
    public class TestMemberSummary : MonoBehaviour
    {
        /// <summary>
        /// 成员 “” Summary 注释 ????
        /// &lt;para&gt;aaa&lt;/para&gt;
        /// <para>aaa</para>
        /// </summary>
        /// <param name=""filePath"">以 Assets 开头的相对路径即可</param>
        /// 
        [ChineseSummary(""xxxxxxx"" +
                        """" +
                        ""xxxxxxx"")]
        [Obsolete(""临时方法"")]
        
        public static void MethodA(string filePath)
        {
            // 方法体
            Debug.Log(""测试成员Summary注释"");
        }
    }";

        #endregion

        #region 测试移除功能的源代码

        public const string REMOVE_SUMMARY_CODE_A = @"    /// <summary>
    /// 测试XML注释中的特性：[ChineseSummary(""XML注释特性"")]
    /// </summary>
    // 测试单行注释中的特性：[ChineseSummary(""单行注释特性"")]
    [Obsolete]
    [ChineseSummary(""代码中的特性"")]
    public class TestRemoveSummary
    {
        // 行内注释 [ChineseSummary(""行内注释特性"")]
        [Obsolete] [ChineseSummary(""方法上的特性"")]
        public void Method() { }
    }";

        public const string REMOVE_SUMMARY_CODE_B = @"    /// <summary>
    /// 测试移除 ChineseSummary
    /// </summary>
    [Obsolete(""临时方法"")]
    [ChineseSummary(""测试"" +
                    ""移除多行的"" +
                    "" ChineseSummary"")]
    public class TestRemoveSummaryB
    {
        [Obsolete(""临时方法"")] [ChineseSummary(""测试"" +
                                           ""成员的多行的"" +
                                           "" ChineseSummary"")]
        public void Method()
        {
            Debug.Log(""测试移除多行的 ChineseSummary"");
        }
    }";

        #endregion
    }
}
