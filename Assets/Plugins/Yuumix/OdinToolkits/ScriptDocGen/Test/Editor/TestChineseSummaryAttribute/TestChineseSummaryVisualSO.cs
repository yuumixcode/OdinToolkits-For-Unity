using Sirenix.OdinInspector;
using System.IO;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Editor;

namespace Yuumix.OdinToolkits.ScriptDocGen.Editor.Test.TestChineseSummaryAttribute
{
    [TypeInfoBox("其他特性指非 [ChineseSummary] 的特性")]
    public class TestChineseSummaryVisualSO : ScriptableObject
    {
        #region Serialized Fields

        [FilePath]
        [Title("文件路径")]
        [HideLabel]
        public string filePath;

        [PropertyOrder(10)]
        [Title("源码文本")]
        [TextArea(5, 30)]
        [HideLabel]
        public string sourceCodeText;

        [PropertyOrder(10)]
        [Title("预览处理后文本")]
        [TextArea(5, 30)]
        [HideLabel]
        public string previewText;

        #endregion

        [FoldoutGroup("同步操作测试")]
        [PropertySpace]
        [Button("读取路径文本，同步 Summary 到特性", ButtonSizes.Medium)]
        public void TestSyncSummaryToAttribute()
        {
            var sourceCode = File.ReadAllText(filePath);
            sourceCodeText = sourceCode;
            previewText = SummaryAttributeConverter.GetCodeWithSyncToChineseSummary(sourceCode);
        }

        [FoldoutGroup("同步操作测试")]
        [Button("同步测试源代码 A 的 Summary 到特性", ButtonSizes.Medium)]
        public void TestSyncA()
        {
            sourceCodeText = TestChineseSummaryCodeRepository.CLASS_SUMMARY_A;
            previewText =
                SummaryAttributeConverter.GetCodeWithSyncToChineseSummary(TestChineseSummaryCodeRepository
                    .CLASS_SUMMARY_A);
        }

        [FoldoutGroup("同步操作测试")]
        [Button("同步测试源代码 B 的 Summary 到特性", ButtonSizes.Medium)]
        public void TestSyncB()
        {
            sourceCodeText = TestChineseSummaryCodeRepository.CLASS_SUMMARY_B;
            previewText =
                SummaryAttributeConverter.GetCodeWithSyncToChineseSummary(TestChineseSummaryCodeRepository
                    .CLASS_SUMMARY_B);
        }

        [FoldoutGroup("同步操作测试")]
        [Button("同步测试源代码 C 的 Summary 到特性", ButtonSizes.Medium)]
        public void TestSyncC()
        {
            sourceCodeText = TestChineseSummaryCodeRepository.CLASS_SUMMARY_C;
            previewText =
                SummaryAttributeConverter.GetCodeWithSyncToChineseSummary(TestChineseSummaryCodeRepository
                    .CLASS_SUMMARY_C);
        }

        [FoldoutGroup("同步操作测试")]
        [Button("同步测试源代码 D 的 Summary 到特性", ButtonSizes.Medium)]
        public void TestSyncD()
        {
            sourceCodeText = TestChineseSummaryCodeRepository.CLASS_SUMMARY_D;
            previewText =
                SummaryAttributeConverter.GetCodeWithSyncToChineseSummary(TestChineseSummaryCodeRepository
                    .CLASS_SUMMARY_D);
        }

        [FoldoutGroup("同步操作测试")]
        [Button("同步测试源代码 E 的 Summary 到特性", ButtonSizes.Medium)]
        public void TestSyncE()
        {
            sourceCodeText = TestChineseSummaryCodeRepository.CLASS_SUMMARY_E;
            previewText =
                SummaryAttributeConverter.GetCodeWithSyncToChineseSummary(TestChineseSummaryCodeRepository
                    .CLASS_SUMMARY_E);
        }

        [FoldoutGroup("移除操作测试")]
        [Button("移除目标文本中的 ChineseSummary", ButtonSizes.Medium)]
        public void TestRemove()
        {
            var sourceCode = File.ReadAllText(filePath);
            sourceCodeText = sourceCode;
            previewText = SummaryAttributeConverter.GetCodeWithRemoveAllChineseSummary(sourceCode);
        }

        [FoldoutGroup("移除操作测试")]
        [Button("移除测试源代码 A 中的 ChineseSummary", ButtonSizes.Medium)]
        public void TestRemoveA()
        {
            sourceCodeText = TestChineseSummaryCodeRepository.REMOVE_SUMMARY_CODE_A;
            previewText =
                SummaryAttributeConverter.GetCodeWithRemoveAllChineseSummary(TestChineseSummaryCodeRepository
                    .REMOVE_SUMMARY_CODE_A);
        }

        [FoldoutGroup("移除操作测试")]
        [Button("移除测试源代码 B 中的 ChineseSummary", ButtonSizes.Medium)]
        public void TestRemoveB()
        {
            sourceCodeText = TestChineseSummaryCodeRepository.REMOVE_SUMMARY_CODE_B;
            previewText =
                SummaryAttributeConverter.GetCodeWithRemoveAllChineseSummary(TestChineseSummaryCodeRepository
                    .REMOVE_SUMMARY_CODE_B);
        }
    }
}
