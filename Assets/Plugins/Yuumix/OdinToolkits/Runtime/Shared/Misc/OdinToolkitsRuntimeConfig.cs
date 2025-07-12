using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using Yuumix.OdinToolkits.Core;
using UnityEngine;
using Yuumix.OdinToolkits.Shared;

namespace Yuumix.OdinToolkits.Shared
{
    public class OdinToolkitsRuntimeConfig : ScriptableObject, IOdinToolkitsReset
    {
        static OdinToolkitsRuntimeConfig _cachedInstance;

        public static OdinToolkitsRuntimeConfig Instance
        {
            get
            {
                if (_cachedInstance)
                {
                    return _cachedInstance;
                }

                _cachedInstance = Resources.Load<OdinToolkitsRuntimeConfig>("OdinToolkitsRuntimeConfig");
                if (!_cachedInstance)
                {
                    YuumixLogger.EditorLogError(string.Format("加载 {0} 资源失败，检查加载路径！",
                        typeof(OdinToolkitsRuntimeConfig)));
                }

                return _cachedInstance;
            }
        }

        public MultiLanguageHeaderWidget header = new MultiLanguageHeaderWidget(
            "Odin Toolkits 运行时配置",
            "Odin Toolkits Runtime Config",
            "可以在运行时和编辑器两个阶段读取的配置文件",
            "Configuration files that can be read at both runtime and in the editor.");

        void OnEnable()
        {
            editorStageLogSavePath = Application.dataPath.Replace("/Assets", "") + "/Logs/Yuumix_OdinToolkitsData";
        }

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
        public string editorStageLogSavePath;

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
