using Sirenix.OdinInspector;
using System;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common;
using Yuumix.OdinToolkits.Common.InspectorMultiLanguage;
using Yuumix.OdinToolkits.Common.Logger;

namespace Yuumix.OdinToolkits.Samples.TestYuumixLogger
{
    public class SampleYuumixLogger : MonoBehaviour
    {
        const string SampleMessage = "This is a sample log message.";

        [PropertyOrder(-5)]
        [Button("打开日志文件夹", ButtonSizes.Large)]
        public void OpenLogDataFolder()
        {
            EditorUtility.OpenWithDefaultApp(OdinToolkitsRuntimeConfig.Instance.RuntimeStageLogSavePath);
        }

        [PropertyOrder(0)]
        [MultiLanguageTitle("测试按钮", "Test Button")]
        [OnInspectorGUI]
        public void Title1() { }

        [Button("执行一万次写入测试", ButtonSizes.Large)]
        public void TestManyLog()
        {
            // 测试分段文件生成，执行 100000 次写入测试
            for (var i = 0; i < 10000; i++)
            {
                YuumixLogger.EditorLog(SampleMessage);
            }
        }

        [Button("Complete Log")]
        public void CallCustomLog()
        {
            YuumixLogger.CompleteLog(SampleMessage, LogType.Log, null, null, true, "CustomPrefix", Color.blue,
                true, "CustomSuffix", Color.cyan, writeToFile: true);
        }

        [Button("Yuumix Tag")]
        public void YuumixPrefixLog()
        {
            YuumixLogger.CompleteLog(SampleMessage, LogType.Log, typeof(YuumixEditorLogTagSO), this,
                useCallerSuffix: true);
        }

        [Button("Log")]
        public void CallLog()
        {
            YuumixLogger.Log(SampleMessage, null, "LogPrefix", null, true, true);
        }

        [Button("Log Warning")]
        public void CallLogWarning()
        {
            YuumixLogger.LogWarning(SampleMessage, null, "WarningPrefix", null, true, true);
        }

        [Button("Log Error")]
        public void CallLogError()
        {
            YuumixLogger.LogError(SampleMessage, null, "ErrorPrefix", null, true, true);
        }

        [Button("Editor Log")]
        public void CallEditorLog()
        {
            YuumixLogger.EditorLog(SampleMessage, null, "EditorLogPrefix");
        }

        [Button("Editor Log Warning")]
        public void CallEditorLogWarning()
        {
            YuumixLogger.EditorLogWarning(SampleMessage, null, "EditorWarningPrefix");
        }

        [Button("Editor Log Error")]
        public void CallEditorLogError()
        {
            YuumixLogger.EditorLogError(SampleMessage, null, "EditorErrorPrefix");
        }

        public void MaxTest() { }
    }
}
