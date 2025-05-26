using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class TableColumnWidthExample : ExampleScriptableObject
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
