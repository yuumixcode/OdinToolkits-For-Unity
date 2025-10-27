using NUnit.Framework;
using UnityEngine;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.Editor;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    public class UnitTestAddSummaryAttribute
    {
        /// <summary>
        /// 测试 Type 级别的 Summary 注释脚本，有其他特性，且 Summary 注释是多行，中间有空格，且有换行符，有命名空间，且有 using 语句
        /// </summary>
        const string TYPE_SUMMARY_A = @"using System;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    /// <summary>
    /// 测试类级别（包括结构体，接口等）的 Summary，
    /// 以 class 为例
    /// </summary>
    [Serializable]
    public class TestClassSummary { }
}
";

        /// <summary>
        /// 测试 Type 级别的 Summary 注释脚本
        /// 主要测试点：子标签以及其他类型的标签、特性没有换行
        /// </summary>
        const string TYPE_SUMMARY_B = @"using System;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    /// <summary>
    /// 成员 “” Summary 注释 ????
    /// &lt;para&gt;aaa&lt;/para&gt;
    /// <para>aaa</para>
    /// </summary>
    /// <remarks>AAAAA</remarks>>
    [Obsolete(""临时方法"")] public struct TestStructSummary { }
}
";

        /// <summary>
        /// 测试方法成员的 Summary 注释脚本
        /// 主要测试点：有 // 简单注释、有其他的 /// 类型注释、有特性、特性不换行、方法内部有 // 注释
        /// </summary>
        const string METHOD_SUMMARY_A = @"using System;
