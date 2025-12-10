using Yuumix.OdinToolkits.Core.SafeEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Core;
using YuumixEditor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    /// <summary>
    /// Attribute Overview 案例的 ScriptableObject 基类，继承 IOdinToolkitsEditorReset
    /// </summary>
    public abstract class AttributeExampleSO<T> : ScriptableObject where T : ScriptableObject, IOdinToolkitsEditorReset
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
