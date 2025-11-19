using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Core.Editor;
using Yuumix.OdinToolkits.OdinAttributeOverviewPro.Editor;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [CustomEditor(typeof(OdinAttributeContainerSO), true)]
    public class AttributeContainerDrawer : OdinEditor
    {
        const int Padding = 12;
        const int ContainerContentPadding = 10;
        const int CodeDefaultHeight = 400;
        const int CodeDefaultWidth = 600;
        readonly List<GUITable> _resolvedParamGUITables = new List<GUITable>();
        GUIStyle _codeTextStyle;
        OdinAttributeContainerSO _container;
        GUIStyle _containerContentStyle;
        GUIStyle _containerTitleStyle;
        Color _darkLineColor;
        Color _lightLineColor;
        GUITable _paramValueGUITable;
        Vector2 _scrollPosition;
        GUIStyle _tableCellTextStyle;
        GUITable _tipGUITable;

        #region Event Functions

        protected override void OnEnable()
        {
            base.OnEnable();
            _container = target as OdinAttributeContainerSO;
            if (_container)
            {
                _container.hideFlags = HideFlags.None;

                if (_container)
                {
                    CreateTipGUITable();
                    CreateParamGUITable();
                    CreateResolvedParamGUITables();
                }
            }

            _darkLineColor = EditorGUIUtility.isProSkin
                ? SirenixGUIStyles.BorderColor
                : new Color(0f, 0f, 0f, 0.2f);
            _lightLineColor = EditorGUIUtility.isProSkin
                ? new Color(1f, 1f, 1f, 0.1f)
                : new Color(1f, 1f, 1f, 1f);
            var property =
                Tree.RootProperty.FindChild(p => p.Name == nameof(OdinAttributeContainerSO.example), false);
            var property2 =
                Tree.RootProperty.FindChild(p => p.Name == nameof(OdinAttributeContainerSO.exampleOdin), false);
            property.Children
                .Recurse()
                .ForEach(child => child.State.Expanded = false);
            property2.Children
                .Recurse()
                .ForEach(child => child.State.Expanded = false);
            OdinAttributeOverviewProWindow.OnWindowResized = CalculateAllTableSize;
            EditorApplication.delayCall += CalculateAllTableSize;
        }

        void OnDestroy()
        {
            EditorApplication.delayCall -= CalculateAllTableSize;
        }

        #endregion

        void CreateResolvedParamGUITables()
        {
            foreach (var resolvedParam in _container.ResolvedParams)
            {
                _resolvedParamGUITables.Add(resolvedParam.CreateGUITable());
            }
        }

        public override void OnInspectorGUI()
        {
            EnsureGUIStyles();
            DrawHeaderAndBrief();
            DrawGUITable(_tipGUITable, _container.UseTips, "使用提示", out var rect2);
            DrawGUITable(_paramValueGUITable, _container.ParamValues, "特性参数", out var rect3);
            DrawResolvedParams();
            DrawExample();
            DrawSeparator();
            DrawCode();
            if (EditorApplication.timeSinceStartup % 0.5f > 0.01f)
            {
                return;
            }

            CalculateAllTableSize();
        }

        void EnsureGUIStyles()
        {
            _tableCellTextStyle ??= new GUIStyle(SirenixGUIStyles.MultiLineCenteredLabel)
            {
                clipping = TextClipping.Overflow,
                richText = true
            };

            _containerContentStyle ??= new GUIStyle(SirenixGUIStyles.ToolbarBackground)
            {
                stretchHeight = false,
                padding = new RectOffset(
                    ContainerContentPadding,
                    ContainerContentPadding,
                    ContainerContentPadding,
                    ContainerContentPadding)
            };
            _containerTitleStyle ??= new GUIStyle(SirenixGUIStyles.TitleCentered)
            {
                fontSize = 15
            };
            _codeTextStyle ??= new GUIStyle(SirenixGUIStyles.MultiLineLabel)
            {
                normal = new GUIStyleState
                {
                    textColor = OdinSyntaxHighlighterSO.TextColor
                },
                active = new GUIStyleState
                {
                    textColor = OdinSyntaxHighlighterSO.TextColor
                },
                focused = new GUIStyleState
                {
                    textColor = OdinSyntaxHighlighterSO.TextColor
                },
                wordWrap = false,
                fontSize = 13
            };
        }

        void CreateTipGUITable()
        {
            _tipGUITable = GUITable.Create(_container.UseTips, null,
                new GUITableColumn
                {
                    ColumnTitle = "序号",
                    Width = 50,
                    Resizable = false,
                    OnGUI = (rect, i) => { DrawTableCell(rect, (i + 1).ToString()); }
                },
                new GUITableColumn
                {
                    ColumnTitle = "提示",
                    MinWidth = 100,
                    OnGUI = (rect, i) => { DrawTableCell(rect, _container.UseTips[i]); }
                }
            );
        }

        void CreateParamGUITable()
        {
            _paramValueGUITable = GUITable.Create(_container.ParamValues, null, new GUITableColumn
                {
                    ColumnTitle = "序号",
                    Width = 40,
                    Resizable = false,
                    OnGUI = (rect, i) => { EditorGUI.LabelField(rect, (i + 1).ToString(), _tableCellTextStyle); }
                },
                new GUITableColumn
                {
                    ColumnTitle = "返回值类型",
                    Width = 150,
                    OnGUI = (rect, i) => { DrawTableCell(rect, _container.ParamValues[i].ReturnType); }
                },
                new GUITableColumn
                {
                    ColumnTitle = "参数名",
                    Width = 200,
                    OnGUI = (rect, i) => { DrawTableCell(rect, _container.ParamValues[i].ParameterName); }
                },
                new GUITableColumn
                {
                    ColumnTitle = "参数描述",
                    MinWidth = 220,
                    OnGUI = (rect, i) => { DrawTableCell(rect, _container.ParamValues[i].ParameterDescription); }
                });
        }

        void DrawTableCell(Rect rect, string text, GUIStyle style = null)
        {
            EditorGUI.LabelField(rect, text, style ?? _tableCellTextStyle);
        }

        void DrawResolvedParams()
        {
            if (_resolvedParamGUITables.Count <= 0)
            {
                return;
            }

            // 需要容器
            DrawContainer("特性解析字符串的方法签名", DrawInternal);
            DrawSeparator();
            return;

            void DrawInternal()
            {
                EditorGUILayout.BeginVertical();
                for (var i = 0; i < _resolvedParamGUITables.Count; i++)
                {
                    var style = new GUIStyle(SirenixGUIStyles.TitleCentered)
                    {
                        fontSize = 14
                    };
                    SirenixEditorGUI.BeginBoxHeader();
                    var rect = GUILayoutUtility.GetRect(GUIHelper.TempContent(_container.ResolvedParams[i].ParamName)
                        , style).AlignCenter(600);
                    EditorGUI.LabelField(rect.Split(0, 2), "特性参数: " + _container.ResolvedParams[i].ParamName, style);
                    var rect3 = rect.Split(1, 2);
                    if (_container.ResolvedParams[i].ReturnType == typeof(void).Name)
                    {
                        EditorGUI.LabelField(rect3, "方法无返回值 void", style);
                    }
                    else
                    {
                        EditorGUI.LabelField(rect3, "方法返回值类型为：" + _container.ResolvedParams[i].ReturnType, style);
                    }

                    SirenixEditorGUI.EndBoxHeader();
                    _resolvedParamGUITables[i].DrawTable();
                    if (i == _resolvedParamGUITables.Count - 1)
                    {
                        continue;
                    }

                    GUILayout.Space(10f);
                    SirenixEditorGUI.HorizontalLineSeparator(_lightLineColor, 2);
                    GUILayout.Space(15f);
                }

                EditorGUILayout.EndVertical();
            }
        }

        void DrawExample()
        {
            // 原生绘制 Example
            var rect = DrawContainer("使用案例预览", base.OnInspectorGUI);
            Type exampleType = null;
            if (_container.example)
            {
                exampleType = _container.example.GetType();
            }

            if (_container.exampleOdin)
            {
                exampleType = _container.exampleOdin.GetType();
            }

            if (exampleType is null)
            {
                return;
            }

            var attribute = TypeCache
                .GetTypesWithAttribute<OdinToolkitsAttributeExampleAttribute>()
                .Where(type => type == exampleType)
                .Select(type => type.GetAttribute<OdinToolkitsAttributeExampleAttribute>())
                .SingleOrDefault();
            // Debug.Log("attribute: " + attribute);
            var path = attribute?.FilePath;
            // Debug.Log("绝对路径: " + path);
            // Debug.Log("项目路径: " + Application.dataPath);
            // 绝对路径转相对路径
            var relative = "Assets/" + PathUtilities.MakeRelative(Application.dataPath, path);
            // Debug.Log("相对路径: " + relative);
            if (string.IsNullOrEmpty(relative))
            {
                return;
            }

            // Assets/OdinToolkits/ChineseGuide/ChineseAttributesOverview/Editor/PreviewExamples/Scripts/CustomValueDrawerExample.cs
            var script = AssetDatabase.LoadAssetAtPath<MonoScript>(relative);
            // Debug.Log("script: " + script);
            var rect0 = rect.AlignCenterY(rect.height).AlignRight(200);
            var rect1 = rect0.Split(0, 2);
            if (GUI.Button(rect1, GUIHelper.TempContent("跳转到脚本文件"),
                    SirenixGUIStyles.ToolbarButton))
            {
                EditorGUIUtility.PingObject(script);
            }

            var rect2 = rect0.Split(1, 2);
            if (GUI.Button(rect2, GUIHelper.TempContent("重置案例"),
                    SirenixGUIStyles.ToolbarButton))
            {
                if (_container.example)
                {
                    _container.example.SetDefaultValue();
                }

                if (_container.exampleOdin)
                {
                    _container.exampleOdin.SetDefaultValue();
                }

                // Debug.Log("重置");
            }
            // SirenixEditorGUI.DrawBorders(rect, 1, Color.green);
        }

        Rect DrawContainer(string title, Action drawContent, GUIStyle titleStyle = null)
        {
            titleStyle ??= _containerTitleStyle;
            var headerRect = SirenixEditorGUI.BeginHorizontalToolbar(30f);
            var titleWidth = titleStyle.CalcSize(GUIHelper.TempContent(title)).x;
            var titleRect = headerRect.AlignCenter(titleWidth);
            EditorGUI.LabelField(titleRect, title, titleStyle);
            // SirenixEditorGUI.DrawBorders(titleRect, 1, Color.green);
            GUILayout.FlexibleSpace();
            SirenixEditorGUI.EndHorizontalToolbar();
            GUILayout.Space(-2);
            var contentRect = EditorGUILayout.BeginVertical(_containerContentStyle);
            drawContent();
            EditorGUILayout.EndVertical();
            SirenixEditorGUI.DrawBorders(contentRect, 1);
            return headerRect;
        }

        void DrawCode()
        {
            var headerRect = DrawContainer("代码预览", CodePreview);
            if (GUI.Button(headerRect.AlignCenterY(headerRect.height).AlignRight(80), GUIHelper.TempContent("拷贝代码"),
                    SirenixGUIStyles.ToolbarButton))
            {
                Clipboard.Copy(_container.OriginalCode);
            }
        }

        void CodePreview()
        {
            var rect = SirenixEditorGUI.BeginBox();
            GUILayoutUtility.GetRect(GUIHelper.TempContent("宽度卡位"), GUIStyle.none, GUILayout.Width(CodeDefaultWidth),
                GUILayout.Height(5));
            SirenixEditorGUI.DrawSolidRect(rect, OdinSyntaxHighlighterSO.BackgroundColor);
            var highlighterCode = OdinSyntaxHighlighterSO.ApplyCodeHighlighting(_container.OriginalCode);
            var calcHeight = _codeTextStyle.CalcHeight(GUIHelper.TempContent(highlighterCode), CodeDefaultWidth);
            GUILayout.BeginVertical();
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, false, false,
                GUILayout.Height(calcHeight + 30), GUILayout.MaxHeight(CodeDefaultHeight));
            EditorGUI.SelectableLabel(GUILayoutUtility.GetRect(CodeDefaultWidth + 50f, calcHeight + 10).AddXMin(4f),
                highlighterCode, _codeTextStyle);
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            SirenixEditorGUI.EndBox();
            // GUILayout.Label(GUIHelper.TempContent(rect.ToString()));
        }

        void DrawGUITable(GUITable table, IList dataList, string containerTitle,
            out Rect rect)
        {
            rect = EditorGUILayout.BeginVertical();
            if (table != null && dataList.Count > 0)
            {
                DrawContainer(containerTitle, table.DrawTable);
                DrawSeparator();
            }

            EditorGUILayout.EndVertical();
        }

        void CalculateAllTableSize()
        {
            CalculateTipTableSize();
            CalculateParamValueTableSize();
            CalculateAllResolvedParamTableSize();
            // Debug.Log("重新计算表格大小");
        }

        void CalculateAllResolvedParamTableSize()
        {
            for (var i = 0; i < _resolvedParamGUITables.Count; i++)
            {
                var table = _resolvedParamGUITables[i];
                for (var row = 1; row < table.RowCount; row++)
                {
                    var returnTypeCellHeight =
                        CalculateHeight(_container.ResolvedParams[i].ParamValues[row - 1].ReturnType, table, 1, row);
                    var paramNameCellHeight =
                        CalculateHeight(_container.ResolvedParams[i].ParamValues[row - 1].ParameterName, table, 2, row);
                    var paramDescriptionCellHeight =
                        CalculateHeight(_container.ResolvedParams[i].ParamValues[row - 1].ParameterDescription, table, 3,
                            row);
                    table[1, row].Height =
                        Mathf.Max(returnTypeCellHeight, paramNameCellHeight, paramDescriptionCellHeight) + 15f;
                }

                table.ReCalculateSizes();
            }
        }

        void CalculateTipTableSize()
        {
            var table = _tipGUITable;
            for (var row = 1; row < table.RowCount; row++)
            {
                table[1, row].Height = CalculateHeight(_container.UseTips[row - 1], table, 1, row) + 15f;
            }

            table.ReCalculateSizes();
        }

        void CalculateParamValueTableSize()
        {
            var table = _paramValueGUITable;
            for (var row = 1; row < table.RowCount; row++)
            {
                var returnTypeCellHeight =
                    CalculateHeight(_container.ParamValues[row - 1].ReturnType, table, 1, row);
                var paramNameCellHeight =
                    CalculateHeight(_container.ParamValues[row - 1].ParameterName, table, 2, row);
                var paramDescriptionCellHeight =
                    CalculateHeight(_container.ParamValues[row - 1].ParameterDescription, table, 3, row);
                table[1, row].Height =
                    Mathf.Max(returnTypeCellHeight, paramNameCellHeight, paramDescriptionCellHeight) + 15f;
            }

            table.ReCalculateSizes();
        }

        float CalculateHeight(string content, GUITable table, int col,
            int row)
        {
            _tableCellTextStyle ??= new GUIStyle(SirenixGUIStyles.MultiLineCenteredLabel)
            {
                clipping = TextClipping.Overflow,
                richText = true
            };

            return _tableCellTextStyle.CalcHeight(
                GUIHelper.TempContent(content),
                table[col, row].Rect.width);
        }

        void DrawHeaderAndBrief()
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Label(_container.SectionHeader, new GUIStyle(SirenixGUIStyles.TitleCentered)
            {
                fontSize = 24
            });
            DrawSeparator();
            GUILayout.Label(_container.Introduction, SirenixGUIStyles.MultiLineCenteredLabel);
            DrawSeparator();
            EditorGUILayout.EndVertical();
        }

        void DrawSeparator(float spaceBefore = Padding, float spaceAfter = Padding)
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Space(spaceBefore);
            SirenixEditorGUI.HorizontalLineSeparator(_darkLineColor);
            SirenixEditorGUI.HorizontalLineSeparator(_lightLineColor);
            GUILayout.Space(spaceAfter);
            EditorGUILayout.EndVertical();
        }
    }
}
