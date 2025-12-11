using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Core.SafeEditor;

namespace Yuumix.OdinToolkits.Core
{
    public abstract class OdinScriptableSingleton<T> : SerializedScriptableObject
        where T : OdinScriptableSingleton<T>
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

                _instance = ScriptableObjectSafeEditorUtility.GetSingletonAssetAndDeleteOther<T>(
                    OdinToolkitsEditorPaths.ALL_DATA_ROOT_FOLDER + "/SO");
                return _instance;
            }
        }
    }
}
