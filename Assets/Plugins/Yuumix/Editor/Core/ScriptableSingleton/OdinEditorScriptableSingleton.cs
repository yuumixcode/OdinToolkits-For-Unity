using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Core.RootLocator;
using YuumixEditor;

namespace Yuumix.OdinToolkits.Editor.Core
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

                _instance = ScriptableObjectEditorUtil.GetAssetDeleteExtra<T>(
                    OdinToolkitsPaths.OdinToolkitsAnyDataRootFolder + "/Editor/SO");
                return _instance;
            }
        }
    }
}
