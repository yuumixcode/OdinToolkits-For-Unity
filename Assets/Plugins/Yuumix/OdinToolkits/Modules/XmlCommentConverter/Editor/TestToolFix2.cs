
using UnityEngine;

namespace TestFix
{
    public class TestToolFix2
    {
        [UnityEditor.MenuItem("Test/Test Fixed Tool Again")]
        public static void TestFixedToolAgain()
        {
            string testCode = @"
namespace Yuumix.OdinToolkits.Modules.Editor
{
    /// <summary>
    /// 测试 Summary 同步
    /// </summary>
    [EnglishSummary]
    public class TestAttributeRegex
    {
        /// <summary>
        /// aaaaa
        /// </summary>
        [EnglishSummary] public int A;
    }
}";

            Debug.Log("=== 原始代码 ===");
            Debug.Log(testCode);

            string result = UnityTools.SummaryAttributeTool.SyncSummaryToAttribute(testCode);

            Debug.Log("=== 处理后代码 ===");
            Debug.Log(result);

            // 验证结果
            bool hasEnglishSummary = result.Contains("[EnglishSummary]");
            bool hasChineseSummary = result.Contains("[ChineseSummary(\"测试 Summary 同步\")]");
            bool hasChineseSummaryForField = result.Contains("[ChineseSummary(\"aaaaa\")]");
            bool correctFormat = !result.Contains("EnglishSummary[ChineseSummary"); // 确保没有错误的格式

            Debug.Log("=== 验证结果 ===");
            Debug.Log($"EnglishSummary 特性保持: {hasEnglishSummary}");
            Debug.Log($"ChineseSummary 特性添加(类): {hasChineseSummary}");
            Debug.Log($"ChineseSummary 特性添加(字段): {hasChineseSummaryForField}");
            Debug.Log($"格式正确(无错误拼接): {correctFormat}");

            if (hasEnglishSummary && hasChineseSummary && hasChineseSummaryForField && correctFormat)
            {
                Debug.Log("✅ 测试通过！所有特性都正确处理，格式正确。");
            }
            else
            {
                Debug.Log("❌ 测试失败！特性处理或格式有问题。");
            }
        }
    }
}
