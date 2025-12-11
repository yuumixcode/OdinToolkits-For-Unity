using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
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
