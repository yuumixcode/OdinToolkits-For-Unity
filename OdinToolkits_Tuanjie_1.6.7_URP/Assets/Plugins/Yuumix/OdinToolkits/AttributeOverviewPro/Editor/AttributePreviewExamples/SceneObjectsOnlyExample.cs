using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class SceneObjectsOnlyExample : ExampleSO
    {
        #region Serialized Fields

        [Title("Scene Objects only")]
        [SceneObjectsOnly]
        public List<GameObject> onlySceneObjects;

        [SceneObjectsOnly]
        public GameObject someSceneObject;

        [SceneObjectsOnly]
        public MeshRenderer someMeshRenderer;

        #endregion
    }
}
