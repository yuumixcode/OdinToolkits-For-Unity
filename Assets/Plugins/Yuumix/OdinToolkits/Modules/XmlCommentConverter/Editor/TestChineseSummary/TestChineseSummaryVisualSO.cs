using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityTools;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [TypeInfoBox("其他特性指非 [ChineseSummary] 的特性")]
    public class TestChineseSummaryVisualSO : ScriptableObject
    {
        [Title("预览文本")]
        [TextArea(5, 15)]
        [HideLabel]
        public string previewText;

        [Button("其他特性和类声明在同一行", ButtonSizes.Medium)]
        public void ClassA()
        {
            previewText =
                XmlToAttributeTool.SyncSummaryToAttribute(TestChineseSummarySourceCodeStrings.CLASS_SUMMARY_A);
            if (previewText == @"    /// <summary>
    /// 测试类级别（包括结构体，接口等）的 Summary，以 class 为例
    /// </summary>
    [ChineseSummary(""测试类级别（包括结构体，接口等）的 Summary，以 class 为例"")] [EmptyPlaceholder] public class TestClassSummary { }")
            {
                Debug.Log("[成功] 其他特性和类声明在同一行".ToGreen());
            }
            else
            {
                Debug.Log("[失败] 其他特性和类声明在同一行".ToRed());
            }
        }

        [Button("其他特性和类声明不在同一行", ButtonSizes.Medium)]
        public void ClassB()
        {
            previewText =
                XmlToAttributeTool.SyncSummaryToAttribute(TestChineseSummarySourceCodeStrings.CLASS_SUMMARY_B);
            if (previewText == @"    /// <summary>
    /// 测试类级别（包括结构体，接口等）的 Summary，以 class 为例
    /// </summary>
    [ChineseSummary(""测试类级别（包括结构体，接口等）的 Summary，以 class 为例"")] [EmptyPlaceholder]
    public class TestClassSummary { }")
            {
                Debug.Log("[成功] 其他特性和类声明不在同一行".ToGreen());
            }
            else
            {
                Debug.Log("[失败] 其他特性和类声明不在同一行".ToRed());
            }
        }

        [Button("Summary 注释均在同一行的情况", ButtonSizes.Medium)]
        public void ClassC()
        {
            previewText =
                XmlToAttributeTool.SyncSummaryToAttribute(TestChineseSummarySourceCodeStrings.CLASS_SUMMARY_C);
            if (previewText == @"    /// <summary> 测试类级别（包括结构体，接口等）的 Summary，以 class 为例 </summary>
    [ChineseSummary(""测试类级别（包括结构体，接口等）的 Summary，以 class 为例"")] [EmptyPlaceholder]
    public class TestClassSummary { }")
            {
                Debug.Log("[成功] Summary 注释均在同一行的情况".ToGreen());
            }
            else
            {
                Debug.Log("[失败] Summary 注释均在同一行的情况".ToRed());
            }
        }
    }
}
