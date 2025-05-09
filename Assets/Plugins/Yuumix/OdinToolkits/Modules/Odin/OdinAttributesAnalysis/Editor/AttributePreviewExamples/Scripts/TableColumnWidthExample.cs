using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class TableColumnWidthExample : ExampleScriptableObject
    {
        [TableList] public List<CustomClass> list = new()
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
            [TableColumnWidth(40)] [LabelText("序号")]
            public int index;

            [PreviewField(Height = 50)] [TableColumnWidth(60, false)]
            public Texture2D icon;

            [TableColumnWidth(80)] public string name;

            [TableColumnWidth(100)] public GameObject obj;
        }
    }
}