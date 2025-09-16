using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class DocGeneratorVisualSO : SerializedScriptableObject
    {
        [InlineEditor]
        public TypeAnalyzerVisualSO typeAnalyzerVisualSO;

        [Button("生成类型介绍部分", ButtonSizes.Medium)]
        public void PreviewHeaderTypeIntroductionText()
        {
            headerTypeIntroductionText =
                DocGenerator.CreateHeaderTypeIntroduction(typeAnalyzerVisualSO.typeAnalysisData).ToString();
        }

        [Title("文档的类型介绍部分预览")]
        [HideLabel]
        [TextArea(5,10)]
        public string headerTypeIntroductionText;
    }
}
