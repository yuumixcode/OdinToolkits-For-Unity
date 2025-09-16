using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityTools;
using Yuumix.OdinToolkits.Modules.Editor;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class XMLSummaryConverterVisualSO : ScriptableObject
    {
        [FilePath]
        [Title("脚本路径")]
        [HideLabel]
        public string scriptFilePath;

        [Button("提取 Summary 并输出", ButtonSizes.Medium)]
        public void Extract()
        {
            List<SummaryData> list = XMLSummaryConverter.ExtractSummaryData(File.ReadAllLines(scriptFilePath));
            foreach (SummaryData item in list)
            {
                Debug.Log("注释内容为：" + item.SummaryContent + ", 它对应的类或者成员声明的行数为：" + item.MemberDeclarationLineNumber);
            }
        }

        [Button("提取 Summary 并插入 BilingualComment 特性", ButtonSizes.Medium)]
        public void InsertBilingualComment()
        {
            XMLSummaryConverter.InsertChineseSummaryAttribute(scriptFilePath);
            Debug.Log("插入 BilingualComment 特性");
        }

        [Button("移除所有 BilingualComment 特性", ButtonSizes.Medium)]
        public void RemoveAllBilingualCommentAttributes()
        {
            XMLSummaryConverter.RemoveAllBilingualCommentAttributes(scriptFilePath);
            Debug.Log("移除所有 BilingualComment 特性");
        }

        [Button("同步 Summary 到特性", ButtonSizes.Medium)]
        public void SyncSummaryToAttribute()
        {
            previewText = SummaryAttributeTool.SyncSummaryToAttribute(File.ReadAllText(scriptFilePath));
            Debug.Log(previewText);
        }

        [Button("写入同步后的脚本", ButtonSizes.Medium)]
        public void SyncSummaryToAttributeIO()
        {
            previewText = SummaryAttributeTool.SyncSummaryToAttribute(File.ReadAllText(scriptFilePath));
            File.WriteAllText(scriptFilePath, previewText);
        }

        [Button("删除所有的中文注释", ButtonSizes.Medium)]
        public void RemoveAllChineseSummary()
        {
            previewText = SummaryAttributeTool.RemoveAllChineseSummary(File.ReadAllText(scriptFilePath));
        }

        [TextArea(5, 15)]
        public string previewText;
    }
}
