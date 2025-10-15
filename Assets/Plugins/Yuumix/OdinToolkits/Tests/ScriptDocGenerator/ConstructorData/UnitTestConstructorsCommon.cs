using NUnit.Framework;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.AdvancedTypeAnalyzer;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    public class UnitTestConstructorsCommon
    {
        static readonly ConstructorInfo[] TestClassConstructorInfos =
            typeof(TestClass).GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                              BindingFlags.Static);

        static readonly IConstructorData[] TestClassConstructorDataArray = TestClassConstructorInfos
            .Select(x => UnitTestAnalysisFactory.Default.CreateConstructorData(x)).ToArray();

        [Test]
        public void TestConstructor()
        {
            string[] strings =
            {
                "public UnitTestConstructorsCommon.TestClass()",
                "public UnitTestConstructorsCommon.TestClass(bool b, int a)",
                "private static UnitTestConstructorsCommon.TestClass()",
                "private UnitTestConstructorsCommon.TestClass(string s)"
            };

            var signatures = TestClassConstructorDataArray.Select(x => x.Signature).ToArray();
            foreach (var s in signatures)
            {
                Debug.Log(s);
            }

            Assert.IsTrue(strings[0] == signatures[0] &&
                          strings[1] == signatures[1] &&
                          strings[2] == signatures[2] &&
                          strings[3] == signatures[3]);
        }

        #region Nested type: TestClass

        public class TestClass : TestClassAbstract
        {
            public TestClass() { }

            public TestClass(bool b, int a) : base(a) { }

            TestClass(string s) { }
        }

        #endregion

        #region Nested type: TestClassAbstract

        public abstract class TestClassAbstract
        {
            protected TestClassAbstract(int a) { }

            protected TestClassAbstract() { }
        }

        #endregion
    }
}
