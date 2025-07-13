using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Editor
{
    [OdinToolkitsAttributeExample]
    public class MinMaxValueExample : ExampleSO
    {
        [Title("Int")]
        [MinValue(0)]
        public int intMinValue0;

        [MaxValue(10)]
        public int intMaxValue0;

        [Title("Float")]
        [MinValue(0)]
        public float floatMinValue0;

        [MaxValue(10)]
        public float floatMaxValue0;

        [Title("Vectors")]
        [MinValue(0)]
        public Vector3 vector3MinValue0;

        [MaxValue(10)]
        public Vector3 vector3MaxValue0;
    }
}
