using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Yuumix.OdinToolkits.ScriptDocGenerator;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    /// <summary>
    /// 测试字段不同的访问修饰符
    /// </summary>
    public class UnitTestFieldsWithDifferentAccessModifier
    {
        static readonly IAnalysisDataFactory TargetFactory = UnitTestAnalysisFactory.Default;

        static readonly FieldInfo[] TestFields = typeof(TestClass).GetRuntimeFields()
            .ToArray();

        static readonly IFieldData[] TestFieldData = TestFields.Select(f => TargetFactory.CreateFieldData(f))
            .ToArray();

        static readonly Dictionary<string, string> FieldExpectedSignatureMaps = new Dictionary<string, string>
        {
            { "_privateField", "private int _privateField;" },
            { "InternalField", "internal int InternalField;" },
            { "PrivateProtectedField", "private protected int PrivateProtectedField;" },
            { "ProtectedField", "protected int ProtectedField;" },
            { "ProtectedInternalField", "protected internal int ProtectedInternalField;" },
            { "PublicField", "public int PublicField;" }
        };

        [Test]
        public void TestPrivateField()
        {
            var fieldData = TestFieldData.First(f => ((MemberData)f).Name == "_privateField");
            Assert.AreEqual(FieldExpectedSignatureMaps["_privateField"], fieldData.Signature);
        }

        [Test]
        public void TestInternalField()
        {
            var fieldData = TestFieldData.First(f => ((MemberData)f).Name == "InternalField");
            Assert.AreEqual(FieldExpectedSignatureMaps["InternalField"], fieldData.Signature);
        }

        [Test]
        public void TestPrivateProtectedField()
        {
            var fieldData = TestFieldData.First(f => ((MemberData)f).Name == "PrivateProtectedField");
            Assert.AreEqual(FieldExpectedSignatureMaps["PrivateProtectedField"], fieldData.Signature);
        }

        [Test]
        public void TestProtectedField()
        {
            var fieldData = TestFieldData.First(f => ((MemberData)f).Name == "ProtectedField");
            Assert.AreEqual(FieldExpectedSignatureMaps["ProtectedField"], fieldData.Signature);
        }

        [Test]
        public void TestProtectedInternalField()
        {
            var fieldData = TestFieldData.First(f => ((MemberData)f).Name == "ProtectedInternalField");
            Assert.AreEqual(FieldExpectedSignatureMaps["ProtectedInternalField"], fieldData.Signature);
        }

        [Test]
        public void TestPublicField()
        {
            var fieldData = TestFieldData.First(f => ((MemberData)f).Name == "PublicField");
            Assert.AreEqual(FieldExpectedSignatureMaps["PublicField"], fieldData.Signature);
        }

        #region Nested type: TestClass

        class TestClass
        {
            /// <summary>
            /// 私有字段
            /// </summary>
            int _privateField;

            /// <summary>
            /// 内部字段
            /// </summary>
            internal int InternalField;

            /// <summary>
            /// 私有受保护字段
            /// </summary>
            private protected int PrivateProtectedField;

            /// <summary>
            /// 受保护字段
            /// </summary>
            protected int ProtectedField;

            /// <summary>
            /// 受保护内部字段
            /// </summary>
            protected internal int ProtectedInternalField;

            /// <summary>
            /// 公共字段
            /// </summary>
            public int PublicField;
        }

        #endregion
    }
}
