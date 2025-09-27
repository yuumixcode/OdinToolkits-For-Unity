using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor.Test.TestMethod
{
    /// <summary>
    /// 泛型方法测试类
    /// </summary>
    public class TestMethodB<T>
    {
        public static void MethodA(T t)
        {
            Debug.Log(typeof(T));
        }

        public static void MethodB<TA>(TA t)
        {
            Debug.Log(typeof(TA));
        }
    }
}
