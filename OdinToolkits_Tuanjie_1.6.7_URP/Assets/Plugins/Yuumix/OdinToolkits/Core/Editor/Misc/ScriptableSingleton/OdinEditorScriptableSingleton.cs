using Yuumix.OdinToolkits.Core.SafeEditor;
using Sirenix.OdinInspector;
using YuumixEditor;

namespace Yuumix.OdinToolkits.Core.Editor
{
    /// <summary>
    /// 使用了 Odin 序列化的，编辑器阶段的 ScriptableObject 单例抽象类
    /// </summary>
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

                _instance = ScriptableObjectSafeEditorUtility.GetSingletonAssetAndDeleteOther<T>(
                    OdinToolkitsEditorPaths.ODIN_TOOLKITS_ANY_DATA_ROOT_FOLDER + "/Editor/SO");
                return _instance;
            }
        }
    }
}
