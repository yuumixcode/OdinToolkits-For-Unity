using System;
using System.Reflection;
using System.Text;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules
{
    [Serializable]
    public class EventAnalysisData : MemberAnalysisData
    {
        public bool isStatic;

        public static EventAnalysisData FromEventInfo(EventInfo eventInfo, Type type)
        {
            var eventData = new EventAnalysisData
            {
                memberType = MemberTypes.Event,
                belongToType = type.FullName,
                declaringType = eventInfo.DeclaringType?.FullName,
                memberAccessModifierType = TypeAnalyzerUtility.GetEventAccessModifierType(eventInfo),
                returnType = TypeAnalyzerUtility.GetReadableEventReturnType(eventInfo),
                isObsolete = eventInfo.IsDefined(typeof(ObsoleteAttribute)),
                name = eventInfo.Name
            };
            TypeAnalyzerUtility.IsStaticEvent(eventInfo, eventData);
            var keyword = "";
            if (eventData.isStatic)
            {
                keyword = "static ";
            }

            eventData.fullSignature =
                eventData.AccessModifier + " " + keyword + "event " + eventData.returnType + " " + eventData.name;
            eventData.fullSignature += ";";
            eventData.partSignature = eventData.fullSignature;
            var declarationStringBuilder = new StringBuilder();
            object[] attributesObj = eventInfo.GetCustomAttributes(false);
            foreach (object attr in attributesObj)
            {
                string attributeName = attr.GetType().Name;
                if (attributeName.EndsWith("Attribute"))
                {
                    attributeName = attributeName[..^"Attribute".Length];
                }

                declarationStringBuilder.AppendLine($"[{attributeName}]");
            }

            declarationStringBuilder.Append(eventData.fullSignature);
            eventData.fullDeclaration = declarationStringBuilder.ToString();
            // Summary
            eventData.chineseSummary = ChineseSummaryAttribute.GetChineseSummary(eventInfo) ?? "无";
            return eventData;
        }
    }
}
