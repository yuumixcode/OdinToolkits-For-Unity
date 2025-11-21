using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [AttributeOverviewProExample]
    public class TableListExample : ExampleSO
    {
        #region Serialized Fields

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [Title("IsReadOnly", "IsReadOnly = true")]
        [TableList(IsReadOnly = true)]
        public List<CustomClass> isReadOnlyList = new List<CustomClass>
        {
            new CustomClass
            {
                序号 = 1,
                name = "OdinToolkits"
            }
        };

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [Title("ShowPaging 和 NumberOfItemsPerPage", "ShowPaging = true，NumberOfItemsPerPage = 5")]
        [TableList(ShowPaging = true, NumberOfItemsPerPage = 5)]
        public List<CustomClass> numberOfItemsPerPageList = new List<CustomClass>
        {
            new CustomClass
            {
                序号 = 1,
                name = "OdinToolkits"
            }
        };

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [Title("DefaultMinColumnWidth", "DefaultMinColumnWidth = 80")]
        [TableList(DefaultMinColumnWidth = 80)]
        public List<CustomClass> defaultMinColumnWidthList = new List<CustomClass>
        {
            new CustomClass
            {
                序号 = 1,
                name = "OdinToolkits"
            }
        };

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [Title("ShowIndexLabels", "ShowIndexLabels = true")]
        [TableList(ShowIndexLabels = true)]
        public List<CustomClass> showIndexLabelsList = new List<CustomClass>
        {
            new CustomClass
            {
                序号 = 1,
                name = "OdinToolkits"
            }
        };

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [Title("AlwaysExpanded", "AlwaysExpanded = true")]
        [TableList(AlwaysExpanded = true)]
        public List<CustomClass> alwaysExpandedList = new List<CustomClass>
        {
            new CustomClass
            {
                序号 = 1,
                name = "OdinToolkits"
            }
        };

        [PropertyOrder(10)]
        [FoldoutGroup("进阶使用")]
        [Title("DrawScrollView 和 MinScrollViewHeight 和 MaxScrollViewHeight",
            "DrawScrollView = true, MinScrollViewHeight = 100, MaxScrollViewHeight = 200")]
        [TableList(DrawScrollView = true, MinScrollViewHeight = 100, MaxScrollViewHeight = 200)]
        public List<CustomClass> scrollViewHeightList = new List<CustomClass>
        {
            new CustomClass
            {
                序号 = 1,
                name = "OdinToolkits"
            }
        };

        [PropertyOrder(10)]
        [FoldoutGroup("进阶使用")]
        [Title("HideToolbar", "HideToolbar = true")]
        [TableList(HideToolbar = true, AlwaysExpanded = true, ShowIndexLabels = true)]
        public List<CustomClass> hideToolbarList = new List<CustomClass>
        {
            new CustomClass
            {
                序号 = 1,
                name = "OdinToolkits"
            },
            new CustomClass
            {
                序号 = 2,
                name = "OdinInspector"
            }
        };

        [PropertyOrder(10)]
        [FoldoutGroup("进阶使用")]
        [Title("CellPadding", "CellPadding = 10")]
        [TableList(CellPadding = 10)]
        public List<CustomClass> cellPaddingList2 = new List<CustomClass>
        {
            new CustomClass
            {
                序号 = 1,
                name = "OdinToolkits"
            }
        };

        #endregion

        #region Nested type: ${0}

        [Serializable]
        public class CustomClass
        {
            #region Serialized Fields

            [TableColumnWidth(40, false)]
            public int 序号;

            [PreviewField(Height = 50)]
            [TableColumnWidth(60, false)]
            public Texture2D icon;

            public string name;

            public GameObject obj;

            #endregion
        }

        #endregion
    }
}
