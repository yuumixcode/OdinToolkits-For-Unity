namespace Yuumix.OdinToolkits.Modules.Editor
{
    /// <summary>
    /// 测试参数有默认值的构造方法
    /// </summary>
    public class TestConstructorC
    {
        public int A;
        public int B;

        public TestConstructorC(int a, int b = 10)
        {
            A = a;
            B = b;
        }
    }
}
