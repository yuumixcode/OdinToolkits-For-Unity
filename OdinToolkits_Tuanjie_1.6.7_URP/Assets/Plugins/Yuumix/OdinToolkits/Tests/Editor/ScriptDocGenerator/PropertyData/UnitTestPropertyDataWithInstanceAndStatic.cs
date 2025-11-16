using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.ScriptDocGenerator;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    public class UnitTestPropertyDataWithInstanceAndStatic
    {
        static readonly PropertyInfo[] PropertyInfos = typeof(TestClass).GetRuntimeProperties().ToArray();

        static readonly IPropertyData[] PropertiesData =
            PropertyInfos.Select(p => UnitTestAnalysisFactory.Default.CreatePropertyData(p)).ToArray();

        static readonly Dictionary<string, string> ExpectedSignatureMaps =
            new Dictionary<string, string>
            {
                {
                    nameof(TestClass.IntPropertyPublicGetPublicSet),
                    "public int IntPropertyPublicGetPublicSet { get; set; }"
                },
                {
                    nameof(TestClass.StringPropertyPublicGetInternalSet),
                    "public string StringPropertyPublicGetInternalSet { get; internal set; }"
                },
                {
                    nameof(TestClass.FloatPropertyPublicGetProtectedSet),
                    "public float FloatPropertyPublicGetProtectedSet { get; protected set; }"
                },
                {
                    nameof(TestClass.BoolPropertyPublicGetPrivateSet),
                    "public bool BoolPropertyPublicGetPrivateSet { get; private set; }"
                },
                {
                    nameof(TestClass.IntPropertyInternalGetPublicSet),
                    "public int IntPropertyInternalGetPublicSet { internal get; set; }"
                },
                {
                    nameof(TestClass.FloatPropertyProtectedGetPublicSet),
                    "public float FloatPropertyProtectedGetPublicSet { protected get; set; }"
                },
                {
                    nameof(TestClass.BoolPropertyPrivateGetPublicSet),
                    "public bool BoolPropertyPrivateGetPublicSet { private get; set; }"
                },
                {
                    nameof(TestClass.StaticIntPropertyPublicGetPublicSet),
                    "public int StaticIntPropertyPublicGetPublicSet { get; set; }"
                }
            };

        [Test]
        public void TestIntPropertyPublicGetPublicSet()
        {
            var propertyData = PropertiesData.First(p => ((IMemberData)p).Name == "IntPropertyPublicGetPublicSet");
            Debug.Log(propertyData.Signature);
            Assert.AreEqual(ExpectedSignatureMaps[nameof(TestClass.IntPropertyPublicGetPublicSet)],
                propertyData.Signature);
        }

        [Test]
        public void TestStringPropertyPublicGetInternalSet()
        {
            var propertyData = PropertiesData.First(p => ((IMemberData)p).Name == "StringPropertyPublicGetInternalSet");
            Debug.Log(propertyData.Signature);
            Assert.AreEqual(ExpectedSignatureMaps[nameof(TestClass.StringPropertyPublicGetInternalSet)],
                propertyData.Signature);
        }

        [Test]
        public void TestFloatPropertyPublicGetProtectedSet()
        {
            var propertyData = PropertiesData.First(p => ((IMemberData)p).Name == "FloatPropertyPublicGetProtectedSet");
            Debug.Log(propertyData.Signature);
            Assert.AreEqual(ExpectedSignatureMaps[nameof(TestClass.FloatPropertyPublicGetProtectedSet)],
                propertyData.Signature);
        }

        [Test]
        public void TestBoolPropertyPublicGetPrivateSet()
        {
            var propertyData = PropertiesData.First(p => ((IMemberData)p).Name == "BoolPropertyPublicGetPrivateSet");
            Debug.Log(propertyData.Signature);
            Assert.AreEqual(ExpectedSignatureMaps[nameof(TestClass.BoolPropertyPublicGetPrivateSet)],
                propertyData.Signature);
        }

        [Test]
        public void TestIntPropertyInternalGetPublicSet()
        {
            var propertyData = PropertiesData.First(p => ((IMemberData)p).Name == "IntPropertyInternalGetPublicSet");
            Debug.Log(propertyData.Signature);
            Assert.AreEqual(ExpectedSignatureMaps[nameof(TestClass.IntPropertyInternalGetPublicSet)],
                propertyData.Signature);
        }

        [Test]
        public void TestFloatPropertyProtectedGetPublicSet()
        {
            var propertyData = PropertiesData.First(p => ((IMemberData)p).Name == "FloatPropertyProtectedGetPublicSet");
            Debug.Log(propertyData.Signature);
            Assert.AreEqual(ExpectedSignatureMaps[nameof(TestClass.FloatPropertyProtectedGetPublicSet)],
                propertyData.Signature);
        }

        [Test]
        public void TestBoolPropertyPrivateGetPublicSet()
        {
            var propertyData = PropertiesData.First(p => ((IMemberData)p).Name == "BoolPropertyPrivateGetPublicSet");
            Debug.Log(propertyData.Signature);
            Assert.AreEqual(ExpectedSignatureMaps[nameof(TestClass.BoolPropertyPrivateGetPublicSet)],
                propertyData.Signature);
        }

        [Test]
        public void TestStaticIntPropertyPublicGetPublicSet()
        {
            var propertyData =
                PropertiesData.First(p => ((IMemberData)p).Name == "StaticIntPropertyPublicGetPublicSet");
            Debug.Log(propertyData.Signature);
            Assert.AreEqual(ExpectedSignatureMaps[nameof(TestClass.StaticIntPropertyPublicGetPublicSet)],
                propertyData.Signature);
        }

        #region Nested type: TestClass

        class TestClass
        {
            public int IntPropertyPublicGetPublicSet { get; set; }
            public string StringPropertyPublicGetInternalSet { get; internal set; }
            public float FloatPropertyPublicGetProtectedSet { get; protected set; }
            public bool BoolPropertyPublicGetPrivateSet { get; private set; }
            public int IntPropertyInternalGetPublicSet { internal get; set; }
            public float FloatPropertyProtectedGetPublicSet { protected get; set; }
            public bool BoolPropertyPrivateGetPublicSet { private get; set; }
            public int StaticIntPropertyPublicGetPublicSet { get; set; }
        }

        #endregion
    }
}
