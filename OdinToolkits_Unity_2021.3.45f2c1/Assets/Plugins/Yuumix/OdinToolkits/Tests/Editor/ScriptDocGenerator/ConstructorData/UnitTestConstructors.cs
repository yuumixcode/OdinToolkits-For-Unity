using NUnit.Framework;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.ScriptDocGenerator;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    public class UnitTestConstructors
    {
        static readonly ConstructorInfo[] TestClassConstructorInfos =
            typeof(TestClass).GetConstructors();

        static readonly IConstructorData[] TestClassConstructorDataArray = TestClassConstructorInfos
            .Select(x => UnitTestAnalysisFactory.Default.CreateConstructorData(x))
            .ToArray();

        [Test]
        public void TestConstructor()
        {
            string[] strings =
            {
                "public UnitTestConstructors.TestClass()",
                "public UnitTestConstructors.TestClass(bool b, int a)"
            };

            var signatures = TestClassConstructorDataArray.Select(x => x.Signature).ToArray();
            foreach (var s in signatures)
            {
                Debug.Log(s);
            }

            Assert.IsTrue(strings[0] == signatures[0] &&
                          strings[1] == signatures[1]);
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
