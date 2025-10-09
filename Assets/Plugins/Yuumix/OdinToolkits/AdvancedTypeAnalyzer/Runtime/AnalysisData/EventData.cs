using System;
using System.Reflection;
using Yuumix.OdinToolkits.Core;

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
    public class EventData : MemberData, IEventData
    {
        public EventData(EventInfo eventInfo) : base(eventInfo)
        {
            IsStatic = eventInfo.GetAddMethod(true).IsStatic;
            AccessModifier = eventInfo.GetEventAccessModifierType();
            MemberType = eventInfo.MemberType;
            // --- 事件特有信息 ---
            EventType = eventInfo.EventHandlerType;
            Signature = GetEventSignature(AccessModifierName, IsStatic, EventTypeName, Name);
        }

        #region IEventData 接口实现

        public Type EventType { get; }
        public string EventTypeName => EventType.GetReadableTypeName();
        public string EventTypeFullName => EventType.GetReadableTypeName(true);

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
        public string MemberTypeName => MemberType.ToString();
        public AccessModifierType AccessModifier { get; }
        public string AccessModifierName => AccessModifier.ConvertToString();
        public string Signature { get; }
        public string FullDeclarationWithAttributes => AttributesDeclaration + Signature;

        #endregion
    }
}
