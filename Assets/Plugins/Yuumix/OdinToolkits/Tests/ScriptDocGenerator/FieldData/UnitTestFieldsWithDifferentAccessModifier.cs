using NUnit.Framework;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.AdvancedTypeAnalyzer;

#pragma warning disable CS0414 // 字段已被赋值，但它的值从未被使用
namespace Yuumix.OdinToolkits.Tests.Editor
{
    /// <summary>
    /// 测试字段的访问修饰符
    /// </summary>
    public class UnitTestFieldsWithDifferentAccessModifier
    {
        static readonly IAnalysisDataFactory TargetFactory = UnitTestAnalysisFactory.Instance;

        /// <summary>
        /// 测试拥有不同访问修饰符的字段
        /// </summary>
        [Test]
        public void UnitTestFields()
        {
            var testType = typeof(TestClass);
            var testFields = testType.GetRuntimeFields().ToArray();
            var fieldData = testFields.Select(f => TargetFactory.CreateFieldData(f)).ToArray();
            Assert.IsTrue(fieldData.Length == testFields.Length, "FieldData 数量不等于 GetRuntimeFields() 方法获取的字段数量");
            Assert.IsTrue(fieldData.Length == 6, "FieldData 数量不等于 6，存在自动生成的字段");
            foreach (var data in fieldData)
            {
                var name = data is IMemberData memberData ? memberData.Name : "Unknown";
                Debug.Log($"解析数据包含的字段完整签名: {data.Signature}");
                var expectedDeclaration = GetExpectedSignature(name);
                Debug.Log($"预期字段完整签名: {expectedDeclaration}");
                Assert.AreEqual(expectedDeclaration, data.Signature,
                    $"字段 {name} 的 Signature 不等于预期字段完整签名");
            }

            return;

            string GetExpectedSignature(string fieldName)
            {
                return fieldName switch
                {
                    "_privateField" => "private int _privateField;",
                    "InternalField" => "internal int InternalField;",
                    "PrivateProtectedField" => "private protected int PrivateProtectedField;",
                    "ProtectedField" => "protected int ProtectedField;",
                    "ProtectedInternalField" => "protected internal int ProtectedInternalField;",
                    "PublicField" => "public int PublicField;",
                    _ => null
                };
            }
        }

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
    }
}
