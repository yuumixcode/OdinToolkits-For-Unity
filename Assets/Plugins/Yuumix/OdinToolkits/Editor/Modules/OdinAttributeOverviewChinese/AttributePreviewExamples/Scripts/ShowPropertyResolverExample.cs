using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class ShowPropertyResolverExample : ExampleOdinScriptableObject
    {
        [ShowPropertyResolver]
        public string myString;

        [ShowPropertyResolver]
        public List<int> myList;

        [ShowPropertyResolver]
        public Dictionary<int, Vector3> MyDictionary;
    }
}
