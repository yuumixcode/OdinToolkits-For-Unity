using NUnit.Framework;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.AdvancedTypeAnalyzer;

#pragma warning disable CS0414 // 字段已被赋值，但它的值从未被使用

namespace Yuumix.OdinToolkits.Tests.Editor
{
    /// <summary>
    /// 测试有默认值的字段
    /// </summary>
    public class UnitTestFieldWithCustomDefaultValue
    {
        static readonly IAnalysisDataFactory TargetFactory = UnitTestAnalysisFactory.Instance;

        [Test]
        public void ConstantField_ShouldReturnCorrectValue()
        {
            var fieldInfo = typeof(TestClass).GetField("CONST_FIELD");
            var fieldData = TargetFactory.CreateFieldData(fieldInfo);
            Assert.AreEqual(100, fieldData.DefaultValue);
            Assert.IsTrue(fieldData.IsConstant);
            StringAssert.Contains("= 100", fieldData.Signature);
        }

        [Test]
        public void StaticFieldWithDefault_ShouldReturnCorrectValue()
        {
            var fieldInfo = typeof(TestClass).GetField("StaticFieldWithDefault");
            var fieldData = TargetFactory.CreateFieldData(fieldInfo);
            Assert.AreEqual(200, fieldData.DefaultValue);
            Assert.IsTrue(fieldData.IsStatic);
            Assert.IsFalse(fieldData.IsConstant);
            StringAssert.Contains("= 200", fieldData.Signature);
        }

        [Test]
        public void StaticReadOnlyFieldWithDefault_ShouldReturnCorrectValue()
        {
            var fieldInfo = typeof(TestClass).GetField("StaticReadOnlyFieldWithDefault");
            var fieldData = TargetFactory.CreateFieldData(fieldInfo);
            Assert.AreEqual(300, fieldData.DefaultValue);
            Assert.IsTrue(fieldData.IsStatic);
            Assert.IsTrue(fieldData.IsReadOnly);
            Assert.IsFalse(fieldData.IsConstant);
            StringAssert.Contains("= 300", fieldData.Signature);
        }

        [Test]
        public void ReadOnlyFieldWithDefault_ShouldReturnCorrectValue()
        {
            var fieldInfo = typeof(TestClass).GetField("ReadOnlyFieldWithDefault");
            var fieldData = TargetFactory.CreateFieldData(fieldInfo);
            StringAssert.Contains("public readonly int ReadOnlyFieldWithDefault = 500;", fieldData.Signature);
        }

        [Test]
        public void InstanceFieldNoDefaultWithInit()
        {
            var fieldInfo = typeof(TestClass).GetField("_instanceFieldNoDefault",
                BindingFlags.Instance | BindingFlags.NonPublic
            );
            Assert.IsNotNull(fieldInfo, "_instanceFieldNoDefault 字段不存在");
            var fieldData = TargetFactory.CreateFieldData(fieldInfo);
            // 如果字段在无参数构造函数中被赋值，那么也认定为默认值
            Assert.IsFalse(fieldData.IsStatic);
            Assert.IsFalse(fieldData.IsConstant);
            Debug.Log("fieldData 的默认值为：" + fieldData.DefaultValue);
            Debug.Log("fieldData 字段签名为：" + fieldData.Signature);
            Assert.IsTrue(fieldData.DefaultValue.ToString() == 999.ToString(),
                "fieldData 的默认值为：" + fieldData.DefaultValue + "，不是 999");
            StringAssert.Contains("= 999;", fieldData.Signature);
        }

        [Test]
        public void StringFieldWithDefault_ShouldReturnCorrectValue()
        {
            var fieldInfo = typeof(TestClass).GetField("StringFieldWithDefault");
            var fieldData = TargetFactory.CreateFieldData(fieldInfo);
            Assert.AreEqual("Hello", fieldData.DefaultValue);
            StringAssert.Contains("= \"Hello\"", fieldData.Signature);
        }

        [Test]
        public void BoolFieldWithDefault_ShouldReturnCorrectValue()
        {
            var fieldInfo = typeof(TestClass).GetField("BoolFieldWithDefault");
            var fieldData = TargetFactory.CreateFieldData(fieldInfo);

            Assert.AreEqual(true, fieldData.DefaultValue);
            StringAssert.Contains("= true", fieldData.Signature);
        }

        [Test]
        public void FloatFieldWithDefault_ShouldReturnCorrectValue()
        {
            var fieldInfo = typeof(TestClass).GetField("FloatFieldWithDefault");
            var fieldData = TargetFactory.CreateFieldData(fieldInfo);
            Assert.AreEqual(3.14f, fieldData.DefaultValue);
            StringAssert.Contains("= 3.14f", fieldData.Signature);
        }

        /// <summary>
        /// 测试用的类，包含各种类型的字段和默认值
        /// </summary>
        class TestClass
        {
            // 常量字段
            public const int CONST_FIELD = 100;

            // 静态字段
            public static int StaticFieldWithDefault = 200;

            // 静态只读字段
            public static readonly int StaticReadOnlyFieldWithDefault = 300;

            readonly int _instanceFieldNoDefault;

            // 只读实例字段
            public readonly int ReadOnlyFieldWithDefault = 500;
            public bool BoolFieldWithDefault = true;

            public float FloatFieldWithDefault = 3.14f;

            // 其他类型字段
            public string StringFieldWithDefault = "Hello";

            // 构造函数
            public TestClass()
            {
                // 在构造函数中初始化一些字段
                if (_instanceFieldNoDefault == 0)
                {
                    _instanceFieldNoDefault = 999;
                }
            }
        }
    }
}
