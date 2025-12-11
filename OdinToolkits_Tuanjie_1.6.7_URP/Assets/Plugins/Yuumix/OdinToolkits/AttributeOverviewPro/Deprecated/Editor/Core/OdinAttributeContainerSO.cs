using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Yuumix.OdinToolkits.Core.SafeEditor;
using UnityEditor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;
using Yuumix.OdinToolkits.Core;
using YuumixEditor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public abstract class OdinAttributeContainerSO : SerializedScriptableObject
    {
        public static BilingualHeaderWidget GlobalTempHeader =
            new BilingualHeaderWidget("全局通用 Header 实例", "Global Header Instance",
                "简介",
                "Introduction");

        #region Serialized Fields

        [HideIf("HasOdinExample")]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public ExampleSO example;

        [HideIf("HasExample")]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public ExampleOdinSO exampleOdin;

        #endregion

        [PropertyOrder(-99)]
        [PropertySpace(0, 10)]
        [EnableGUI]
        public BilingualHeaderWidget Header => GetHeaderWidget();

        public List<ResolvedParam> ResolvedParams => GetResolvedParams();

        public string SectionHeader => GetHeader() + " 解析";

        public string Introduction => GetIntroduction();

        public List<string> UseTips => GetTips();

        public List<ParameterValue> ParamValues => GetParamValues();

        public string OriginalCode => GetOriginalCode();

        bool HasExample() => example && !exampleOdin;

        bool HasOdinExample() => exampleOdin && !example;
        public virtual List<ResolvedParam> GetResolvedParams() => new List<ResolvedParam>();
        protected virtual List<ParameterValue> GetParamValues() => new List<ParameterValue>();

        protected virtual BilingualHeaderWidget GetHeaderWidget() => GlobalTempHeader;

        protected abstract string GetHeader();
        protected abstract string GetIntroduction();
        protected abstract List<string> GetTips();
        protected abstract string GetOriginalCode();

        protected static string ReadCodeWithoutNamespace(Type exampleType)
        {
            try
            {
                var overviewProExampleAttribute = TypeCache
                    .GetTypesWithAttribute<AttributeOverviewProExampleAttribute>()
                    .Where(type => type == exampleType)
                    .Select(type => type.GetAttribute<AttributeOverviewProExampleAttribute>())
                    .SingleOrDefault();

                if (overviewProExampleAttribute == null)
                {
                    YuumixLogger.EditorLogError(
                        $"{exampleType.Name} 没有标注 IsChineseAttributeExampleAttribute");
                    return "";
                }

                try
                {
                    var readLines = File.ReadLines(overviewProExampleAttribute.FilePath);
                    var final = new List<string>();
                    var isInNamespace = false;
                    foreach (var line in readLines)
                    {
                        if (line.StartsWith("using") && !isInNamespace)
                        {
                            final.Add(line);
                            continue;
                        }

                        if (line.StartsWith("#"))
                        {
                            final.Add(line);
                            continue;
                        }

                        if (line.StartsWith("namespace"))
                        {
                            isInNamespace = true;
                            continue;
                        }

                        if (line.TrimStart()
                            .StartsWith("[IsChineseAttributeExample]"))
                        {
                            continue;
                        }

                        if (isInNamespace)
                        {
                            if (line.StartsWith("{"))
                            {
                                continue;
                            }

                            if (line.StartsWith("}"))
                            {
                                isInNamespace = false;
                                continue;
                            }

                            if (line.Length > 4)
                            {
                                final.Add(line[4..]);
                            }
                            else
                            {
                                final.Add(line);
                            }
                        }
                        else
                        {
                            final.Add(line);
                        }
                    }

                    return string.Join("\n", final);
                }
                catch (FileNotFoundException)
                {
                    YuumixLogger.OdinToolkitsError($"文件未找到: {overviewProExampleAttribute.FilePath}");
                    return "";
                }
                catch (IOException ex)
                {
                    YuumixLogger.OdinToolkitsError($"读取文件时发生IO异常: {ex.Message}");
                    return "";
                }
            }
            catch (InvalidOperationException ex)
            {
                YuumixLogger.OdinToolkitsError($"处理类型属性时发生异常: {ex.Message}");
                return "";
            }
        }
    }
}
