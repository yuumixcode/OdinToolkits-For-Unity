using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yuumix.OdinToolkits.ScriptDocGenerator;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    /// <summary>
    /// 测试实例字段的默认值
    /// </summary>
    public class UnitTestFieldsIsInstanceWithDefaultValue
    {
        static readonly FieldInfo[] TestFields = typeof(TestClass).GetRuntimeFields().ToArray();

        static readonly IFieldData[] TestFieldData =
            TestFields.Select(f => UnitTestAnalysisFactory.Default.CreateFieldData(f)).ToArray();

        static readonly Dictionary<string, string> FieldExpectedSignatureMaps = new Dictionary<string, string>
        {
            // 四个基础类型 - 直接设置初始值
            {
                nameof(TestClass.StringField),
                "public string " + nameof(TestClass.StringField) + " = \"Hello, World!\";"
            },
            {
                nameof(TestClass.IntField),
                "public int " + nameof(TestClass.IntField) + " = 2147483647;"
            },
            {
                nameof(TestClass.FloatField),
                "public float " + nameof(TestClass.FloatField) + " = 3.14159f;"
            },
            {
                nameof(TestClass.BooleanField),
                "public bool " + nameof(TestClass.BooleanField) + " = true;"
            },
            // 四个基础类型 - 构造函数设置初始值（自定义默认值）
            {
                nameof(TestClass.StringFieldInitOnCtor),
                "public string " + nameof(TestClass.StringFieldInitOnCtor) + " = \"Initialized in constructor!\";"
            },
            {
                nameof(TestClass.IntFieldInitOnCtor),
                "public int " + nameof(TestClass.IntFieldInitOnCtor) + " = -123456789;"
            },
            {
                nameof(TestClass.FloatFieldInitOnCtor),
                "public float " + nameof(TestClass.FloatFieldInitOnCtor) + " = -2.71828f;"
            },
            {
                nameof(TestClass.BooleanFieldInitOnCtor),
                "public bool " + nameof(TestClass.BooleanFieldInitOnCtor) + " = true;"
            },
            // ---
            // 其他完美支持的类型 - 直接设置初始值
            {
                nameof(TestClass.CharField),
                "public char " + nameof(TestClass.CharField) + " = 'A';"
            },
            {
                nameof(TestClass.ByteField),
                "public byte " + nameof(TestClass.ByteField) + " = 255;"
            },
            {
                nameof(TestClass.SbyteField),
                "public sbyte " + nameof(TestClass.SbyteField) + " = -128;"
            },
            {
                nameof(TestClass.ShortField),
                "public short " + nameof(TestClass.ShortField) + " = 32767;"
            },
            {
                nameof(TestClass.UshortField),
                "public ushort " + nameof(TestClass.UshortField) + " = 65535;"
            },
            {
                nameof(TestClass.LongField),
                "public long " + nameof(TestClass.LongField) + " = 9223372036854775807L;"
            },
            {
                nameof(TestClass.UlongField),
                "public ulong " + nameof(TestClass.UlongField) + " = 18446744073709551615ul;"
            },
            {
                nameof(TestClass.UintField),
                "public uint " + nameof(TestClass.UintField) + " = 4294967295u;"
            },
            // 其他完美支持的类型 - 构造函数设置初始值（自定义默认值）
            {
                nameof(TestClass.CharFieldInitOnCtor),
                "public char " + nameof(TestClass.CharFieldInitOnCtor) + " = 'Z';"
            },
            {
                nameof(TestClass.ByteFieldInitOnCtor),
                "public byte " + nameof(TestClass.ByteFieldInitOnCtor) + " = 128;"
            },
            {
                nameof(TestClass.SbyteFieldInitOnCtor),
                "public sbyte " + nameof(TestClass.SbyteFieldInitOnCtor) + " = 127;"
            },
            {
                nameof(TestClass.ShortFieldInitOnCtor),
                "public short " + nameof(TestClass.ShortFieldInitOnCtor) + " = -32768;"
            },
            {
                nameof(TestClass.UshortFieldInitOnCtor),
                "public ushort " + nameof(TestClass.UshortFieldInitOnCtor) + " = 32768;"
            },
            {
                nameof(TestClass.LongFieldInitOnCtor),
                "public long " + nameof(TestClass.LongFieldInitOnCtor) + " = -9223372036854775808L;"
            },
            {
                nameof(TestClass.UlongFieldInitOnCtor),
                "public ulong " + nameof(TestClass.UlongFieldInitOnCtor) + " = 9223372036854775808ul;"
            },
            {
                nameof(TestClass.UintFieldInitOnCtor),
                "public uint " + nameof(TestClass.UintFieldInitOnCtor) + " = 2147483648u;"
            },
            // --- 需要特殊处理的类型 - 直接设置初始值
            {
                nameof(TestClass.DoubleField),
                "public double " + nameof(TestClass.DoubleField) + " = 2.71828182845904d;"
            },
            {
                nameof(TestClass.DecimalField),
                "public decimal " + nameof(TestClass.DecimalField) + " = 123.456m;"
            },
            {
                nameof(TestClass.EnumField),
                "public " + nameof(ScriptDocGeneratorTestEnum) + " " + nameof(TestClass.EnumField) +
                " = " + nameof(ScriptDocGeneratorTestEnum) + ".Value2;"
            },
            {
                nameof(TestClass.NestedEnumField),
                "public " + nameof(UnitTestFieldsIsInstanceWithDefaultValue) + "." + nameof(TestEnum) + " " +
                nameof(TestClass.NestedEnumField) +
                " = " + nameof(TestEnum) + ".Value2;"
            },
            // 需要特殊处理的类型 - 构造函数设置初始值（自定义默认值）
            {
                nameof(TestClass.DoubleFieldInitOnCtor),
                "public double " + nameof(TestClass.DoubleFieldInitOnCtor) + " = -3.14159265358979d;"
            },
            {
                nameof(TestClass.DecimalFieldInitOnCtor),
                "public decimal " + nameof(TestClass.DecimalFieldInitOnCtor) + " = -987.654m;"
            },
            {
                nameof(TestClass.EnumFieldInitOnCtor),
                "public " + nameof(ScriptDocGeneratorTestEnum) + " " + nameof(TestClass.EnumFieldInitOnCtor) +
                " = " + nameof(ScriptDocGeneratorTestEnum) + ".Value3;"
            },
            {
                nameof(TestClass.NestedEnumFieldInitOnCtor),
                "public " + nameof(UnitTestFieldsIsInstanceWithDefaultValue) + "." + nameof(TestEnum) + " " +
                nameof(TestClass.NestedEnumFieldInitOnCtor) + " = " + nameof(TestEnum) + ".Value3;"
            }
        };

        #region 四个基础类型的实例字段的测试

        [Test]
        public void TestStringField()
        {
            const string fieldName = nameof(TestClass.StringField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        [Test]
        public void TestIntField()
        {
            const string fieldName = nameof(TestClass.IntField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        [Test]
        public void TestFloatField()
        {
            const string fieldName = nameof(TestClass.FloatField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        [Test]
        public void TestBooleanField()
        {
            const string fieldName = nameof(TestClass.BooleanField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试字符串实例字段（构造函数初始化）
        /// </summary>
        [Test]
        public void TestStringFieldInitOnCtor()
        {
            const string fieldName = nameof(TestClass.StringFieldInitOnCtor);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试整型实例字段（构造函数初始化）
        /// </summary>
        [Test]
        public void TestIntFieldInitOnCtor()
        {
            const string fieldName = nameof(TestClass.IntFieldInitOnCtor);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试单精度浮点型实例字段（构造函数初始化）
        /// </summary>
        [Test]
        public void TestFloatFieldInitOnCtor()
        {
            const string fieldName = nameof(TestClass.FloatFieldInitOnCtor);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试布尔实例字段（构造函数初始化）
        /// </summary>
        [Test]
        public void TestBooleanFieldInitOnCtor()
        {
            const string fieldName = nameof(TestClass.BooleanFieldInitOnCtor);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        #endregion

        #region 其他完美支持的实例字段的测试

        /// <summary>
        /// 测试字符实例字段
        /// </summary>
        [Test]
        public void TestCharField()
        {
            const string fieldName = nameof(TestClass.CharField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试字节实例字段
        /// </summary>
        [Test]
        public void TestByteField()
        {
            const string fieldName = nameof(TestClass.ByteField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试有符号字节实例字段
        /// </summary>
        [Test]
        public void TestSbyteField()
        {
            const string fieldName = nameof(TestClass.SbyteField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试短整型实例字段
        /// </summary>
        [Test]
        public void TestShortField()
        {
            const string fieldName = nameof(TestClass.ShortField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试无符号短整型实例字段
        /// </summary>
        [Test]
        public void TestUshortField()
        {
            const string fieldName = nameof(TestClass.UshortField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试长整型实例字段
        /// </summary>
        [Test]
        public void TestLongField()
        {
            const string fieldName = nameof(TestClass.LongField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试无符号长整型实例字段
        /// </summary>
        [Test]
        public void TestUlongField()
        {
            const string fieldName = nameof(TestClass.UlongField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试无符号整型实例字段
        /// </summary>
        [Test]
        public void TestUintField()
        {
            const string fieldName = nameof(TestClass.UintField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试字符实例字段（构造函数初始化）
        /// </summary>
        [Test]
        public void TestCharFieldInitOnCtor()
        {
            const string fieldName = nameof(TestClass.CharFieldInitOnCtor);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试字节实例字段（构造函数初始化）
        /// </summary>
        [Test]
        public void TestByteFieldInitOnCtor()
        {
            const string fieldName = nameof(TestClass.ByteFieldInitOnCtor);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试有符号字节实例字段（构造函数初始化）
        /// </summary>
        [Test]
        public void TestSbyteFieldInitOnCtor()
        {
            const string fieldName = nameof(TestClass.SbyteFieldInitOnCtor);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试短整型实例字段（构造函数初始化）
        /// </summary>
        [Test]
        public void TestShortFieldInitOnCtor()
        {
            const string fieldName = nameof(TestClass.ShortFieldInitOnCtor);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试无符号短整型实例字段（构造函数初始化）
        /// </summary>
        [Test]
        public void TestUshortFieldInitOnCtor()
        {
            const string fieldName = nameof(TestClass.UshortFieldInitOnCtor);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试长整型实例字段（构造函数初始化）
        /// </summary>
        [Test]
        public void TestLongFieldInitOnCtor()
        {
            const string fieldName = nameof(TestClass.LongFieldInitOnCtor);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试无符号长整型实例字段（构造函数初始化）
        /// </summary>
        [Test]
        public void TestUlongFieldInitOnCtor()
        {
            const string fieldName = nameof(TestClass.UlongFieldInitOnCtor);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试无符号整型实例字段（构造函数初始化）
        /// </summary>
        [Test]
        public void TestUintFieldInitOnCtor()
        {
            const string fieldName = nameof(TestClass.UintFieldInitOnCtor);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        #endregion

        #region 有限制支持的实例字段的测试

        /// <summary>
        /// 测试双精度浮点型实例字段
        /// </summary>
        [Test]
        public void TestDoubleField_Max15()
        {
            const string fieldName = nameof(TestClass.DoubleField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试十进制实例字段
        /// </summary>
        [Test]
        public void TestDecimalField()
        {
            const string fieldName = nameof(TestClass.DecimalField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试枚举实例字段
        /// </summary>
        [Test]
        public void TestEnumField()
        {
            const string fieldName = nameof(TestClass.EnumField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试嵌套类的枚举实例字段
        /// </summary>
        [Test]
        public void TestNestedEnumField()
        {
            const string fieldName = nameof(TestClass.NestedEnumField);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试双精度浮点型实例字段（构造函数初始化）
        /// </summary>
        [Test]
        public void TestDoubleFieldInitOnCtor_Max15()
        {
            const string fieldName = nameof(TestClass.DoubleFieldInitOnCtor);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试十进制实例字段（构造函数初始化）
        /// </summary>
        [Test]
        public void TestDecimalFieldInitOnCtor()
        {
            const string fieldName = nameof(TestClass.DecimalFieldInitOnCtor);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试枚举实例字段（构造函数初始化）
        /// </summary>
        [Test]
        public void TestEnumFieldInitOnCtor()
        {
            const string fieldName = nameof(TestClass.EnumFieldInitOnCtor);
            Assert.AreEqual(FieldExpectedSignatureMaps[fieldName],
                TestFieldData.First(f => ((MemberData)f).Name == fieldName).Signature);
        }

        /// <summary>
        /// 测试嵌套类的枚举实例字段（构造函数初始化）
        /// </summary>
        [Test]
        public void TestNestedEnumFieldInitOnCtor()
        {
            const string fieldName = nameof(TestClass.NestedEnumFieldInitOnCtor);
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
            /// 布尔实例字段
            /// </summary>
            public bool BooleanField = true;

            /// <summary>
            /// 布尔实例字段（构造函数初始化）
            /// </summary>
            public bool BooleanFieldInitOnCtor;

            /// <summary>
            /// 字节实例字段
            /// </summary>
            public byte ByteField = 255;

            /// <summary>
            /// 字节实例字段（构造函数初始化）
            /// </summary>
            public byte ByteFieldInitOnCtor;

            /// <summary>
            /// 字符实例字段
            /// </summary>
            public char CharField = 'A';

            /// <summary>
            /// 字符实例字段（构造函数初始化）
            /// </summary>
            public char CharFieldInitOnCtor;

            /// <summary>
            /// 十进制实例字段
            /// </summary>
            public decimal DecimalField = 123.456m;

            /// <summary>
            /// 十进制实例字段（构造函数初始化）
            /// </summary>
            public decimal DecimalFieldInitOnCtor;

            /// <summary>
            /// 双精度浮点型实例字段
            /// </summary>
            public double DoubleField = 2.71828182845904d;

            /// <summary>
            /// 双精度浮点型实例字段（构造函数初始化）
            /// </summary>
            public double DoubleFieldInitOnCtor;

            /// <summary>
            /// 枚举实例字段
            /// </summary>
            public ScriptDocGeneratorTestEnum EnumField = ScriptDocGeneratorTestEnum.Value2;

            /// <summary>
            /// 枚举实例字段（构造函数初始化）
            /// </summary>
            public ScriptDocGeneratorTestEnum EnumFieldInitOnCtor;

            /// <summary>
            /// 单精度浮点型实例字段
            /// </summary>
            public float FloatField = 3.14159f;

            /// <summary>
            /// 单精度浮点型实例字段（构造函数初始化）
            /// </summary>
            public float FloatFieldInitOnCtor;

            /// <summary>
            /// 整型实例字段
            /// </summary>
            public int IntField = 2147483647;

            /// <summary>
            /// 整型实例字段（构造函数初始化）
            /// </summary>
            public int IntFieldInitOnCtor;

            /// <summary>
            /// 长整型实例字段
            /// </summary>
            public long LongField = 9223372036854775807L;

            /// <summary>
            /// 长整型实例字段（构造函数初始化）
            /// </summary>
            public long LongFieldInitOnCtor;

            /// <summary>
            /// 嵌套类的枚举实例字段
            /// </summary>
            public TestEnum NestedEnumField = TestEnum.Value2;

            /// <summary>
            /// 嵌套类的枚举实例字段（构造函数初始化）
            /// </summary>
            public TestEnum NestedEnumFieldInitOnCtor;

            /// <summary>
            /// 有符号字节实例字段
            /// </summary>
            public sbyte SbyteField = -128;

            /// <summary>
            /// 有符号字节实例字段（构造函数初始化）
            /// </summary>
            public sbyte SbyteFieldInitOnCtor;

            /// <summary>
            /// 短整型实例字段
            /// </summary>
            public short ShortField = 32767;

            /// <summary>
            /// 短整型实例字段（构造函数初始化）
            /// </summary>
            public short ShortFieldInitOnCtor;

            /// <summary>
            /// 字符串实例字段
            /// </summary>
            public string StringField = "Hello, World!";

            // --- 构造函数初始化的字段

            /// <summary>
            /// 字符串实例字段（构造函数初始化）
            /// </summary>
            public string StringFieldInitOnCtor;

            /// <summary>
            /// 无符号整型实例字段
            /// </summary>
            public uint UintField = 4294967295u;

            /// <summary>
            /// 无符号整型实例字段（构造函数初始化）
            /// </summary>
            public uint UintFieldInitOnCtor;

            /// <summary>
            /// 无符号长整型实例字段
            /// </summary>
            public ulong UlongField = 18446744073709551615ul;

            /// <summary>
            /// 无符号长整型实例字段（构造函数初始化）
            /// </summary>
            public ulong UlongFieldInitOnCtor;

            /// <summary>
            /// 无符号短整型实例字段
            /// </summary>
            public ushort UshortField = 65535;

            /// <summary>
            /// 无符号短整型实例字段（构造函数初始化）
            /// </summary>
            public ushort UshortFieldInitOnCtor;

            public TestClass()
            {
                StringFieldInitOnCtor = "Initialized in constructor!";
                IntFieldInitOnCtor = -123456789;
                FloatFieldInitOnCtor = -2.71828f;
                BooleanFieldInitOnCtor = true;
                CharFieldInitOnCtor = 'Z';
                ByteFieldInitOnCtor = 128;
                SbyteFieldInitOnCtor = 127;
                ShortFieldInitOnCtor = -32768;
                UshortFieldInitOnCtor = 32768;
                LongFieldInitOnCtor = -9223372036854775808L;
                UlongFieldInitOnCtor = 9223372036854775808ul;
                UintFieldInitOnCtor = 2147483648u;
                DoubleFieldInitOnCtor = -3.14159265358979d;
                DecimalFieldInitOnCtor = -987.654m;
                EnumFieldInitOnCtor = ScriptDocGeneratorTestEnum.Value3;
                NestedEnumFieldInitOnCtor = TestEnum.Value3;
            }
        }

        #endregion
    }
}
