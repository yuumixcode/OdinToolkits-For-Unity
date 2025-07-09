using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Modules.Utilities;

namespace Yuumix.OdinToolkits
{
    [Serializable]
    public class EventData : MemberData
    {
        public static EventData FromEventInfo(EventInfo eventInfo, Type type)
        {
            var eventData = new EventData
            {
                memberType = MemberTypes.Event,
                belongToType = type.FullName,
                declaringType = eventInfo.DeclaringType?.FullName,
                memberAccessModifierType = eventInfo.GetEventAccessModifierType(),
                returnType = ReflectionUtil.GetReadableEventReturnType(eventInfo),
                isStatic = eventInfo.IsStaticEvent(),
                isObsolete = eventInfo.IsDefined(typeof(ObsoleteAttribute)),
                name = eventInfo.Name
            };

            eventData.accessModifier = eventData.memberAccessModifierType.GetAccessModifierString();
            var keyword = "";
            if (eventData.isStatic)
            {
                keyword = "static ";
            }

            eventData.fullSignature =
                eventData.accessModifier + " " + keyword + "event " + eventData.returnType + " " + eventData.name;
            if (eventInfo.GetCustomAttribute<MultiLanguageCommentAttribute>() == null)
            {
                eventData.chineseComment = "无";
                eventData.englishComment = "No Comment";
                return eventData;
            }

            var commentAttr = eventInfo.GetCustomAttribute<MultiLanguageCommentAttribute>();
            eventData.chineseComment = commentAttr.ChineseComment;
            eventData.englishComment = commentAttr.EnglishComment;
            return eventData;
        }
    }
}
