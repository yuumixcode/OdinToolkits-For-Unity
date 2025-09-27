using NUnit.Framework;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.AdvancedTypeAnalyzer;

// ReSharper disable UnusedMember.Local

namespace Yuumix.OdinToolkits.Tests.Editor
{
    /// <summary>
    /// 测试不同复合关键字的字段
    /// </summary>
    public class UnitTestFieldsWithCompositeKeyword
    {
        static readonly IAnalysisDataFactory TargetFactory = UnitTestAnalysisFactory.Instance;

        [Test]
        public void UnitTestFields()
        {
            var testType = typeof(TestClass);
            var testFields = testType.GetRuntimeFields().ToArray();
            var fieldDataArray = testFields.Select(f => TargetFactory.CreateFieldData(f)).ToArray();
            foreach (var fieldData in fieldDataArray)
            {
                var name = fieldData is IMemberData memberData ? memberData.Name : "Unknown";
                Debug.Log($"解析数据包含的字段完整签名: {fieldData.Signature}");
                var expectedDeclaration = GetExpectedSignature(name);
                Debug.Log($"预期字段完整签名: {expectedDeclaration}");
                Assert.AreEqual(expectedDeclaration, fieldData.Signature,
                    $"字段 {name} 的 Signature 不等于预期字段完整签名");
            }
        }

        static string GetExpectedSignature(string fieldName)
        {
            return fieldName switch
            {
                "CONST_FIELD" => "public const int CONST_FIELD = 42;",
                "StaticField" => "public static int StaticField;",
                "ReadOnlyField" => "public readonly int ReadOnlyField;",
                "StaticReadOnlyField" => "public static readonly int StaticReadOnlyField;",
                _ => null
            };
        }

        class TestClass
        {
            /// <summary>
            /// 常量字段
            /// </summary>
            public const int CONST_FIELD = 42;
            
            /// <summary>
            /// 静态字段
            /// </summary>
            public static int StaticField;

            /// <summary>
            /// 只读字段
            /// </summary>
            public readonly int ReadOnlyField;

            /// <summary>
            /// 静态只读字段
            /// </summary>
            public static readonly int StaticReadOnlyField;
        }
    }
}
