using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.RuntimeExamples.EnableIn.Scripts
{
    public class EnableInNoPrefabComponent : MonoBehaviour
    {
        [EnableIn(PrefabKind.None)] [InfoBox("此时 PrefabKind.None 有意义，因为满足条件才可以获取焦点")]
        public GameObject instance;

        [EnableIn(PrefabKind.NonPrefabInstance)] [InfoBox("该物体在场景中且不是预制体时，可以选中")]
        public GameObject noPrefabInstance;
    }
}