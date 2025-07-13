using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.RuntimeExamples.RequiredListLength.Scripts
{
    public class RequiredListLengthNoPrefabComponent : MonoBehaviour
    {
        [InfoBox("长度限制不会生效，因为此条件无法满足")]
        [RequiredListLength(3, PrefabKind = PrefabKind.None)]
        public List<int> list = new List<int>();

        [InfoBox("该物体在场景中且不是预制体时，长度限制生效")]
        [RequiredListLength(3, PrefabKind = PrefabKind.NonPrefabInstance)]
        public List<int> list2 = new List<int>();
    }
}
