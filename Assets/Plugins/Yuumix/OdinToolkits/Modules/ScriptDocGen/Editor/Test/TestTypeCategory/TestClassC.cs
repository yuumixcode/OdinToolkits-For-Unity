namespace Yuumix.OdinToolkits.Modules.Editor
{
    public interface ITestInterfaceA { }

    /// <summary>
    /// 泛型类
    /// </summary>
    public class TestClassC<T> where T : ITestInterfaceA
    {
        public T Owner;
    }
}
