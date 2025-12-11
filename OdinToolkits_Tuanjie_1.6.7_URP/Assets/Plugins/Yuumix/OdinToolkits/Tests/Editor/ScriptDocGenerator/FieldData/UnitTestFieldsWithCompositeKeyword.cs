using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Yuumix.OdinToolkits.ScriptDocGenerator;

// ReSharper disable UnusedMember.Local

namespace Yuumix.OdinToolkits.Tests.Editor
{
    /// <summary>
    /// 测试不同复合关键字的字段
    /// </summary>
    public class UnitTestFieldsWithCompositeKeyword
    {
        static readonly IAnalysisDataFactory TargetFactory = UnitTestAnalysisFactory.Default;

        static readonly FieldInfo[] TestFields = typeof(TestClass).GetRuntimeFields().ToArray();

        static readonly IFieldData[] TestFieldData =
            TestFields.Select(f => TargetFactory.CreateFieldData(f)).ToArray();

        static readonly Dictionary<string, string> FieldExpectedSignatureMaps = new Dictionary<string, string>
        {
            { "CONST_FIELD", "public const bool CONST_FIELD = true;" },
            { "StaticReadOnlyField", "public static readonly bool StaticReadOnlyField;" },
            { "StaticField", "public static bool StaticField;" },
            { "ReadOnlyField", "public readonly bool ReadOnlyField;" }
        };

        [Test]
        public void TestConstField()
        {
            var fieldData = TestFieldData.First(f => ((MemberData)f).Name == "CONST_FIELD");
            Assert.AreEqual(FieldExpectedSignatureMaps["CONST_FIELD"], fieldData.Signature);
        }

        [Test]
        public void TestStaticReadOnlyField()
        {
            var fieldData = TestFieldData.First(f => ((MemberData)f).Name == "StaticReadOnlyField");
            Assert.AreEqual(FieldExpectedSignatureMaps["StaticReadOnlyField"], fieldData.Signature);
        }

        [Test]
        public void TestStaticField()
        {
            var fieldData = TestFieldData.First(f => ((MemberData)f).Name == "StaticField");
            Assert.AreEqual(FieldExpectedSignatureMaps["StaticField"], fieldData.Signature);
        }

        [Test]
        public void TestReadOnlyField()
        {
            var fieldData = TestFieldData.First(f => ((MemberData)f).Name == "ReadOnlyField");
            Assert.AreEqual(FieldExpectedSignatureMaps["ReadOnlyField"], fieldData.Signature);
        }

        #region Nested type: TestClass

        class TestClass
        {
            /// <summary>
            /// 常量字段
            /// </summary>
            public const bool CONST_FIELD = true;

            /// <summary>
            /// 静态只读字段
            /// </summary>
            public static readonly bool StaticReadOnlyField;

            /// <summary>
            /// 静态字段
            /// </summary>
            public static bool StaticField;

            /// <summary>
            /// 只读字段
            /// </summary>
            public readonly bool ReadOnlyField;
        }

        #endregion
    }
}
