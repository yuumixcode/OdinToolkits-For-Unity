namespace Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor.Test.TestProperty
{
    /// <summary>
    /// 测试解析属性
    /// </summary>
    public class TestPropertyA
    {
        public int A { get; set; }

        public int B { get; private set; }

        public int C { get; protected set; }

        public int D { get; internal set; }
    }
}
