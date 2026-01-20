using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    public class SuppressInvalidErrorExample : ExampleSO
    {
        #region Serialized Fields

        [Range(0, 10)]
        public string InvalidAttributeError = "此特性不匹配，会报错";

        [Range(0, 10)]
        [SuppressInvalidAttributeError]
        public string SuppressedError = "此特性不匹配，但是标记了抑制，也不会报错";

        #endregion
    }
}
