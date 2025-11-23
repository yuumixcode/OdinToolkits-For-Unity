using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated
{
    public class RequiredInNoPrefabComponent : MonoBehaviour
    {
        #region Serialized Fields

        [RequiredIn(PrefabKind.None)]
        [InfoBox("PrefabKind.None 无意义，所以无法满足这个条件")]
        public GameObject instance;

        [RequiredIn(PrefabKind.NonPrefabInstance)]
        [InfoBox("该物体在场景中且不是预制体时，判断是否为空")]
        public GameObject nonPrefabInstance;

        #endregion
    }
}
