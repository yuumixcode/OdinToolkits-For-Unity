using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules
{
    public class CommonInlineObject : ScriptableObject
    {
        public string sharedField = "普通字段";

        [DisableInInlineEditors]
        public string disableInlineEditor;

        [ShowInInlineEditors]
        public string showInlineEditor;

        [HideInInlineEditors]
        public string hideInlineEditor;
    }
}
