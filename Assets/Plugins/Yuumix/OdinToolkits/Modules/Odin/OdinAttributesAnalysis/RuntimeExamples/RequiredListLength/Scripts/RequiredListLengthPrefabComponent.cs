using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.RuntimeExamples.RequiredListLength.Scripts
{
    public class RequiredListLengthPrefabComponent : MonoBehaviour
    {
        [Title(nameof(PrefabKind.InstanceInScene))]
        [RequiredListLength(3, PrefabKind = PrefabKind.InstanceInScene)]
        [InfoBox("该物体是场景中的预制体时，开启长度限制")]
        public List<int> list = new List<int>();

        [Title(nameof(PrefabKind.InstanceInPrefab))]
        [RequiredListLength(3, PrefabKind = PrefabKind.InstanceInPrefab)]
        [InfoBox("该物体是场景中的嵌套预制体时，开启长度限制")]
        public List<int> list2 = new List<int>();

        [Title(nameof(PrefabKind.Regular))]
        [RequiredListLength(3, PrefabKind = PrefabKind.Regular)]
        [InfoBox("该物体是常规预制体资产，不是场景中的物体时，开启长度限制")]
        public List<int> list3 = new List<int>();

        [Title(nameof(PrefabKind.Variant))]
        [RequiredListLength(3, PrefabKind = PrefabKind.Variant)]
        [InfoBox("该物体是预制体变体资产，不是场景中的物体时，开启长度限制")]
        public List<int> list4 = new List<int>();

        [Title(nameof(PrefabKind.PrefabInstance))]
        [RequiredListLength(3, PrefabKind = PrefabKind.PrefabInstance)]
        [InfoBox("该物体是场景中的预制体或者嵌套预制体时，开启长度限制")]
        public List<int> list5 = new List<int>();

        [Title(nameof(PrefabKind.PrefabAsset))]
        [RequiredListLength(3, PrefabKind = PrefabKind.PrefabAsset)]
        [InfoBox("该物体是预制体资产，不是场景中的物体时，开启长度限制")]
        public List<int> list6 = new List<int>();

        [Title(nameof(PrefabKind.PrefabInstanceAndNonPrefabInstance))]
        [RequiredListLength(3, PrefabKind = PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [InfoBox("该物体是场景中的预制体或者嵌套预制体或者非预制体时，开启长度限制")]
        public List<int> list7 = new List<int>();

        [Title(nameof(PrefabKind.All))]
        [RequiredListLength(3, PrefabKind = PrefabKind.All)]
        [InfoBox("只要和预制体有关，就开启长度限制")]
        public List<int> list8 = new List<int>();
    }
}
