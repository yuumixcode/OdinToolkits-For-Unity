using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [AttributeOverviewProExample]
    public class TableColumnWidthExample : ExampleSO
    {
        #region Serialized Fields

        [TableList]
        public List<CustomClass> list = new List<CustomClass>
        {
            new CustomClass
            {
                index = 1,
                name = "OdinToolkits"
            },
            new CustomClass
            {
                index = 2,
                name = "OdinInspector"
            }
        };

        #endregion

        #region Nested type: ${0}

        [Serializable]
        public class CustomClass
        {
            #region Serialized Fields

            [TableColumnWidth(40)]
            [LabelText("序号")]
            public int index;

            [PreviewField(Height = 50)]
            [TableColumnWidth(60, false)]
            public Texture2D icon;

            [TableColumnWidth(80)]
            public string name;

            [TableColumnWidth(100)]
            public GameObject obj;

            #endregion
        }

        #endregion
    }
}
