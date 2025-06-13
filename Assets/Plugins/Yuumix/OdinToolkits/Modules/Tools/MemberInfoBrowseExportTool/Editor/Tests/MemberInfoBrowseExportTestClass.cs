using System;
using UnityEngine;
using Object = UnityEngine.Object;

#pragma warning disable CS0067 // 事件从未使用过
#pragma warning disable CS0414 // 字段已被赋值，但它的值从未被使用

namespace Yuumix.OdinToolkits.Modules.Tools.MemberInfoBrowseExportTool.Editor.Tests
{
    public abstract class MemberInfoBrowseExportTestClassBase : Object
    {
        #region 字段（Field）

        // 公共静态整型字段
        public static int PublicStaticIntField = 10;

        // 公共整型字段
        public int PublicIntField = 20;

        // 私有字符串字段
        string PrivateStringField = "Private String";

        // 受保护的浮点型字段
        protected float ProtectedFloatField = 3.14f;

        // 内部布尔型字段
        internal bool InternalBoolField = true;

        // 公共静态字符串字段
        public static string PublicStaticStringField = "Public Static String";

        // 公共布尔型字段
        public bool PublicBoolField = false;

        // 私有浮点型字段
        float PrivateFloatField = 1.23f;

        // 受保护的字符串字段
        protected string ProtectedStringField = "Protected String";

        // 内部整型字段
        internal int InternalIntField = 50;

        #endregion

        #region 属性（Property）

        // 公共自动属性，字符串类型
        public string PublicAutoProperty { get; set; } = "Default Value";

        // 私有属性，整型
        int PrivateProperty { get; } = 50;

        // 受保护的只读属性，将其声明为 virtual，以便派生类可以重写
        protected virtual float ProtectedReadOnlyProperty => ProtectedFloatField * 2;

        // 内部可读写属性，布尔型
        internal bool InternalProperty { get; set; } = false;

        // 公共静态属性，字符串类型
        public static string PublicStaticStringProperty { get; set; } = "Public Static Property";

        // 公共只读属性，整型
        public int PublicReadOnlyIntProperty => PublicIntField * 2;

        // 私有可读写属性，浮点型
        float PrivateFloatProperty { get; set; } = 2.34f;

        // 受保护的可读写属性，字符串类型
        protected string ProtectedStringProperty { get; set; } = "Protected Property";

        // 内部只读属性，布尔型
        internal bool InternalReadOnlyBoolProperty => InternalBoolField;

        // 添加一个受保护的方法来访问 PrivateProperty
        protected int GetPrivatePropertyValue() => PrivateProperty;

        #endregion

        #region 构造函数（Constructor）

        // 公共无参构造函数
        public MemberInfoBrowseExportTestClassBase()
        {
            Debug.Log("Public parameterless constructor called");
        }

        // 受保护的有参构造函数
        protected MemberInfoBrowseExportTestClassBase(int value)
        {
            PublicIntField = value;
            Debug.Log($"Protected constructor with parameter {value} called");
        }

        #endregion

        #region 方法（Method）

        // 公共无返回值方法
        public void PublicVoidMethod()
        {
            Debug.Log("PublicVoidMethod called");
        }

        // 受保护的有返回值方法，返回字符串
        protected string ProtectedStringMethod() => "ProtectedStringMethod result";

        // 内部静态方法，返回布尔型
        internal static bool InternalStaticBoolMethod() => true;

        // 抽象方法，返回整型
        public abstract int AbstractIntMethod();

        // 公共虚方法，返回字符串
        public virtual string PublicVirtualStringMethod() => "Public virtual string method result";

        // 受保护的虚方法，无返回值
        protected virtual void ProtectedVirtualVoidMethod()
        {
            Debug.Log("Protected virtual void method called");
        }

        #endregion

        #region 事件（Event）

        // 公共事件，使用 EventHandler
        public event EventHandler PublicEvent;

        // 受保护的事件，使用 Action
        protected event Action ProtectedActionEvent;

        // Func<string> 事件
        protected event Func<string> ProtectedFuncStringEvent;

