using Sirenix.OdinInspector;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.Editor.ScriptableSingleton;
using Yuumix.OdinToolkits.Common.InspectorLocalization;
using Yuumix.OdinToolkits.Common.InspectorLocalization.Attributes;
using Yuumix.OdinToolkits.Common.InspectorLocalization.Attributes.WidgetConfigs;
using Yuumix.OdinToolkits.Common.InspectorLocalization.GUIWidgets;
using Yuumix.OdinToolkits.Common.Runtime.ResetTool;
using Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Runtime;

namespace Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Editor
{
    public class ScriptDocGenToolSO : OdinEditorScriptableSingleton<ScriptDocGenToolSO>, IPluginReset
    {
        public LocalizedHeaderWidget header = new LocalizedHeaderWidget(
            "脚本文档生成工具",
            "Script Doc Generate Tool",
            "给定一个 `Type` 类型的值，生成 Markdown 格式的文档，可选 Scripting API 或者所有成员文档，默认支持 MkDocs-material",
            "Given a value of type `Type`, generate a document in the format of Markdown, optional Scripting API and all member documents, and MkDocs-material is supported by default."
        );

        [PropertyOrder(1)]
        [LocalizedTitle("目标类型", "Target Type")]
        [HideLabel]
        public Type TargetType;

        [PropertyOrder(2)]
        [LocalizedTitle("生成文档的文件夹路径", "Folder Path For Doc")]
        [HideLabel]
        [FolderPath(AbsolutePath = true)]
        public string folderPath;

        [PropertyOrder(51)]
        [ReadOnly]
        public TypeData typeData;

        [PropertyOrder(20)]
        [LocalizedButtonWidgetConfig("分析 Type", "Analyze Type",
            ButtonSizes.Large, ButtonStyle.Box, SdfIconType.Activity)]
        public LocalizedButtonWidget analyzeType = new LocalizedButtonWidget(Analyze);

        [PropertyOrder(20)]
        [LocalizedTitle("生成文档按钮", "Generate Document Buttons")]
        [LocalizedButtonWidgetConfig("生成 MkDocs-material 文档", "Generate MkDocs-material Doc",
            ButtonSizes.Large, ButtonStyle.Box, SdfIconType.FileEarmarkPlus)]
        public LocalizedButtonWidget generateButton = new LocalizedButtonWidget(GenerateMkDocs);

        public bool IsChinese => InspectorLocalizationManagerSO.Instance.IsChinese;
        public bool IsEnglish => InspectorLocalizationManagerSO.Instance.IsEnglish;

        public void PluginReset()
        {
            TargetType = null;
            folderPath = "";
            typeData = new TypeData();
        }

        [PropertyOrder(50)]
        [LocalizedTitle("过程数据", "Process Data")]
        [OnInspectorGUI]
        public void Title() { }

        static void GenerateMkDocs()
        {
            var data = Instance.typeData;
            if (!Directory.Exists(Instance.folderPath))
            {
                Debug.LogError("请选择有效的目标路径");
            }

            Analyze();
            if (data.IsObsolete)
            {
                if (!EditorUtility.DisplayDialog("警告提示", "此类已经被标记为过时，继续生成文档吗？", "确认", "取消"))
                {
                    return;
                }
            }

            var headerIntroduction = CreateHeaderIntroduction(data);
            var constructorsContent = CreateConstructorsContent(data);
            var fieldsContent = CreateCurrentFieldsContent(data);
            var finalStringBuilder = headerIntroduction
                .Append(constructorsContent)
                .Append(fieldsContent);
            var filePathWithExtensions = Instance.folderPath + "/" + Instance.TargetType.Name + ".md";
            if (File.Exists(filePathWithExtensions))
            {
                if (!EditorUtility.DisplayDialog("提示", "已经存在该文件，是否覆盖？", "确认", "取消"))
                {
                    return;
                }
            }

            File.WriteAllText(Instance.folderPath + "/" + Instance.TargetType.Name + ".md",
                finalStringBuilder.ToString(),
                Encoding.UTF8);
            Debug.Log("文档生成完毕");
            AssetDatabase.Refresh();
            EditorUtility.OpenWithDefaultApp(Instance.folderPath + "/" + Instance.TargetType.Name + ".md");
        }

