using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class PolymorphicDrawerSettingsContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "PolymorphicDrawerSettings";
        }

        protected override string SetBrief()
        {
            return "设置多态字段的绘制样式";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "Unity 默认是无法序列化多态类型的（接口，抽象类），但是可以使用 Odin 序列化",
                "对于接口一定要注意是否采用了 Odin 序列化，如果选择了 EditorOnly 序列化，则构建时将会剔除 Odin 序列化的部分，不要将序列化写入业务逻辑",
                "该 Example 采用了 Odin 序列化"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "bool",
                    paramName = "ShowBaseType",
                    paramDescription = "是否显示基类字段，默认为 false"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "ReadOnlyIfNotNullReference",
                    paramDescription = "如果引用不为空，是否只读，默认为 false"
                },
                new()
                {
                    returnType = "string",
                    paramName = "CreateInstanceFunction",
                    paramDescription = "自定义创建实例的函数名，默认为 null"
                },
                new()
                {
                    returnType = "NonDefaultConstructorPreference 枚举",
                    paramName = "NonDefaultConstructorPreference",
                    paramDescription = "没有默认构造函数的处理设置"
                },
                new()
                {
                    returnType = ">>> NonDefaultConstructorPreference 枚举",
                    paramName = "NonDefaultConstructorPreference.Exclude",
                    paramDescription = "直接剔除没有默认构造函数的类，使其无法点击创建对象"
                },
                new()
                {
                    returnType = ">>> NonDefaultConstructorPreference 枚举",
                    paramName = "NonDefaultConstructorPreference.ConstructIdeal",
                    paramDescription = "如果找不到默认构造函数，尝试使用最简单直接的一个构造函数创建对象，优先设置对应字段的默认值"
                },
                new()
                {
                    returnType = ">>> NonDefaultConstructorPreference 枚举",
                    paramName = "NonDefaultConstructorPreference.PreferUninitialized",
                    paramDescription = "如果找不到默认构造函数，调用 C# 的 GetUninitializedObject(Type) "
                },

                new()
                {
                    returnType = ">>> NonDefaultConstructorPreference 枚举",
                    paramName = "NonDefaultConstructorPreference.LogWarning",
                    paramDescription = "如果选择的类没有默认构造函数，则在点击后发出警告"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(PolymorphicDrawerSettingsExample));
        }
    }
}