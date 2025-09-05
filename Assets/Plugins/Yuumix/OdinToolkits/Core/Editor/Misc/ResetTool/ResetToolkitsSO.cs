using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core.Runtime.Editor
{
    public class ResetToolkitsSO : OdinEditorScriptableSingleton<ResetToolkitsSO>
    {
        [AssetList(CustomFilterMethod = nameof(FilterSO))]
        [BilingualTitle("选择将要进行重置的资源文件")]
        public List<ScriptableObject> wantToResetSOList;

        [Button("重置选中的 SO 文件", ButtonSizes.Large)]
        public void Reset()
        {
            if (!EditorUtility.DisplayDialog("重置资源",
                    "准备重置选中的 ScriptableObject 资源，无法撤回，确定重置吗？",
                    "确认",
                    "取消"))
            {
                return;
            }

            foreach (ScriptableObject item in wantToResetSOList)
            {
                if (item is IOdinToolkitsReset reset)
                {
                    reset.OdinToolkitsReset();
                }
            }
        }

        ScriptableObject FilterSO(ScriptableObject asset)
        {
            return asset.GetType()
                .GetInterfaces()
                .Any(x => x == typeof(IOdinToolkitsReset))
                ? asset
                : null;
        }
    }
}
