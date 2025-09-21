using System;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    /// <summary>
    /// 测试XML注释中的特性：[ChineseSummary("XML注释特性")]
    /// </summary>
    // 测试单行注释中的特性：[ChineseSummary("单行注释特性")]
    [Obsolete]
    [ChineseSummary("代码中的特性")]
    public class TestRemoveSummary
    {
        // 行内注释 [ChineseSummary("行内注释特性")]
        [Obsolete]
        [ChineseSummary("方法上的特性")]
        public void Method() { }
    }
}
