using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Runtime.Scripts
{
    public class CommonInlineObject : ScriptableObject
    {
        #region Serialized Fields

        public string sharedField = "普通字段";

        [DisableInInlineEditors]
        public string disableInlineEditor;

        [ShowInInlineEditors]
        public string showInlineEditor;

        [HideInInlineEditors]
        public string hideInlineEditor;

        #endregion
    }
}
