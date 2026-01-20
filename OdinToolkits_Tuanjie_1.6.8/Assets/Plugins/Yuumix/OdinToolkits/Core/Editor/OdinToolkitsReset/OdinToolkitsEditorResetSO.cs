using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core.Editor
{
    public class OdinToolkitsEditorResetSO : OdinEditorScriptableSingleton<OdinToolkitsEditorResetSO>
    {
        #region Serialized Fields

        [AssetList(CustomFilterMethod = nameof(FilterOdinResetSO))]
        [BilingualTitle("选择将要进行重置的资源文件")]
        public List<ScriptableObject> wantToResetSOList;

        #endregion

        #region Event Functions

        [Button("重置选中的 SO 文件", ButtonSizes.Large)]
        public void Reset()
        {
            if (!EditorUtility.DisplayDialog("重置资源", "准备重置选中的 ScriptableObject 资源，无法撤回，确定重置吗？", "确认", "取消"))
            {
                return;
            }

            foreach (var item in wantToResetSOList)
            {
                switch (item)
                {
                    case IOdinToolkitsEditorReset editorReset:
                        Debug.Log(item.name + "执行 Editor Reset");
                        editorReset.EditorReset();
                        break;
                    case IOdinToolkitsRuntimeReset runtimeReset:
                        Debug.Log(item.name + "执行 Runtime Reset");
                        runtimeReset.RuntimeReset();
                        break;
                }
            }
        }

        #endregion

        public static ScriptableObject FilterOdinResetSO(ScriptableObject asset)
        {
            return asset.GetType()
                .GetInterfaces()
                .Any(x => x == typeof(IOdinToolkitsEditorReset) || x == typeof(IOdinToolkitsRuntimeReset))
                ? asset
                : null;
        }
    }
}
