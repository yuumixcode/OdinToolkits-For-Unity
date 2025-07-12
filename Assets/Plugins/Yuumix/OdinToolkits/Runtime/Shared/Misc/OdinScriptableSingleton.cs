using Sirenix.OdinInspector;

#if UNITY_EDITOR
using YuumixEditor;
#endif

namespace Yuumix.OdinToolkits.Shared
{
    public abstract class OdinScriptableSingleton<T> : SerializedScriptableObject where T : OdinScriptableSingleton<T>
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
                _instance = ScriptableObjectEditorUtil.GetAssetAndDeleteExtra<T>(
                    OdinToolkitsPaths.ODIN_TOOLKITS_ANY_DATA_ROOT_FOLDER + "/SO");
#endif
                return _instance;
            }
        }
    }
}
