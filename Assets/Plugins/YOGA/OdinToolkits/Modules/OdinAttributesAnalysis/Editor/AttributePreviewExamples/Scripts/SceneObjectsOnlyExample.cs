using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class SceneObjectsOnlyExample : ExampleScriptableObject
    {
        [Title("Scene Objects only")] [SceneObjectsOnly]
        public List<GameObject> onlySceneObjects;

        [SceneObjectsOnly] public GameObject someSceneObject;

        [SceneObjectsOnly] public MeshRenderer someMeshRenderer;
    }
}