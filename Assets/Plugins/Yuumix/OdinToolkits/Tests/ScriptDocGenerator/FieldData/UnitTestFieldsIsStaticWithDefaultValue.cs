using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yuumix.OdinToolkits.AdvancedTypeAnalyzer;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    /// <summary>
    /// 测试静态字段的默认值
    /// </summary>
    public class UnitTestFieldsIsStaticWithDefaultValue
    {
        static readonly FieldInfo[] TestFields = typeof(TestClass).GetRuntimeFields().ToArray();

        static readonly IFieldData[] TestFieldData =
            TestFields.Select(f => UnitTestAnalysisFactory.Default.CreateFieldData(f)).ToArray();

        static readonly Dictionary<string, string> FieldExpectedSignatureMaps = new Dictionary<string, string>
        {
            {
                nameof(TestClass.StringStaticField),
                "public static string " + nameof(TestClass.StringStaticField) + " = \"Hello, World!\";"
            },
            {
                nameof(TestClass.INTStaticField),
                "public static int " + nameof(TestClass.INTStaticField) + " = 2147483647;"
            },
            {
                nameof(TestClass.FloatStaticField),
                "public static float " + nameof(TestClass.FloatStaticField) + " = 3.14159f;"
            },
            {
                nameof(TestClass.BooleanStaticField),
                "public static bool " + nameof(TestClass.BooleanStaticField) + " = true;"
            },
            // ---
            {
                nameof(TestClass.CharStaticField),
                "public static char " + nameof(TestClass.CharStaticField) + " = 'A';"
            },
            {
                nameof(TestClass.ByteStaticField),
                "public static byte " + nameof(TestClass.ByteStaticField) + " = 255;"
            },
            {
                nameof(TestClass.SbyteStaticField),
                "public static sbyte " + nameof(TestClass.SbyteStaticField) + " = -128;"
            },
            {
                nameof(TestClass.ShortStaticField),
                "public static short " + nameof(TestClass.ShortStaticField) + " = 32767;"
            },
            {
                nameof(TestClass.UshortStaticField),
                "public static ushort " + nameof(TestClass.UshortStaticField) + " = 65535;"
            },
            {
                nameof(TestClass.LongStaticField),
                "public static long " + nameof(TestClass.LongStaticField) + " = 9223372036854775807L;"
            },
            {
                nameof(TestClass.UlongStaticField),
                "public static ulong " + nameof(TestClass.UlongStaticField) + " = 18446744073709551615ul;"
            },
            {
                nameof(TestClass.UintStaticField),
                "public static uint " + nameof(TestClass.UintStaticField) + " = 4294967295u;"
            },
            // --- 需要特殊处理
            {
                nameof(TestClass.DoubleStaticField),
                "public static double " + nameof(TestClass.DoubleStaticField) + " = 2.71828182845904d;"
            },
            {
                nameof(TestClass.DecimalStaticField),
                "public static decimal " + nameof(TestClass.DecimalStaticField) + " = 123.456m;"
            },
            {
                nameof(TestClass.EnumStaticField),
                "public static " + nameof(ScriptDocGeneratorTestEnum) + " " + nameof(TestClass.EnumStaticField) +
                " = " + nameof(ScriptDocGeneratorTestEnum) + ".Value2;"
            },
            {
                nameof(TestClass.NestedEnumStaticField),
                "public static " + nameof(UnitTestFieldsIsStaticWithDefaultValue) + "." + nameof(TestEnum) + " " +
                nameof(TestClass.NestedEnumStaticField) +
                " = " + nameof(TestEnum) + ".Value3;"
            }
        };

        #region 四个基础类型的静态字段的测试

        [Test]
        public void TestStringStaticField()
        {
            const string fieldName = nameof(TestClass.StringStaticField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        [Test]
        public void TestIntStaticField()
        {
            const string fieldName = nameof(TestClass.INTStaticField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        [Test]
        public void TestFloatStaticField()
        {
            const string fieldName = nameof(TestClass.FloatStaticField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        [Test]
        public void TestBooleanStaticField()
        {
            const string fieldName = nameof(TestClass.BooleanStaticField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        #endregion

        #region 其他完美支持的静态字段的测试

        /// <summary>
        /// 测试字符静态字段
        /// </summary>
        [Test]
        public void TestCharStaticField()
        {
            const string fieldName = nameof(TestClass.CharStaticField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试字节静态字段
        /// </summary>
        [Test]
        public void TestByteStaticField()
        {
            const string fieldName = nameof(TestClass.ByteStaticField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试有符号字节静态字段
        /// </summary>
        [Test]
        public void TestSbyteStaticField()
        {
            const string fieldName = nameof(TestClass.SbyteStaticField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试短整型静态字段
        /// </summary>
        [Test]
        public void TestShortStaticField()
        {
            const string fieldName = nameof(TestClass.ShortStaticField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试无符号短整型静态字段
        /// </summary>
        [Test]
        public void TestUshortStaticField()
        {
            const string fieldName = nameof(TestClass.UshortStaticField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试长整型静态字段
        /// </summary>
        [Test]
        public void TestLongStaticField()
        {
            const string fieldName = nameof(TestClass.LongStaticField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试无符号长整型静态字段
        /// </summary>
        [Test]
        public void TestUlongStaticField()
        {
            const string fieldName = nameof(TestClass.UlongStaticField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试无符号整型静态字段
        /// </summary>
        [Test]
        public void TestUintStaticField()
        {
            const string fieldName = nameof(TestClass.UintStaticField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        #endregion

        #region 有限制支持的静态字段的测试

        /// <summary>
        /// 测试双精度浮点型静态字段
        /// </summary>
        [Test]
        public void TestDoubleStaticField_Max15()
        {
            const string fieldName = nameof(TestClass.DoubleStaticField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试十进制静态字段
        /// </summary>
        [Test]
        public void TestDecimalStaticField()
        {
            const string fieldName = nameof(TestClass.DecimalStaticField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试枚举静态字段
        /// </summary>
        [Test]
        public void TestEnumStaticField()
        {
            const string fieldName = nameof(TestClass.EnumStaticField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试嵌套类的枚举静态字段
        /// </summary>
        [Test]
        public void TestNestedEnumStaticField()
        {
            const string fieldName = nameof(TestClass.NestedEnumStaticField);
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
            /// 字符串静态字段
            /// </summary>
            public static string StringStaticField = "Hello, World!";

            /// <summary>
            /// 整型静态字段
            /// </summary>
            public static int INTStaticField = 2147483647;

            /// <summary>
            /// 单精度浮点型静态字段
            /// </summary>
            public static float FloatStaticField = 3.14159f;

            /// <summary>
            /// 布尔静态字段
            /// </summary>
            public static bool BooleanStaticField = true;

            /// <summary>
            /// 字符静态字段
            /// </summary>
            public static char CharStaticField = 'A';

            /// <summary>
            /// 字节静态字段
            /// </summary>
            public static byte ByteStaticField = 255;

            /// <summary>
            /// 有符号字节静态字段
            /// </summary>
            public static sbyte SbyteStaticField = -128;

            /// <summary>
            /// 短整型静态字段
            /// </summary>
            public static short ShortStaticField = 32767;

            /// <summary>
            /// 无符号短整型静态字段
            /// </summary>
            public static ushort UshortStaticField = 65535;

            /// <summary>
            /// 长整型静态字段
            /// </summary>
            public static long LongStaticField = 9223372036854775807L;

            /// <summary>
            /// 无符号长整型静态字段
            /// </summary>
            public static ulong UlongStaticField = 18446744073709551615ul;

            /// <summary>
            /// 无符号整型静态字段
            /// </summary>
            public static uint UintStaticField = 4294967295u;

            /// <summary>
            /// 双精度浮点型静态字段
            /// </summary>
            public static double DoubleStaticField = 2.71828182845904d;

            /// <summary>
            /// 十进制静态字段
            /// </summary>
            public static decimal DecimalStaticField = 123.456m;

            /// <summary>
            /// 枚举静态字段
            /// </summary>
            public static ScriptDocGeneratorTestEnum EnumStaticField = ScriptDocGeneratorTestEnum.Value2;

            /// <summary>
            /// 嵌套类的枚举静态字段
            /// </summary>
            public static TestEnum NestedEnumStaticField = TestEnum.Value3;
        }

        #endregion
    }
}
