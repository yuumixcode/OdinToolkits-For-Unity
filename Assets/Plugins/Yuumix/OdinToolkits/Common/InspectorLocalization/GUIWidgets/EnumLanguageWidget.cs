using Sirenix.OdinInspector;
using System;

namespace Yuumix.OdinToolkits.Common.InspectorLocalization.GUIWidgets
{
    [Serializable]
    [InlineProperty]
    [HideLabel]
    public class EnumLanguageWidget
    {
        [EnableGUI]
        [ShowInInspector]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public InspectorLocalizationManagerSO LanguageLocalizationManager
        {
            get
            {
#if UNITY_EDITOR
                return InspectorLocalizationManagerSO.Instance;
#else
                return null;
#endif
            }
        }
    }
}
