using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated
{
    public class ShowInNoPrefabComponent : MonoBehaviour
    {
        #region Serialized Fields

        [ShowIn(PrefabKind.None)]
        [InfoBox("此时不会显示，因为无法满足显示条件")]
        public GameObject instance;

        [Title("该物体在场景中且不是预制体时，才能看到 noPrefabInstance 字段")]
        [MultiLineProperty]
        public string info2 = "该物体在场景中且不是预制体时，才能看到 noPrefabInstance 字段";

        [ShowIn(PrefabKind.NonPrefabInstance)]
        public GameObject noPrefabInstance;

        #endregion
    }
}
