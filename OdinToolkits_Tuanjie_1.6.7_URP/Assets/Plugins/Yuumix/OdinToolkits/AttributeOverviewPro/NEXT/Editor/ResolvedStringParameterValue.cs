using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Modules.Editor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    /// <summary>
    /// 被解析的字符串参数
    /// </summary>
    public class ResolvedStringParameterValue
    {
        static readonly BilingualData ResolverTypeLabel = new BilingualData("解析器类型", "Resolver Type");
        static readonly BilingualData ResolverTargetTypeLabel = new BilingualData("解析器目标类型", "Resolver Target Type");
        static readonly BilingualData FallbackValueLabel = new BilingualData("回退值", "Fallback Value");
        static readonly BilingualData NamedValuesLabel = new BilingualData("特殊命名参数值 - Named Values", "Named Values");

        static readonly List<ParameterValue> DefaultExistedNamedValues = new List<ParameterValue>
        {
            new ParameterValue(typeof(InspectorProperty).FullName, "$property",
                new BilingualData("InspectorProperty 代表检查器中的一个 Property，即应用此特性的成员",
                    "The InspectorProperty representing the member that has attribute applied to it."))
        };

        public ResolvedStringParameterValue(string parameterName, ResolverType resolverType,
            string resolverTargetType, string fallbackValue, List<ParameterValue> additionalNamedValues)
        {
            ParameterName = parameterName;
            ResolverType = resolverType;
            ResolverTargetType = resolverTargetType;
            FallbackValue = fallbackValue;
            NamedValues = DefaultExistedNamedValues.Concat(additionalNamedValues).ToList();
            CreateResolverInfoTable();
            CreateNamedValueTable();
            ResizeTables();
        }

        public string ParameterName { get; set; }
        public string Introduction { get; set; }
        public ResolverType ResolverType { get; set; }
        public string ResolverTargetType { get; set; }
        public string FallbackValue { get; set; }
        public List<ParameterValue> NamedValues { get; set; }
        public GUITable ResolverInfoTable { get; private set; }
        public GUITable NamedValueTable { get; private set; }

        public void CreateResolverInfoTable()
        {
            ResolverInfoTable = GUITable.Create(1, null,
                new GUITableColumn
                {
                    ColumnTitle = ResolverTypeLabel,
                    MinWidth = 100f,
                    OnGUI = (rect, _) => { DrawTableCell(rect, GetResolverTypeString()); }
                },
                new GUITableColumn
                {
                    ColumnTitle = ResolverTargetTypeLabel,
                    MinWidth = 140f,
                    OnGUI = (rect, _) => { DrawTableCell(rect, ResolverTargetType); }
                },
                new GUITableColumn
                {
                    ColumnTitle = FallbackValueLabel,
                    MinWidth = 100f,
                    OnGUI = (rect, _) => { DrawTableCell(rect, FallbackValue); }
                });
        }

        public void CreateNamedValueTable()
        {
            NamedValueTable = GUITable.Create(NamedValues, NamedValuesLabel,
                new GUITableColumn
                {
                    ColumnTitle = new BilingualData("参数类型", "Parameter Type"),
                    MinWidth = 140f,
                    OnGUI = (rect, index) => { DrawTableCell(rect, NamedValues[index].ReturnType); }
                },
                new GUITableColumn
                {
                    ColumnTitle = new BilingualData("参数名", "Parameter Name"),
                    MinWidth = 140f,
                    OnGUI = (rect, index) => { DrawTableCell(rect, NamedValues[index].ParameterName); }
                },
                new GUITableColumn
                {
                    ColumnTitle = new BilingualData("参数描述", "Parameter Description"),
                    MinWidth = 200f,
                    OnGUI = (rect, index) => { DrawTableCell(rect, NamedValues[index].GetDescription()); }
                });
        }

        public void ResizeTables()
        {
            var resolverTypeHeight = CalculateHeight(GetResolverTypeString(), ResolverInfoTable, 0, 1);
            var resolvesToHeight = CalculateHeight(ResolverTargetType, ResolverInfoTable, 1, 1);
            var fallbackValueHeight = CalculateHeight(FallbackValue, ResolverInfoTable, 2, 1);
            var maxHeight = Mathf.Max(resolverTypeHeight, resolvesToHeight, fallbackValueHeight);
            ResolverInfoTable[0, 1].Height = maxHeight + 10f;

            for (var row = 2; row < NamedValueTable.RowCount; row++)
            {
                var namedValue = NamedValues[row - 2];
                var nameHeight = CalculateHeight(namedValue.ParameterName, NamedValueTable, 0, row);
                var typeHeight = CalculateHeight(namedValue.ReturnType, NamedValueTable, 1, row);
                var descriptionHeight = CalculateHeight(namedValue.GetDescription(), NamedValueTable, 2, row);
                maxHeight = Mathf.Max(nameHeight, typeHeight, descriptionHeight);

                for (var col = 0; col < NamedValueTable.ColumnCount; col++)
                {
                    NamedValueTable[col, row].Height = maxHeight + 10f;
                }
            }

            ResolverInfoTable.ReCalculateSizes();
            NamedValueTable.ReCalculateSizes();
        }

        static float CalculateHeight(string content, GUITable table, int col, int row) =>
            AttributeOverviewUtility.TableCellTextStyle.CalcHeight(GUIHelper.TempContent(content),
                table[col, row].Rect.width);

        static void DrawTableCell(Rect rect, string text)
        {
            EditorGUI.LabelField(rect, text, AttributeOverviewUtility.TableCellTextStyle);
        }

        string GetResolverTypeString()
        {
            return ResolverType switch
            {
                ResolverType.ValueResolver => "Value Resolver",
                ResolverType.ActionResolver => "Action Resolver",
                _ => ResolverType.ToString()
            };
        }
    }
}
