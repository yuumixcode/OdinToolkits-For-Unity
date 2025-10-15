using Sirenix.OdinInspector;
using System;
using System.Reflection;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Modules;

namespace Yuumix.OdinToolkits.AdvancedTypeAnalyzer
{
    /// <summary>
    /// 事件数据接口，继承自 IDerivedMemberData，包含事件特有的数据信息和方法，派生类的通用数据信息和方法
    /// </summary>
    public interface IEventData : IDerivedMemberData
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        Type EventType { get; }

        /// <summary>
        /// 事件类型名称
        /// </summary>
        string EventTypeName { get; }

        /// <summary>
        /// 事件类型的完整名称，包括命名空间
        /// </summary>
        string EventTypeFullName { get; }

        /// <summary>
        /// 获取事件的签名，包括访问修饰符、静态修饰符、事件类型和事件名称
        /// </summary>
        /// <param name="accessModifier">访问修饰符字符串</param>
        /// <param name="isStatic">是否为静态</param>
        /// <param name="eventType">事件类型字符串</param>
        /// <param name="eventName">事件名称字符串</param>
        string GetEventSignature(string accessModifier, bool isStatic, string eventType, string eventName);
    }

    /// <summary>
    /// 事件解析数据类，用于存储事件的解析数据
    /// </summary>
    [Serializable]
    public class EventData : MemberData, IEventData
    {
        public EventData(EventInfo eventInfo, IAttributeFilter filter = null) : base(eventInfo, filter)
        {
            // IEventData
            EventType = eventInfo.EventHandlerType;
            EventTypeName = EventType.GetReadableTypeName();
            EventTypeFullName = EventType.GetReadableTypeName(true);
            // IDerivedMemberData 
            IsStatic = eventInfo.GetAddMethod(true).IsStatic;
            MemberType = eventInfo.MemberType;
            MemberTypeName = MemberType.ToString();
            AccessModifier = eventInfo.GetEventAccessModifierType();
            AccessModifierName = AccessModifier.ConvertToString();
            Signature = GetEventSignature(AccessModifierName, IsStatic, EventTypeName, Name);
            FullDeclarationWithAttributes = AttributesDeclaration + Signature;
        }

        #region IEventData 接口实现

        public Type EventType { get; }
        public string EventTypeName { get; }

        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("事件类型完整名称", nameof(EventTypeFullName))]
        [HideLabel]
        public string EventTypeFullName { get; }

        public string GetEventSignature(string accessModifier, bool isStatic, string eventType, string eventName)
        {
            var signature = accessModifier + " ";
            if (isStatic)
            {
                signature += "static ";
            }

            signature += $"event {eventType} {eventName};";
            return signature;
        }

        #endregion

        #region IDerivedMemberData 接口实现

        public bool IsStatic { get; }
        public MemberTypes MemberType { get; }
        public string MemberTypeName { get; }
        public AccessModifierType AccessModifier { get; }
        public string AccessModifierName { get; }

        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("事件签名", nameof(Signature))]
        [HideLabel]
        public string Signature { get; private set; }

        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("完整事件声明 - 包含特性和签名 - 默认剔除 [Summary] 特性",
            nameof(FullDeclarationWithAttributes) + " - Include Attributes and Signature - Default Exclude [Summary]")]
        [HideLabel]
        [MultiLineProperty]
        public string FullDeclarationWithAttributes { get; }

        #endregion
    }
}
