using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class SceneObjectsOnlyExample : ExampleScriptableObject
    {
        [Title("Scene Objects only")]
        [SceneObjectsOnly]
        public List<GameObject> onlySceneObjects;

        [SceneObjectsOnly]
        public GameObject someSceneObject;

        [SceneObjectsOnly]
        public MeshRenderer someMeshRenderer;
    }
}