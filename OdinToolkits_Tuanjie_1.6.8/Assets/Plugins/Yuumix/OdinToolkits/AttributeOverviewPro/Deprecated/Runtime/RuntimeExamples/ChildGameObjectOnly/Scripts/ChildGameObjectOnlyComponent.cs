using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated
{
    public class ChildGameObjectOnlyComponent : MonoBehaviour
    {
        #region Serialized Fields

        [ChildGameObjectsOnly(IncludeSelf = false)]
        public GameObject onlyChild;

        [ChildGameObjectsOnly(IncludeInactive = true)]
        public BoxCollider onlyCollider;

        [ChildGameObjectsOnly(IncludeSelf = true)]
        public List<GameObject> children;

        #endregion
    }
}
