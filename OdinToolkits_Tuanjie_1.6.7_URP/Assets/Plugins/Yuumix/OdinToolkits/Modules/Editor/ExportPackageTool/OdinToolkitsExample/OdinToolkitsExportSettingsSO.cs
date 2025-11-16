using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.Editor;
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

        [PropertyOrder(-5)]
        [Button("同步 Odin Toolkits 编辑器信息的版本")]
        public void SyncVersion()
        {
            version = OdinToolkitsEditorInfo.VERSION_NUMBER;
        }

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

        public class OdinToolkitsExportSettingsSOAttributeProcessor : OdinAttributeProcessor
        {
            public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
                List<Attribute> attributes)
            {
                if (member.Name == nameof(OdinToolkitsExportSettingsSO.version))
                {
                    attributes.Add(new ReadOnlyAttribute());
                }
            }
        }
    }
}
