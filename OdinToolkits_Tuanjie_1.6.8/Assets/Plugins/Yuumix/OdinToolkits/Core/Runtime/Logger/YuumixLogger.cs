using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Pool;
using Debug = UnityEngine.Debug;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// Yuumix Odin Toolkits 的日志工具，提供多种封装的 Log 方法
    /// </summary>
    public static class YuumixLogger
    {
        static readonly IObjectPool<StringBuilder> StringBuilderPool = new ObjectPool<StringBuilder>(
            () => new StringBuilder(), actionOnRelease: sb => sb.Clear(), defaultCapacity: 50, maxSize: 100);

        static string NowTimeString => DateTime.Now.ToString("HH:mm:ss");
        static YuumixLoggerConfig Config => OdinToolkitsRuntimeConfigSO.Instance.yuumixLoggerConfig;

        public static void CompleteLog(string message, LogType logType = LogType.Log, Type logTagType = null,
            object sender = null, bool showTimeStamp = true, string prefix = "", Color prefixColor = default,
            bool useCallerSuffix = false, string suffix = "", Color suffixColor = default,
            bool writeToFile = false, [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            LogInternal(message, logType, logTagType, sender, showTimeStamp, prefix, prefixColor,
                useCallerSuffix, suffix, suffixColor, writeToFile, filePath, lineNumber, memberName);
        }

        public static void Log(string message, Type logTagType = null, string prefix = "",
            object sender = null, bool showTimeStamp = true, bool writeToFile = false,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "")
        {
            LogInternal(message, LogType.Log, logTagType, sender, showTimeStamp, prefix, Color.green, true,
                null, default, writeToFile, filePath, lineNumber, memberName);
        }

        public static void LogWarning(string message, Type logTagType = null, string prefix = "",
            object sender = null, bool showTimeStamp = true, bool writeToFile = false,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "")
        {
            LogInternal(message, LogType.Warning, logTagType, sender, showTimeStamp, prefix, Color.yellow,
                true, null, default, writeToFile, filePath, lineNumber, memberName);
        }

        public static void LogError(string message, Type logTagType = null, string prefix = "",
            object sender = null, bool showTimeStamp = true, bool writeToFile = false,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "")
        {
            LogInternal(message, LogType.Error, logTagType, sender, showTimeStamp, prefix, Color.red, true,
                null, default, writeToFile, filePath, lineNumber, memberName);
        }

        [Conditional("UNITY_EDITOR")]
        public static void EditorLog(string message, Type logTagType = null, string prefix = "",
            object sender = null, bool showTimeStamp = true, bool writeToFile = true,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "")
        {
            LogInternal(message, LogType.Log, logTagType, sender, showTimeStamp, prefix, Color.green, true,
                null, default, writeToFile, filePath, lineNumber, memberName);
        }

        [Conditional("UNITY_EDITOR")]
        public static void EditorLogWarning(string message, Type logTagType = null, string prefix = "",
            object sender = null, bool showTimeStamp = true, bool writeToFile = true,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "")
        {
            LogInternal(message, LogType.Warning, logTagType, sender, showTimeStamp, prefix, Color.yellow,
                true, null, default, writeToFile, filePath, lineNumber, memberName);
        }

        [Conditional("UNITY_EDITOR")]
        public static void EditorLogError(string message, Type logTagType = null, string prefix = "",
            object sender = null, bool showTimeStamp = true, bool writeToFile = true,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "")
        {
            LogInternal(message, LogType.Error, logTagType, sender, showTimeStamp, prefix, Color.red, true,
                null, default, writeToFile, filePath, lineNumber, memberName);
        }

        [Conditional("UNITY_EDITOR")]
        public static void OdinToolkitsLog(string message, [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            const string prefix = "Odin Toolkits Info";
            LogInternal(message, LogType.Log, null, null, true, prefix, Color.green, true, null, default,
                true, filePath, lineNumber, memberName);
        }

        [Conditional("UNITY_EDITOR")]
        public static void OdinToolkitsWarning(string message, [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            const string prefix = "Odin Toolkits Warning";
            LogInternal(message, LogType.Warning, null, null, true, prefix, Color.yellow, true, null, default,
                true, filePath, lineNumber, memberName);
        }

        [Conditional("UNITY_EDITOR")]
        public static void OdinToolkitsError(string message, [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            const string prefix = "Odin Toolkits Error";
            LogInternal(message, LogType.Error, null, null, true, prefix, Color.red, true, null, default,
                true, filePath, lineNumber, memberName);
        }

        static void LogInternal(string message, LogType logType = LogType.Log, Type logTagType = null,
            object sender = null, bool showTimeStamp = true, string prefix = "", Color prefixColor = default,
            bool useCallerSuffix = false, string suffix = "", Color suffixColor = default,
            bool writeToFile = false, string filePath = "", int lineNumber = 0, string memberName = "")
        {
            if (logTagType != null && !Config.CanLog(logTagType))
            {
                return;
            }

            var sb = StringBuilderPool.Get();
            sb = CreateMessage(sb, message, sender, showTimeStamp, prefix, prefixColor, useCallerSuffix,
                suffix, suffixColor);
            if (useCallerSuffix)
            {
                sb.AppendLine()
                    .Append(" [")
                    .Append(Path.GetFileName(filePath))
                    .Append(" - line: ")
                    .Append(lineNumber)
                    .Append(" - ")
                    .Append(memberName)
                    .Append("]");
            }
#if UNITY_EDITOR
            var relativePath =
                PathUtilities.MakeRelative(Application.dataPath.Replace("/Assets", ""), filePath);
            var jumpTag =
                $"<a style='text-decoration: underline; href=\"{relativePath}\" line=\"{lineNumber}\">{relativePath}:{lineNumber}</a>";
            sb.Append("\n")
                .Append("堆栈面板中点击跳转: [ at <color=#ff6565>")
                .Append(jumpTag)
                .Append("</color> ]");
#endif
            var logMessage = sb.ToString();
            switch (logType)
            {
                case LogType.Log:
                case LogType.Assert:
                    Debug.Log(logMessage);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(logMessage);
                    break;
                case LogType.Error:
                case LogType.Exception:
                    Debug.LogError(logMessage);
                    break;
                default:
                    Debug.Log(logMessage);
                    break;
            }

            if (writeToFile)
            {
                WriteToFileExtension.Execute(logMessage, logType);
            }

            StringBuilderPool.Release(sb);
        }

        static StringBuilder CreateMessage(StringBuilder sb, string message, object sender = null,
            bool showTimeStamp = true, string prefix = "", Color prefixColor = default,
            bool useCallerSuffix = true, string suffix = "", Color suffixColor = default)
        {
            if (showTimeStamp)
            {
                sb.Append("[")
                    .Append(NowTimeString)
                    .Append("] ");
            }

            if (sender != null)
            {
                sb.Append("[")
                    .Append(sender)
                    .Append("] ");
            }

            if (!prefix.IsNullOrWhiteSpace())
            {
                if (prefixColor != default)
                {
                    var prefixColorString = prefixColor.ToHexString();
                    sb.Append("<color=#")
                        .Append(prefixColorString)
                        .Append(">")
                        .Append("[")
                        .Append(prefix)
                        .Append("]</color> ");
                }
            }

            message = message.Trim(' ');
            sb.Append(message);
            if (useCallerSuffix || suffix.IsNullOrWhiteSpace() || suffixColor == default)
            {
                return sb;
            }

            var suffixColorString = suffixColor.ToHexString();
            sb.Append("<color=#")
                .Append(suffixColorString)
                .Append(">")
                .Append("[")
                .Append(suffix)
                .Append("]</color> ");

            return sb;
        }
    }
}
