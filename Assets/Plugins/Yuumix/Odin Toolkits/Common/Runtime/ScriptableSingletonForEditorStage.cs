using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Common.RootLocator;

#if UNITY_EDITOR
using Yuumix.YuumixEditor;
#else
using UnityEngine;
#endif

namespace Yuumix.OdinToolkits.Common
{
    /// <summary>
    /// 提供给不在 Editor 文件夹下的编辑器阶段使用的配置文件的单例，有些类实例需要被非 Editor 文件夹的类获取。
    /// </summary>
    public abstract class ScriptableSingletonForEditorStage<T> :
#if UNITY_EDITOR
        SerializedScriptableObject
#else
        ScriptableObject
#endif
        where T : ScriptableSingletonForEditorStage<T>
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
                _instance = ScriptableObjectEditorUtil.GetAssetDeleteExtra<T>(
                    OdinToolkitsPaths.OdinToolkitsAnyDataRootFolder + "/Editor/SO");
#else
                Debug.Log("[ScriptableSingletonForEditorStage] 该单例资源不应该在运行时获取")
                _instance = null;
#endif
                return _instance;
            }
        }
    }
}