        static void Analyze()
        {
            Instance.typeData = TypeData.FromType(Instance.TargetType);
        }

        static StringBuilder CreateHeaderIntroduction(TypeData data)
        {
            var sb = new StringBuilder();
            if (data.IsStatic)
            {
                sb.AppendLine(
                    $"# `{data.TypeName} static {data.TypeCategory.ToString().ToLower(CultureInfo.CurrentCulture)}`");
            }
            else if (data.IsAbstract)
            {
                sb.AppendLine(
                    $"# `{data.TypeName} abstract {data.TypeCategory.ToString().ToLower(CultureInfo.CurrentCulture)}`");
            }
            else
            {
                sb.AppendLine(
                    $"# `{data.TypeName} {data.TypeCategory.ToString().ToLower(CultureInfo.CurrentCulture)}`");
            }

            sb.AppendLine("## Introduction");
            sb.AppendLine($"- NameSpace: `{data.NamespaceName}`");
            sb.AppendLine($"- Assembly: `{data.AssemblyName}`");
            sb.AppendLine();
            sb.AppendLine("``` csharp");
            sb.AppendLine(data.TypeDeclaration);
            sb.AppendLine("```");
            if (!string.IsNullOrEmpty(data.ChineseComment) || !string.IsNullOrEmpty(data.EnglishComment))
            {
                sb.AppendLine("### Description");
                if (!string.IsNullOrEmpty(data.ChineseComment))
                {
                    sb.AppendLine("- " + data.ChineseComment);
                }

                if (!string.IsNullOrEmpty(data.EnglishComment))
                {
                    sb.AppendLine("- " + data.EnglishComment);
                }
            }

            sb.AppendLine();
            return sb;
        }

        static StringBuilder CreateConstructorsContent(TypeData data)
        {
            var sb = new StringBuilder();
            if (data.IsStatic || data.Constructors.Length <= 0)
            {
                return sb;
            }

            sb.AppendLine("## Constructors");
            sb.AppendLine("| 构造函数 | 注释 | Comment |");
            sb.AppendLine("| :--- | :--- | :--- |");
            foreach (var item in data.Constructors.Where(x => !x.isObsolete))
            {
                sb.AppendLine("| " + $"`{item.fullSignature}`" + " | " + item.chineseComment + " | " +
                              item.englishComment + " |");
            }

            if (!data.Constructors.Any(x => x.isObsolete))
            {
                return sb;
            }

            sb.AppendLine("### Obsolete");
            sb.AppendLine("| 构造函数 |");
            sb.AppendLine("| :--- |");
            foreach (var item in data.Constructors.Where(x => x.isObsolete))
            {
                sb.AppendLine("| " + $"`{item.fullSignature}`" + " |");
            }

            return sb;
        }

        static StringBuilder CreateCurrentFieldsContent(TypeData data)
        {
            var sb = new StringBuilder();
            if (data.CurrentFields.Length <= 0)
            {
                return sb;
            }

            sb.AppendLine("## Fields");
            sb.AppendLine("| 字段 | 注释 | Comment |");
            sb.AppendLine("| :--- | :--- | :--- |");
            foreach (var item in data.CurrentFields.Where(x => !x.isObsolete))
            {
                sb.AppendLine("| " + $"`{item.fullSignature}`" + " | " + item.chineseComment + " | " +
                              item.englishComment + " |");
            }

            if (!data.CurrentFields.Any(x => x.isObsolete))
            {
                return sb;
            }

            sb.AppendLine("### Obsolete");
            sb.AppendLine("| 字段|");
            sb.AppendLine("| :--- |");
            foreach (var item in data.CurrentFields.Where(x => x.isObsolete))
            {
                sb.AppendLine("| " + $"`{item.fullSignature}`" + " |");
            }

            return sb;
        }
    }
}
