using System.Linq;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using Yuumix.OdinToolkits.ScriptDocGenerator;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    public class UnitTestFieldsIsSpecialType
    {
        static readonly FieldInfo[] FieldInfos = typeof(TestClass).GetRuntimeFields().ToArray();

        static readonly IFieldData[] FieldDataArray =
            FieldInfos.Select(f => UnitTestAnalysisFactory.Default.CreateFieldData(f)).ToArray();

        [Test]
        public void TestAbstractField()
        {
            var fieldData =
                FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.AbstractField));
            Debug.Log(fieldData.Signature);
            Assert.AreEqual("public UnitTestFieldsIsSpecialType.TestAbstractClass AbstractField;",
                fieldData.Signature);
        }

        [Test]
        public void TestDynamicField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.DynamicField));
            Debug.Log(fieldData.Signature);
            Assert.AreEqual("public dynamic DynamicField;", fieldData.Signature);
        }

        [Test]
        public void TestInterfaceField()
        {
            var fieldData =
                FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.InterfaceField));
            Debug.Log(fieldData.Signature);
            Assert.AreEqual("public UnitTestFieldsIsSpecialType.ITestInterface InterfaceField;",
                fieldData.Signature);
        }

        [Test]
        public void TestNullableField()
        {
            var fieldData =
                FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.NullableField));
            Debug.Log(fieldData.Signature);
            Assert.AreEqual("public int? NullableField;", fieldData.Signature);
        }

        #region Nested type: ITestInterface

        /// <summary>
        /// 测试接口
        /// </summary>
        public interface ITestInterface { }

        #endregion

        #region Nested type: TestAbstractClass

        /// <summary>
        /// 测试抽象类
        /// </summary>
        public abstract class TestAbstractClass
        {
            public abstract void AbstractMethod();
        }

        #endregion

        #region Nested type: TestClass

        public class TestClass
        {
            /// <summary>
            /// 抽象类字段
            /// </summary>
            public TestAbstractClass AbstractField;

            /// <summary>
            /// 动态字段
            /// </summary>
            public dynamic DynamicField;

            /// <summary>
            /// 接口字段
            /// </summary>
            public ITestInterface InterfaceField;

            /// <summary>
            /// 可空字段
            /// </summary>
            public int? NullableField = null;
        }

        #endregion
    }
}
