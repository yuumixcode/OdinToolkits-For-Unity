using System.Linq;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using Yuumix.OdinToolkits.ScriptDocGenerator;
using Assert = UnityEngine.Assertions.Assert;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    public class UnitTestFieldInstance
    {
        static readonly FieldInfo[] FieldInfos = typeof(TestClass).GetRuntimeFields().ToArray();

        static readonly IFieldData[] FieldDataArray =
            FieldInfos.Select(f => UnitTestAnalysisFactory.Default.CreateFieldData(f)).ToArray();

        [Test]
        public void TestStringField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.StringField));
            Debug.Log(fieldData.Signature);
            Assert.AreEqual("public string StringField;", fieldData.Signature);
        }

        [Test]
        public void TestIntField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.IntField));
            Debug.Log(fieldData.Signature);
            Assert.AreEqual("public int IntField;", fieldData.Signature);
        }

        [Test]
        public void TestFloatField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.FloatField));
            Debug.Log(fieldData.Signature);
            Assert.AreEqual("public float FloatField;", fieldData.Signature);
        }

        [Test]
        public void TestBooleanField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.BooleanField));
            Debug.Log(fieldData.Signature);
            Assert.AreEqual("public bool BooleanField;", fieldData.Signature);
        }

        [Test]
        public void TestCharField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.CharField));
            Debug.Log(fieldData.Signature);
            Assert.AreEqual("public char CharField;", fieldData.Signature);
        }

        [Test]
        public void TestByteField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.ByteField));
            Debug.Log(fieldData.Signature);
            Assert.AreEqual("public byte ByteField;", fieldData.Signature);
        }

        [Test]
        public void TestSbyteField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.SbyteField));
            Debug.Log(fieldData.Signature);
            Assert.AreEqual("public sbyte SbyteField;", fieldData.Signature);
        }

        [Test]
        public void TestShortField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.ShortField));
            Debug.Log(fieldData.Signature);
            Assert.AreEqual("public short ShortField;", fieldData.Signature);
        }

        [Test]
        public void TestUshortField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.UshortField));
            Debug.Log(fieldData.Signature);
            Assert.AreEqual("public ushort UshortField;", fieldData.Signature);
        }

        [Test]
        public void TestLongField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.LongField));
            Debug.Log(fieldData.Signature);
            Assert.AreEqual("public long LongField;", fieldData.Signature);
        }

        [Test]
        public void TestUlongField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.UlongField));
            Debug.Log(fieldData.Signature);
            Assert.AreEqual("public ulong UlongField;", fieldData.Signature);
        }

        [Test]
        public void TestUintField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.UintField));
            Debug.Log(fieldData.Signature);
            Assert.AreEqual("public uint UintField;", fieldData.Signature);
        }

        [Test]
        public void TestDoubleField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.DoubleField));
            Debug.Log(fieldData.Signature);
            Assert.AreEqual("public double DoubleField;", fieldData.Signature);
        }

        [Test]
        public void TestDecimalField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.DecimalField));
            Debug.Log(fieldData.Signature);
            Assert.AreEqual("public decimal DecimalField;", fieldData.Signature);
        }

        [Test]
        public void TestEnumField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.EnumField));
            Debug.Log(fieldData.Signature);
            Assert.AreEqual("public ScriptDocGeneratorTestEnum EnumField;", fieldData.Signature);
        }

        [Test]
        public void TestNestedEnumField()
        {
            var fieldData =
                FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.NestedEnumField));
            Debug.Log(fieldData.Signature);
            Assert.AreEqual("public UnitTestFieldInstance.TestEnum NestedEnumField;", fieldData.Signature);
        }

        #region Nested type: TestClass

        class TestClass
        {
            public bool BooleanField;
            public byte ByteField;
            public char CharField;
            public decimal DecimalField;
            public double DoubleField;
            public ScriptDocGeneratorTestEnum EnumField;
            public float FloatField;
            public int IntField;
            public long LongField;
            public TestEnum NestedEnumField;
            public sbyte SbyteField;
            public short ShortField;
            public string StringField;
            public uint UintField;
            public ulong UlongField;
            public ushort UshortField;
        }

        #endregion

        #region Nested type: TestEnum

        enum TestEnum
        {
            Value1,
            Value2
        }

        #endregion
    }
}
