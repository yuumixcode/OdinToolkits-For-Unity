﻿using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Yuumix.OdinToolkits.Core.Runtime
{
    /// <summary>
    /// 基于 Odin 的 InterfaceReference 自定义类，接口引用类
    /// </summary>
    /// <typeparam name="TInterface">
    /// 此处表示必须为接口，class 约束确保 TInterface 必须是一个引用类型。
    /// 引用类型包括类（class）、接口（interface）、委托（delegate）和数组。
    /// </typeparam>
    /// <typeparam name="TObject">
    /// 需要实现对象或者资产拖拽操作，被分配的对象实例的类型必须为 Unity 的引用类型，
    /// 即 UnityEngine.Object 的派生类
    /// </typeparam>
    [Serializable]
    public class InterfaceReference<TInterface, TObject>
        where TInterface : class where TObject : Object
    {
        /// <summary>
        /// 基础对象，用于存储可以被分配为 TInterface 类型字段的，继承自 UnityEngine.Object 的对象
        /// </summary>
        [SerializeField]
        TObject underlyingObject;

        // 构造函数
        public InterfaceReference() { }
        public InterfaceReference(TInterface interfaceValue) => underlyingObject = interfaceValue as TObject;
        public InterfaceReference(TObject target) => underlyingObject = target;

        /// <summary>
        /// 基础对象
        /// </summary>
        public TObject UnderlyingObject
        {
            get => underlyingObject;
            set => underlyingObject = value;
        }

        /// <summary>
        /// 接口类型对象
        /// </summary>
        /// <exception cref="InvalidCastException"></exception>
        public TInterface InterfaceValue
        {
            get
            {
                // 如果没有被分配对象，则返回 null
                if (!underlyingObject)
                {
                    return null;
                }

                // 判断是否实现了 TInterface 接口，这个模式匹配语句不一定需要原生接口类型，派生类型也成立
                if (underlyingObject is TInterface @interface)
                {
                    return @interface;
                }

                throw new InvalidCastException(
                    $"{underlyingObject} needs to implement interface {nameof(TInterface)}");
            }
            set
            {
                // if ( value is TObject obj )
                // 判断 value 是否为 TObject 类型或者是其派生类，是则赋值，否则抛出异常
                underlyingObject = value switch
                {
                    null => null,
                    TObject obj => obj,
                    _ => throw new InvalidCastException($"{value} needs to be of type {typeof(TObject)}")
                };
            }
        }
    }

    /// <summary>
    /// Unity 的简化版本，因为 Unity 中的所有类都继承自 UnityEngine.Object，
    /// 这是一个适用于 Unity 的通用类型，除非有特殊要求，否则使用这个更简单的版本,仅需一个泛型
    /// </summary>
    [Serializable]
    public class InterfaceReference<TInterface> : InterfaceReference<TInterface, Object>
        where TInterface : class { }
}
