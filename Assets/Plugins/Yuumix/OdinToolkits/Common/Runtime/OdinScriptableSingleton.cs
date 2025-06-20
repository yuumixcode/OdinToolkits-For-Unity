using Sirenix.OdinInspector;

#if UNITY_EDITOR
using Yuumix.OdinToolkits.Common.YuumixEditor;
#else 
using UnityEngine;
#endif

namespace Yuumix.OdinToolkits.Common.Runtime
{
    public abstract class OdinScriptableSingleton<T> :
#if UNITY_EDITOR
        SerializedScriptableObject
#else
        ScriptableObject
#endif
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
#if UNITY_EDITOR
                _instance = ScriptableObjectEditorUtil.GetAssetDeleteExtra<T>();
#endif
                return _instance;
            }
        }
    }
}
