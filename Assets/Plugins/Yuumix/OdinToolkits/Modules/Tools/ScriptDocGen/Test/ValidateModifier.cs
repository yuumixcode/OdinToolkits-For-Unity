using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Test
{
    // ===
    // public 访问修饰符省略验证，所有都可以使用
    // ===
    public class ValidateModifier :
#if UNITY_EDITOR
        SerializedMonoBehaviour
#else
        MonoBehaviour
#endif
    {
        #region class

        #region private

        // ===
        // private 只能对嵌套类使用，如果非嵌套类，省略访问修饰符的时候，默认为 internal
        // ===

        // static 
        static class StaticClass { }

        // abstract 
        abstract class AbstractClass { }

        // sealed
        sealed class SealedClass { }

        // partial
        partial class PartialClass { }

        partial class PartialClass { }

        #endregion

        #region protected

        // protected static
        protected static class ProtectedStaticClass { }

        // protected abstract
        protected abstract class ProtectedAbstractClass { }

        // protected sealed
        protected sealed class ProtectedSealedClass { }

        // protected partial
        protected partial class ProtectedPartialClass { }

        protected partial class ProtectedPartialClass { }

        // protected internal
        protected internal class ProtectedInternalClass { }

        #endregion

        #region internal

        internal class InternalClass { }

        // internal static
        internal static class InternalStaticClass { }

        // internal abstract
        internal abstract class InternalAbstractClass { }

        // internal sealed
        internal sealed class InternalSealedClass { }

        // internal partial
        internal partial class InternalPartialClass { }

        internal partial class InternalPartialClass { }

        #endregion

        #endregion

        #region struct

        // ===
        // struct 类型修饰符验证
        // ===

        #region private

        // ===
        // private 只能对嵌套结构体使用
        // ===

        // readonly
        readonly struct PrivateReadonlyStruct { }

        // ref
        ref struct PrivateRefStruct { }

        // partial
        partial struct PrivatePartialStruct { }

        partial struct PrivatePartialStruct { }

        #endregion

        #region protected

        // protected readonly
        protected readonly struct ProtectedReadonlyStruct { }

        // protected ref
        protected ref struct ProtectedRefStruct { }

        // protected partial
        protected partial struct ProtectedPartialStruct { }

        protected partial struct ProtectedPartialStruct { }

        // protected internal
        protected internal struct ProtectedInternalStruct { }

        #endregion

        #region internal

        internal struct InternalStruct { }

        // internal readonly
        internal readonly struct InternalReadonlyStruct { }

        // internal ref
        internal ref struct InternalRefStruct { }

        // internal partial
        internal partial struct InternalPartialStruct { }

        internal partial struct InternalPartialStruct { }

        #endregion

        #endregion

        #region interface

        // ===
        // interface 接口修饰符验证
        // ===

        #region private

        // ===
        // private 只能对嵌套接口使用
        // ===

        // partial
        partial interface IPrivatePartialInterface { }

        partial interface IPrivatePartialInterface { }

        #endregion

        #region protected

        // protected partial
        protected partial interface IProtectedPartialInterface { }

        protected partial interface IProtectedPartialInterface { }

        // protected internal
        protected internal interface IProtectedInternalInterface { }

        #endregion

        #region internal

        internal interface IInternalInterface { }

        // internal partial
        internal partial interface IInternalPartialInterface { }

        internal partial interface IInternalPartialInterface { }

        #endregion

        #endregion

        #region enum

        // ===
        // enum 枚举修饰符验证
        // ===

        #region private

        // ===
        // private 只能对嵌套枚举使用
        // ===

        // 基本枚举
        enum PrivateEnum
        {
            Value1,
            Value2
        }

        #endregion

        #region protected

        // protected 枚举
        protected enum ProtectedEnum
        {
            Value1,
            Value2
        }

        // protected internal
        protected internal enum ProtectedInternalEnum
        {
            Value1,
            Value2
        }

        #endregion

        #region internal

        internal enum InternalEnum
        {
            Value1,
            Value2
        }

        #endregion

        #endregion

        #region delegate

        // ===
        // delegate 委托修饰符验证
        // ===

        #region private

        // ===
        // private 只能对嵌套委托使用
        // ===

        // 基本委托
        delegate void PrivateDelegate();

        // 带返回值的委托
        delegate int PrivateIntDelegate();

        #endregion

        #region protected

        // protected 委托
        protected delegate void ProtectedDelegate();

        // protected internal
        protected internal delegate void ProtectedInternalDelegate();

        #endregion

        #region internal

        internal delegate void InternalDelegate();

        // 带参数的委托
        internal delegate void InternalParameterDelegate(int param);

        #endregion

        #endregion

        #region 组合测试

        // ===
        // 混合修饰符测试
        // ===

        // 类：public + partial
        public partial class PublicPartialClass { }

        public partial class PublicPartialClass { }

        // 结构体：internal + readonly + partial
        internal readonly partial struct ReadonlyPartialStruct { }

        internal readonly partial struct ReadonlyPartialStruct { }

        // 接口：public + partial
        public partial interface IPublicPartialInterface { }

        public partial interface IPublicPartialInterface { }

        // 委托：public + 泛型
        public delegate T GenericDelegate<T>(T input);

        #endregion

        #region 无效组合测试（注释掉）

        // 以下为无效组合（编译错误），仅作为参考
        /*
        // 无效：类不能同时使用 static 和 abstract
        static abstract class InvalidStaticAbstractClass { }

        // 无效：结构体不能同时使用 readonly 和 ref
        readonly ref struct InvalidReadonlyRefStruct { }

        // 无效：枚举不能使用 partial
        partial enum InvalidPartialEnum { }

        // 无效：委托不能使用 static
        static delegate void InvalidStaticDelegate();

        // 无效：接口不能使用 sealed
        sealed interface IInvalidSealedInterface { }
        */

        #endregion
    }
}
