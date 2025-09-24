using System;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor.Test
{
    public class TypeAnalyzerVisualSO : SerializedScriptableObject
    {
        [Title("目标类型")]
        [HideLabel]
        public Type TargetType;

        [PropertyOrder(10)]
        [Title("类型分析数据")]
        public TypeAnalysisData typeAnalysisData;

        [Title("按钮操作")]
        [Button("解析目标类型", ButtonSizes.Medium)]
        public void Analyze()
        {
            typeAnalysisData = new TypeAnalysisDataFactory().CreateFromType(TargetType);
        }
    }
}
