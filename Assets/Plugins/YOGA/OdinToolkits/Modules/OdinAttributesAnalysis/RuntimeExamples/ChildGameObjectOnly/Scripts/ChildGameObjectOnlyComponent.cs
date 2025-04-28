using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.RuntimeExamples.ChildGameObjectOnly.Scripts
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