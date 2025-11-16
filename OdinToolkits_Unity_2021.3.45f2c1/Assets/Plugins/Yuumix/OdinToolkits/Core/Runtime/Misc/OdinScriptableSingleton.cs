using Sirenix.OdinInspector;
#if UNITY_EDITOR
using YuumixEditor;
#endif

namespace Yuumix.OdinToolkits.Core
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
                _instance = ScriptableObjectEditorUtility.GetAssetAndDeleteExtra<T>(
                    OdinToolkitsEditorPaths.ODIN_TOOLKITS_ANY_DATA_ROOT_FOLDER + "/SO");
#endif
                return _instance;
            }
        }
    }
}
