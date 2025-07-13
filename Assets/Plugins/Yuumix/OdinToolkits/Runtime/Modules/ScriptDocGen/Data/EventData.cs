using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Shared;

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
                returnType = eventInfo.GetReadableEventReturnType(),
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
            IEnumerable<Attribute> attributes = eventInfo.GetCustomAttributes();
            if (attributes
                    .First(x => typeof(IMultiLanguageComment).IsAssignableFrom(x.GetType())) is not
                IMultiLanguageComment comment)
            {
                eventData.chineseComment = "无";
                eventData.englishComment = "No Comment";
                return eventData;
            }

            eventData.chineseComment = comment.GetChineseComment();
            eventData.englishComment = comment.GetEnglishComment();
            return eventData;
        }
    }
}
