using UnityEngine;
using Yuumix.OdinToolkits.Modules.Utilities.YuumiEditor;

namespace Yuumix.OdinToolkits.Common.Editor
{
    public enum Language
    {
        Cn,
        En,
    }

    public sealed class LanguageSwitchSO : ScriptableObject
    {
        public static LanguageSwitchSO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = ProjectEditorUtility.SO.GetScriptableObjectDeleteExtra<LanguageSwitchSO>();
                }

                return _instance;
            }
        }

        private static LanguageSwitchSO _instance;
        public Language CurrentLanguage;
    }
}
