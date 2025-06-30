using Sirenix.OdinInspector;
using Yuumix.YuumixEditor;

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

                _instance = ScriptableObjectEditorUtil.GetAssetDeleteExtra<T>();
                return _instance;
            }
        }
    }
}
