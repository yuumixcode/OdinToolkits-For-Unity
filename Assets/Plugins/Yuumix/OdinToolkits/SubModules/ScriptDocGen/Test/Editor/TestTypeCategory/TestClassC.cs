namespace Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor.Test.TestTypeCategory
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
