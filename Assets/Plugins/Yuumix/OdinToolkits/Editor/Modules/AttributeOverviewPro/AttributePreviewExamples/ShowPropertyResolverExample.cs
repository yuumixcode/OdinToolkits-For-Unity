using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Editor
{
    [OdinToolkitsAttributeExample]
    public class ShowPropertyResolverExample : ExampleOdinSO
    {
        [ShowPropertyResolver]
        public string myString;

        [ShowPropertyResolver]
        public List<int> myList;

        [ShowPropertyResolver]
        public Dictionary<int, Vector3> MyDictionary;
    }
}
