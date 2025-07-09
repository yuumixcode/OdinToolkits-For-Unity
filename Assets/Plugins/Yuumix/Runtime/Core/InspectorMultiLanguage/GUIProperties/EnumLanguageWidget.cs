using Sirenix.OdinInspector;
using System;

namespace Yuumix.OdinToolkits.Core
{
    [Serializable]
    [InlineProperty]
    [HideLabel]
    public class EnumLanguageWidget
    {
        [EnableGUI]
        [ShowInInspector]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public InspectorMultiLanguageManagerSO LanguageMultiLanguageManager => InspectorMultiLanguageManagerSO.Instance;
    }
}
