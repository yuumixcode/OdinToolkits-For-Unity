using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules
{
    public class HideInNoPrefabComponent : MonoBehaviour
    {
        #region Serialized Fields

        [Title("该物体在场景中且不是预制体时，无法看到 noPrefabInstance 字段")]
        [MultiLineProperty]
        public string info2 = "该物体在场景中且不是预制体时，无法看到 noPrefabInstance 字段";

        [HideIn(PrefabKind.None)]
        [InfoBox("此时 PrefabKind.None 无意义，因为满足条件才会消失，而这个条件无法满足")]
        public GameObject instance;

        [HideIn(PrefabKind.NonPrefabInstance)]
        public GameObject noPrefabInstance;

        #endregion
    }
}
