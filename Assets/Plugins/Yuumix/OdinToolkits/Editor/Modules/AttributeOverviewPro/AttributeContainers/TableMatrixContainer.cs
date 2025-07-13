using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Editor
{
    public class TableMatrixContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "TableMatrix";

        protected override string GetIntroduction() => "将二维数组绘制成一个表格";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "二维数组需要使用 Odin 序列化，案例继承 SerializedScriptableObject",
                "默认绘制表格和代码结构的二维数组是相反的，可以使用参数 Transpose 反转",
                "自定义绘制元素样式要注意 UNITY_EDITOR 宏定义",
                "可以拖拽更换不同行或者列的值，同时 Odin 新增了表格鼠标右键的功能"
            };

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "Transpose",
                    paramDescription = "是否转置，默认为 false"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "Labels",
                    paramDescription =
                        "自定义绘制表头的方法，返回一个元组，方法签名可选: (T, LabelDirection) GetLabel(T[,] array, TableAxis axis, int index)，" +
                        DescriptionConfigs.SupportMemberResolverLite
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "IsReadOnly",
                    paramDescription = "是否只读，默认为 false"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "ResizableColumns",
                    paramDescription = "是否可以修改列宽，默认为 true"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "HorizontalTitle",
                    paramDescription = "横向标题"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "VerticalTitle",
                    paramDescription = "纵向标题"
                },
                new ParamValue
                {
                    returnType = "int",
                    paramName = "RowHeight",
                    paramDescription = "行高"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "SquareCells",
                    paramDescription = "是否使单元格保持正方形，默认为 false"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "HideColumnIndices",
                    paramDescription = "隐藏绘制图表的列标，但实际指的是二维数组的行标，第一列，指的是二维数组第一行的内容"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "HideRowIndices",
                    paramDescription = "隐藏绘制图表的行标，但实际指的是二维数组的列标，第一行，指的是二维数组第一列的内容"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "RespectIndentLevel",
                    paramDescription = "绘制的表是否应遵循当前GUI缩进级别"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "DrawElementMethod",
                    paramDescription = "自定义绘制二维数组中的元素样式，方法签名可选: T CustomDrawElement2(Rect rect, T[,] array, " +
                                       "int x, int y, T value)，其中 array[x,y] == value，" +
                                       DescriptionConfigs.SupportMemberResolverLite
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(TableMatrixExample));
    }
}
