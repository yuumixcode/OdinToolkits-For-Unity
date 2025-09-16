using UnityEngine;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Modules;

/// <summary>
/// 这是一个用于测试XML注释提取功能的测试脚本
/// </summary>
[ChineseSummary("这是一个用于测试XML注释提取功能的测试脚本")]
public class XMLConverterTestScript : MonoBehaviour
{
    /// <summary>
    /// 这是一个测试的静态字段
    /// </summary>
    [ChineseSummary("这是一个测试的静态字段")]
    public static float testStaticField = 1.0f;

    /// <summary>
    /// 这是一个测试字段
    /// </summary>
    [ChineseSummary("这是一个测试字段")]
    public int testField;

    /// <summary>
    /// 这是一个测试的只读字段
    /// </summary>
    [ChineseSummary("这是一个测试的只读字段")]
    public readonly string testReadonlyField = "test";

    /// <summary>
    /// 这是一个测试属性
    /// </summary>
    [ChineseSummary("这是一个测试属性")]
    public string TestProperty { get; set; }

    /// <summary>
    /// 这是一个测试的只读属性
    /// </summary>
    [ChineseSummary("这是一个测试的只读属性")]
    public int TestReadonlyProperty { get; private set; }

    /// <summary>
    /// 这是一个测试的静态属性
    /// </summary>
    [ChineseSummary("这是一个测试的静态属性")]
    public static bool TestStaticProperty { get; set; }

    /// <summary>
    /// 这是一个测试方法
    /// </summary>
    [ChineseSummary("这是一个测试方法")]
    public void TestMethod()
    {
        Debug.Log("TestMethod executed");
    }

    /// <summary>
    /// 这是一个带参数的测试方法
    /// </summary>
    /// <param name="value">输入值</param>
    /// <returns>返回值</returns>
    [ChineseSummary("这是一个带参数的测试方法")]
    public int TestMethodWithParams(int value) => value * 2;

    /// <summary>
    /// 这是一个静态测试方法
    /// </summary>
    [ChineseSummary("这是一个静态测试方法")]
    public static void TestStaticMethod()
    {
        Debug.Log("TestStaticMethod executed");
    }

    /// <summary>
    /// 这是一个带泛型参数的方法
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    /// <param name="item">输入项</param>
    /// <returns>返回项</returns>
    [ChineseSummary("这是一个带泛型参数的方法")]
    public T TestGenericMethod<T>(T item) => item;
}
