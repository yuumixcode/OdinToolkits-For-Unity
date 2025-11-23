using UnityEngine;
using YuumixEditor;

namespace Yuumix.OdinToolkits.Core.Editor
{
    /// <summary>
    /// 编辑器阶段的 ScriptableObject 单例抽象类，不使用 Odin 序列化
    /// </summary>
    public abstract class EditorScriptableSingleton<T> : ScriptableObject
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

                _instance = ScriptableObjectEditorUtility.GetAssetAndDeleteExtra<T>(
                    OdinToolkitsEditorPaths.ODIN_TOOLKITS_ANY_DATA_ROOT_FOLDER + "/Editor/SO");
                return _instance;
            }
        }
    }
}
