using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using Yuumix.OdinToolkits;
using Yuumix.OdinToolkits.Common.Runtime;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor
{
    public abstract class AbsContainer : SerializedScriptableObject
    {
        static DateTime _lastUpdate = new DateTime(2022, 2, 1);

        [HideIf("HasOdinExample")]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public ExampleScriptableObject example;

        [HideIf("HasExample")]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public ExampleOdinScriptableObject exampleOdin;

        public List<ResolvedParam> ResolvedParams => SetResolvedParams();

        public string SectionHeader => SetHeader() + " 解析";

        public string Brief => SetBrief();

        public List<string> UseTips => SetTip();

        public List<ParamValue> ParamValues => SetParamValues();

        public string OriginalCode => SetOriginalCode();

        public DateTime LastUpdate => SetLastUpdateTime();

        public virtual List<ResolvedParam> SetResolvedParams() => new List<ResolvedParam>();

        protected virtual DateTime SetLastUpdateTime() => _lastUpdate;

        protected virtual List<ParamValue> SetParamValues() => new List<ParamValue>();

        protected abstract string SetHeader();
        protected abstract string SetBrief();
        protected abstract List<string> SetTip();

        protected abstract string SetOriginalCode();

        bool HasExample() => example != null && exampleOdin == null;

        bool HasOdinExample() => exampleOdin != null && example == null;

        protected static string ReadCodeWithoutNamespace(Type exampleType)
        {
            try
            {
                var exampleAttribute = TypeCache
                    .GetTypesWithAttribute<IsChineseAttributeExampleAttribute>()
                    .Where(type => type == exampleType)
                    .Select(type => type.GetAttribute<IsChineseAttributeExampleAttribute>())
                    .SingleOrDefault();

                if (exampleAttribute == null)
                {
                    OdinEditorLog.Error(
                        $"{exampleType.Name} 没有标注 IsChineseAttributeExampleAttribute");
                    return "";
                }

                try
                {
                    var readLines = File.ReadLines(exampleAttribute.FilePath);
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
                    OdinEditorLog.Error($"文件未找到: {exampleAttribute.FilePath}");
                    return "";
                }
                catch (IOException ex)
                {
                    OdinEditorLog.Error($"读取文件时发生IO异常: {ex.Message}");
                    return "";
                }
            }
            catch (InvalidOperationException ex)
            {
                OdinEditorLog.Error($"处理类型属性时发生异常: {ex.Message}");
                return "";
            }
        }
    }
}
