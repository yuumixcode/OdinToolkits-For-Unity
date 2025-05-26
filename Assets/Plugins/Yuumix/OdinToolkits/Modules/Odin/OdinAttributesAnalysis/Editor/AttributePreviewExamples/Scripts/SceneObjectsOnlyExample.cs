using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
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
