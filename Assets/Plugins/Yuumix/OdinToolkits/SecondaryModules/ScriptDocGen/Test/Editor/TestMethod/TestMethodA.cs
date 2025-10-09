using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor.Test.TestMethod
{
    /// <summary>
    /// 测试方法解析
    /// </summary>
    public class TestMethodA
    {
        /// <summary>
        /// 无参数方法，无返回值
        /// </summary>
        public void MethodA()
        {
            Debug.Log("a");
        }

        /// <summary>
        /// 有参数方法，无返回值，私有方法
        /// </summary>
        void MethodB(int a)
        {
            Debug.Log(a);
        }

        /// <summary>
        /// 有返回值方法，protected
        /// </summary>
        protected bool GetBool() => true;

        /// <summary>
        /// 静态方法
        /// </summary>
        public static string GetString() => "string";
    }
}
