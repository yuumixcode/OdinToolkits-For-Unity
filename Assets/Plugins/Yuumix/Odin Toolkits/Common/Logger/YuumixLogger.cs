using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.Pool;
using Yuumix.OdinToolkits.Common.InspectorMultiLanguage;
using Yuumix.OdinToolkits.Modules.Utilities;
using Debug = UnityEngine.Debug;

namespace Yuumix.OdinToolkits.Common.Logger
{
    /// <summary>
    /// Yuumix Odin Toolkits 的日志工具，提供多种封装的 Log 方法
    /// </summary>
    /// <remarks>游戏运行过程中，如果要存储日志，是否需要做一个检测，每次打开游戏去清空日志，或者文件夹大小超过一定值时进行清理？</remarks>
    /// <seealso href="https://www.odintoolkits.cn/" />
    [MultiLanguageComment("Yuumix Odin Toolkits 的日志工具，提供多种封装的 Log 方法",
        "The logging tool of Yuumix Odin Toolkits offers a variety of encapsulated Log methods.")]
    public static class YuumixLogger
    {
        static string NowTimeString => DateTime.Now.ToString("HH:mm:ss");

        static readonly IObjectPool<StringBuilder> StringBuilderPool = new ObjectPool<StringBuilder>(
            () => new StringBuilder(),
            actionOnRelease: sb => sb.Clear(),
            defaultCapacity: 100,
            maxSize: 1000
        );

        public static void CompleteLog(string message, LogType logType = LogType.Log,
            Type logTagType = null,
            object sender = null,
            bool showTimeStamp = true,
            string prefix = "", Color prefixColor = default,
            bool useCallerSuffix = false,
            string suffix = "", Color suffixColor = default,
            bool writeToFile = false,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "")
        {
            LogInternal(message, logType, logTagType, sender, showTimeStamp, prefix, prefixColor, useCallerSuffix,
                suffix, suffixColor, writeToFile, filePath, lineNumber, memberName);
        }

        public static void Log(string message,
            Type logTagType = null,
            string prefix = "",
            object sender = null,
            bool showTimeStamp = true,
            bool writeToFile = false,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "")
        {
            LogInternal(message, LogType.Log, logTagType, sender, showTimeStamp, prefix, Color.green, true, null,
                default, writeToFile, filePath, lineNumber, memberName);
        }

        public static void LogWarning(string message,
            Type logTagType = null,
            string prefix = "",
            object sender = null,
            bool showTimeStamp = true,
            bool writeToFile = false,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "")
        {
            LogInternal(message, LogType.Warning, logTagType, sender, showTimeStamp, prefix, Color.yellow, true, null,
                default, writeToFile, filePath, lineNumber, memberName);
        }

        public static void LogError(string message,
            Type logTagType = null,
            string prefix = "",
            object sender = null,
            bool showTimeStamp = true,
            bool writeToFile = false,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "")
        {
            LogInternal(message, LogType.Error, logTagType, sender, showTimeStamp, prefix, Color.red, true, null,
                default, writeToFile, filePath, lineNumber, memberName);
        }

        [Conditional("UNITY_EDITOR")]
        public static void EditorLog(string message,
            Type logTagType = null,
            string prefix = "",
            object sender = null,
            bool showTimeStamp = true,
            bool writeToFile = true,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "")
        {
            LogInternal(message, LogType.Log, logTagType, sender, showTimeStamp, prefix, Color.green, true, null,
                default, writeToFile, filePath, lineNumber, memberName);
        }

        [Conditional("UNITY_EDITOR")]
        public static void EditorLogWarning(string message,
            Type logTagType = null,
            string prefix = "",
            object sender = null,
            bool showTimeStamp = true,
            bool writeToFile = true,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "")
        {
            LogInternal(message, LogType.Warning, logTagType, sender, showTimeStamp, prefix, Color.yellow, true, null,
                default, writeToFile, filePath, lineNumber, memberName);
        }

        [Conditional("UNITY_EDITOR")]
        public static void EditorLogError(string message,
            Type logTagType = null,
            string prefix = "",
            object sender = null,
            bool showTimeStamp = true,
            bool writeToFile = true,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "")
        {
            LogInternal(message, LogType.Error, logTagType, sender, showTimeStamp, prefix, Color.red, true, null,
                default, writeToFile, filePath, lineNumber, memberName);
        }

        static void LogInternal(string message, LogType logType = LogType.Log,
            Type logTagType = null,
            object sender = null,
            bool showTimeStamp = true,
            string prefix = "", Color prefixColor = default,
            bool useCallerSuffix = false,
            string suffix = "", Color suffixColor = default,
            bool writeToFile = false,
            string filePath = "",
            int lineNumber = 0,
            string memberName = "")
        {
            if (logTagType != null && !OdinToolkitsRuntimeConfig.Instance.CanLog(logTagType))
            {
                return;
            }

            var sb = StringBuilderPool.Get();
            sb = CreateMessage(sb, message, sender, showTimeStamp, prefix, prefixColor, useCallerSuffix,
                suffix, suffixColor
            );
            if (useCallerSuffix)
            {
                sb.Append(" [")
                    .Append(Path.GetFileName(filePath))
                    .Append(" - line: ").Append(lineNumber)
                    .Append(" - ").Append(memberName)
                    .Append("]");
            }

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

        static StringBuilder CreateMessage(StringBuilder sb, string message,
            object sender = null,
            bool showTimeStamp = true,
            string prefix = "", Color prefixColor = default,
            bool useCallerSuffix = true,
            string suffix = "", Color suffixColor = default
        )
        {
            if (showTimeStamp)
            {
                sb.Append("[").Append(NowTimeString).Append("] ");
            }

            if (sender != null)
            {
                sb.Append("[").Append(sender).Append("] ");
            }

            if (!prefix.IsNullOrWhiteSpace())
            {
                if (prefixColor != default)
                {
                    var prefixColorString = prefixColor.ToHexString();
                    sb.Append("<color=#").Append(prefixColorString).Append(">")
                        .Append("[").Append(prefix)
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
            sb.Append("<color=#").Append(suffixColorString).Append(">")
                .Append("[").Append(suffix)
                .Append("]</color> ");

            return sb;
        }
    }
}
