using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class PolymorphicDrawerSettingsContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "PolymorphicDrawerSettings";

        protected override string GetIntroduction() => "设置多态字段的绘制样式";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "Unity 默认是无法序列化多态类型的（接口，抽象类），但是可以使用 Odin 序列化",
                "对于接口一定要注意是否采用了 Odin 序列化，如果选择了 EditorOnly 序列化，则构建时将会剔除 Odin 序列化的部分，不要将序列化写入业务逻辑",
                "该 Example 采用了 Odin 序列化"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "ShowBaseType",
                    ParameterDescription = "是否显示基类字段，默认为 false"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "ReadOnlyIfNotNullReference",
                    ParameterDescription = "如果引用不为空，是否只读，默认为 false"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "CreateInstanceFunction",
                    ParameterDescription = "自定义创建实例的函数名，默认为 null"
                },
                new ParameterValue
                {
                    ReturnType = "NonDefaultConstructorPreference 枚举",
                    ParameterName = "NonDefaultConstructorPreference",
                    ParameterDescription = "没有默认构造函数的处理设置"
                },
                new ParameterValue
                {
                    ReturnType = ">>> NonDefaultConstructorPreference 枚举",
                    ParameterName = "NonDefaultConstructorPreference.Exclude",
                    ParameterDescription = "直接剔除没有默认构造函数的类，使其无法点击创建对象"
                },
                new ParameterValue
                {
                    ReturnType = ">>> NonDefaultConstructorPreference 枚举",
                    ParameterName = "NonDefaultConstructorPreference.ConstructIdeal",
                    ParameterDescription = "如果找不到默认构造函数，尝试使用最简单直接的一个构造函数创建对象，优先设置对应字段的默认值"
                },
                new ParameterValue
                {
                    ReturnType = ">>> NonDefaultConstructorPreference 枚举",
                    ParameterName = "NonDefaultConstructorPreference.PreferUninitialized",
                    ParameterDescription = "如果找不到默认构造函数，调用 C# 的 GetUninitializedObject(Type) "
                },

                new ParameterValue
                {
                    ReturnType = ">>> NonDefaultConstructorPreference 枚举",
                    ParameterName = "NonDefaultConstructorPreference.LogWarning",
                    ParameterDescription = "如果选择的类没有默认构造函数，则在点击后发出警告"
                }
            };

        protected override string GetOriginalCode() =>
            ReadCodeWithoutNamespace(typeof(PolymorphicDrawerSettingsExample));
    }
}
