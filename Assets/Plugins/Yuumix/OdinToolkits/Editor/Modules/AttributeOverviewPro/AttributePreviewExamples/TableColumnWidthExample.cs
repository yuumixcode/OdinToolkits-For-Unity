using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Editor
{
    [OdinToolkitsAttributeExample]
    public class TableColumnWidthExample : ExampleSO
    {
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

        [Serializable]
        public class CustomClass
        {
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
        }
    }
}
