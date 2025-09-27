using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

#pragma warning disable CS0414 // 字段已被赋值，但它的值从未被使用

namespace Yuumix.OdinToolkits.Tests
{
    public abstract class AbstractTestFieldClass : MonoBehaviour
    {
        protected int NewField;
    }

    /// <summary>
    /// 包含所有可能字段样式的测试类
    /// </summary>
    public class TestFieldDataClass : AbstractTestFieldClass
    {

        #region 基本类型字段
        
       
        #endregion

        #region 引用类型字段

        /// <summary>
        /// 字符串字段
        /// </summary>
        public string stringField = "Hello World";

        /// <summary>
        /// 数组字段
        /// </summary>
        public int[] arrayField = new int[] { 1, 2, 3, 4, 5 };

        /// <summary>
        /// 多维数组字段
        /// </summary>
        public int[,] MultiArrayField = new int[,] { { 1, 2 }, { 3, 4 } };

        /// <summary>
        /// 交错数组字段
        /// </summary>
        public int[][] JaggedArrayField = new int[][] { new int[] { 1, 2 }, new int[] { 3, 4, 5 } };

        /// <summary>
        /// 列表字段
        /// </summary>
        public List<string> listField = new List<string> { "Item1", "Item2", "Item3" };

        /// <summary>
        /// 字典字段
        /// </summary>
        public Dictionary<string, int> DictionaryField = new Dictionary<string, int>
        {
            { "Key1", 1 },
            { "Key2", 2 },
            { "Key3", 3 }
        };

        /// <summary>
        /// 枚举字段
        /// </summary>
        public TestValue valueField = TestValue.Value2;

        /// <summary>
        /// 事件字段
        /// </summary>
        public event EventHandler TestEvent;

        #endregion

        #region 泛型类型字段

        /// <summary>
        /// 复杂泛型字段
        /// </summary>
        public Dictionary<string, List<int>> ComplexGenericField = new Dictionary<string, List<int>>
        {
            { "Group1", new List<int> { 1, 2, 3 } },
            { "Group2", new List<int> { 4, 5, 6 } }
        };

        /// <summary>
        /// 嵌套泛型字段
        /// </summary>
        public List<Dictionary<string, int>> NestedGenericField = new List<Dictionary<string, int>>
        {
            new Dictionary<string, int> { { "A", 1 }, { "B", 2 } },
            new Dictionary<string, int> { { "C", 3 }, { "D", 4 } }
        };

        /// <summary>
        /// 自定义泛型字段
        /// </summary>
        public GenericWrapper<int> CustomGenericField = new GenericWrapper<int>(42);

        #endregion

        #region Unity 特有字段

        /// <summary>
        /// 序列化字段
        /// </summary>
        [SerializeField]
        int serializedField = 100;

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
        public Vector3 vector3Field = Vector3.one;

        /// <summary>
        /// Quaternion 字段
        /// </summary>
        public Quaternion quaternionField = Quaternion.identity;

        /// <summary>
        /// Color 字段
        /// </summary>
        public Color colorField = Color.white;

        /// <summary>
        /// LayerMask 字段
        /// </summary>
        public LayerMask layerMaskField;

        #endregion

        #region 带特性的字段

        /// <summary>
        /// 带多个特性的字段
        /// </summary>
        [SerializeField]
        [Tooltip("This is a tooltip")]
        [Range(0, 100)]
        public float fieldWithMultipleAttributes = 50f;

        /// <summary>
        /// 带颜色特性的字段
        /// </summary>
        [ColorUsage(true, true)]
        public Color playerColor = Color.red;

        /// <summary>
        /// 带非序列化特性的字段
        /// </summary>
        [NonSerialized]
        public int NonSerializedField = 999;

        /// <summary>
        /// 带隐藏特性的字段
        /// </summary>
        [HideInInspector]
        public int hiddenField = 888;

        /// <summary>
        /// 带旧特性标记的字段
        /// </summary>
        [Obsolete("Use newField instead")]
        public int obsoleteField = 777;

        #endregion

        #region 边界情况字段

        /// <summary>
        /// 可空字段
        /// </summary>
        public int? NullableField = null;

        /// <summary>
        /// 动态字段
        /// </summary>
        public dynamic DynamicField = "Dynamic Value";

        /// <summary>
        /// 接口字段
        /// </summary>
        public ITestInterface InterfaceField;

        /// <summary>
        /// 抽象类字段
        /// </summary>
        public TestAbstractClass AbstractField;

        /// <summary>
        /// 新字段（隐藏基类字段）
        /// </summary>
        public new int NewField = 300;

        /// <summary>
        /// volatile 字段
        /// </summary>
        public volatile int volatileField = 0;

        #endregion

        #region 常量字段组

        /// <summary>
        /// 字符串常量
        /// </summary>
        public const string STRING_CONSTANT = "Constant String";

        /// <summary>
        /// 布尔常量
        /// </summary>
        public const bool BOOL_CONSTANT = true;

        /// <summary>
        /// 字符常量
        /// </summary>
        public const char CHAR_CONSTANT = 'Z';

        /// <summary>
        /// 浮点常量
        /// </summary>
        public const float FLOAT_CONSTANT = 3.14159f;

        /// <summary>
        /// 双精度常量
        /// </summary>
        public const double DOUBLE_CONSTANT = 2.71828;

        /// <summary>
        /// 十进制常量
        /// </summary>
        public const decimal DECIMAL_CONSTANT = 1.61803m;

        #endregion

        #region 事件和委托相关字段

        /// <summary>
        /// Action 字段
        /// </summary>
        public Action ActionField;

        /// <summary>
        /// Action 带参数字段
        /// </summary>
        public Action<int, string> ActionWithParamsField;

        /// <summary>
        /// Func 字段
        /// </summary>
        public Func<bool> FuncField;

        /// <summary>
        /// Func 带参数字段
        /// </summary>
        public Func<int, string, bool> FuncWithParamsField;

        /// <summary>
        /// Predicate 字段
        /// </summary>
        public Predicate<int> PredicateField;

        /// <summary>
        /// Comparison 字段
        /// </summary>
        public Comparison<string> ComparisonField;

        #endregion

        #region 测试方法

        /// <summary>
        /// 触发测试事件
        /// </summary>
        public void TriggerTestEvent()
        {
            TestEvent?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 获取字段信息
        /// </summary>
        /// <returns>所有字段的名称列表</returns>
        public List<string> GetAllFieldNames()
        {
            var fieldNames = new List<string>();
            FieldInfo[] fields = GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic |
                                                     BindingFlags.Instance |
                                                     BindingFlags.Static);

            foreach (FieldInfo field in fields)
            {
                fieldNames.Add(field.Name);
            }

            return fieldNames;
        }

        #endregion
    }

    /// <summary>
    /// 测试枚举
    /// </summary>
    public enum TestValue
    {
        Value1 = 0,
        Value2 = 1
    }

    /// <summary>
    /// 测试接口
    /// </summary>
    public interface ITestInterface { }

    /// <summary>
    /// 测试抽象类
    /// </summary>
    public abstract class TestAbstractClass
    {
        public abstract void AbstractMethod();
    }

    /// <summary>
    /// 泛型包装器类
    /// </summary>
    /// <typeparam name="T">泛型类型参数</typeparam>
    public class GenericWrapper<T>
    {
        public T Value { get; }

        public GenericWrapper(T value) => Value = value;
    }
}
