using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.RuntimeExamples.DisableIn.Scripts
{
    public class DisableInNoPrefabComponent : MonoBehaviour
    {
        [DisableIn(PrefabKind.None)]
        [InfoBox("证明 PrefabKind.None 无意义，仅占位")]
        public GameObject instance;

        [DisableIn(PrefabKind.NonPrefabInstance)]
        [InfoBox("该物体在场景中且不是预制体时，无法选中")]
        public GameObject noPrefabInstance;
    }
}