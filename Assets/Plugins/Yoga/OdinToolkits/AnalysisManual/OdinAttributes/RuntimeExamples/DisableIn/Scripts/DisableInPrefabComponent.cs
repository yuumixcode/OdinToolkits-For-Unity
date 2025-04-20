using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.RuntimeExamples.DisableIn.Scripts
{
    public class DisableInPrefabComponent : MonoBehaviour
    {
        [Title("PrefabKind.InstanceInScene")]
        [InfoBox("该物体是场景中的预制体时，无法选中")]
        [DisableIn(PrefabKind.InstanceInScene)]
        public string instanceInScene = "Instances of prefabs in scenes";

        [Title("PrefabKind.InstanceInPrefab")]
        [InfoBox("该物体是场景中的嵌套预制体时，无法选中")]
        [DisableIn(PrefabKind.InstanceInPrefab)]
        public string instanceInPrefab = "Instances of prefabs nested inside other prefabs";

        [Title("PrefabKind.Regular")]
        [InfoBox("该物体是常规预制体资产，不是场景中的物体时，无法选中")]
        [DisableIn(PrefabKind.Regular)]
        public string regular = "Regular prefab assets";

        [Title("PrefabKind.Regular")]
        [InfoBox("该物体是预制体变体资产，不是场景中的物体时，无法选中")]
        [DisableIn(PrefabKind.Variant)]
        public string variant = "Prefab variant assets";

        [Title("PrefabKind.PrefabInstance")]
        [InfoBox("该物体是场景中的预制体或者嵌套预制体时，无法选中")]
        [DisableIn(PrefabKind.PrefabInstance)]
        public string prefabInstance =
            "Instances of regular prefabs, and prefab variants in scenes or nested in other prefabs";

        [Title("PrefabKind.PrefabAsset")]
        [InfoBox("该物体是预制体资产，不是场景中的物体时，无法选中")]
        [DisableIn(PrefabKind.PrefabAsset)]
        public string prefabAsset = "Prefab assets and prefab variant assets";

        [Title("PrefabKind.PrefabAsset")]
        [InfoBox("该物体是场景中的预制体或者嵌套预制体或者非预制体时，无法选中")]
        [DisableIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        public string prefabInstanceAndNonPrefabInstance = "Prefab Instances, as well as non-prefab instances";
    }
}