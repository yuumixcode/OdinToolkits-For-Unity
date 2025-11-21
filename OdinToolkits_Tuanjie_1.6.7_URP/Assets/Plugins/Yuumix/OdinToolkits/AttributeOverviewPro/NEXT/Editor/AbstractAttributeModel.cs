using System.Linq;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;
using YuumixEditor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    public abstract class AbstractAttributeModel
    {
        public BilingualHeaderWidget HeaderWidget { get; protected set; }

        public string[] UsageTips { get; protected set; }

        public ParameterValue[] AttributeParameters { get; protected set; }

        public ResolvedStringParameterValue[] ResolvedStringParameters { get; protected set; }

        public AttributeExamplePreviewItem[] ExamplePreviewItems { get; protected set; }

        public abstract void Initialize();

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