        protected virtual void OnPublicEvent()
        {
            PublicEvent?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnProtectedActionEvent()
        {
            ProtectedActionEvent?.Invoke();
        }

        #endregion
    }

    // 实现类
    public class MemberInfoBrowseExportTestClass : MemberInfoBrowseExportTestClassBase
    {
        // 嵌套类
        public class NestedPublicClass
        {
            #region 字段（Field）

            // 公共整型字段
            public int PublicNestedIntField = 100;

            // 私有字符串字段
            string PrivateNestedStringField = "Nested Private String";

            #endregion

            #region 方法（Method）

            // 公共无返回值方法
            public void PublicNestedVoidMethod()
            {
                Debug.Log("PublicNestedVoidMethod called");
            }

            // 公共有返回值方法，返回布尔型
            public bool PublicNestedBoolMethod() => true;

            #endregion
        }

        #region 字段（Field）

        // 新增的公共静态布尔型字段
        public static bool PublicStaticBoolField = false;

        // 新增的私有浮点型字段
        readonly float PrivateFloatField = 1.23f;

        // 无参数 Action 委托
        public Action NoParamAction;

        // 无参数 Func 委托，返回一个字符串
        public Func<string> NoParamFunc;

        #endregion

        #region 属性（Property）

        // 重写受保护的只读属性
        protected override float ProtectedReadOnlyProperty => PrivateFloatField * 3;

        // 公共自动属性，浮点型
        public float PublicAutoFloatProperty { get; set; } = 3.45f;

        // 私有属性，字符串类型
        string PrivateStringProperty { get; set; } = "Private String Property";

        // 受保护的可读写属性，布尔型
        protected bool ProtectedBoolProperty { get; set; } = true;

        // 内部只读属性，整型
        internal int InternalReadOnlyIntProperty => InternalIntField * 2;

        #endregion

        #region 构造函数（Constructor）

        // 公共无参构造函数
        public MemberInfoBrowseExportTestClass()
        {
            Debug.Log("Public parameterless constructor of derived class called");
        }

        // 公共有参构造函数
        public MemberInfoBrowseExportTestClass(int value) : base(value)
        {
            Debug.Log($"Public constructor with parameter {value} of derived class called");
        }

        // 私有构造函数
        MemberInfoBrowseExportTestClass(string str)
        {
            PrivateStringProperty = str;
            Debug.Log($"Private constructor with parameter {str} of derived class called");
        }

        #endregion

        #region 方法（Method）

        // 实现抽象方法，通过受保护的方法访问 PrivateProperty
        public override int AbstractIntMethod() => PublicIntField + GetPrivatePropertyValue();

        // 重写公共虚方法
        public override string PublicVirtualStringMethod() => "Overridden public virtual string method result";

        // 重写受保护的虚方法
        protected override void ProtectedVirtualVoidMethod()
        {
            Debug.Log("Overridden protected virtual void method called");
        }

        // 公共有返回值方法，返回浮点型
        public float PublicFloatMethod() => PrivateFloatField + ProtectedFloatField;

        // 运算符重载
        public static MemberInfoBrowseExportTestClass operator +(MemberInfoBrowseExportTestClass a, MemberInfoBrowseExportTestClass b)
        {
            var result = new MemberInfoBrowseExportTestClass();
            result.PublicIntField = a.PublicIntField + b.PublicIntField;
            return result;
        }

        // 调用无参数 Action 委托的方法
        public void CallNoParamAction()
        {
            NoParamAction?.Invoke();
        }

        // 调用无参数 Func 委托的方法
        public string CallNoParamFunc() => NoParamFunc != null ? NoParamFunc() : null;

        #endregion

        #region 委托（Delegate）

        // 公共 Action 委托字段
        public Action<int> PublicIntAction;

        // 私有 Func 委托字段
        Func<string, bool> PrivateStringFunc;

        public void CallPublicIntAction(int value)
        {
            PublicIntAction?.Invoke(value);
        }

        public bool CallPrivateStringFunc(string input) => PrivateStringFunc != null ? PrivateStringFunc(input) : false;

        #endregion
    }
}
