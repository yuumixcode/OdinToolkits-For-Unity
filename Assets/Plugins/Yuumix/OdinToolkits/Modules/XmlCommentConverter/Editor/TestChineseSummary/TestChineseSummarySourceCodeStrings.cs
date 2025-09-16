using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    /// <summary>
    /// 用于测试 ChineseSummary 同步的不同情况的源代码字符串仓库
    /// </summary>
    public static class TestChineseSummarySourceCodeStrings
    {
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

        #endregion

        /// <summary>
        /// 成员级别（包括字段，属性，方法等）的 Summary，最推荐的代码样式，每个特性独立一行
        /// </summary>
        public const string MEMBER_SUMMARY_A = @"    /// <summary>
    /// 测试成员级别（包括字段，属性，方法等）的 Summary，以字段为例
    /// </summary>
    [EmptyPlaceholder]
    [TextArea]
    public string testMemberSummary;";
    }
}
