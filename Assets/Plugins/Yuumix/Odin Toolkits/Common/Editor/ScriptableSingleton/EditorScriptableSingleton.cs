using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Common.RootLocator;
using Yuumix.YuumixEditor;

namespace Yuumix.OdinToolkits.Common.Editor
{
    public abstract class EditorScriptableSingleton<T> : SerializedScriptableObject
        where T : EditorScriptableSingleton<T>
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

                _instance = ScriptableObjectEditorUtil.GetAssetDeleteExtra<T>(
                    OdinToolkitsPaths.OdinToolkitsAnyDataRootFolder + "/Editor/SO");
                return _instance;
            }
        }
    }
}
