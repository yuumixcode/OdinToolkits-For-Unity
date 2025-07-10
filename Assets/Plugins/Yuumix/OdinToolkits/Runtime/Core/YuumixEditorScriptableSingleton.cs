using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using YuumixEditor;

#else
using UnityEngine;
#endif

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 提供给不在 Editor 文件夹下的编辑器阶段使用的配置文件的单例，有些类实例需要被非 Editor 文件夹的类获取。
    /// </summary>
    public abstract class YuumixEditorScriptableSingleton<T> :
#if UNITY_EDITOR
        SerializedScriptableObject
#else
        ScriptableObject
#endif
        where T : YuumixEditorScriptableSingleton<T>
    {
        static T _instance;

        public static T Instance
        {
            get
            {
#if UNITY_EDITOR
                if (_instance)
                {
                    return _instance;
                }

                _instance = ScriptableObjectEditorUtil.GetAssetAndDeleteExtra<T>(
                    OdinToolkitsPaths.ODIN_TOOLKITS_ANY_DATA_ROOT_FOLDER + "/Editor/SO");
#else
                Debug.LogWarning("[YuumixEditorScriptableSingleton] 该单例资源不应该在运行时获取");
                return null;
#endif
                return _instance;
            }
        }
    }
}
