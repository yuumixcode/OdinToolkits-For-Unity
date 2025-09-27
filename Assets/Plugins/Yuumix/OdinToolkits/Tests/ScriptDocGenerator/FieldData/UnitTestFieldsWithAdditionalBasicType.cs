using NUnit.Framework;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.AdvancedTypeAnalyzer;

#pragma warning disable CS0414 // 字段已被赋值，但它的值从未被使用
namespace Yuumix.OdinToolkits.Tests.Editor
{
    /// <summary>
    /// 测试额外基本类型的字段
    /// </summary>
    public class UnitTestFieldsWithAdditionalBasicType
    {
        static readonly IAnalysisDataFactory TargetFactory = UnitTestAnalysisFactory.Instance;

        /// <summary>
        /// 测试所有额外基本类型字段的签名
        /// </summary>
        [Test]
        public void UnitTestFields()
        {
            var testType = typeof(TestClass);
            var testFields = testType.GetRuntimeFields().ToArray();
            var fieldDataArray = testFields.Select(f => TargetFactory.CreateFieldData(f)).ToArray();

            // 验证字段数量
            Assert.AreEqual(10, fieldDataArray.Length, "FieldData 数量不等于预期数量");

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

        /// <summary>
        /// 获取预期的字段签名
        /// </summary>
        static string GetExpectedSignature(string fieldName)
        {
            return fieldName switch
            {
                "CharField" => "public char CharField = 'A';",
                "DecimalField" => "public decimal DecimalField;",
                "DoubleField" => "public double DoubleField;",
                "LongField" => "public long LongField;",
                "ObjectField" => "public object ObjectField;",
                "SbyteField" => "public sbyte SbyteField;",
                "ShortField" => "public short ShortField;",
                "UintField" => "public uint UintField;",
                "UlongField" => "public ulong UlongField;",
                "UshortField" => "public ushort UshortField;",
                _ => null
            };
        }

        class TestClass
        {
            /// <summary>
            /// 字符字段
            /// </summary>
            public char CharField = 'A';

            /// <summary>
            /// 十进制字段
            /// </summary>
            public decimal DecimalField;

            /// <summary>
            /// 双精度字段
            /// </summary>
            public double DoubleField;

            /// <summary>
            /// 长整数字段
            /// </summary>
            public long LongField;

            /// <summary>
            /// 对象字段
            /// </summary>
            public object ObjectField = new object();

            /// <summary>
            /// 有符号字节字段
            /// </summary>
            public sbyte SbyteField;

            /// <summary>
            /// 短整数字段
            /// </summary>
            public short ShortField;

            /// <summary>
            /// 无符号整数字段
            /// </summary>
            public uint UintField;

            /// <summary>
            /// 无符号长整数字段
            /// </summary>
            public ulong UlongField;

            /// <summary>
            /// 无符号短整数字段
            /// </summary>
            public ushort UshortField;
        }
    }
}
