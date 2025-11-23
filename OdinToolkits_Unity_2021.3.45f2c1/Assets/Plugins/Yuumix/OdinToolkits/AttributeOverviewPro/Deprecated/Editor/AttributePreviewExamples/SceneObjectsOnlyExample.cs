using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
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
