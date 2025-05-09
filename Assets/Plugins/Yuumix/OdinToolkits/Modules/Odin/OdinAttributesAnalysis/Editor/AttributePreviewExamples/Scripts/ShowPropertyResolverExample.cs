using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class ShowPropertyResolverExample : ExampleOdinScriptableObject
    {
        [ShowPropertyResolver] public string myString;

        [ShowPropertyResolver] public List<int> myList;

        [ShowPropertyResolver] public Dictionary<int, Vector3> MyDictionary;
    }
}