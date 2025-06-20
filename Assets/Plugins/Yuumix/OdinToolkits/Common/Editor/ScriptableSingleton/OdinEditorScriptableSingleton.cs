using Sirenix.OdinInspector;
#if UNITY_EDITOR
using Yuumix.OdinToolkits.Common.YuumixEditor;
#endif

namespace Yuumix.OdinToolkits.Common.Editor
{
    public abstract class OdinEditorScriptableSingleton<T> : SerializedScriptableObject
        where T : OdinEditorScriptableSingleton<T>
    {
        static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance)
                {
                    return _instance;
                }
#if UNITY_EDITOR
                _instance = ScriptableObjectEditorUtil.GetAssetDeleteExtra<T>();
#endif
                return _instance;
            }
        }
    }
}
