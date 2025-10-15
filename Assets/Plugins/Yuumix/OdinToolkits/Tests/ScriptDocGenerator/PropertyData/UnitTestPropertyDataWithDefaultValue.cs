using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.AdvancedTypeAnalyzer;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    public class UnitTestPropertyDataWithDefaultValue
    {
        static readonly PropertyInfo[] PropertyInfos =
            typeof(TestClass).GetRuntimeProperties().ToArray();

        static readonly IPropertyData[] PropertyDataArray =
            PropertyInfos.Select(p => UnitTestAnalysisFactory.Default.CreatePropertyData(p)).ToArray();

        static readonly Dictionary<string, string> ExpectedSignatureMaps = new Dictionary<string, string>
        {
            {
                nameof(TestClass.StaticIntPropertyWithDefaultValue),
                "public static int StaticIntPropertyWithDefaultValue { get; set; } = 1;"
            },
            {
                nameof(TestClass.StaticFloatPropertyWithDefaultValue),
                "public static float StaticFloatPropertyWithDefaultValue { get; set; } = 1f;"
            },
            {
                nameof(TestClass.StaticBoolPropertyWithDefaultValue),
                "public static bool StaticBoolPropertyWithDefaultValue { get; set; } = true;"
            },
            {
                nameof(TestClass.StaticStringPropertyWithDefaultValue),
                "public static string StaticStringPropertyWithDefaultValue { get; set; } = \"Hello\";"
            },
            {
                nameof(TestClass.StaticEnumPropertyWithDefaultValue),
                "public static UnitTestPropertyDataWithDefaultValue.TestEnum StaticEnumPropertyWithDefaultValue { get; set; } = TestEnum.B;"
            },
            {
                nameof(TestClass.IntPropertyWithDefaultValue),
                "public int IntPropertyWithDefaultValue { get; internal set; } = 77;"
            },
            {
                nameof(TestClass.FloatPropertyWithDefaultValue),
                "public float FloatPropertyWithDefaultValue { get; protected set; } = 77f;"
            },
            {
                nameof(TestClass.BoolPropertyWithDefaultValue),
                "public bool BoolPropertyWithDefaultValue { get; private set; } = true;"
            },
            {
                nameof(TestClass.StringPropertyWithDefaultValue),
                "public string StringPropertyWithDefaultValue { get; set; } = \"World\";"
            },
            {
                nameof(TestClass.EnumPropertyWithDefaultValue),
                "public UnitTestPropertyDataWithDefaultValue.TestEnum EnumPropertyWithDefaultValue { get; set; } = TestEnum.C;"
            },
            {
                nameof(TestClass.StringPropertyInitOnCtor),
                "public string StringPropertyInitOnCtor { get; set; } = \"Hello World\";"
            }
        };

        [Test]
        public void TestStaticIntProperty()
        {
            var propertyData = PropertyDataArray.First(f =>
                ((MemberData)f).Name == nameof(TestClass.StaticIntPropertyWithDefaultValue));
            Debug.Log(propertyData.Signature);
            Assert.AreEqual(ExpectedSignatureMaps[nameof(TestClass.StaticIntPropertyWithDefaultValue)],
                propertyData.Signature);
        }

        [Test]
        public void TestStaticFloatPropertyWithDefaultValue()
        {
            var propertyData =
                PropertyDataArray.First(f =>
                    ((MemberData)f).Name == nameof(TestClass.StaticFloatPropertyWithDefaultValue));
            Debug.Log(propertyData.Signature);
            Assert.AreEqual(ExpectedSignatureMaps[nameof(TestClass.StaticFloatPropertyWithDefaultValue)],
                propertyData.Signature);
        }

        [Test]
        public void TestStaticBoolPropertyWithDefaultValue()
        {
            var propertyData =
                PropertyDataArray.First(f =>
                    ((MemberData)f).Name == nameof(TestClass.StaticBoolPropertyWithDefaultValue));
            Debug.Log(propertyData.Signature);
            Assert.AreEqual(ExpectedSignatureMaps[nameof(TestClass.StaticBoolPropertyWithDefaultValue)],
                propertyData.Signature);
        }

        [Test]
        public void TestStaticStringPropertyWithDefaultValue()
        {
            var propertyData =
                PropertyDataArray.First(f =>
                    ((MemberData)f).Name == nameof(TestClass.StaticStringPropertyWithDefaultValue));
            Debug.Log(propertyData.Signature);
            Assert.AreEqual(ExpectedSignatureMaps[nameof(TestClass.StaticStringPropertyWithDefaultValue)],
                propertyData.Signature);
        }

        [Test]
        public void TestStaticEnumProperty()
        {
            var propertyData = PropertyDataArray.First(f =>
                ((MemberData)f).Name == nameof(TestClass.StaticEnumPropertyWithDefaultValue));
            Debug.Log(propertyData.Signature);
            Assert.AreEqual(ExpectedSignatureMaps[nameof(TestClass.StaticEnumPropertyWithDefaultValue)],
                propertyData.Signature);
        }

        [Test]
        public void TestIntPropertyWithDefaultValue()
        {
            var propertyData =
                PropertyDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.IntPropertyWithDefaultValue));
            Debug.Log(propertyData.Signature);
            Assert.AreEqual(ExpectedSignatureMaps[nameof(TestClass.IntPropertyWithDefaultValue)],
                propertyData.Signature);
        }

        [Test]
        public void TestFloatPropertyWithDefaultValue()
        {
            var propertyData =
                PropertyDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.FloatPropertyWithDefaultValue));
            Debug.Log(propertyData.Signature);
            Assert.AreEqual(ExpectedSignatureMaps[nameof(TestClass.FloatPropertyWithDefaultValue)],
                propertyData.Signature);
        }

        [Test]
        public void TestBoolPropertyWithDefaultValue()
        {
            var propertyData =
                PropertyDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.BoolPropertyWithDefaultValue));
            Debug.Log(propertyData.Signature);
            Assert.AreEqual(ExpectedSignatureMaps[nameof(TestClass.BoolPropertyWithDefaultValue)],
                propertyData.Signature);
        }

        [Test]
        public void TestStringPropertyWithDefaultValue()
        {
            var propertyData =
                PropertyDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.StringPropertyWithDefaultValue));
            Debug.Log(propertyData.Signature);
            Assert.AreEqual(ExpectedSignatureMaps[nameof(TestClass.StringPropertyWithDefaultValue)],
                propertyData.Signature);
        }

        [Test]
        public void TestEnumProperty()
        {
            var propertyData = PropertyDataArray.First(f =>
                ((MemberData)f).Name == nameof(TestClass.EnumPropertyWithDefaultValue));
            Debug.Log(propertyData.Signature);
            Assert.AreEqual(ExpectedSignatureMaps[nameof(TestClass.EnumPropertyWithDefaultValue)],
                propertyData.Signature);
        }

        [Test]
        public void TestStringPropertyInitOnCtor()
        {
            var propertyData = PropertyDataArray.First(f =>
                ((MemberData)f).Name == nameof(TestClass.StringPropertyInitOnCtor));
            Debug.Log(propertyData.Signature);
            Assert.AreEqual(ExpectedSignatureMaps[nameof(TestClass.StringPropertyInitOnCtor)],
                propertyData.Signature);
        }

        #region Nested type: TestClass

        class TestClass
        {
            public TestClass() => StringPropertyInitOnCtor = "Hello World";
            public static int StaticIntPropertyWithDefaultValue { get; set; } = 1;
            public static float StaticFloatPropertyWithDefaultValue { get; set; } = 1f;
            public static bool StaticBoolPropertyWithDefaultValue { get; set; } = true;
            public static string StaticStringPropertyWithDefaultValue { get; set; } = "Hello";
            public static TestEnum StaticEnumPropertyWithDefaultValue { get; set; } = TestEnum.B;
            public int IntPropertyWithDefaultValue { get; internal set; } = 77;
            public float FloatPropertyWithDefaultValue { get; protected set; } = 77f;
            public bool BoolPropertyWithDefaultValue { get; private set; } = true;
            public string StringPropertyWithDefaultValue { get; set; } = "World";
            public TestEnum EnumPropertyWithDefaultValue { get; set; } = TestEnum.C;
            public string StringPropertyInitOnCtor { get; set; }
        }

        #endregion

        #region Nested type: TestEnum

        enum TestEnum
        {
            A,
            B,
            C
        }

        #endregion
    }
}
