using System;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGen.Editor.Test.TestChineseSummaryAttribute
{
    /// <summary>
    /// 测试移除 ChineseSummary
    /// </summary>
    [Obsolete("临时方法")]
    [Summary("测试" +
                    "移除多行的" +
                    " ChineseSummary")]
    public class TestRemoveSummaryB
    {
        [Obsolete("临时方法")]
        [Summary("测试" +
                        "成员的多行的" +
                        " ChineseSummary")]
        public void Method()
        {
            Debug.Log("测试移除多行的 ChineseSummary");
        }
    }
}
