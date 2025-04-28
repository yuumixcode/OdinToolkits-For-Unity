using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class ShowPropertyResolverExample : ExampleOdinScriptableObject
    {
        [ShowPropertyResolver] public string myString;

        [ShowPropertyResolver] public List<int> myList;

        [ShowPropertyResolver] public Dictionary<int, Vector3> MyDictionary;
    }
}