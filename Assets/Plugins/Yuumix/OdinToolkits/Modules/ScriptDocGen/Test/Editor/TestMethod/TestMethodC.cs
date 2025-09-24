using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor.Test.TestMethod
{
    /// <summary>
    /// 测试默认值的方法
    /// </summary>
    [ChineseSummary("测试默认值的方法")]
    public class TestMethodC
    {
        /// <summary>
        /// 带默认值的方法
        /// </summary>
        [ChineseSummary("带默认值的方法")]
        public void MethodA(int a, bool b = false, string c = "default",
            TypeCategory d = TypeCategory.Class) { }
    }
}
