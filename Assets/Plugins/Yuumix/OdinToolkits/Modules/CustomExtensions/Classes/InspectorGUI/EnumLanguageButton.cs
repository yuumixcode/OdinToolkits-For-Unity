using Sirenix.OdinInspector;
using System;
using Yuumix.OdinToolkits.Common.EditorLocalization;

namespace Yuumix.OdinToolkits.Modules.CustomExtensions.Classes.InspectorGUI
{
    [Serializable]
    [InlineProperty]
    [HideLabel]
    public class EnumLanguageButton
    {
        [EnableGUI]
        [ShowInInspector]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public EditorLocalizationManagerSO LanguageLocalizationManager
        {
            get
            {
#if UNITY_EDITOR
                return EditorLocalizationManagerSO.Instance;
#else
                return null;
#endif
            }
        }
    }
}
