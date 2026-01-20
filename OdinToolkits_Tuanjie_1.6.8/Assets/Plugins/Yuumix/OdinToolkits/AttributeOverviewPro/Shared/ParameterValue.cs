using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Shared
{
    public class ParameterValue
    {
        readonly BilingualData _parameterDescriptionData;

        public ParameterValue(string returnType, string parameterName, string parameterDescription)
        {
            ReturnType = returnType;
            ParameterName = parameterName;
            ParameterDescription = parameterDescription;
            _parameterDescriptionData = BilingualData.Empty;
        }

        public ParameterValue(string returnType, string parameterName, BilingualData parameterDescriptionData)
        {
            ReturnType = returnType;
            ParameterName = parameterName;
            _parameterDescriptionData = parameterDescriptionData;
            ParameterDescription = string.Empty;
        }

        public ParameterValue() { }
        public string ReturnType { get; set; }
        public string ParameterName { get; set; }
        public string ParameterDescription { get; set; }

        public string GetDescription() =>
            _parameterDescriptionData != BilingualData.Empty
                ? _parameterDescriptionData.GetCurrentOrFallback()
                : ParameterDescription;
    }
}
