using Yuumix.OdinToolkits.AdvancedTypeAnalyzer;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGen.Editor.Test.TestMethod
{
    /// <summary>
    /// 测试默认值的方法
    /// </summary>
    [Summary("测试默认值的方法")]
    public class TestMethodC
    {
        /// <summary>
        /// 带默认值的方法
        /// </summary>
        [Summary("带默认值的方法")]
        public void MethodA(int a, bool b = false, string c = "default",
            TypeCategory d = TypeCategory.Class) { }
    }
}
