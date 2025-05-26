using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class TableListContainer : AbsContainer
    {
        protected override string SetHeader() => "TableList";

        protected override string SetBrief() => "更换 List 的显示样式，让列表元素像表格一样显示";

        protected override List<string> SetTip() =>
            new List<string>
            {
                "主要用于数据类型的列表，可以像表格一样根据横列快速查询"
            };

        protected override List<ParamValue> SetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "IsReadOnly",
                    paramDescription = "是否只读，默认为 false"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "ShowPaging",
                    paramDescription = "是否显示分页，默认为 false"
                },
                new ParamValue
                {
                    returnType = "int",
                    paramName = "NumberOfItemsPerPage",
                    paramDescription = "每页显示的元素数量"
                },
                new ParamValue
                {
                    returnType = "int",
                    paramName = "DefaultMinColumnWidth",
                    paramDescription = "默认最小列宽，为40，会被 [TableColumnWidth] 覆盖"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "ShowIndexLabels",
                    paramDescription = "是否显示序号，默认为 false"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "AlwaysExpanded",
                    paramDescription = "是否总是展开，默认为 false"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "DrawScrollView",
                    paramDescription = "是否绘制滚动视图，默认为 true，但是需要设置 ScrollViewHeight 进行配合使用"
                },
                new ParamValue
                {
                    returnType = "int",
                    paramName = "MinScrollViewHeight",
                    paramDescription = "滚动视图的最小高度，默认为 350"
                },
                new ParamValue
                {
                    returnType = "int",
                    paramName = "MaxScrollViewHeight",
                    paramDescription = "滚动视图的最大高度，默认为 0，需要修改默认值"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "HideToolbar",
                    paramDescription = "是否隐藏工具栏，默认为 false"
                },
                new ParamValue
                {
                    returnType = "int",
                    paramName = "CellPadding",
                    paramDescription = "单元格内边距，默认为 2"
                }
            };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(TableListExample));
    }
}
