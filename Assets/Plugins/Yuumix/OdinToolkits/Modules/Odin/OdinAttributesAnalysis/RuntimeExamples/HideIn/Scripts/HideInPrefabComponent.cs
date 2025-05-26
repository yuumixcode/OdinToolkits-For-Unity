using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.RuntimeExamples.HideIn.Scripts
{
    public class HideInPrefabComponent : MonoBehaviour
    {
        [Title("PrefabKind.InstanceInScene", "该物体是场景中的预制体时，无法看到 instanceInScene 字段")]
        [MultiLineProperty]
        public string info1 = "该物体是场景中的预制体时，无法看到 instanceInScene 字段";

        [HideIn(PrefabKind.InstanceInScene)]
        public string instanceInScene = "Instances of prefabs in scenes";

        [Title("PrefabKind.InstanceInPrefab", "该物体是场景中的嵌套预制体时，无法看到 instanceInPrefab 字段")]
        [MultiLineProperty]
        public string info2 = "该物体是场景中的嵌套预制体时，无法看到 instanceInPrefab 字段";

        [HideIn(PrefabKind.InstanceInPrefab)]
        public string instanceInPrefab = "Instances of prefabs nested inside other prefabs";

        [Title("PrefabKind.Regular", "该物体是常规预制体资产，不是场景中的物体时，无法看到 regular 字段")]
        [MultiLineProperty]
        public string info3 = "该物体是常规预制体资产，不是场景中的物体时，无法看到 regular 字段";

        [HideIn(PrefabKind.Regular)]
        public string regular = "Regular prefab assets";

        [Title("PrefabKind.Variant", "该物体是预制体变体资产，不是场景中的物体时，无法看到 variant 字段")]
        [MultiLineProperty]
        public string info4 = "该物体是预制体变体资产，不是场景中的物体时，无法看到 variant 字段";

        [HideIn(PrefabKind.Variant)]
        public string variant = "Prefab variant assets";

        [Title("PrefabKind.PrefabInstance")]
        [MultiLineProperty]
        public string info5 = "该物体是场景中的预制体或者嵌套预制体时，无法看到 prefabInstance 字段";

        [HideIn(PrefabKind.PrefabInstance)]
        public string prefabInstance =
            "Instances of regular prefabs, and prefab variants in scenes or nested in other prefabs";

        [Title("PrefabKind.PrefabAsset")]
        [MultiLineProperty]
        public string info6 = "该物体是预制体资产，不是场景中的物体时，无法看到 prefabAsset 字段";

        [HideIn(PrefabKind.PrefabAsset)]
        public string prefabAsset = "Prefab assets and prefab variant assets";

        [Title("PrefabKind.PrefabAsset")]
        [MultiLineProperty]
        public string info7 = "该物体是场景中的预制体或者嵌套预制体或者非预制体时，无法看到 prefabInstanceAndNonPrefabInstance 字段";

        [HideIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        public string prefabInstanceAndNonPrefabInstance = "Prefab Instances, as well as non-prefab instances";
    }
}
