namespace Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor.Test.TestMethod
{
    public abstract class TestMethodFA
    {
        public virtual void VirtualMethod() { }
        public abstract void AbstractMethod();
    }

    /// <summary>
    /// 测试虚方法，抽象方法，override 方法
    /// </summary>
    public class TestMethodF : TestMethodFA
    {
        public virtual void Method() { }
        public override void AbstractMethod() { }
        public override void VirtualMethod() { }
    }
}
