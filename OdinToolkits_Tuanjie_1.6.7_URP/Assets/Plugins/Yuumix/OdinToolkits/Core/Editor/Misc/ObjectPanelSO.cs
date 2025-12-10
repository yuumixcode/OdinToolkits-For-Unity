using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Core.SafeEditor;
using YuumixEditor;

namespace Yuumix.OdinToolkits.Core.Editor
{
    [Summary("ScriptableObject 面板基类")]
    public abstract class ObjectPanelSO<T> : SerializedScriptableObject, IOdinToolkitsEditorReset
        where T : ObjectPanelSO<T>
    {
        static T _panelSO;

        public static T Instance
        {
            get
            {
                if (_panelSO)
                {
                    return _panelSO;
                }

                _panelSO = ScriptableObjectSafeEditorUtility.GetSingletonAssetAndDeleteOther<T>(
                    OdinToolkitsEditorPaths.ODIN_TOOLKITS_ANY_DATA_ROOT_FOLDER + "/Editor/PanelSO");
                return _panelSO;
            }
        }

        #region IOdinToolkitsEditorReset Members

        public abstract void EditorReset();

        #endregion
    }
}
