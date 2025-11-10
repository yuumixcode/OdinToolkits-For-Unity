using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Modules.Editor;

namespace Yuumix.OdinToolkits.Module.Editor
{
    public class OdinToolkitsExportSettingsSO : ExportSettingsSO
    {
        #region Serialized Fields

        [AssetList(CustomFilterMethod = nameof(FilterOdinResetSO))]
        [BilingualTitle("在打包导出前需要重置的资源文件", "Before Export, Need To Reset The Resource Files")]
        public List<ScriptableObject> wantToResetSOList;

        #endregion

        public override void BeforeExportReset()
        {
            foreach (var so in wantToResetSOList)
            {
                switch (so)
                {
                    case IOdinToolkitsEditorReset editorReset:
                        Debug.Log(so.name + "执行 Editor Reset");
                        editorReset.EditorReset();
                        break;
                    case IOdinToolkitsRuntimeReset runtimeReset:
                        Debug.Log(so.name + "执行 Runtime Reset");
                        runtimeReset.RuntimeReset();
                        break;
                }
            }
        }

        static ScriptableObject FilterOdinResetSO(ScriptableObject asset)
        {
            if (asset is OdinToolkitsExportSettingsSO)
            {
                return null;
            }

            if (asset is ExportPackageToolVisualPanelSO)
            {
                return null;
            }

            return asset.GetType()
                .GetInterfaces()
                .Any(x => x == typeof(IOdinToolkitsEditorReset) || x == typeof(IOdinToolkitsRuntimeReset))
                ? asset
                : null;
        }
    }
}
