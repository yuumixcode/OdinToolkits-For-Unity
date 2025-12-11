using System.Linq;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.SafeEditor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    [Summary("Attribute 信息数据抽象类，用于存储特性的介绍信息。")]
    public abstract class AbstractAttributeData
    {
        public abstract BilingualHeaderWidget HeaderWidget { get; set; }

        public abstract BilingualData[] UsageTips { get; set; }

        public abstract ParameterValue[] AttributeParameters { get; set; }

        public abstract ResolvedStringParameterValue[] ResolvedStringParameters { get; set; }

        public abstract AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; }
        

        public ScriptableObject GetInitialExample()
        {
            if (ExamplePreviewItems != null)
            {
                return ExamplePreviewItems.Any(x => x.ExampleType == AttributeExampleType.OdinSerialized)
                    ? ExamplePreviewItems[0].OdinSerializedExample
                    : ExamplePreviewItems[0].UnitySerializedExample;
            }

            return null;
        }
    }
}
