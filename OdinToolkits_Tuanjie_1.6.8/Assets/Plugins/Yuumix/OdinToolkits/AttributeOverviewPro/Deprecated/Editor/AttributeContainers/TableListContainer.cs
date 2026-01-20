using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class TableListContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "TableList";

        protected override string GetIntroduction() => "更换 List 的显示样式，让列表元素像表格一样显示";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "主要用于数据类型的列表，可以像表格一样根据横列快速查询"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "IsReadOnly",
                    ParameterDescription = "是否只读，默认为 false"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "ShowPaging",
                    ParameterDescription = "是否显示分页，默认为 false"
                },
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "NumberOfItemsPerPage",
                    ParameterDescription = "每页显示的元素数量"
                },
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "DefaultMinColumnWidth",
                    ParameterDescription = "默认最小列宽，为40，会被 [TableColumnWidth] 覆盖"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "ShowIndexLabels",
                    ParameterDescription = "是否显示序号，默认为 false"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "AlwaysExpanded",
                    ParameterDescription = "是否总是展开，默认为 false"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "DrawScrollView",
                    ParameterDescription = "是否绘制滚动视图，默认为 true，但是需要设置 ScrollViewHeight 进行配合使用"
                },
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "MinScrollViewHeight",
                    ParameterDescription = "滚动视图的最小高度，默认为 350"
                },
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "MaxScrollViewHeight",
                    ParameterDescription = "滚动视图的最大高度，默认为 0，需要修改默认值"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "HideToolbar",
                    ParameterDescription = "是否隐藏工具栏，默认为 false"
                },
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "CellPadding",
                    ParameterDescription = "单元格内边距，默认为 2"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(TableListExample));
    }
}
