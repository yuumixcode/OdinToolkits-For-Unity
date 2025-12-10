using System;
using System.Reflection;
using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    /// <summary>
    /// 事件解析数据类，用于存储事件的解析数据
    /// </summary>
    [Summary("事件解析数据类，用于存储事件的解析数据")]
    [Serializable]
    public class EventData : MemberData, IEventData
    {
        public EventData(EventInfo eventInfo, IAttributeFilter filter = null) : base(eventInfo,
            filter)
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

        static string GetEventSignature(string accessModifier, bool isStatic, string eventType,
            string eventName)
        {
            var signature = accessModifier + " ";
            if (isStatic)
            {
                signature += "static ";
            }

            signature += $"event {eventType} {eventName};";
            return signature;
        }

        #region IDerivedMemberData

        [Summary("是否为静态事件")]
        public bool IsStatic { get; }

        [Summary("成员类型")]
        public MemberTypes MemberType { get; }

        [Summary("成员类型名称")]
        public string MemberTypeName { get; }

        [Summary("访问修饰符类型")]
        public AccessModifierType AccessModifier { get; }

        [Summary("访问修饰符名称")]
        public string AccessModifierName { get; }

        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("事件签名", nameof(Signature))]
        [HideLabel]
        [Summary("事件的完整签名")]
        public string Signature { get; private set; }

        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("完整事件声明 - 包含特性和签名 - 默认剔除 [Summary] 特性",
            nameof(FullDeclarationWithAttributes) +
            " - Include Attributes and Signature - Default Exclude [Summary]")]
        [HideLabel]
        [MultiLineProperty]
        [Summary("包含特性和签名的完整事件声明")]
        public string FullDeclarationWithAttributes { get; }

        #endregion

        #region IEventData

        [Summary("事件类型")]
        public Type EventType { get; }

        [Summary("事件类型名称")]
        public string EventTypeName { get; }

        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("事件类型完整名称", nameof(EventTypeFullName))]
        [HideLabel]
        [Summary("事件类型的完整名称，包括命名空间")]
        public string EventTypeFullName { get; }

        #endregion
    }
}
