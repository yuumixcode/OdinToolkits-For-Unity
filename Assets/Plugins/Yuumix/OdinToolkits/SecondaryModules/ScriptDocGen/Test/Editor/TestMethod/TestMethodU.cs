using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor.Test.TestMethod
{
    /// <summary>
    /// 测试扩展方法
    /// </summary>
    public static class TestMethodU
    {
        public static void Test(this MyClassA a)
        {
            Debug.Log(a);
        }

        public class MyClassA { }
    }
}
