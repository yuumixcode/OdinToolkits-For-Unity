using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class TableListContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "TableList";
        }

        protected override string SetBrief()
        {
            return "更换 List 的显示样式，让列表元素像表格一样显示";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "主要用于数据类型的列表，可以像表格一样根据横列快速查询"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "bool",
                    paramName = "IsReadOnly",
                    paramDescription = "是否只读，默认为 false"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "ShowPaging",
                    paramDescription = "是否显示分页，默认为 false"
                },
                new()
                {
                    returnType = "int",
                    paramName = "NumberOfItemsPerPage",
                    paramDescription = "每页显示的元素数量"
                },
                new()
                {
                    returnType = "int",
                    paramName = "DefaultMinColumnWidth",
                    paramDescription = "默认最小列宽，为40，会被 [TableColumnWidth] 覆盖"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "ShowIndexLabels",
                    paramDescription = "是否显示序号，默认为 false"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "AlwaysExpanded",
                    paramDescription = "是否总是展开，默认为 false"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "DrawScrollView",
                    paramDescription = "是否绘制滚动视图，默认为 true，但是需要设置 ScrollViewHeight 进行配合使用"
                },
                new()
                {
                    returnType = "int",
                    paramName = "MinScrollViewHeight",
                    paramDescription = "滚动视图的最小高度，默认为 350"
                },
                new()
                {
                    returnType = "int",
                    paramName = "MaxScrollViewHeight",
                    paramDescription = "滚动视图的最大高度，默认为 0，需要修改默认值"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "HideToolbar",
                    paramDescription = "是否隐藏工具栏，默认为 false"
                },
                new()
                {
                    returnType = "int",
                    paramName = "CellPadding",
                    paramDescription = "单元格内边距，默认为 2"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(TableListExample));
        }
    }
}