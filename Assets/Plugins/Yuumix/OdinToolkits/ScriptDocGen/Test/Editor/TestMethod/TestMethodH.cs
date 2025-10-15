namespace Yuumix.OdinToolkits.ScriptDocGen.Editor.Test.TestMethod
{
    /// <summary>
    /// 测试运算符重载方法
    /// </summary>
    public class TestMethodH
    {
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType();
        }

        public override int GetHashCode() => 0;

        public static TestMethodH operator ==(TestMethodH a, TestMethodH b) =>
            new TestMethodH();

        public static TestMethodH operator !=(TestMethodH a, TestMethodH b) =>
            new TestMethodH();

        public static TestMethodH operator +(TestMethodH a, TestMethodH b) =>
            new TestMethodH();

        public static TestMethodH operator -(TestMethodH a, TestMethodH b) =>
            new TestMethodH();

        public static TestMethodH operator *(TestMethodH a, TestMethodH b) =>
            new TestMethodH();

        public static TestMethodH operator /(TestMethodH a, TestMethodH b) =>
            new TestMethodH();
    }
}
