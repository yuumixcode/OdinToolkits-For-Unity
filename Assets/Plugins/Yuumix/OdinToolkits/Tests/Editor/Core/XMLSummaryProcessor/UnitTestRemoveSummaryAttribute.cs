using NUnit.Framework;
using UnityEngine;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.Editor;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    public class UnitTestRemoveSummaryAttribute
    {
        /// <summary>
        /// 测试删除所有的 Summary 特性
        /// 主要测试点：多行特性、单行但不换行的特性、有xml注释干扰的情况
        /// </summary>
        const string CODE_A = @"using System;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    /// <summary>
    /// 测试移除 ChineseSummary
    /// </summary>
    [Obsolete(""临时方法"")]
    [Summary(""测试"" +
             ""移除多行的"" +
             "" ChineseSummary"")]
    public class TestRemoveSummaryB
    {
        [Obsolete(""临时方法"")] [Summary(""AAA"")] public void Method()
        {
            Debug.Log(""测试移除多行的 ChineseSummary"");
        }
    }
}
";

        /// <summary>
        /// 测试删除所有的 Summary 特性
        /// 主要测试点：多行特性、单行但不换行的特性、有xml注释干扰的情况
        /// </summary>
        [Test]
        public void TestRemoveSummaryAttribute()
        {
            var xmlSyncTool = new XMLSummaryProcessor(CODE_A);
            Assert.IsNotNull(xmlSyncTool.sourceScriptLines);
            xmlSyncTool.ParseSourceScript();
            Debug.Log("删除处理模式，删除所有 Summary 之后的结果为：\n" +
                      xmlSyncTool.GetProcessedSourceScript(XMLSummaryProcessor.ProcessMode.RemoveSummary));
            Assert.AreEqual(@"using System;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    /// <summary>
    /// 测试移除 ChineseSummary
    /// </summary>
    [Obsolete(""临时方法"")]
    public class TestRemoveSummaryB
    {
        [Obsolete(""临时方法"")] public void Method()
        {
            Debug.Log(""测试移除多行的 ChineseSummary"");
        }
    }
}
", xmlSyncTool.GetProcessedSourceScript(XMLSummaryProcessor.ProcessMode.RemoveSummary));
        }
    }
}
