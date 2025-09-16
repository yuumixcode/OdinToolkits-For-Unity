using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.Utilities;
using Yuumix.OdinToolkits.Modules.CustomAttributes;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.Editor
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
                memberAccessModifierType = TypeAnalyzerUtility.GetEventAccessModifierType(eventInfo),
                returnType = TypeAnalyzerUtility.GetReadableEventReturnType(eventInfo),
                isStatic = eventInfo.IsStatic(),
                isObsolete = eventInfo.IsDefined(typeof(ObsoleteAttribute)),
                name = eventInfo.Name
            };

            eventData.accessModifier = eventData.memberAccessModifierType.ConvertToString();
            var keyword = "";
            if (eventData.isStatic)
            {
                keyword = "static ";
            }

            eventData.fullSignature =
                eventData.accessModifier + " " + keyword + "event " + eventData.returnType + " " + eventData.name;
            // Summary
            eventData.chineseSummary = ChineseSummaryAttribute.GetChineseSummary(eventInfo) ?? "无";
            return eventData;
        }
    }
}
