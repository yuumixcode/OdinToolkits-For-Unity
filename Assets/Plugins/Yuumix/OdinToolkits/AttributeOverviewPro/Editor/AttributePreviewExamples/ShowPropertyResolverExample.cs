using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class ShowPropertyResolverExample : ExampleOdinSO
    {
        #region Serialized Fields

        [ShowPropertyResolver]
        public Dictionary<int, Vector3> MyDictionary;

        [ShowPropertyResolver]
        public List<int> myList;

        [ShowPropertyResolver]
        public string myString;

        #endregion
    }
}
