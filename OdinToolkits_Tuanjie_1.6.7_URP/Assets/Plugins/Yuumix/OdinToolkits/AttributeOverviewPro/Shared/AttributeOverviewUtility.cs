using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Yuumix.OdinToolkits.Core;
#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
using UnityEditor;
#endif

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Shared
{
    public static class AttributeOverviewUtility
    {
        const int CONTAINER_CONTENT_PADDING = 10;
        static GUIStyle _containerTitleStyle;
        static GUIStyle _containerContentStyle;
        static GUIStyle _tableCellTextStyle;
        static GUIStyle _resolvedStringParameterValueTitleStyle;
        static GUIStyle _tabButtonCellTextStyle;

        public static GUIStyle ContainerTitleStyle
        {
            get
            {
                _containerTitleStyle ??= new GUIStyle(SirenixGUIStyles.TitleCentered)
                {
                    fontSize = 16
                };
                return _containerTitleStyle;
            }
        }

        public static GUIStyle ContainerContentStyle
        {
            get
            {
                _containerContentStyle ??= new GUIStyle(SirenixGUIStyles.ToolbarBackground)
                {
                    stretchHeight = false,
                    padding = new RectOffset(
                        CONTAINER_CONTENT_PADDING,
                        CONTAINER_CONTENT_PADDING,
                        CONTAINER_CONTENT_PADDING,
                        CONTAINER_CONTENT_PADDING)
                };
                return _containerContentStyle;
            }
        }

        public static GUIStyle TableCellTextStyle
        {
            get
            {
                _tableCellTextStyle ??= new GUIStyle(SirenixGUIStyles.MultiLineCenteredLabel)
                {
                    padding = new RectOffset(5, 5, 5, 5),
                    clipping = TextClipping.Overflow,
                    richText = true
                };
                return _tableCellTextStyle;
            }
        }

        public static GUIStyle ResolvedStringParameterValueTitleStyle
        {
            get
            {
                _resolvedStringParameterValueTitleStyle ??= new GUIStyle(SirenixGUIStyles.TitleCentered)
                {
                    fontSize = 14
                };
                return _resolvedStringParameterValueTitleStyle;
            }
        }

        public static GUIStyle TabButtonCellTextStyle
        {
            get
            {
                _tabButtonCellTextStyle ??= new GUIStyle
                {
                    padding = new RectOffset(10, 10, 10, 10),
                    alignment = TextAnchor.MiddleCenter,
                    clipping = TextClipping.Overflow
                };
                return _tabButtonCellTextStyle;
            }
        }

        public static AttributeOverviewProExampleAttribute GetAttributeInExampleType(Type exampleType)
        {
            if (exampleType != null)
            {
                return TypeCache.GetTypesWithAttribute<AttributeOverviewProExampleAttribute>()
                    .First(type => type == exampleType)
                    .GetCustomAttribute<AttributeOverviewProExampleAttribute>();
            }

            YuumixLogger.OdinToolkitsError("exampleType 不能为空");
            return null;
        }

        public static string GetExampleSourceCodeWithoutNamespace(AttributeOverviewProExampleAttribute attribute)
        {
            try
            {
                var readLines = File.ReadLines(attribute.FilePath);
                var excludeNamespaceCodeLines = new List<string>();
                var isInNamespace = false;
                foreach (var line in readLines)
                {
                    if ((line.StartsWith("using") && !isInNamespace) || line.StartsWith("#"))
                    {
                        excludeNamespaceCodeLines.Add(line);
                        continue;
                    }

                    if (line.StartsWith("namespace"))
                    {
                        isInNamespace = true;
                        continue;
                    }

                    if (line.TrimStart().StartsWith(
                            "[" + nameof(AttributeOverviewProExampleAttribute)[
                                ..(nameof(AttributeOverviewProExampleAttribute).Length - "Attribute".Length)] +
                            "]"))
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

                        excludeNamespaceCodeLines.Add(line.Length > 4 ? line[4..] : line);
                    }
                    else
                    {
                        excludeNamespaceCodeLines.Add(line);
                    }
                }

                return string.Join("\n", excludeNamespaceCodeLines);
            }
            catch (IOException ex)
            {
                YuumixLogger.OdinToolkitsError($"读取文件时发生IO异常: {ex.Message}");
                return "";
            }
        }

        public static string GetExampleSourceCodeWithoutNamespace(Type exampleType)
        {
            var exampleAttribute = GetAttributeInExampleType(exampleType);
            if (exampleAttribute == null)
            {
                YuumixLogger.EditorLogError(
                    $"{exampleType.Name} 没有标记 " + nameof(AttributeOverviewProExampleAttribute));
                return "";
            }

            return GetExampleSourceCodeWithoutNamespace(exampleAttribute);
        }
#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
        static void Reset()
        {
            _containerTitleStyle = null;
            _containerContentStyle = null;
            _tableCellTextStyle = null;
            _resolvedStringParameterValueTitleStyle = null;
            _tabButtonCellTextStyle = null;
        }
#endif
    }
}
