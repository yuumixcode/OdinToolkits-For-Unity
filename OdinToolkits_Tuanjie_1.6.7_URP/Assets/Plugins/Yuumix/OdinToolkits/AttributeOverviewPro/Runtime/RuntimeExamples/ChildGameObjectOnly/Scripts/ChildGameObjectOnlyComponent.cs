using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules
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
