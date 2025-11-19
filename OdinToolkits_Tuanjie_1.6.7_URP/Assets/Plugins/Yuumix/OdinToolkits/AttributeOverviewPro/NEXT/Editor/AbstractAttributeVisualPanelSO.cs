using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;
using Yuumix.OdinToolkits.Core;
using YuumixEditor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    /// <summary>
    /// Attribute 介绍可视化面板
    /// </summary>
    public abstract class AbstractAttributeVisualPanelSO : SerializedScriptableObject, IOdinToolkitsEditorReset
    {
        static BilingualData _guiTableNumberLabel = new BilingualData("序号", "Number");

        #region Serialized Fields

        bool _selectShowResolvedStringParameters = true;

        [PropertyOrder(-100)]
        [PropertySpace(0, 10)]
        public BilingualHeaderWidget headerWidget;

        #endregion

        [PropertyOrder(-90)]
        [ResponsiveButtonGroup(AnimateVisibility = false)]
        [GUIColor("green")]
        [ShowIf("$_selectShowResolvedStringParameters")]
        [BilingualButton("显示状态-被解析的字符串参数", "Resolved String Parameters Shown")]
        public void HideResolvedStringParameters()
        {
            _selectShowResolvedStringParameters = false;
        }

        [PropertyOrder(-90)]
        [ResponsiveButtonGroup(AnimateVisibility = false)]
        [ShowIf("@!_selectShowResolvedStringParameters")]
        [BilingualButton("隐藏状态-被解析的字符串参数", "Resolved String Parameters Hidden")]
        public void ShowResolvedStringParameters()
        {
            _selectShowResolvedStringParameters = true;
        }

        bool SelectResolvedStringParametersButtonDown => _selectShowResolvedStringParameters = true;

        [NonSerialized]
        AbstractAttributeModel _model;

        bool CanShowResolvedStringParameters =>
            _selectShowResolvedStringParameters && _resolvedStringParameters is { Count: > 0 };

        #region IOdinToolkitsEditorReset Members

        public virtual void EditorReset()
        {
            _selectShowResolvedStringParameters = true;
        }

        #endregion

        protected void SetModel(AbstractAttributeModel attributeModel)
        {
            _model = attributeModel;
            _model.Initialize();
            headerWidget = _model.HeaderWidget;
            _usageTips = _model.UsageTips;
            if (_usageTips != null)
            {
                CreateUsageTipsTable();
                ResizeUsageTipsTable();
                InspectorBilingualismConfigSO.OnLanguageChanged -= CreateUsageTipsTable;
                InspectorBilingualismConfigSO.OnLanguageChanged += CreateUsageTipsTable;
            }

            _attributeParameters = _model.AttributeParameters;
            if (_attributeParameters != null)
            {
                CreateAttributeParametersGUITable();
                ResizeAttributeParameterTable();
                InspectorBilingualismConfigSO.OnLanguageChanged -= CreateAttributeParametersGUITable;
                InspectorBilingualismConfigSO.OnLanguageChanged += CreateAttributeParametersGUITable;
            }

            _resolvedStringParameters = _model.ResolvedStringParameters;
            if (_resolvedStringParameters != null)
            {
                foreach (var rValue in _resolvedStringParameters)
                {
                    InspectorBilingualismConfigSO.OnLanguageChanged -= rValue.CreateResolverInfoTable;
                    InspectorBilingualismConfigSO.OnLanguageChanged += rValue.CreateResolverInfoTable;
                    InspectorBilingualismConfigSO.OnLanguageChanged -= rValue.CreateNamedValueTable;
                    InspectorBilingualismConfigSO.OnLanguageChanged += rValue.CreateNamedValueTable;
                }
            }
        }

        void Test_ShowAttributeParameterTableRect(int col, int row)
        {
            Debug.Log($"GUITable 的 {col} 列 {row} 行的 Rect 为 {_attributeParametersTable[col, row].Rect}");
            SirenixEditorGUI.DrawBorders(_attributeParametersTable[col, row].Rect, 1, Color.green);
        }

        #region Usage Tips

        static BilingualData _usageTipsLabel = new BilingualData("使用提示", "Usage Tips");
        Rect _usageTipContentRect;
        List<string> _usageTips;

        #region GUITable

        [HideIf("$UsageTipIsEmpty")]
        [PropertyOrder(-60)]
        [OnInspectorGUI]
        [PropertySpace(0, 20)]
        void DrawUsageTips()
        {
            _usageTipContentRect = BeginDrawContainer(_usageTipsLabel);
            _usageTipsTable.DrawTable();
            ResizeUsageTipsTable();
            EndDrawContainer(_usageTipContentRect);
        }

        GUITable _usageTipsTable;

        void CreateUsageTipsTable()
        {
            _usageTipsTable = GUITable.Create(_usageTips, null, new GUITableColumn
                {
                    ColumnTitle = _guiTableNumberLabel,
                    Width = 60,
                    OnGUI = (rect, index) =>
                    {
                        EditorGUI.LabelField(rect, (index + 1).ToString(),
                            AttributeOverviewUtility.TableCellTextStyle);
                    }
                },
                new GUITableColumn
                {
                    ColumnTitle = _usageTipsLabel,
                    MinWidth = 200,
                    OnGUI = (rect, index) =>
                    {
                        EditorGUI.LabelField(rect, _usageTips[index],
                            AttributeOverviewUtility.TableCellTextStyle);
                    }
                });
        }

        void ResizeUsageTipsTable()
        {
            var tableRowHeight = new int[_usageTipsTable.RowCount];
            for (var row = 0; row < _usageTipsTable.RowCount; row++)
            {
                for (var col = 0; col < _usageTipsTable.ColumnCount; col++)
                {
                    switch (row)
                    {
                        case 0:
                            tableRowHeight[0] = (int)AttributeOverviewUtility.TableCellTextStyle.CalcHeight(
                                _usageTipsLabel,
                                _usageTipsTable[col, row].Rect.width);
                            break;
                    }

                    if (row != 0)
                    {
                        tableRowHeight[row] = (int)AttributeOverviewUtility.TableCellTextStyle.CalcHeight(
                            _usageTips[row - 1],
                            _usageTipsTable[col, row].Rect.width);
                    }
                }

                _usageTipsTable[0, row].Height = tableRowHeight[row] + 10f;
            }

            _usageTipsTable.ReCalculateSizes();
        }

        bool UsageTipIsEmpty => _usageTips == null || _usageTips.Count == 0;

        #endregion

        #endregion

        #region Attribute Parameters

        static BilingualData _attributeParametersTitleLabel = new BilingualData("特性参数", "Attribute Parameters");

        static BilingualData _attributeParameterReturnTypeLabel = new BilingualData("返回值类型", "Return Type");
        static BilingualData _attributeParameterParamNameLabel = new BilingualData("参数名", "Parameter Name");

        static BilingualData _attributeParameterParamDescriptionLabel =
            new BilingualData("参数描述", "Parameter Description");

        List<ParameterValue> _attributeParameters;

        [HideIf("$AttributeParameterIsEmpty")]
        [PropertyOrder(-10)]
        [OnInspectorGUI]
        [PropertySpace(0, 20)]
        void DrawAttributeParameters()
        {
            _attributeParametersContentRect = BeginDrawContainer(_attributeParametersTitleLabel);
            _attributeParametersTable.DrawTable();
            ResizeAttributeParameterTable();
            EndDrawContainer(_attributeParametersContentRect);
        }

        bool AttributeParameterIsEmpty => _attributeParameters == null || _attributeParameters.Count == 0;
        Rect _attributeParametersContentRect;

        #region GUITable

        GUITable _attributeParametersTable;

        void CreateAttributeParametersGUITable()
        {
            _attributeParametersTable = GUITable.Create(_attributeParameters, null,
                new GUITableColumn
                {
                    ColumnTitle = _guiTableNumberLabel,
                    Width = 60,
                    OnGUI = (rect, index) =>
                    {
                        EditorGUI.LabelField(rect, (index + 1).ToString(),
                            AttributeOverviewUtility.TableCellTextStyle);
                    }
                },
                new GUITableColumn
                {
                    ColumnTitle = _attributeParameterReturnTypeLabel,
                    Width = 140,
                    OnGUI = (rect, index) =>
                    {
                        EditorGUI.LabelField(rect, _attributeParameters[index].ReturnType,
                            AttributeOverviewUtility.TableCellTextStyle);
                    }
                },
                new GUITableColumn
                {
                    ColumnTitle = _attributeParameterParamNameLabel,
                    MinWidth = 140,
                    OnGUI = (rect, index) =>
                    {
                        EditorGUI.LabelField(rect, _attributeParameters[index].ParameterName,
                            AttributeOverviewUtility.TableCellTextStyle);
                    }
                },
                new GUITableColumn
                {
                    ColumnTitle = _attributeParameterParamDescriptionLabel,
                    MinWidth = 200,
                    OnGUI = (rect, index) =>
                    {
                        EditorGUI.LabelField(rect, _attributeParameters[index].GetDescription(),
                            AttributeOverviewUtility.TableCellTextStyle);
                    }
                });
        }

        void ResizeAttributeParameterTable()
        {
            var tableRowHeight = new int[_attributeParametersTable.RowCount];
            for (var row = 0; row < _attributeParametersTable.RowCount; row++)
            {
                for (var col = 0; col < _attributeParametersTable.ColumnCount; col++)
                {
                    switch (row)
                    {
                        case 0:
                            tableRowHeight[0] = (int)Mathf.Max(
                                AttributeOverviewUtility.TableCellTextStyle.CalcHeight(
                                    _attributeParametersTitleLabel,
                                    _attributeParametersTable[col, row].Rect.width),
                                AttributeOverviewUtility.TableCellTextStyle.CalcHeight(
                                    _attributeParameterParamNameLabel, _attributeParametersTable[col, row].Rect.width),
                                AttributeOverviewUtility.TableCellTextStyle.CalcHeight(
                                    _attributeParameterParamDescriptionLabel,
                                    _attributeParametersTable[col, row].Rect.width));
                            break;
                    }

                    if (row != 0)
                    {
                        tableRowHeight[row] = (int)Mathf.Max(
                            AttributeOverviewUtility.TableCellTextStyle.CalcHeight(
                                _attributeParameters[row - 1].ReturnType,
                                _attributeParametersTable[col, row].Rect.width),
                            AttributeOverviewUtility.TableCellTextStyle.CalcHeight(
                                _attributeParameters[row - 1].ParameterName,
                                _attributeParametersTable[col, row].Rect.width),
                            AttributeOverviewUtility.TableCellTextStyle.CalcHeight(
                                _attributeParameters[row - 1].GetDescription(),
                                _attributeParametersTable[col, row].Rect.width));
                    }
                }

                _attributeParametersTable[0, row].Height = tableRowHeight[row] + 10f;
            }

            _attributeParametersTable.ReCalculateSizes();
        }

        #endregion

        #endregion

        #region Resolved String Parameters

        static BilingualData _resolvedStringParameterLabel =
            new BilingualData("被解析的字符串参数", "Resolved String Parameters");

        [HideIf("$ResolvedStringParametersIsEmpty")]
        [PropertyOrder(0)]
        [OnInspectorGUI]
        void DrawBeforeResolvedStringParameters()
        {
            _resolvedStringParametersContentRect = BeginDrawContainer(_resolvedStringParameterLabel,
                AttributeOverviewUtility.ContainerTitleStyle);
            SirenixEditorGUI.BeginVerticalList(false);

            for (var i = 0; i < _resolvedStringParameters.Count; i++)
            {
                SirenixEditorGUI.BeginListItem(false);
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(8);
                EditorGUILayout.BeginVertical();
                GUILayout.Space(5);
                GUILayout.Label(_resolvedStringParameters[i].ParameterName,
                    AttributeOverviewUtility.ResolvedStringParameterValueTitleStyle);
                GUILayout.Space(5);
                SirenixEditorGUI.HorizontalLineSeparator(new Color(1, 1, 1, 0.4f));
                GUILayout.Space(10);
                _resolvedStringParameters[i].ResolverInfoTable.DrawTable();
                GUILayout.Space(10);
                _resolvedStringParameters[i].NamedValueTable.DrawTable();
                _resolvedStringParameters[i].ResizeTables();
                GUILayout.Space(8);
                EditorGUILayout.EndVertical();
                GUILayout.Space(5);
                EditorGUILayout.EndHorizontal();
                SirenixEditorGUI.EndListItem();
            }

            SirenixEditorGUI.EndVerticalList();
            EndDrawContainer(_resolvedStringParametersContentRect);
        }

        List<ResolvedStringParameterValue> _resolvedStringParameters;

        Rect _resolvedStringParametersContentRect;

        bool ResolvedStringParametersIsEmpty =>
            _resolvedStringParameters == null || _resolvedStringParameters.Count == 0;

        #endregion

        #region Draw Container

        static Rect BeginDrawContainer(string title, GUIStyle titleStyle = null)
        {
            titleStyle ??= AttributeOverviewUtility.ContainerTitleStyle;
            var titleWidth = titleStyle.CalcSize(GUIHelper.TempContent(title)).x;
            var titleHeight = titleStyle.CalcSize(GUIHelper.TempContent(title)).y;
            var headerRect = SirenixEditorGUI.BeginHorizontalToolbar(titleHeight + 15f);
            var titleRect = headerRect.AlignCenter(titleWidth);
            EditorGUI.LabelField(titleRect, title, titleStyle);
            GUILayout.FlexibleSpace();
            SirenixEditorGUI.EndHorizontalToolbar();
            GUILayout.Space(-2);
            return EditorGUILayout.BeginVertical(AttributeOverviewUtility.ContainerContentStyle);
        }

        static void EndDrawContainer(Rect contentRect)
        {
            EditorGUILayout.EndVertical();
            SirenixEditorGUI.DrawBorders(contentRect, 1);
        }

        #endregion
    }

    /// <summary>
    /// 被解析的字符串参数的 GUITable 组，包含解析器信息表和命名值表，通过 ParameterName 和 ResolvedStringParameterValue 进行关联
    /// </summary>
    public class ResolvedStringParameterGUITableGroup
    {
        public string ParameterName;
        public GUITable ResolverInfoTable;
        public GUITable NamedValueTable;
        public ResolvedStringParameterGUITableGroup(string parameterName) => ParameterName = parameterName;
    }
}
