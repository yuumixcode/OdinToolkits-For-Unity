using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated
{
    public class RequiredInPrefabComponent : MonoBehaviour
    {
        public string MessageProperty => useAlternativeMessage ? alternativeMessage : message;

        string GetMessage() => useAlternativeMessage ? alternativeMessage : message;

        #region Serialized Fields

        public bool useAlternativeMessage;
        public string message = "Peace, Love & Ducks";
        public string alternativeMessage = "Peace, Love & Yuumi Zeus";

        [Title("序号 1 PrefabKind.InstanceInScene")]
        [InfoBox("该物体是场景中的预制体时，判断是否为空，ErrorMessage 可以解析成员名称，但是在 Rider 中没有高亮显示")]
        [RequiredIn(PrefabKind.InstanceInScene, ErrorMessage = "$message")]
        public GameObject instanceInScene;

        [Title("序号 2 PrefabKind.InstanceInPrefab")]
        [InfoBox("该物体是场景中的嵌套预制体时，判断是否为空，ErrorMessage 可以解析成员名称，但是在 Rider 中没有高亮显示")]
        [RequiredIn(PrefabKind.InstanceInPrefab, ErrorMessage = "$MessageProperty")]
        public GameObject instanceInPrefab;

        [Title("序号 3 PrefabKind.Regular")]
        [InfoBox("该物体是常规预制体资产，不是场景中的物体时，判断是否为空，ErrorMessage 可以解析表达式，但是在 Rider 中没有高亮显示")]
        [RequiredIn(PrefabKind.Regular,
            ErrorMessage = "@useAlternativeMessage ? alternativeMessage : message")]
        public GameObject regular;

        [Title("序号 4 PrefabKind.Variant")]
        [InfoBox("该物体是预制体变体资产，不是场景中的物体时，判断是否为空，ErrorMessage 可以解析方法名，但是在 Rider 中没有高亮显示")]
        [RequiredIn(PrefabKind.Variant, ErrorMessage = "$GetMessage")]
        public GameObject variant;

        [Title("序号 5 PrefabKind.PrefabInstance")]
        [InfoBox("该物体是场景中的预制体或者嵌套预制体时，判断是否为空")]
        [RequiredIn(PrefabKind.PrefabInstance)]
        public GameObject prefabInstance;

        [Title("序号 6 PrefabKind.PrefabAsset")]
        [InfoBox("该物体是预制体资产，不是场景中的物体时，判断是否为空")]
        [RequiredIn(PrefabKind.PrefabAsset)]
        public GameObject prefabAsset;

        [Title("序号 7 PrefabKind.PrefabInstanceAndNonPrefabInstance")]
        [InfoBox("该物体是场景中的预制体或者嵌套预制体或者非预制体时，判断是否为空")]
        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        public GameObject prefabInstanceAndNonPrefabInstance;

        [Title("序号 8 PrefabKind.All")]
        [RequiredIn(PrefabKind.All)]
        public GameObject prefabAll;

        #endregion
    }
}
