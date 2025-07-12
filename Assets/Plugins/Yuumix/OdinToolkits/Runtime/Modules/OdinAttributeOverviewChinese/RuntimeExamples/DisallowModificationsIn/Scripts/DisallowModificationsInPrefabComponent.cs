using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.OdinAttributeOverviewChinese
{
    public class DisallowModificationsInPrefabComponent : MonoBehaviour
    {
        [Title("PrefabKind.InstanceInScene")]
        [InfoBox("该物体是场景中的预制体时，无法修改")]
        [DisallowModificationsIn(PrefabKind.InstanceInScene)]
        public string instanceInScene = "Instances of prefabs in scenes";

        [Title("PrefabKind.InstanceInPrefab")]
        [InfoBox("该物体是场景中的嵌套预制体时，无法修改")]
        [DisallowModificationsIn(PrefabKind.InstanceInPrefab)]
        public string instanceInPrefab = "Instances of prefabs nested inside other prefabs";

        [Title("PrefabKind.Regular")]
        [InfoBox("该物体是常规预制体资产，不是场景中的物体时，无法修改")]
        [DisallowModificationsIn(PrefabKind.Regular)]
        public string regular = "Regular prefab assets";

        [Title("PrefabKind.Regular")]
        [InfoBox("该物体是预制体变体资产，不是场景中的物体时，无法修改")]
        [DisallowModificationsIn(PrefabKind.Variant)]
        public string variant = "Prefab variant assets";

        [Title("PrefabKind.PrefabInstance")]
        [InfoBox("该物体是场景中的预制体或者嵌套预制体时，无法修改")]
        [DisallowModificationsIn(PrefabKind.PrefabInstance)]
        public string prefabInstance =
            "Instances of regular prefabs, and prefab variants in scenes or nested in other prefabs";

        [Title("PrefabKind.PrefabAsset")]
        [InfoBox("该物体是预制体资产，不是场景中的物体时，无法修改")]
        [DisallowModificationsIn(PrefabKind.PrefabAsset)]
        public string prefabAsset = "Prefab assets and prefab variant assets";

        [Title("PrefabKind.PrefabAsset")]
        [InfoBox("该物体是场景中的预制体或者嵌套预制体或者非预制体时，无法修改")]
        [DisallowModificationsIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        public string prefabInstanceAndNonPrefabInstance = "Prefab Instances, as well as non-prefab instances";

        [PropertySpace(20)]
        [Button("修改 instanceInScene 值", ButtonSizes.Large)]
        void Change1()
        {
            instanceInScene = "尝试代码修改值";
            Debug.Log("修改 instanceInScene 值");
        }

        [Button("修改 instanceInPrefab 值", ButtonSizes.Large)]
        void Change2()
        {
            instanceInPrefab = "尝试代码修改值";
            Debug.Log("修改 instanceInPrefab 值");
        }

        [Button("修改 regular 值", ButtonSizes.Large)]
        void Change3()
        {
            regular = "尝试代码修改值";
            Debug.Log("修改 regular 值");
        }

        [Button("修改 variant 值", ButtonSizes.Large)]
        void Change4()
        {
            variant = "尝试代码修改值";
            Debug.Log("修改 variant 值");
        }

        [Button("修改 prefabInstance 值", ButtonSizes.Large)]
        void Change5()
        {
            prefabInstance = "尝试代码修改值";
            Debug.Log("修改 prefabInstance 值");
        }

        [Button("修改 prefabAsset 值", ButtonSizes.Large)]
        void Change6()
        {
            prefabAsset = "尝试代码修改值";
            Debug.Log("修改 prefabAsset 值");
        }

        [Button("修改 prefabInstanceAndNonPrefabInstance 值", ButtonSizes.Large)]
        void Change7()
        {
            prefabInstanceAndNonPrefabInstance = "尝试代码修改值";
            Debug.Log("修改 prefabInstanceAndNonPrefabInstance 值");
        }
    }
}
