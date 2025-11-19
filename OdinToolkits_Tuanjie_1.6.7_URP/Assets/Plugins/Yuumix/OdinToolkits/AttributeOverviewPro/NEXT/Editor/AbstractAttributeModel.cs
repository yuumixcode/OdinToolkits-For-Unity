using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;
using YuumixEditor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    public abstract class AbstractAttributeModel
    {
        public BilingualHeaderWidget HeaderWidget { get; protected set; }

        public List<string> UsageTips { get; protected set; }

        public List<ParameterValue> AttributeParameters { get; protected set; }

        public List<ResolvedStringParameterValue> ResolvedStringParameters { get; protected set; }

        public abstract void Initialize();
    }
}
