using Sirenix.OdinInspector;
using System;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.Logger;

namespace Yuumix.OdinToolkits.Samples.TestYuumixLogger
{
    public class SampleYuumixLogger : MonoBehaviour
    {
        const string SampleMessage = "This is a sample log message.";

        [MultiLanguageInfoBox("执行一万次写入测试之后，重新编译，查看文件夹，检查是否会触发文件自动删除",
            "After performing 10,000 write tests, recompile and check the folder to see if the automatic file deletion is triggered.")]
        [PropertyOrder(-5)]
        [Button("打开编辑器阶段日志文件夹", ButtonSizes.Large)]
        public void OpenLogDataFolder()
        {
            EditorUtility.RevealInFinder(OdinToolkitsRuntimeConfig.Instance.editorStageLogSavePath);
        }

        [PropertyOrder(0)]
        [MultiLanguageTitle("测试按钮", "Test Button")]
        [OnInspectorGUI]
        public void Title1() { }

        [Button("执行一万次写入测试-点击后将出现卡顿", ButtonSizes.Large)]
        public void TestManyLog()
        {
            // 测试分段文件生成，执行 100000 次写入测试
            for (var i = 0; i < 10000; i++)
            {
                YuumixLogger.EditorLog(SampleMessage);
            }
        }

        [Button("Complete Log", ButtonSizes.Large)]
        public void CallCustomLog()
        {
            YuumixLogger.CompleteLog(SampleMessage, LogType.Log, null, null, true, "CustomPrefix", Color.blue,
                true, "CustomSuffix", Color.cyan, writeToFile: true);
        }

        [Button("Yuumix Tag", ButtonSizes.Large)]
        public void YuumixPrefixLog()
        {
            YuumixLogger.CompleteLog(SampleMessage, LogType.Log, typeof(YuumixEditorLogTagSO), this,
                useCallerSuffix: true);
        }

        [Button("Log", ButtonSizes.Large)]
        public void CallLog()
        {
            YuumixLogger.Log(SampleMessage, null, "LogPrefix", null, true, true);
        }

        [Button("Log Warning", ButtonSizes.Large)]
        public void CallLogWarning()
        {
            YuumixLogger.LogWarning(SampleMessage, null, "WarningPrefix", null, true, true);
        }

        [Button("Log Error", ButtonSizes.Large)]
        public void CallLogError()
        {
            YuumixLogger.LogError(SampleMessage, null, "ErrorPrefix", null, true, true);
        }

        [Button("Editor Log", ButtonSizes.Large)]
        public void CallEditorLog()
        {
            YuumixLogger.EditorLog(SampleMessage, null, "EditorLogPrefix");
        }

        [Button("Editor Log Warning", ButtonSizes.Large)]
        public void CallEditorLogWarning()
        {
            YuumixLogger.EditorLogWarning(SampleMessage, null, "EditorWarningPrefix");
        }

        [Button("Editor Log Error", ButtonSizes.Large)]
        public void CallEditorLogError()
        {
            YuumixLogger.EditorLogError(SampleMessage, null, "EditorErrorPrefix");
        }
    }
}
