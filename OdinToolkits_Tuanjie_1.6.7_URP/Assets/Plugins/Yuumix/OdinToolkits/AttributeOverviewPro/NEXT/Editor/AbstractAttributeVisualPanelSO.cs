using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Linq;
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

        AbstractAttributeModel _model;

        #endregion

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

            _examplePreviewItems = _model.ExamplePreviewItems;
            currentSelectedExample = _model.GetInitialExample();
        }

        void Test_ShowAttributeParameterTableRect(int col, int row)
        {
            Debug.Log($"GUITable 的 {col} 列 {row} 行的 Rect 为 {_attributeParametersTable[col, row].Rect}");
            SirenixEditorGUI.DrawBorders(_attributeParametersTable[col, row].Rect, 1, Color.green);
        }

        #region Usage Tips

        static BilingualData _usageTipsLabel = new BilingualData("使用提示", "Usage Tips");
        Rect _usageTipContentRect;
        string[] _usageTips;

        #region GUITable

        [HideIf("$UsageTipIsEmpty")]
        [PropertyOrder(-60)]
        [OnInspectorGUI]
        [PropertySpace(0, 20)]
        void DrawUsageTips()
        {
            _usageTipContentRect = BeginDrawContainerWithTitle(_usageTipsLabel, out _);
            _usageTipsTable.DrawTable();
            ResizeUsageTipsTable();
            EndDrawContainerWithTitle(_usageTipContentRect);
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

        bool UsageTipIsEmpty => _usageTips == null || _usageTips.Length == 0;

        #endregion

        #endregion

        #region Attribute Parameters

        static BilingualData _attributeParametersTitleLabel = new BilingualData("特性参数", "Attribute Parameters");
        static BilingualData _attributeParameterReturnTypeLabel = new BilingualData("返回值类型", "Return Type");
        static BilingualData _attributeParameterParamNameLabel = new BilingualData("参数名", "Parameter Name");

        static BilingualData _attributeParameterParamDescriptionLabel =
            new BilingualData("参数描述", "Parameter Description");

        ParameterValue[] _attributeParameters;

        [HideIf("$AttributeParameterIsEmpty")]
        [PropertyOrder(-20)]
        [OnInspectorGUI]
        [PropertySpace(0, 20)]
        void DrawAttributeParameters()
        {
            _attributeParametersContentRect = BeginDrawContainerWithTitle(_attributeParametersTitleLabel, out _);
            _attributeParametersTable.DrawTable();
            ResizeAttributeParameterTable();
            EndDrawContainerWithTitle(_attributeParametersContentRect);
        }

        bool AttributeParameterIsEmpty => _attributeParameters == null || _attributeParameters.Length == 0;
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

        ResolvedStringParameterValue[] _resolvedStringParameters;

        Rect _resolvedStringParametersContentRect;

        bool ResolvedStringParametersIsEmpty =>
            _resolvedStringParameters == null || _resolvedStringParameters.Length == 0;

        [HideIf("$ResolvedStringParametersIsEmpty")]
        [PropertyOrder(-10)]
        [OnInspectorGUI]
        [PropertySpace(0, 20)]
        void DrawResolvedStringParameters()
        {
            _resolvedStringParametersContentRect = BeginDrawContainerWithTitle(_resolvedStringParameterLabel, out _);
            SirenixEditorGUI.BeginVerticalList(false);
            foreach (var resolvedString in _resolvedStringParameters)
            {
                SirenixEditorGUI.BeginListItem(false);
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(8);
                EditorGUILayout.BeginVertical();
                GUILayout.Space(5);
                GUILayout.Label(resolvedString.ParameterName,
                    AttributeOverviewUtility.ResolvedStringParameterValueTitleStyle);
                GUILayout.Space(5);
                SirenixEditorGUI.HorizontalLineSeparator(new Color(1, 1, 1, 0.4f));
                GUILayout.Space(10);
                resolvedString.ResolverInfoTable.DrawTable();
                GUILayout.Space(10);
                resolvedString.NamedValueTable.DrawTable();
                resolvedString.ResizeAllTables();
                GUILayout.Space(8);
                EditorGUILayout.EndVertical();
                GUILayout.Space(5);
                EditorGUILayout.EndHorizontal();
                SirenixEditorGUI.EndListItem();
            }

            SirenixEditorGUI.EndVerticalList();
            EndDrawContainerWithTitle(_resolvedStringParametersContentRect);
        }

        #endregion

        #region Usage Example

        static BilingualData _usageExampleLabel =
            new BilingualData("使用案例", "Usage Examples");

        AttributeExamplePreviewItem[] _examplePreviewItems;
        Rect _usageExampleContentRect;
        Rect _usageHeaderToolbarRect;
        const int EXAMPLE_NUMBER_ONE_ROW = 3;

        bool UsageExampleItemsIsEmpty => _examplePreviewItems == null || _examplePreviewItems.Length == 0;

        [HideIf("$UsageExampleItemsIsEmpty")]
        [OnInspectorGUI]
        [PropertyOrder(-1)]
        void DrawUsageExamplePreview()
        {
            _usageExampleContentRect = BeginDrawContainerWithTitle(_usageExampleLabel, out var headerToolbarRect);
            _usageHeaderToolbarRect = headerToolbarRect;
            DrawExamplePreviewItems();
        }

        [HideIf("$UsageExampleItemsIsEmpty")]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        [PropertyOrder(0)]
        public ScriptableObject currentSelectedExample;

        [HideIf("$UsageExampleItemsIsEmpty")]
        [PropertySpace(0, 20)]
        [PropertyOrder(100)]
        [OnInspectorGUI]
        void EndDrawUsageExampleContainer()
        {
            EndDrawContainerWithTitle(_usageExampleContentRect);
            DrawUsageExampleTitleButton();
        }

        void DrawUsageExampleTitleButton()
        {
            var headerButtonRect = _usageHeaderToolbarRect.AlignCenterY(_usageHeaderToolbarRect.height).AlignRight(220);
            var leftButtonRect = headerButtonRect.Split(0, 2);
            if (GUI.Button(leftButtonRect, GUIHelper.TempContent("Ping 脚本文件"),
                    SirenixGUIStyles.ToolbarButton))
            {
                Debug.Log("跳转到案例的脚本文件");
                EditorGUIUtility.PingObject(GetCurrentExampleMonoScript());
            }

            var rightButtonRect = headerButtonRect.Split(1, 2);
            if (GUI.Button(rightButtonRect, GUIHelper.TempContent("重置案例"), SirenixGUIStyles.ToolbarButton))
            {
                Debug.Log("重置当前案例");
            }
        }

        UnityEngine.Object GetCurrentExampleMonoScript()
        {
            if (!currentSelectedExample)
            {
                return null;
            }

            var markExampleAttribute = TypeCache
                .GetTypesWithAttribute<AttributeOverviewProExampleAttribute>()
                .First(type => type == currentSelectedExample.GetType())
                .GetCustomAttribute<AttributeOverviewProExampleAttribute>();
            var monoScriptAbsolutePath = markExampleAttribute.FilePath;
            var assetRelativePath =
                "Assets/" + PathUtilities.MakeRelative(Application.dataPath, monoScriptAbsolutePath);
            return AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetRelativePath);
        }

        void DrawExamplePreviewItems()
        {
            if (_examplePreviewItems is not { Length: > 1 })
            {
                return;
            }

            EditorGUILayout.BeginVertical();
            for (var i = 0; i < _examplePreviewItems.Length; i += 3)
            {
                EditorGUILayout.BeginHorizontal();
                for (var j = 0; j < EXAMPLE_NUMBER_ONE_ROW && i + j < _examplePreviewItems.Length; j++)
                {
                    DrawExampleTabButton(_examplePreviewItems[i + j]);
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
            GUILayout.Space(10f);
        }

        void DrawExampleTabButton(AttributeExamplePreviewItem item)
        {
            var content = GUIHelper.TempContent(" " + item.ItemName,
                GUIHelper.GetAssetThumbnail(null, typeof(MonoBehaviour), false));
            var iconSizeBackup = EditorGUIUtility.GetIconSize();
            EditorGUIUtility.SetIconSize(new Vector2(16f, 16f));
            var rect3 = GUILayoutUtility.GetRect(content, AttributeOverviewUtility.TabButtonCellTextStyle,
                GUILayoutOptions.Height(26));
            SirenixEditorGUI.DrawBorders(rect3, 1);
            var selectExample = item.ExampleType == AttributeExampleType.OdinSerialized
                ? item.OdinSerializedExample
                : item.UnitySerializedExample;
            if (selectExample == currentSelectedExample)
            {
                var color = EditorGUIUtility.isProSkin
                    ? new Color(0.25f, 0.4f, 0.6f, 1f)
                    : new Color(0.7f, 0.8f, 0.9f, 1f);
                var innerRect = new Rect(rect3.x + 1f, rect3.y + 1f, rect3.width - 2f, rect3.height - 2f);
                EditorGUI.DrawRect(innerRect, color);
            }

            if (GUI.Button(rect3, GUIContent.none, GUIStyle.none))
            {
                currentSelectedExample = selectExample;
            }

            if (currentSelectedExample != selectExample && rect3.Contains(Event.current.mousePosition))
            {
                GUIHelper.PushColor(new Color(1f, 1f, 1f, 0.4f));
                var hoverInnerRect = new Rect(
                    rect3.x + 1f,
                    rect3.y + 1f,
                    rect3.width - 2f,
                    rect3.height - 2f
                );
                EditorGUI.DrawRect(hoverInnerRect, SirenixGUIStyles.DarkEditorBackground);
                GUIHelper.PopColor();
            }

            var labelStyle = EditorGUIUtility.isProSkin
                ? SirenixGUIStyles.WhiteLabelCentered
                : SirenixGUIStyles.LabelCentered;
            GUI.Label(rect3, content, labelStyle);
            EditorGUIUtility.SetIconSize(iconSizeBackup);
        }

        #endregion

        #region Draw Container

        static Rect BeginDrawContainerWithTitle(string title, out Rect headerToolBarRect)
        {
            var titleStyle = AttributeOverviewUtility.ContainerTitleStyle;
            var titleWidth = titleStyle.CalcSize(GUIHelper.TempContent(title)).x;
            var titleHeight = titleStyle.CalcSize(GUIHelper.TempContent(title)).y;
            headerToolBarRect = SirenixEditorGUI.BeginHorizontalToolbar(titleHeight + 12f);
            var titleRect = headerToolBarRect.AlignCenter(titleWidth);
            EditorGUI.LabelField(titleRect, title, titleStyle);
            GUILayout.FlexibleSpace();
            SirenixEditorGUI.EndHorizontalToolbar();
            GUILayout.Space(-2);
            return EditorGUILayout.BeginVertical(AttributeOverviewUtility.ContainerContentStyle);
        }

        static void EndDrawContainerWithTitle(Rect contentRect)
        {
            EditorGUILayout.EndVertical();
            SirenixEditorGUI.DrawBorders(contentRect, 1);
        }

        static void DrawContainerWithTitle(string title, Action drawContent, out Rect headerToolBarRect)
        {
            var contentRect = BeginDrawContainerWithTitle(title, out headerToolBarRect);
            drawContent();
            EndDrawContainerWithTitle(contentRect);
        }

        #endregion

        // #region Selected Button
        //
        // [PropertyOrder(-90)]
        // [ResponsiveButtonGroup(AnimateVisibility = false)]
        // [GUIColor("green")]
        // [ShowIf("$_selectShowResolvedStringParameters")]
        // [BilingualButton("显示状态-被解析的字符串参数", "Resolved String Parameters Shown")]
        // public void HideResolvedStringParameters()
        // {
        //     _selectShowResolvedStringParameters = false;
        // }
        //
        // [PropertyOrder(-90)]
        // [ResponsiveButtonGroup(AnimateVisibility = false)]
        // [ShowIf("@!_selectShowResolvedStringParameters")]
        // [BilingualButton("隐藏状态-被解析的字符串参数", "Resolved String Parameters Hidden")]
        // public void ShowResolvedStringParameters()
        // {
        //     _selectShowResolvedStringParameters = true;
        // }
        //
        // bool SelectResolvedStringParametersButtonDown => _selectShowResolvedStringParameters = true;
        //
        // bool CanShowResolvedStringParameters =>
        //     _selectShowResolvedStringParameters && _resolvedStringParameters is { Count: > 0 };
        //
        // #endregion
    }
}
