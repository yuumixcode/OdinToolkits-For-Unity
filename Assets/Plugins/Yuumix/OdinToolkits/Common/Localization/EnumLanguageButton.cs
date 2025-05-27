using Sirenix.OdinInspector;
using System;

namespace Yuumix.OdinToolkits.Common.Localization
{
    [Serializable]
    [InlineProperty]
    [HideLabel]
    public class EnumLanguageButton
    {
        [EnableGUI]
        [ShowInInspector]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public InspectorLanguageManagerSO LanguageManager
        {
            get
            {
#if UNITY_EDITOR
                return InspectorLanguageManagerSO.Instance;
#else
                return null;
#endif
            }
        }
    }
}
