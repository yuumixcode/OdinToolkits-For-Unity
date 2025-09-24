using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor.Test
{
    public class DocGeneratorVisualSO : SerializedScriptableObject
    {
        [InlineEditor]
        public TypeAnalyzerVisualSO typeAnalyzerVisualSO;

        [PropertyOrder(5)]
        [Title("文档的类型介绍部分预览")]
        [HideLabel]
        [TextArea(5, 10)]
        public string introductionDocPreviewText;

        [PropertyOrder(5)]
        [Title("文档的构造方法部分预览")]
        [HideLabel]
        [TextArea(5, 10)]
        public string constructorDocPreviewText;

        [PropertyOrder(5)]
        [Title("文档的非构造方法部分预览")]
        [HideLabel]
        [TextArea(5, 10)]
        public string methodsDocPreviewText;

        [PropertyOrder(5)]
        [Title("文档的事件部分预览")]
        [HideLabel]
        [TextArea(5, 10)]
        public string eventsDocPreviewText;

        [PropertyOrder(5)]
        [Title("文档的属性部分预览")]
        [HideLabel]
        [TextArea(5, 10)]
        public string propertyDocPreviewText;

        [PropertyOrder(5)]
        [Title("文档的字段部分预览")]
        [HideLabel]
        [TextArea(5, 10)]
        public string fieldDocPreviewText;

        #region 中文 API 文档生成器测试

        [FoldoutGroup("中文 API 文档生成器按钮")]
        [Button("生成类型介绍部分", ButtonSizes.Medium)]
        public void PreviewHeaderTypeIntroductionText()
        {
            introductionDocPreviewText =
                CnAPIDocGeneratorSO.CreateIntroductionContent(typeAnalyzerVisualSO.typeAnalysisData)
                    .ToString();
        }

        [FoldoutGroup("中文 API 文档生成器按钮")]
        [Button("生成构造方法部分", ButtonSizes.Medium)]
        public void PreviewConstructorsDocText()
        {
            constructorDocPreviewText = CnAPIDocGeneratorSO
                .CreateConstructorsContent(typeAnalyzerVisualSO.typeAnalysisData).ToString();
        }

        [FoldoutGroup("中文 API 文档生成器按钮")]
        [Button("生成非构造方法部分", ButtonSizes.Medium)]
        public void PreviewMethodsDocText()
        {
            methodsDocPreviewText = CnAPIDocGeneratorSO
                .CreateMethodsContent(typeAnalyzerVisualSO.typeAnalysisData).ToString();
        }

        [FoldoutGroup("中文 API 文档生成器按钮")]
        [Button("生成事件部分", ButtonSizes.Medium)]
        public void PreviewEventsDocText()
        {
            eventsDocPreviewText =
                CnAPIDocGeneratorSO.CreateEventsContent(typeAnalyzerVisualSO.typeAnalysisData).ToString();
        }

        [FoldoutGroup("中文 API 文档生成器按钮")]
        [Button("生成属性部分", ButtonSizes.Medium)]
        public void PreviewPropertyDocText()
        {
            propertyDocPreviewText = CnAPIDocGeneratorSO.CreatePropertiesContent(typeAnalyzerVisualSO.typeAnalysisData)
                .ToString();
        }

        [FoldoutGroup("中文 API 文档生成器按钮")]
        [Button("生成字段部分", ButtonSizes.Medium)]
        public void PreviewFieldDocText()
        {
            fieldDocPreviewText =
                CnAPIDocGeneratorSO.CreateFieldsContent(typeAnalyzerVisualSO.typeAnalysisData).ToString();
        }

        #endregion
    }
}
