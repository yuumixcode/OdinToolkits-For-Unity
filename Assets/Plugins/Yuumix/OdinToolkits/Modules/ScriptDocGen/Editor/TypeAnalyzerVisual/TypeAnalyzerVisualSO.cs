using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class TypeAnalyzerVisualSO : SerializedScriptableObject
    {
        [Title("目标类型")]
        [HideLabel]
        public Type TargetType;

        [Title("按钮操作")]
        [Button("解析目标类型", ButtonSizes.Medium)]
        public void Analyze()
        {
            typeAnalysisData = TypeAnalysisData.CreateFromType(TargetType);
        }

        [PropertyOrder(10)]
        [Title("类型分析数据")]
        public TypeAnalysisData typeAnalysisData;
    }
}
