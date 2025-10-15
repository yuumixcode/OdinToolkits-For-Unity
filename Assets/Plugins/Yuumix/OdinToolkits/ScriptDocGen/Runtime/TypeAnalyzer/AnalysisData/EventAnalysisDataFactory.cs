using System;
using System.Reflection;
using System.Text;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules
{
    public class EventAnalysisDataFactory : IEventAnalysisDataFactory
    {
        #region IEventAnalysisDataFactory Members

        public EventAnalysisData CreateFromEventInfo(EventInfo eventInfo, Type type)
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
            var attributesObj = eventInfo.GetCustomAttributes(false);
            foreach (var attr in attributesObj)
            {
                var attributeName = attr.GetType().Name;
                if (attributeName.EndsWith("Attribute"))
                {
                    attributeName = attributeName[..^"Attribute".Length];
                }

                declarationStringBuilder.AppendLine($"[{attributeName}]");
            }

            declarationStringBuilder.Append(eventData.fullSignature);
            eventData.fullDeclaration = declarationStringBuilder.ToString();

            // Summary
            eventData.chineseSummary = SummaryAttribute.GetSummary(eventInfo) ?? "无";
            return eventData;
        }

        #endregion
    }
}
