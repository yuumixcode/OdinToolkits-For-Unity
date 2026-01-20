using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using Yuumix.OdinToolkits.ScriptDocGenerator;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    public class UnitTestFieldsIsDelegate
    {
        static readonly FieldInfo[] TestFields = typeof(TestClass).GetRuntimeFields()
            .ToArray();

        static readonly IFieldData[] TestFieldData = TestFields
            .Select(f => UnitTestAnalysisFactory.Default.CreateFieldData(f))
            .ToArray();

        static readonly Dictionary<string, string> FieldExpectedSignatureMaps = new Dictionary<string, string>
        {
            { nameof(Action), "public Action ActionField;" },
            { typeof(Action<int, string>).Name, "public Action<int, string> ActionWithParamsField;" },
            { typeof(Func<int, string, bool>).Name, "public Func<int, string, bool> FuncWithParamsField;" },
            { typeof(Predicate<int>).Name, "public Predicate<int> PredicateField;" },
            { typeof(Comparison<string>).Name, "public Comparison<string> ComparisonField;" }
        };

        [Test]
        public void TestActionField()
        {
            var fieldData = TestFieldData.First(f => ((MemberData)f).Name == "ActionField");
            Debug.Log(fieldData.Signature);
            Assert.AreEqual(FieldExpectedSignatureMaps[nameof(Action)], fieldData.Signature);
        }

        [Test]
        public void TestActionWithParamsField()
        {
            var fieldData = TestFieldData.First(f => ((MemberData)f).Name == "ActionWithParamsField");
            Debug.Log(fieldData.Signature);
            Assert.AreEqual(FieldExpectedSignatureMaps[typeof(Action<int, string>).Name],
                fieldData.Signature);
        }

        [Test]
        public void TestFuncWithParamsField()
        {
            var fieldData = TestFieldData.First(f => ((MemberData)f).Name == "FuncWithParamsField");
            Debug.Log(fieldData.Signature);
            Assert.AreEqual(FieldExpectedSignatureMaps[typeof(Func<int, string, bool>).Name],
                fieldData.Signature);
        }

        [Test]
        public void TestPredicateField()
        {
            var fieldData = TestFieldData.First(f => ((MemberData)f).Name == "PredicateField");
            Debug.Log(fieldData.Signature);
            Assert.AreEqual(FieldExpectedSignatureMaps[typeof(Predicate<int>).Name], fieldData.Signature);
        }

        [Test]
        public void TestComparisonField()
        {
            var fieldData = TestFieldData.First(f => ((MemberData)f).Name == "ComparisonField");
            Debug.Log(fieldData.Signature);
            Assert.AreEqual(FieldExpectedSignatureMaps[typeof(Comparison<string>).Name], fieldData.Signature);
        }

        #region Nested type: TestClass

        class TestClass
        {
            /// <summary>
            /// Action 字段
            /// </summary>
            public Action ActionField;

            /// <summary>
            /// Action 带参数字段
            /// </summary>
            public Action<int, string> ActionWithParamsField;

            /// <summary>
            /// Comparison 字段
            /// </summary>
            public Comparison<string> ComparisonField;

            /// <summary>
            /// Func 带参数字段
            /// </summary>
            public Func<int, string, bool> FuncWithParamsField;

            /// <summary>
            /// Predicate 字段
            /// </summary>
            public Predicate<int> PredicateField;
        }

        #endregion
    }
}
