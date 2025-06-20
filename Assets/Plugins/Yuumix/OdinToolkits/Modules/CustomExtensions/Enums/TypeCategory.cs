namespace Yuumix.OdinToolkits.Modules.CustomExtensions
{
    /// <summary>
    /// 类型种类枚举
    /// </summary>
    public enum TypeCategory
    {
        Class,     // 类
        Struct,    // 结构体
        Interface, // 接口
        Enum,      // 枚举
        Delegate,  // 委托
        Unknown
    }

    // ===
    // class 类可以赋值 Type 类型的字段(默认已知)
    // enum 枚举可以赋值 Type 类型的字段
    // delegate 函数可以赋值 Type 类型的字段
    // interface 接口可以赋值 Type 类型的字段
    // struct 结构体可以赋值 Type 类型的字段
    // ===
    public delegate void OnTypeCategoryDelegate(TypeCategory typeCategory);

    public struct TypeCategoryStruct { }

    public interface ITypeCategoryInterface { }
}
