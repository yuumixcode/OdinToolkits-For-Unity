using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core
{
    [Serializable]
    public class YuumixLoggerConfig : IOdinToolkitsRuntimeReset
    {
        #region Serialized Fields

        [PropertyOrder(10)]
        [BilingualText("允许输出的 LogTag 设置", "LogTag List which are allowed Log")]
        [AssetList]
        [CustomContextMenu("Reset", nameof(ResetCanLogTag))]
        public List<LogTagSO> canLogTag = new List<LogTagSO>();

        #endregion

        string _editorStageLogSavePath;

        public static string DefaultEditorLogSavePath =>
            Application.dataPath.Replace("/Assets", "") + "/Logs/Yuumix_OdinToolkitsData";

        [PropertyOrder(20)]
        [FolderPath]
        [BilingualText("编辑器阶段日志保存路径", "EditorLogSavePath")]
        [CustomContextMenu("Reset", nameof(ResetEditorStageLogSavePath))]
        [LabelWidth(170)]
        [ShowInInspector]
        public string EditorLogSavePath
        {
            get
            {
                if (string.IsNullOrEmpty(_editorStageLogSavePath))
                {
                    _editorStageLogSavePath = DefaultEditorLogSavePath;
                }

                return _editorStageLogSavePath;
            }
            set => _editorStageLogSavePath = value;
        }

        [PropertyOrder(20)]
        [ShowInInspector]
        [ReadOnly]
        [LabelWidth(170)]
        [BilingualText("运行时阶段日志保存路径", "RuntimeLogSavePath")]
        public string RuntimeLogSavePath => Application.persistentDataPath + "/Yuumix_OdinToolkitsData/Logs";

        IEnumerable<Type> CanLogCategoryTypes => canLogTag.Select(e => e.GetType());

        #region IOdinToolkitsRuntimeReset Members

        public void RuntimeReset()
        {
            ResetCanLogTag();
            ResetEditorStageLogSavePath();
        }

        #endregion

        [PropertyOrder(0)]
        [OnInspectorGUI]
        [BilingualTitle("Yuumix Logger 设置", "Yuumix Logger Setting", beforeSpace: false)]
        public void Title1() { }

        public bool CanLog(Type type) => CanLogCategoryTypes.Contains(type);

        public void ResetCanLogTag()
        {
            canLogTag = new List<LogTagSO>
            {
                Resources.Load<DefaultLogTagSO>("LogTags/DefaultLogTag")
            };
        }

        public void ResetEditorStageLogSavePath()
        {
            _editorStageLogSavePath = DefaultEditorLogSavePath;
        }
    }
}
