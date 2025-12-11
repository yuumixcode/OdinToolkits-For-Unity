using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Yuumix.OdinToolkits.ScriptDocGenerator;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    /// <summary>
    /// 测试常量字段的不同类型
    /// </summary>
    public class UnitTestFieldsIsConstantWithDefaultValue
    {
        static readonly IAnalysisDataFactory TargetFactory = UnitTestAnalysisFactory.Default;

        static readonly FieldInfo[] TestFields = typeof(TestClass).GetRuntimeFields().ToArray();

        static readonly IFieldData[] TestFieldData =
            TestFields.Select(f => TargetFactory.CreateFieldData(f)).ToArray();

        static readonly Dictionary<string, string> FieldExpectedSignatureMaps = new Dictionary<string, string>
        {
            {
                nameof(TestClass.STRING_CONST_FIELD),
                "public const string " + nameof(TestClass.STRING_CONST_FIELD) + " = \"Hello, World!\";"
            },
            {
                nameof(TestClass.INT_CONST_FIELD),
                "public const int " + nameof(TestClass.INT_CONST_FIELD) + " = 2147483647;"
            },
            {
                nameof(TestClass.FLOAT_CONST_FIELD),
                "public const float " + nameof(TestClass.FLOAT_CONST_FIELD) + " = 3.14159f;"
            },
            {
                nameof(TestClass.BOOLEAN_CONST_FIELD),
                "public const bool " + nameof(TestClass.BOOLEAN_CONST_FIELD) + " = true;"
            },
            // ---
            {
                nameof(TestClass.CHAR_CONST_FIELD),
                "public const char " + nameof(TestClass.CHAR_CONST_FIELD) + " = 'A';"
            },
            {
                nameof(TestClass.BYTE_CONST_FIELD),
                "public const byte " + nameof(TestClass.BYTE_CONST_FIELD) + " = 255;"
            },
            {
                nameof(TestClass.SBYTE_CONST_FIELD),
                "public const sbyte " + nameof(TestClass.SBYTE_CONST_FIELD) + " = -128;"
            },
            {
                nameof(TestClass.SHORT_CONST_FIELD),
                "public const short " + nameof(TestClass.SHORT_CONST_FIELD) + " = 32767;"
            },
            {
                nameof(TestClass.USHORT_CONST_FIELD),
                "public const ushort " + nameof(TestClass.USHORT_CONST_FIELD) + " = 65535;"
            },
            {
                nameof(TestClass.LONG_CONST_FIELD),
                "public const long " + nameof(TestClass.LONG_CONST_FIELD) + " = 9223372036854775807L;"
            },
            {
                nameof(TestClass.ULONG_CONST_FIELD),
                "public const ulong " + nameof(TestClass.ULONG_CONST_FIELD) + " = 18446744073709551615ul;"
            },
            {
                nameof(TestClass.UINT_CONST_FIELD),
                "public const uint " + nameof(TestClass.UINT_CONST_FIELD) + " = 4294967295u;"
            },
            // --- 需要特殊处理
            {
                nameof(TestClass.DOUBLE_CONST_FIELD),
                "public const double " + nameof(TestClass.DOUBLE_CONST_FIELD) + " = 2.71828182845904d;"
            },
            {
                nameof(TestClass.DECIMAL_CONST_FIELD),
                "public const decimal " + nameof(TestClass.DECIMAL_CONST_FIELD) + " = 123.456m;"
            },
            {
                nameof(TestClass.ENUM_CONST_FIELD),
                "public const " + nameof(ScriptDocGeneratorTestEnum) + " " +
                nameof(TestClass.ENUM_CONST_FIELD) + " = " + nameof(ScriptDocGeneratorTestEnum) + ".Value1;"
            },
            {
                nameof(TestClass.NESTED_ENUM_CONST_FIELD),
                "public const " + nameof(UnitTestFieldsIsConstantWithDefaultValue) + "." + nameof(TestEnum) +
                " " + nameof(TestClass.NESTED_ENUM_CONST_FIELD) + " = " + nameof(TestEnum) + ".Value3;"
            }
        };

        #region 四个基础类型的常量字段的测试

        [Test]
        public void TestStringConstField()
        {
            const string fieldName = nameof(TestClass.STRING_CONST_FIELD);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        [Test]
        public void TestIntConstField()
        {
            const string fieldName = nameof(TestClass.INT_CONST_FIELD);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        [Test]
        public void TestFloatConstField()
        {
            const string fieldName = nameof(TestClass.FLOAT_CONST_FIELD);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        [Test]
        public void TestBooleanConstField()
        {
            const string fieldName = nameof(TestClass.BOOLEAN_CONST_FIELD);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        #endregion

        #region 其他完美支持的常量字段的测试

        /// <summary>
        /// 测试字符常量字段
        /// </summary>
        [Test]
        public void TestCharConstField()
        {
            const string fieldName = nameof(TestClass.CHAR_CONST_FIELD);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试字节常量字段
        /// </summary>
        [Test]
        public void TestByteConstField()
        {
            const string fieldName = nameof(TestClass.BYTE_CONST_FIELD);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试有符号字节常量字段
        /// </summary>
        [Test]
        public void TestSbyteConstField()
        {
            const string fieldName = nameof(TestClass.SBYTE_CONST_FIELD);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试短整型常量字段
        /// </summary>
        [Test]
        public void TestShortConstField()
        {
            const string fieldName = nameof(TestClass.SHORT_CONST_FIELD);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试无符号短整型常量字段
        /// </summary>
        [Test]
        public void TestUshortConstField()
        {
            const string fieldName = nameof(TestClass.USHORT_CONST_FIELD);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试长整型常量字段
        /// </summary>
        [Test]
        public void TestLongConstField()
        {
            const string fieldName = nameof(TestClass.LONG_CONST_FIELD);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试无符号长整型常量字段
        /// </summary>
        [Test]
        public void TestUlongConstField()
        {
            const string fieldName = nameof(TestClass.ULONG_CONST_FIELD);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试无符号整型常量字段
        /// </summary>
        [Test]
        public void TestUintConstField()
        {
            const string fieldName = nameof(TestClass.UINT_CONST_FIELD);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        #endregion

        #region 有限制支持的常量字段的测试

        /// <summary>
        /// 测试双精度浮点型常量字段
        /// </summary>
        [Test]
        public void TestDoubleConstField_Max15()
        {
            const string fieldName = nameof(TestClass.DOUBLE_CONST_FIELD);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试十进制常量字段，decimal 类型可以编写 const，但是反射时，const 会变成静态只读
        /// </summary>
        [Test]
        public void TestDecimalField_IsNotConst()
        {
            const string fieldName = nameof(TestClass.DECIMAL_CONST_FIELD);
            Assert.AreNotEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        [Test]
        public void TestEnumConstField()
        {
            const string fieldName = nameof(TestClass.ENUM_CONST_FIELD);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        [Test]
        public void TestNestedEnumConstField()
        {
            const string fieldName = nameof(TestClass.NESTED_ENUM_CONST_FIELD);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        #endregion

        #region 用于测试的枚举和类

        enum TestEnum
        {
            Value1,
            Value2,
            Value3
        }

        class TestClass
        {
            /// <summary>
            /// 字符串常量字段
            /// </summary>
            public const string STRING_CONST_FIELD = "Hello, World!";

            /// <summary>
            /// 整型常量字段
            /// </summary>
            public const int INT_CONST_FIELD = 2147483647;

            /// <summary>
            /// 单精度浮点型常量字段
            /// </summary>
            public const float FLOAT_CONST_FIELD = 3.14159f;

            /// <summary>
            /// 布尔常量字段
            /// </summary>
            public const bool BOOLEAN_CONST_FIELD = true;

            /// <summary>
            /// 字符常量字段
            /// </summary>
            public const char CHAR_CONST_FIELD = 'A';

            /// <summary>
            /// 字节常量字段
            /// </summary>
            public const byte BYTE_CONST_FIELD = 255;

            /// <summary>
            /// 有符号字节常量字段
            /// </summary>
            public const sbyte SBYTE_CONST_FIELD = -128;

            /// <summary>
            /// 短整型常量字段
            /// </summary>
            public const short SHORT_CONST_FIELD = 32767;

            /// <summary>
            /// 无符号短整型常量字段
            /// </summary>
            public const ushort USHORT_CONST_FIELD = 65535;

            /// <summary>
            /// 长整型常量字段
            /// </summary>
            public const long LONG_CONST_FIELD = 9223372036854775807L;

            /// <summary>
            /// 无符号长整型常量字段
            /// </summary>
            public const ulong ULONG_CONST_FIELD = 18446744073709551615ul;

            /// <summary>
            /// 无符号整型常量字段
            /// </summary>
            public const uint UINT_CONST_FIELD = 4294967295u;

            /// <summary>
            /// 双精度浮点型常量字段
            /// </summary>
            public const double DOUBLE_CONST_FIELD = 2.71828182845904d;

            /// <summary>
            /// 十进制常量字段
            /// </summary>
            public const decimal DECIMAL_CONST_FIELD = 123.456m;

            /// <summary>
            /// 枚举常量字段
            /// </summary>
            public const ScriptDocGeneratorTestEnum ENUM_CONST_FIELD = ScriptDocGeneratorTestEnum.Value1;

            /// <summary>
            /// 嵌套类的枚举常量字段
            /// </summary>
            public const TestEnum NESTED_ENUM_CONST_FIELD = TestEnum.Value3;
        }

        #endregion
    }
}
