using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.OdinAttributeOverviewChinese
{
    public class DisallowModificationsInNoPrefabComponent : MonoBehaviour
    {
        [DisallowModificationsIn(PrefabKind.None)]
        [InfoBox("PrefabKind.None 无意义，无法满足这个要求，仅占位，")]
        public GameObject instance;

        [DisallowModificationsIn(PrefabKind.NonPrefabInstance)]
        [InfoBox("该物体在场景中且不是预制体时，不允许修改该字段")]
        public GameObject noPrefabInstance;
    }
}
