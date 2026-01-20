using System;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.CustomAttributes
{
    /// <summary>
    /// 要求字段或属性实现指定的接口类型。
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class RequiredInterfaceAttribute : Attribute
    {
        public readonly Type InterfaceType;

        public RequiredInterfaceAttribute(Type interfaceType)
        {
            // 类型必须是接口，如果为 True，则不会抛出异常，否则程序会在调试模式下抛出一个断言失败的错误并显示
            Debug.Assert(interfaceType.IsInterface, $"{nameof(interfaceType)} must be an interface");
            InterfaceType = interfaceType;
        }
    }
}
