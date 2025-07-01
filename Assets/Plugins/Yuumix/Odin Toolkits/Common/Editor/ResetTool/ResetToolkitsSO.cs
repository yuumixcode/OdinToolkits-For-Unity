using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Yuumix.OdinToolkits.Common.Editor
{
    public class ResetToolkitsSO : EditorScriptableSingleton<ResetToolkitsSO>
    {
        [PropertyOrder(-5)]
        [Button("批量重置选中的 SO 文件", ButtonSizes.Large)]
        public void Reset()
        {
            foreach (var item in wantToResetSOList)
            {
                if (item is IOdinToolkitsReset reset)
                {
                    reset.OdinToolkitsReset();
                }
            }
        }

        [AssetList(CustomFilterMethod = nameof(FilterSO))]
        public List<ScriptableObject> wantToResetSOList;

        ScriptableObject FilterSO(ScriptableObject asset)
        {
            return asset.GetType().GetInterfaces().Any(x => x == typeof(IOdinToolkitsReset)) ? asset : null;
        }
    }
}
