namespace Yuumix.OdinToolkits.ScriptDocGen.Editor.Test.TestMethod
{
    /// <summary>
    /// 测试转换运算符
    /// </summary>
    public class TestMethodHA
    {
        public static implicit operator int(TestMethodHA obj) => 0;

        public static explicit operator string(TestMethodHA obj) => "";
    }
}
