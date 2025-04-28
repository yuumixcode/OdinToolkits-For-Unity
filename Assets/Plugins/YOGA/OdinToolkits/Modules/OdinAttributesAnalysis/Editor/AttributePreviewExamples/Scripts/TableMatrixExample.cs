using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class TableMatrixExample : ExampleOdinScriptableObject
    {
        [PropertyOrder(10)]
        [Title("默认 [TableMatrix]")]
        [DetailedInfoBox("默认规则解释...",
            "默认文字方向为从左至右 - LabelDirection.LeftToRight，" +
            "二维数组的 X，默认为列，Y 默认为行，和二维数组的xy相反",
            InfoMessageType.Info)]
        [TableMatrix]
        public string[,] Matrix0 = new string[2, 3]
        {
            { "[0,0]", "[0,1]", "[0,2]" },
            { "[1,0]", "[1,1]", "[1,2]" }
        };

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [Title("Labels", "Labels = \"GetLabel\"")]
        [TableMatrix(Labels = "GetLabel")]
        public string[,] Matrix1 = new string[2, 3]
        {
            { "[0,0]", "[0,1]", "[0,2]" },
            { "[1,0]", "[1,1]", "[1,2]" }
        };

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [Title("Transpose", "Transpose = true")]
        [TableMatrix(Transpose = true)]
        public string[,] Matrix2 = new string[2, 3]
        {
            { "[0,0]", "[0,1]", "[0,2]" },
            { "[1,0]", "[1,1]", "[1,2]" }
        };

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [Title("IsReadOnly", "IsReadOnly = true")]
        [TableMatrix(IsReadOnly = true)]
        public string[,] Matrix3 = new string[2, 3]
        {
            { "[0,0]", "[0,1]", "[0,2]" },
            { "[1,0]", "[1,1]", "[1,2]" }
        };

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [Title("ResizableColumns", "ResizableColumns = false")]
        [TableMatrix(ResizableColumns = false)]
        public string[,] Matrix4 = new string[2, 3]
        {
            { "[0,0]", "[0,1]", "[0,2]" },
            { "[1,0]", "[1,1]", "[1,2]" }
        };

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [Title("HorizontalTitle", "HorizontalTitle = \"横向标题\"")]
        [TableMatrix(HorizontalTitle = "横向标题")]
        public string[,] Matrix5 = new string[2, 3]
        {
            { "[0,0]", "[0,1]", "[0,2]" },
            { "[1,0]", "[1,1]", "[1,2]" }
        };

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [Title("VerticalTitle", "VerticalTitle = \"纵向标题\"")]
        [TableMatrix(VerticalTitle = "纵向标题")]
        public string[,] Matrix6 = new string[2, 3]
        {
            { "[0,0]", "[0,1]", "[0,2]" },
            { "[1,0]", "[1,1]", "[1,2]" }
        };

        [PropertyOrder(10)] [FoldoutGroup("基础使用")] [Title("RowHeight", "RowHeight = 40")] [TableMatrix(RowHeight = 40)]
        public string[,] Matrix7 = new string[2, 3]
        {
            { "[0,0]", "[0,1]", "[0,2]" },
            { "[1,0]", "[1,1]", "[1,2]" }
        };

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [Title("SquareCells", "SquareCells = true")]
        [TableMatrix(SquareCells = true)]
        public string[,] Matrix8 = new string[2, 3]
        {
            { "[0,0]", "[0,1]", "[0,2]" },
            { "[1,0]", "[1,1]", "[1,2]" }
        };

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [Title("HideColumnIndices", "HideColumnIndices = true，隐藏的是二维数组的行标")]
        [TableMatrix(HideColumnIndices = true)]
        public string[,] Matrix9 = new string[2, 3]
        {
            { "[0,0]", "[0,1]", "[0,2]" },
            { "[1,0]", "[1,1]", "[1,2]" }
        };

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [Title("HideRowIndices", "HideRowIndices = true，隐藏的是二维数组的列标")]
        [TableMatrix(HideRowIndices = true)]
        public string[,] Matrix10 = new string[2, 3]
        {
            { "[0,0]", "[0,1]", "[0,2]" },
            { "[1,0]", "[1,1]", "[1,2]" }
        };

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [InfoBox("绘制的表是否应遵循当前GUI缩进级别")]
        [Title("RespectIndentLevel", "RespectIndentLevel = false")]
        [TableMatrix(RespectIndentLevel = false)]
        public string[,] Matrix11 = new string[2, 3]
        {
            { "[0,0]", "[0,1]", "[0,2]" },
            { "[1,0]", "[1,1]", "[1,2]" }
        };

        [PropertyOrder(10)]
        [FoldoutGroup("进阶使用")]
        [Title("DrawElementMethod", "CustomDrawElement1(Rect rect, string value)")]
        [TableMatrix(DrawElementMethod = "CustomDrawElement1")]
        public string[,] Matrix12 = new string[2, 3]
        {
            { "[0,0]", "[0,1]", "[0,2]" },
            { "[1,0]", "[1,1]", "[1,2]" }
        };

        [PropertyOrder(10)]
        [FoldoutGroup("进阶使用")]
        [Title("DrawElementMethod", "CustomDrawElement2(Rect rect, string[,] array, int x, int y)")]
        [TableMatrix(DrawElementMethod = "CustomDrawElement2")]
        public string[,] Matrix13 = new string[2, 3]
        {
            { "[0,0]", "[0,1]", "[0,2]" },
            { "[1,0]", "[1,1]", "[1,2]" }
        };

        private (string, LabelDirection) GetLabel(string[,] array, TableAxis axis, int index)
        {
            const string chessFileLetters = "ABCDEFGH";
            return axis switch
            {
                TableAxis.Y => (chessFileLetters[chessFileLetters.Length - index - 1].ToString(),
                    LabelDirection.BottomToTop),
                TableAxis.X => (chessFileLetters[index].ToString(), LabelDirection.LeftToRight),
                _ => (index.ToString(), LabelDirection.LeftToRight)
            };
        }

        private string CustomDrawElement1(Rect rect, string value)
        {
#if UNITY_EDITOR
            EditorGUI.LabelField(rect, value);
            return value;
#endif
        }

        private string CustomDrawElement2(Rect rect, string[,] array, int x, int y)
        {
#if UNITY_EDITOR
            var guiStyle = new GUIStyle(EditorStyles.centeredGreyMiniLabel)
            {
                fontSize = 14
            };
            array[x, y] = EditorGUI.TextField(rect, array[x, y], guiStyle);
            return array[x, y];
#endif
        }
    }
}