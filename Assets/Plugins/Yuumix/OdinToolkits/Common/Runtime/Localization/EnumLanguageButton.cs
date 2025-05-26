using Sirenix.OdinInspector;
using System;
using UnityEngine.Serialization;

namespace Yuumix.OdinToolkits.Common.Runtime.Localization
{
    [Serializable]
    [InlineProperty]
    [HideLabel]
    public class EnumLanguageButton
    {
        [EnableGUI]
        [ShowInInspector]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public InspectorLanguageManagerSO LanguageManager => InspectorLanguageManagerSO.Instance;
    }
}
