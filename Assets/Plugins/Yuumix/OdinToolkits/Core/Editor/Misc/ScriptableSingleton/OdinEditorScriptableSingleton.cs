using Sirenix.OdinInspector;
using YuumixEditor;

namespace Yuumix.OdinToolkits.Core.Editor
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

                _instance = ScriptableObjectEditorUtility.GetAssetAndDeleteExtra<T>(
                    OdinToolkitsPaths.ODIN_TOOLKITS_ANY_DATA_ROOT_FOLDER + "/Editor/SO");
                return _instance;
            }
        }
    }
}
