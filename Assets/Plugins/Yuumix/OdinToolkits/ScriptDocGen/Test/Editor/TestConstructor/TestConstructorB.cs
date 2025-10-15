using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGen.Editor.Test.TestConstructor
{
    public class TestConstructorBA
    {
        public int A;

        protected TestConstructorBA(int a) => A = a;
    }

    /// <summary>
    /// 测试继承基类构造函数
    /// </summary>
    public class TestConstructorB : TestConstructorBA
    {
        public int B;

        [Summary("继承了基类构造方法")]
        public TestConstructorB(int a, int b) : base(a) => B = b;
    }
}
