using System;

#pragma warning disable CS0414 // 字段已被赋值，但它的值从未被使用

namespace Yuumix.OdinToolkits.Modules.APIBrowser.Tests
{
    public sealed class CustomClassTest : UnityEngine.Object
    {
        public int Field;
    }

    public sealed class CustomGenericClassTest<T1, T2> where T1 : class
    {
        public int GenericField;
    }

    public abstract class FieldClassTestBase : UnityEngine.Object
    {
        // 公共静态整型字段
        public static int PublicStaticIntField = 10;

        // 公共整型字段
        public int PublicIntField = 20;

        [Obsolete]
        // 私有字符串字段
        string _privateStringField = "Private String";

        // 受保护的浮点型字段
        protected float ProtectedFloatField = 3.14f;

        // 内部布尔型字段
        internal bool InternalBoolField = true;

        // 自定义类型
        public CustomClassTest CustomField;

        // 自定义泛型
        public CustomGenericClassTest<UnityEngine.Object, int> CustomGenericField;
        
        
    }
}