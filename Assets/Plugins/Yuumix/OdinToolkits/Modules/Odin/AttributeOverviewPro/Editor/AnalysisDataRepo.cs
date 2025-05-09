using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules.Odin.AttributeOverviewPro.Editor
{
    public static class AnalysisDataRepo
    {
        public static Dictionary<Type, AnalysisData> DataDict = new Dictionary<Type, AnalysisData>()
        {
            {
                typeof(AssetsOnlyAttribute),
                new AnalysisData
                (
                    nameof(AssetsOnlyAttribute),
                    "https://odininspector.com/attributes/assets-only-attribute",
                    "AssetsOnly is used on object properties, and restricts the property to project assets, and not scene objects.Use this when you want to ensure an object is from the project, and not from the scene.",
                    "中文"
                )
            }
        };
    }
}
