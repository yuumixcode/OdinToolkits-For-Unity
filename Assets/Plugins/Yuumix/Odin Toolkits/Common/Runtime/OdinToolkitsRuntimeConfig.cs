using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorMultiLanguage;
using Yuumix.OdinToolkits.Common.Logger;
using Yuumix.OdinToolkits.Common.ResetTool;
using Yuumix.OdinToolkits.Common.RootLocator;
using Yuumix.YuumixEditor;

namespace Yuumix.OdinToolkits.Common
{
    public class OdinToolkitsRuntimeConfig : ScriptableObject, IOdinToolkitsReset
    {
        #region 单例

        static OdinToolkitsRuntimeConfig _cachedInstance;

        public static OdinToolkitsRuntimeConfig Instance
        {
            get
            {
                if (_cachedInstance)
                {
                    return _cachedInstance;
                }

#if UNITY_EDITOR
                _cachedInstance = Application.isPlaying
                    ? RuntimeLoad()
                    : ScriptableObjectEditorUtil.GetAssetDeleteExtra<OdinToolkitsRuntimeConfig>(
                        OdinToolkitsPaths.GetRootPath() + "/Common/Resources/Runtime_OdinToolkitsRuntimeConfig.asset");

                if (!_cachedInstance)
                {
                    YuumixLogger.EditorLogError("加载 " + typeof(OdinToolkitsRuntimeConfig) + " 资源失败，检查加载路径！");
                }
#else
                RuntimeLoad();
#endif
                return _cachedInstance;
            }
        }

        static OdinToolkitsRuntimeConfig RuntimeLoad()
        {
            _cachedInstance = Yuumix.Instance.OdinToolkitsRuntimeConfig;
            return _cachedInstance;
        }

        #endregion

        public MultiLanguageHeaderWidget header = new MultiLanguageHeaderWidget(
            "Odin Toolkits 运行时配置",
            "Odin Toolkits Runtime Config",
            "可以在运行时和编辑器两个阶段读取的配置文件",
            "Configuration files that can be read at both runtime and in the editor.");

        #region Yuumix Logger

        [PropertyOrder(0)]
        [OnInspectorGUI]
        [MultiLanguageTitle("Yuumix Logger 配置", "Yuumix Logger Config", beforeSpace: false)]
        public void Title1() { }

        [PropertyOrder(10)]
        [MultiLanguageText("允许输出的 LogTag 配置", "LogTag List which are allowed Log")]
        [AssetList]
        [CustomContextMenu("Reset", nameof(ResetCanLogTag))]
        public List<LogTagSO> canLogTag = new List<LogTagSO>();

        [PropertyOrder(20)]
        [FolderPath]
        [MultiLanguageText("编辑器阶段日志保存路径", "Editor Stage Log Save Path")]
        [CustomContextMenu("Reset", nameof(ResetEditorStageLogSavePath))]
        [LabelWidth(200)]
        public string editorStageLogSavePath =
            Application.dataPath.Replace("/Assets", "") + "/Logs/Yuumix_OdinToolkitsData";

        [PropertyOrder(20)]
        [ShowInInspector]
        [ReadOnly]
        [LabelWidth(200)]
        [MultiLanguageText("运行时阶段日志保存路径", "RuntimeStageLogSavePath")]
        public string RuntimeStageLogSavePath => Application.persistentDataPath + "/Yuumix_OdinToolkitsData/Logs";

        IEnumerable<Type> CanLogCategoryTypes => canLogTag.Select(e => e.GetType());

        public bool CanLog(Type type) => CanLogCategoryTypes.Contains(type);

        void ResetCanLogTag()
        {
            canLogTag = new List<LogTagSO>()
            {
                Resources.Load<YuumixEditorLogTagSO>("YuumixEditorLogTag")
            };
        }

        void ResetEditorStageLogSavePath()
        {
            editorStageLogSavePath = Application.dataPath.Replace("/Assets", "") + "/Logs/Yuumix_OdinToolkitsData";
        }

        #endregion

        public void OdinToolkitsReset()
        {
            ResetCanLogTag();
            ResetEditorStageLogSavePath();
        }
    }
}
