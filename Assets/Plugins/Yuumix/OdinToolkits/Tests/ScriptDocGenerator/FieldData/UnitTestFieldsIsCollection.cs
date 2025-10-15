using NUnit.Framework;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yuumix.OdinToolkits.AdvancedTypeAnalyzer;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    /// <summary>
    /// 测试集合类型字段
    /// </summary>
    public class UnitTestFieldsIsCollection
    {
        static readonly FieldInfo[] FieldInfos = typeof(TestClass).GetRuntimeFields().ToArray();

        static readonly IFieldData[] FieldDataArray =
            FieldInfos.Select(f => UnitTestAnalysisFactory.Default.CreateFieldData(f)).ToArray();

        static readonly Dictionary<string, string> FieldExpectedSignatureMaps = new Dictionary<string, string>
        {
            { nameof(TestClass.ArrayField), "public int[] ArrayField;" },
            { nameof(TestClass.MultiArrayField), "public int[,] MultiArrayField;" },
            { nameof(TestClass.JaggedArrayField), "public int[][] JaggedArrayField;" },
            { nameof(TestClass.ListField), "public List<string> ListField;" },
            { nameof(TestClass.DictionaryField), "public Dictionary<string, int> DictionaryField;" },
            { nameof(TestClass.HashSetField), "public HashSet<string> HashSetField;" },
            { nameof(TestClass.SortedDictionaryField), "public SortedDictionary<string, int> SortedDictionaryField;" },
            { nameof(TestClass.SortedListField), "public SortedList<string, int> SortedListField;" },
            { nameof(TestClass.StackField), "public Stack<string> StackField;" },
            { nameof(TestClass.QueueField), "public Queue<int> QueueField;" },
            { nameof(TestClass.LinkedListField), "public LinkedList<string> LinkedListField;" },
            { nameof(TestClass.ArrayListField), "public ArrayList ArrayListField;" },
            { nameof(TestClass.HashtableField), "public Hashtable HashtableField;" },
            { nameof(TestClass.ReadOnlyListField), "public IReadOnlyList<string> ReadOnlyListField;" },
            {
                nameof(TestClass.ReadOnlyDictionaryField),
                "public IReadOnlyDictionary<string, int> ReadOnlyDictionaryField;"
            },
            {
                nameof(TestClass.ConcurrentDictionaryField),
                "public ConcurrentDictionary<string, int> ConcurrentDictionaryField;"
            }
        };

        [Test]
        public void TestArrayField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.ArrayField));
            Assert.AreEqual(FieldExpectedSignatureMaps[nameof(TestClass.ArrayField)], fieldData.Signature);
        }

        [Test]
        public void TestMultiArrayField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.MultiArrayField));
            Assert.AreEqual(FieldExpectedSignatureMaps[nameof(TestClass.MultiArrayField)], fieldData.Signature);
        }

        [Test]
        public void TestJaggedArrayField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.JaggedArrayField));
            Assert.AreEqual(FieldExpectedSignatureMaps[nameof(TestClass.JaggedArrayField)], fieldData.Signature);
        }

        [Test]
        public void TestListField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.ListField));
            Assert.AreEqual(FieldExpectedSignatureMaps[nameof(TestClass.ListField)], fieldData.Signature);
        }

        [Test]
        public void TestDictionaryField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.DictionaryField));
            Assert.AreEqual(FieldExpectedSignatureMaps[nameof(TestClass.DictionaryField)], fieldData.Signature);
        }

        [Test]
        public void TestHashSetField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.HashSetField));
            Assert.AreEqual(FieldExpectedSignatureMaps[nameof(TestClass.HashSetField)], fieldData.Signature);
        }

        [Test]
        public void TestSortedDictionaryField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.SortedDictionaryField));
            Assert.AreEqual(FieldExpectedSignatureMaps[nameof(TestClass.SortedDictionaryField)], fieldData.Signature);
        }

        [Test]
        public void TestSortedListField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.SortedListField));
            Assert.AreEqual(FieldExpectedSignatureMaps[nameof(TestClass.SortedListField)], fieldData.Signature);
        }

        [Test]
        public void TestStackField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.StackField));
            Assert.AreEqual(FieldExpectedSignatureMaps[nameof(TestClass.StackField)], fieldData.Signature);
        }

        [Test]
        public void TestQueueField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.QueueField));
            Assert.AreEqual(FieldExpectedSignatureMaps[nameof(TestClass.QueueField)], fieldData.Signature);
        }

        [Test]
        public void TestLinkedListField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.LinkedListField));
            Assert.AreEqual(FieldExpectedSignatureMaps[nameof(TestClass.LinkedListField)], fieldData.Signature);
        }

        [Test]
        public void TestArrayListField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.ArrayListField));
            Assert.AreEqual(FieldExpectedSignatureMaps[nameof(TestClass.ArrayListField)], fieldData.Signature);
        }

        [Test]
        public void TestHashtableField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.HashtableField));
            Assert.AreEqual(FieldExpectedSignatureMaps[nameof(TestClass.HashtableField)], fieldData.Signature);
        }

        [Test]
        public void TestReadOnlyListField()
        {
            var fieldData = FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.ReadOnlyListField));
            Assert.AreEqual(FieldExpectedSignatureMaps[nameof(TestClass.ReadOnlyListField)], fieldData.Signature);
        }

        [Test]
        public void TestReadOnlyDictionaryField()
        {
            var fieldData =
                FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.ReadOnlyDictionaryField));
            Assert.AreEqual(FieldExpectedSignatureMaps[nameof(TestClass.ReadOnlyDictionaryField)], fieldData.Signature);
        }

        [Test]
        public void TestConcurrentDictionaryField()
        {
            var fieldData =
                FieldDataArray.First(f => ((MemberData)f).Name == nameof(TestClass.ConcurrentDictionaryField));
            Assert.AreEqual(FieldExpectedSignatureMaps[nameof(TestClass.ConcurrentDictionaryField)],
                fieldData.Signature);
        }

        #region Nested type: TestClass

        class TestClass
        {
            #region 数组类型

            /// <summary>
            /// 数组字段
            /// </summary>
            public int[] ArrayField;

            /// <summary>
            /// 多维数组字段
            /// </summary>
            public int[,] MultiArrayField;

            /// <summary>
            /// 交错数组字段
            /// </summary>
            public int[][] JaggedArrayField;

            #endregion

            #region 泛型集合类型

            /// <summary>
            /// 列表字段
            /// </summary>
            public List<string> ListField;

            /// <summary>
            /// 字典字段
            /// </summary>
            public Dictionary<string, int> DictionaryField;

            /// <summary>
            /// 集合字段
            /// </summary>
            public HashSet<string> HashSetField;

            /// <summary>
            /// 有序字典字段
            /// </summary>
            public SortedDictionary<string, int> SortedDictionaryField;

            /// <summary>
            /// 有序列表字段
            /// </summary>
            public SortedList<string, int> SortedListField;

            /// <summary>
            /// 堆栈字段
            /// </summary>
            public Stack<string> StackField;

            /// <summary>
            /// 队列字段
            /// </summary>
            public Queue<int> QueueField;

            /// <summary>
            /// 链表字段
            /// </summary>
            public LinkedList<string> LinkedListField;

            #endregion

            #region 非泛型集合类型

            /// <summary>
            /// ArrayList字段
            /// </summary>
            public ArrayList ArrayListField;

            /// <summary>
            /// Hashtable字段
            /// </summary>
            public Hashtable HashtableField;

            #endregion

            #region 只读和并发集合类型

            /// <summary>
            /// 只读列表字段
            /// </summary>
            public IReadOnlyList<string> ReadOnlyListField;

            /// <summary>
            /// 只读字典字段
            /// </summary>
            public IReadOnlyDictionary<string, int> ReadOnlyDictionaryField;

            /// <summary>
            /// 并发字典字段
            /// </summary>
            public ConcurrentDictionary<string, int> ConcurrentDictionaryField;

            #endregion
        }

        #endregion
    }
}
