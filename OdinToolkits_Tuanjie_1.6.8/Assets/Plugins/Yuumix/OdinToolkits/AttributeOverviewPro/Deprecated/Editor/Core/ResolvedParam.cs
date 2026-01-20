using System.Collections.Generic;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public enum ResolverType
    {
        ValueResolver,
        ActionResolver
    }

    public class ResolvedParam
    {
        GUIStyle _tableCellTextStyle;
        public string ParamName;
        public List<ParameterValue> ParamValues;
        public ResolverType ResolverType;
        public string ReturnType;

        public GUITable CreateGUITable()
        {
            _tableCellTextStyle ??= new GUIStyle(SirenixGUIStyles.MultiLineCenteredLabel)
            {
                clipping = TextClipping.Overflow,
                richText = true
            };
            var guiTable = GUITable.Create(ParamValues, null, new GUITableColumn
            {
                ColumnTitle = "序号",
                Width = 50,
                Resizable = false,
                OnGUI = (rect, i) =>
                {
                    EditorGUI.LabelField(rect, (i + 1).ToString(), _tableCellTextStyle);
                }
            }, new GUITableColumn
            {
                ColumnTitle = "返回值类型",
                Width = 120,
                OnGUI = (rect, i) =>
                {
                    DrawTableCell(rect, ParamValues[i].ReturnType);
                }
            }, new GUITableColumn
            {
                ColumnTitle = "参数名",
                Width = 120,
                OnGUI = (rect, i) =>
                {
                    DrawTableCell(rect, ParamValues[i].ParameterName);
                }
            }, new GUITableColumn
            {
                ColumnTitle = "参数描述",
                MinWidth = 150,
                OnGUI = (rect, i) =>
                {
                    DrawTableCell(rect, ParamValues[i].ParameterDescription);
                }
            });
            return guiTable;
        }

        void DrawTableCell(Rect rect, string text, GUIStyle style = null)
        {
            EditorGUI.LabelField(rect, text, style ?? _tableCellTextStyle);
        }
    }
}