using UnityEngine;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    public class TestMemberSummary : MonoBehaviour
    {
        // 两个 // 的简单注释
        /// <summary>
        /// AAA
        /// </summary>
        /// <param name=""filePath"">以 Assets 开头的相对路径即可</param>
        [Obsolete(""临时方法"")] public static void MethodA(string filePath)
        {
            // 方法体
            Debug.Log(""测试成员Summary注释"");
        }
    }
}
";

        /// <summary>
        /// 测试复杂场景
        /// 主要测试点：本身包含 SummaryAttribute、SummaryAttribute 单行以及多行、还存在其他特性、单行 Summary 被夹在中间
        /// </summary>
        const string METHOD_SUMMARY_B = @"using System;
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
        /// <summary>
        /// BBB
        /// </summary>
        [Obsolete(""临时方法"")] [Summary(""AAA"")] public void Method()
        {
            Debug.Log(""测试移除多行的 ChineseSummary"");
        }
    }
}
";

        /// <summary>
        /// 测试 Type 级别的 Summary 注释脚本，有其他特性，且 Summary 注释是多行，中间有空格，且有换行符，有命名空间，且有 using 语句
        /// </summary>
        [Test]
        public void TestTypeSummaryA()
        {
            var xmlSyncTool = new XMLSummaryProcessor(TYPE_SUMMARY_A);
            Assert.IsNotNull(xmlSyncTool.sourceScriptLines);
            xmlSyncTool.ParseSourceScript();
            Debug.Log("拆分后的头部脚本部分为：\n" + xmlSyncTool.HeaderScript);
            var firstXmlCode = xmlSyncTool.xmlCodeParts[0];
            Debug.Log("拆分后的代码部分的第一个元素的 xml 为：\n" + firstXmlCode.xml);
            Debug.Log("拆分后的代码部分的第一个元素的 code 为：\n" + firstXmlCode.code);
            Debug.Log("拆分后的代码部分的第一个元素的 xml 提取的 Summary 为：\n" + firstXmlCode.SummaryValue);
            Debug.Log("首个 XmlCodePart 的 SummaryAttribute 的代码文本为：\n" + firstXmlCode.SummaryAttributeText);
            Debug.Log("同步模式，同步 Summary 之后的结果为：\n" +
                      xmlSyncTool.GetProcessedSourceScript(XMLSummaryProcessor.ProcessMode.SyncSummary));
            Debug.Log("替换模式，替换 Summary 之后的结果为：\n" +
                      xmlSyncTool.GetProcessedSourceScript(XMLSummaryProcessor.ProcessMode.ReplaceSummary));
            Assert.AreEqual(@"using Yuumix.OdinToolkits.Core;
using System;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    /// <summary>
    /// 测试类级别（包括结构体，接口等）的 Summary，
    /// 以 class 为例
    /// </summary>
    [Summary(""测试类级别（包括结构体，接口等）的 Summary， 以 class 为例"")]
    [Serializable]
    public class TestClassSummary { }
}
", xmlSyncTool.GetProcessedSourceScript(XMLSummaryProcessor.ProcessMode.SyncSummary));
            Assert.AreEqual(@"using Yuumix.OdinToolkits.Core;
using System;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    [Summary(""测试类级别（包括结构体，接口等）的 Summary， 以 class 为例"")]
    [Serializable]
    public class TestClassSummary { }
}
", xmlSyncTool.GetProcessedSourceScript(XMLSummaryProcessor.ProcessMode.ReplaceSummary));
        }

        /// <summary>
        /// 测试 Type 级别的 Summary 注释脚本
        /// 主要测试点：子标签以及其他类型的标签、特性没有换行
        /// </summary>
        [Test]
        public void TestTypeSummaryB()
        {
            var xmlSyncTool = new XMLSummaryProcessor(TYPE_SUMMARY_B);
            Assert.IsNotNull(xmlSyncTool.sourceScriptLines);
            xmlSyncTool.ParseSourceScript();
            Debug.Log("同步模式，同步 Summary 之后的结果为：\n" +
                      xmlSyncTool.GetProcessedSourceScript(XMLSummaryProcessor.ProcessMode.SyncSummary));
            Debug.Log("替换模式，替换 Summary 之后的结果为：\n" +
                      xmlSyncTool.GetProcessedSourceScript(XMLSummaryProcessor.ProcessMode.ReplaceSummary));
            Assert.AreEqual(@"using Yuumix.OdinToolkits.Core;
using System;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    /// <summary>
    /// 成员 “” Summary 注释 ????
    /// &lt;para&gt;aaa&lt;/para&gt;
    /// <para>aaa</para>
    /// </summary>
    /// <remarks>AAAAA</remarks>>
    [Summary(""成员 “” Summary 注释 ???? &lt;para&gt;aaa&lt;/para&gt; aaa"")]
    [Obsolete(""临时方法"")] public struct TestStructSummary { }
}
", xmlSyncTool.GetProcessedSourceScript(XMLSummaryProcessor.ProcessMode.SyncSummary));
            Assert.AreEqual(@"using Yuumix.OdinToolkits.Core;
using System;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    /// <remarks>AAAAA</remarks>>
    [Summary(""成员 “” Summary 注释 ???? &lt;para&gt;aaa&lt;/para&gt; aaa"")]
    [Obsolete(""临时方法"")] public struct TestStructSummary { }
}
", xmlSyncTool.GetProcessedSourceScript(XMLSummaryProcessor.ProcessMode.ReplaceSummary));
        }

        /// <summary>
        /// 测试方法成员的 Summary 注释脚本
        /// 主要测试点：有 // 简单注释、有其他的 /// 类型注释、有特性、特性不换行、方法内部有 // 注释
        /// </summary>
        [Test]
        public void TestMethodSummaryA()
        {
            var xmlSyncTool = new XMLSummaryProcessor(METHOD_SUMMARY_A);
            Assert.IsNotNull(xmlSyncTool.sourceScriptLines);
            xmlSyncTool.ParseSourceScript();
            Debug.Log("同步模式，同步 Summary 之后的结果为：\n" +
                      xmlSyncTool.GetProcessedSourceScript(XMLSummaryProcessor.ProcessMode.SyncSummary));
            Debug.Log("替换模式，替换 Summary 之后的结果为：\n" +
                      xmlSyncTool.GetProcessedSourceScript(XMLSummaryProcessor.ProcessMode.ReplaceSummary));
            Assert.AreEqual(@"using Yuumix.OdinToolkits.Core;
using System;
using UnityEngine;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    public class TestMemberSummary : MonoBehaviour
    {
        // 两个 // 的简单注释
        /// <summary>
        /// AAA
        /// </summary>
        /// <param name=""filePath"">以 Assets 开头的相对路径即可</param>
        [Summary(""AAA"")]
        [Obsolete(""临时方法"")] public static void MethodA(string filePath)
        {
            // 方法体
            Debug.Log(""测试成员Summary注释"");
        }
    }
}
", xmlSyncTool.GetProcessedSourceScript(XMLSummaryProcessor.ProcessMode.SyncSummary));
            Assert.AreEqual(@"using Yuumix.OdinToolkits.Core;
using System;
using UnityEngine;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    public class TestMemberSummary : MonoBehaviour
    {
        // 两个 // 的简单注释
        /// <param name=""filePath"">以 Assets 开头的相对路径即可</param>
        [Summary(""AAA"")]
        [Obsolete(""临时方法"")] public static void MethodA(string filePath)
        {
            // 方法体
            Debug.Log(""测试成员Summary注释"");
        }
    }
}
", xmlSyncTool.GetProcessedSourceScript(XMLSummaryProcessor.ProcessMode.ReplaceSummary));
        }

        /// <summary>
        /// 测试复杂场景
        /// 主要测试点：本身包含 SummaryAttribute、SummaryAttribute 单行以及多行、还存在其他特性、单行 Summary 被夹在中间
        /// </summary>
        [Test]
        public void TestMethodSummaryB()
        {
            var xmlSyncTool = new XMLSummaryProcessor(METHOD_SUMMARY_B);
            Assert.IsNotNull(xmlSyncTool.sourceScriptLines);
            xmlSyncTool.ParseSourceScript();
            Debug.Log("同步模式，同步 Summary 之后的结果为：\n" +
                      xmlSyncTool.GetProcessedSourceScript(XMLSummaryProcessor.ProcessMode.SyncSummary));
            Debug.Log("替换模式，替换 Summary 之后的结果为：\n" +
                      xmlSyncTool.GetProcessedSourceScript(XMLSummaryProcessor.ProcessMode.ReplaceSummary));
            Assert.AreEqual(@"using System;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    /// <summary>
    /// 测试移除 ChineseSummary
    /// </summary>
    [Summary(""测试移除 ChineseSummary"")]
    [Obsolete(""临时方法"")]
    public class TestRemoveSummaryB
    {
        /// <summary>
        /// BBB
        /// </summary>
        [Summary(""BBB"")]
        [Obsolete(""临时方法"")] public void Method()
        {
            Debug.Log(""测试移除多行的 ChineseSummary"");
        }
    }
}
", xmlSyncTool.GetProcessedSourceScript(XMLSummaryProcessor.ProcessMode.SyncSummary));
            Assert.AreEqual(@"using System;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    [Summary(""测试移除 ChineseSummary"")]
    [Obsolete(""临时方法"")]
    public class TestRemoveSummaryB
    {
        [Summary(""BBB"")]
        [Obsolete(""临时方法"")] public void Method()
        {
            Debug.Log(""测试移除多行的 ChineseSummary"");
        }
    }
}
", xmlSyncTool.GetProcessedSourceScript(XMLSummaryProcessor.ProcessMode.ReplaceSummary));
        }
    }
}
