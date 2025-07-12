using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Yuumix.OdinToolkits.OdinAttributeOverviewChinese
{
    public class ChildGameObjectOnlyComponent : MonoBehaviour
    {
        [ChildGameObjectsOnly(IncludeSelf = false)]
        public GameObject onlyChild;

        [ChildGameObjectsOnly(IncludeInactive = true)]
        public BoxCollider onlyCollider;

        [ChildGameObjectsOnly(IncludeSelf = true)]
        public List<GameObject> children;
    }
}
