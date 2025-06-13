using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 测试类，包含各种类型的成员用于验证 MemberData 类的功能
/// </summary>
public class MemberDataForTest
{
    // 字段测试
    public int PublicField;
    private string PrivateField;
    protected float ProtectedField;
    internal bool InternalField;
    protected internal decimal ProtectedInternalField;
    private protected char PrivateProtectedField;
    
    // 常量测试
    public const double PI = 3.14159265358979323846;
    private const string SECRET = "This is a secret";
    
    // 静态字段测试
    public static int StaticPublicField;
    private static string StaticPrivateField;
    
    // 只读字段测试
    public readonly Vector3 ReadonlyVector = Vector3.zero;
    
    // 泛型字段测试
    public List<int> PublicGenericField;
    private Dictionary<string, float> PrivateGenericField;
    
    // Action委托字段测试
    public Action PublicAction;
    private Action PrivateAction;
    protected Action ProtectedAction;
    internal Action InternalAction;
    protected internal Action ProtectedInternalAction;
    private protected Action PrivateProtectedAction;
    
    // 带参数的Action委托字段测试
    public Action<int> PublicActionWithParams;
    private Action<string, float> PrivateActionWithParams;
    
    // Func委托字段测试
    public Func<int> PublicFunc;
    private Func<string, bool> PrivateFunc;
    protected Func<float, string, Vector3> ProtectedFunc;
    
    // Unity常用的委托类型
    public System.Action<GameObject> GameObjectAction;
    public System.Func<Transform, bool> TransformFunc;
    
    // 属性测试
    public int PublicProperty { get; set; }
    private string PrivateProperty { get; set; }
    protected float ProtectedProperty { get; set; }
    internal bool InternalProperty { get; set; }
    protected internal decimal ProtectedInternalProperty { get; set; }
    private protected char PrivateProtectedProperty { get; set; }
    
    // 带访问器限制的属性
    public int ReadonlyProperty { get; }
    public int WriteonlyProperty { private get; set; }
    
    // 泛型属性测试
    public List<string> PublicGenericProperty { get; set; }
    private Dictionary<int, string> PrivateGenericProperty { get; set; }
    
    // Action属性测试
    public Action PublicActionProperty { get; set; }
    private Action<int, string> PrivateActionProperty { get; set; }
    
    // Func属性测试
    public Func<float> PublicFuncProperty { get; set; }
    private Func<Vector3, Quaternion> PrivateFuncProperty { get; set; }
    
    // 方法测试
    public void PublicMethod() { }
    private void PrivateMethod() { }
    protected void ProtectedMethod() { }
    internal void InternalMethod() { }
    protected internal void ProtectedInternalMethod() { }
    private protected void PrivateProtectedMethod() { }
    
    // 静态方法测试
    public static void StaticPublicMethod() { }
    private static void StaticPrivateMethod() { }
    
    // 泛型方法测试
    public T GenericMethod<T>(T value) { return value; }
    public void GenericMethodWithConstraints<T>(T value) where T : new() { }
    
    // 带返回值的方法测试
    public string MethodWithReturnValue() { return ""; }
    public int MethodWithParameters(int a, int b) { return a + b; }
    
    // 带可选参数的方法测试
    public void MethodWithOptionalParameters(int a = 0, string b = "default") { }
    
    // 带params参数的方法测试
    public void MethodWithParams(int a, params string[] values) { }
    
    // 带out和ref参数的方法测试
    public void MethodWithOutParam(out int result) { result = 42; }
    public void MethodWithRefParam(ref int value) { value *= 2; }
    
    // Action参数方法测试
    public void MethodWithActionParam(Action action) { action?.Invoke(); }
    public void MethodWithActionParams(Action<int> action, int value) { action?.Invoke(value); }
    
    // Func参数方法测试
    public void MethodWithFuncParam(Func<bool> func) { bool result = func?.Invoke() ?? false; }
    public T MethodWithFuncParams<T>(Func<T> func) { return func != null ? func() ?? default : default; }
    
    // 返回Action的方法
    public Action ReturnAction() { return () => Debug.Log("Action called"); }
    public Action<int> ReturnActionWithParams() { return (value) => Debug.Log($"Action called with {value}"); }
    
    // 返回Func的方法
    public Func<string> ReturnFunc() { return () => "Result"; }
    public Func<float, int> ReturnFuncWithParams() { return (f) => (int)f; }
    
    // 构造函数测试
    public MemberDataForTest() { }
    private MemberDataForTest(int value) { }
    protected MemberDataForTest(string value) { }
    
    // 事件测试
    public event EventHandler PublicEvent;
    private event EventHandler PrivateEvent;
    protected event EventHandler ProtectedEvent;
    internal event EventHandler InternalEvent;
    protected internal event EventHandler ProtectedInternalEvent;
    private protected event EventHandler PrivateProtectedEvent;
    
    // Action事件测试
    public event Action PublicActionEvent;
    private event Action PrivateActionEvent;
    protected event Action ProtectedActionEvent;
    internal event Action InternalActionEvent;
    
    // 带参数的Action事件测试
    public event Action<int> PublicActionEventWithParams;
    private event Action<string, float> PrivateActionEventWithParams;
    
    // Unity常用的事件类型
    public event System.Action<GameObject> GameObjectEvent;
    public event System.Action<Vector3, Quaternion> TransformEvent;
    
    // 泛型事件测试
    public event EventHandler<EventArgs> PublicGenericEvent;
    
    // 委托字段测试
    public delegate void TestDelegate();
    public TestDelegate PublicDelegate;
    
    // 嵌套类型测试
    public class NestedPublicClass { }
    private class NestedPrivateClass { }
    protected class NestedProtectedClass { }
    
    // 索引器测试
    public string this[int index]
    {
        get { return $"Index {index}"; }
        set { /* 实现代码 */ }
    }
    
    // 运算符重载测试
    public static MemberDataForTest operator +(MemberDataForTest a, MemberDataForTest b)
    {
        return new MemberDataForTest();
    }
    
    // 泛型嵌套类型测试
    public class NestedGenericClass<T> { }
    
    // 接口实现测试
    public void InterfaceMethod() { }
    
    // 枚举测试
    public enum TestEnum { Value1, Value2 }
    
    // 带本地化注释的成员
    [LocalizedComment("中文说明", "English description")]
    public int AnnotatedField;
}

// 接口定义
public interface ITestInterface
{
    void InterfaceMethod();
}
