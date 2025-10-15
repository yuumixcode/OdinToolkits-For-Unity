using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.AdvancedTypeAnalyzer;

#pragma warning disable CS0618 // 类型或成员已过时

namespace Yuumix.OdinToolkits.Tests.Editor
{
    /// <summary>
    /// 测试 Unity 的类型以及带特性的字段
    /// </summary>
    public class UnitTestFieldsIsUnityWithAttribute
    {
        static readonly FieldInfo[] FieldInfos = typeof(TestClass).GetRuntimeFields().ToArray();

        static readonly IFieldData[] FieldDataArray =
            FieldInfos.Select(f => UnitTestAnalysisFactory.Default.CreateFieldData(f)).ToArray();

        static readonly Dictionary<string, string> FieldExpectedFullDeclarationWithAttributesMaps =
            new Dictionary<string, string>
            {
                {
                    nameof(TestClass.gameObjectField),
                    "public GameObject gameObjectField;"
                },
                {
                    nameof(TestClass.transformField),
                    "public Transform transformField;"
                },
                {
                    nameof(TestClass.rigidbodyField),
                    "public Rigidbody rigidbodyField;"
                },
                {
                    nameof(TestClass.vector3Field),
                    "public Vector3 vector3Field = new Vector3(1.00, 1.00, 1.00);"
                },
                {
                    nameof(TestClass.quaternionField),
                    @"[SerializeField]
[UnityEngine.Tooltip(""This is a tooltip"")]
[UnityEngine.Range(0, 100)]
public Quaternion quaternionField = new Quaternion(0.00000, 0.00000, 0.00000, 1.00000);"
                },
                {
                    nameof(TestClass.colorField),
                    @"[UnityEngine.ColorUsage(true, true)]
public Color colorField = "
                },
                {
                    nameof(TestClass.layerMaskField),
                    @"[System.Obsolete(""Use newField instead"")]
public LayerMask layerMaskField;"
                }
            };

        [Test]
        public void TestGameObjectField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.gameObjectField));
            Assert.AreEqual(FieldExpectedFullDeclarationWithAttributesMaps[nameof(TestClass.gameObjectField)],
                fieldData.FullDeclarationWithAttributes);
        }

        [Test]
        public void TestTransformField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.transformField));
            Assert.AreEqual(FieldExpectedFullDeclarationWithAttributesMaps[nameof(TestClass.transformField)],
                fieldData.FullDeclarationWithAttributes);
        }

        [Test]
        public void TestRigidbodyField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.rigidbodyField));
            Assert.AreEqual(FieldExpectedFullDeclarationWithAttributesMaps[nameof(TestClass.rigidbodyField)],
                fieldData.FullDeclarationWithAttributes);
        }

        [Test]
        public void TestVector3Field()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.vector3Field));
            Debug.Log(fieldData.FullDeclarationWithAttributes);
            Assert.AreEqual(FieldExpectedFullDeclarationWithAttributesMaps[nameof(TestClass.vector3Field)],
                fieldData.FullDeclarationWithAttributes);
        }

        [Test]
        public void TestQuaternionField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.quaternionField));
            Debug.Log(fieldData.FullDeclarationWithAttributes);
            Assert.AreEqual(FieldExpectedFullDeclarationWithAttributesMaps[nameof(TestClass.quaternionField)],
                fieldData.FullDeclarationWithAttributes);
        }

        [Test]
        public void TestColorField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.colorField));
            Debug.Log(fieldData.FullDeclarationWithAttributes);
            Assert.IsTrue(fieldData.FullDeclarationWithAttributes.Contains(
                FieldExpectedFullDeclarationWithAttributesMaps[nameof(TestClass.colorField)]));
        }

        [Test]
        public void TestLayerMaskField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.layerMaskField));
            Debug.Log(fieldData.FullDeclarationWithAttributes);
            Assert.IsTrue(fieldData.FullDeclarationWithAttributes.Contains(
                FieldExpectedFullDeclarationWithAttributesMaps[nameof(TestClass.layerMaskField)]));
        }

        #region Nested type: TestClass

        [Serializable]
        class TestClass
        {
            #region Serialized Fields

            /// <summary>
            /// GameObject 字段
            /// </summary>
            public GameObject gameObjectField;

            /// <summary>
            /// Transform 字段
            /// </summary>
            public Transform transformField;

            /// <summary>
            /// Rigidbody 字段
            /// </summary>
            public Rigidbody rigidbodyField;

            /// <summary>
            /// Vector3 字段
            /// </summary>
            public Vector3 vector3Field = new Vector3(1, 1, 1);

            /// <summary>
            /// Quaternion 字段
            /// </summary>
            [SerializeField]
            [Tooltip("This is a tooltip")]
            [UnityEngine.Range(0, 100)]
            public Quaternion quaternionField = new Quaternion(0, 0, 0, 1);

            /// <summary>
            /// Color 字段
            /// </summary>
            [ColorUsage(true, true)]
            public Color colorField = Color.white;

            /// <summary>
            /// LayerMask 字段
            /// </summary>
            [Obsolete("Use newField instead")]
            public LayerMask layerMaskField;

            #endregion
        }

        #endregion
    }
}
