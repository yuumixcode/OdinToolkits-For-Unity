using System.Threading.Tasks;

namespace Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor.Test.TestMethod
{
    /// <summary>
    /// 测试异步方法
    /// </summary>
    public class TestMethodD
    {
        public static async Task AsyncMethod()
        {
            await Task.Delay(1);
        }

        public async Task<int> AsyncMethodWithReturnValue()
        {
            await Task.Delay(1);
            return 42;
        }
    }
}
