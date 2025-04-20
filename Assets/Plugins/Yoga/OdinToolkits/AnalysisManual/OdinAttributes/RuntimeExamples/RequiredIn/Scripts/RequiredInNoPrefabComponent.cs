using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.RuntimeExamples.RequiredIn.Scripts
{
    public class RequiredInNoPrefabComponent : MonoBehaviour
    {
        [RequiredIn(PrefabKind.None)]
        [InfoBox("PrefabKind.None 无意义，所以无法满足这个条件")]
        public GameObject instance;

        [RequiredIn(PrefabKind.NonPrefabInstance)]
        [InfoBox("该物体在场景中且不是预制体时，判断是否为空")]
        public GameObject nonPrefabInstance;
    }
}