using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEditor;
using UnityEngine;
using Yuumix.Universal;

namespace Yuumix.OdinToolkits.Core
{
    public static class WriteToFileExtension
    {
        // 自动刷新间隔（毫秒）
        const int FLUSH_INTERVAL_MS = 2000;

        // 最大缓冲区大小（字节）
        const int MAX_BUFFER_SIZE = 8192;

        // 最大队列长度
        const int MAX_QUEUE_SIZE = 10000;

        // 最大文件大小（MB）
        const int MAX_FILE_SIZE_MB = 10;

        // 日志文件夹最大总大小（100 MB）
        const long MAX_TOTAL_FOLDER_SIZE = 100 * 1024 * 1024;

        static readonly ConcurrentQueue<string> LogQueue = new ConcurrentQueue<string>();
        static DateTime _lastFlushTime = DateTime.Now;
        static readonly StringBuilder LogBuffer = new StringBuilder(4096);
        static string _lastLogFilePath;
        static readonly Thread LogWriterThread;
        static readonly CancellationTokenSource CancellationToken;

        static string EditorStageLogSaveFolderPath =>
            OdinToolkitsPreferencesSO.Instance.yuumixLoggerSetting.EditorLogSavePath;

        static string RuntimeStageLogSaveFolderPath =>
            OdinToolkitsPreferencesSO.Instance.yuumixLoggerSetting.RuntimeLogSavePath;

        static WriteToFileExtension()
        {
            if (!Directory.Exists(EditorStageLogSaveFolderPath))
            {
                Directory.CreateDirectory(EditorStageLogSaveFolderPath);
            }

            // 传入一个方法，构造一个线程行为
            LogWriterThread = new Thread(WriteToFileThreadCommand)
            {
                IsBackground = true,
                Name = "YuumixLoggerWriter"
            };
            // 设计一个停止 Token
            CancellationToken = new CancellationTokenSource();
            LogWriterThread.Start();

            Application.quitting -= ShutdownThread;
            Application.quitting += ShutdownThread;
            AppDomain.CurrentDomain.ProcessExit -= EncapsulateShutdownThread;
            AppDomain.CurrentDomain.ProcessExit += EncapsulateShutdownThread;
#if UNITY_EDITOR
            AssemblyReloadEvents.beforeAssemblyReload -= ShutdownThread;
            AssemblyReloadEvents.beforeAssemblyReload += ShutdownThread;
#endif
        }

        static string CurrentLogFilePath
        {
            get
            {
                var latestTimeSegment = DateTime.Now.ToString("yyyy-MM-dd-HH");
                if (!_lastLogFilePath.IsNullOrWhiteSpace() && _lastLogFilePath.Contains(latestTimeSegment))
                {
                    return _lastLogFilePath;
                }

                UpdateFilePath();
                return _lastLogFilePath;

                void UpdateFilePath()
                {
#if UNITY_EDITOR
                    if (!Directory.Exists(EditorStageLogSaveFolderPath))
                    {
                        Directory.CreateDirectory(EditorStageLogSaveFolderPath);
                    }

                    _lastLogFilePath = Path.Combine(EditorStageLogSaveFolderPath,
                        $"YuumixLogger_{DateTime.Now:yyyy-MM-dd-HH}.log");
#else
                    if (!Directory.Exists(RuntimeStageLogSaveFolderPath))
                    {
                        Directory.CreateDirectory(RuntimeStageLogSaveFolderPath);
                    }

                    _lastLogFilePath = Path.Combine(RuntimeStageLogSaveFolderPath,
                        $"YuumixLogger_{DateTime.Now:yyyy-MM-dd-HH}.log");
#endif
                }
            }
        }

        [BilingualComment("执行写入文件的方法，传入参数和日志级别",
            "Execute the method of writing to the file, passing in parameters, and log level")]
        public static void Execute(string message, LogType logType)
        {
            if (LogQueue.Count >= MAX_QUEUE_SIZE)
            {
                YuumixLogger.LogError("日志队列数量大于等于 " + MAX_QUEUE_SIZE + "，日志输出数量似乎异常，检查是否存在每帧输出");
                return;
            }

            string logEntry = BuildLogEntry(message, logType);
            LogQueue.Enqueue(logEntry);
            return;

            // 局部方法
            static string BuildLogEntry(string message, LogType logType)
            {
                var sb = new StringBuilder();
                sb.Append($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] ");
                switch (logType)
                {
                    case LogType.Log:
                        sb.Append("[INFO] >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                        break;
                    case LogType.Warning:
                        sb.Append("[WARNING] >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                        break;
                    case LogType.Error:
                        sb.Append("[ERROR] >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                        break;
                    case LogType.Assert:
                        sb.Append("[ASSERT] >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                        break;
                    case LogType.Exception:
                        sb.Append(
                            "[EXCEPTION] >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                        break;
                }

                sb.AppendLine();
                sb.AppendLine(message);
                var stackTrace = new StackTrace(true);
                sb.AppendLine().AppendLine("========================== Stack Trace Start ==========================");
                sb.Append(stackTrace);
                sb.AppendLine();
                sb.AppendLine("========================== End ==========================");
                sb.AppendLine();
                return sb.ToString();
            }
        }

        /// <summary>
        /// 写入到文件的线程命令
        /// </summary>
        static void WriteToFileThreadCommand()
        {
            // 只要没有停止，则一直尝试写入
            while (!CancellationToken.Token.IsCancellationRequested)
            {
                // 如果日志队列中存在信息，则加载到缓冲区，准备写入
                while (LogQueue.TryDequeue(out string logEntry))
                {
                    LogBuffer.Append(logEntry);

                    // 检查缓冲区大小是否超过限制
                    if (LogBuffer.Length >= MAX_BUFFER_SIZE)
                    {
                        FlushBuffer();
                    }
                }

                if ((DateTime.Now - _lastFlushTime).TotalMilliseconds >= FLUSH_INTERVAL_MS)
                {
                    FlushBuffer();
                }

                // 短暂休眠避免CPU占用过高
                Thread.Sleep(10);
            }

            // 线程结束后，继续刷新一次缓存区，防御性编程
            FlushBuffer();
        }

        // 刷新缓冲区到文件，把缓冲区清空一次算写入一次
        static void FlushBuffer()
        {
            if (LogBuffer.Length == 0)
            {
                return;
            }

            try
            {
                CheckFileSizeAndRename();
                // StreamWriter 可以直接创建新文件
                using (var writer = new StreamWriter(CurrentLogFilePath, true, Encoding.UTF8))
                {
                    writer.Write(LogBuffer.ToString());
                }

                LogBuffer.Clear();
                _lastFlushTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                YuumixLogger.LogError($"Failed to flush log buffer: {ex}", prefix: "YuumixLogger WriteToFile Error");
                LogBuffer.Clear();
            }

            return;

            static string GetArchiveFilePath()
            {
                string basePath = CurrentLogFilePath.Replace(".log", "");
                var index = 1;

                string archiveFilePath;
                do
                {
                    archiveFilePath = $"{basePath}_Archive_{index}.log";
                    index++;
                }
                while (File.Exists(archiveFilePath));

                return archiveFilePath;
            }

            void CheckFileSizeAndRename()
            {
                if (File.Exists(CurrentLogFilePath))
                {
                    var fileInfo = new FileInfo(CurrentLogFilePath);
                    if (fileInfo.Length >= MAX_FILE_SIZE_MB * 1024 * 1024)
                    {
                        string archiveFilePath = GetArchiveFilePath();
                        // 重命名，那么原来路径的文件就没有了
                        if (File.Exists(CurrentLogFilePath))
                        {
                            File.Move(CurrentLogFilePath, archiveFilePath);
                        }
                    }
                }
            }
        }

        static void EncapsulateShutdownThread(object o, EventArgs eventArgs)
        {
            ShutdownThread();
        }

        static void ShutdownThread()
        {
            try
            {
                // 在退出时检查总大小
                CheckTotalFolderSize();
                CancellationToken?.Cancel();
                // 等待最多2秒
                LogWriterThread?.Join(2000);
                // 强制刷新缓存区一次，写入剩余的内容
                FlushBuffer();
            }
            catch (Exception ex)
            {
                YuumixLogger.LogError($"Failed to shutdown YuumixLogger WriteToFileExtension Thread: {ex}",
                    prefix: "YuumixLogger WriteToFile Error");
            }

            return;

            static void CheckTotalFolderSize()
            {
                string logFolderPath;
#if UNITY_EDITOR
                logFolderPath = EditorStageLogSaveFolderPath;
#else
                logFolderPath = RuntimeStageLogSaveFolderPath;
#endif
                if (!Directory.Exists(logFolderPath))
                {
                    return;
                }

                List<FileInfo> allFiles = Directory.GetFiles(logFolderPath)
                    .Select(f => new FileInfo(f))
                    .OrderBy(f => f.LastWriteTime)
                    .ToList();
                long totalSize = allFiles.Sum(file => file.Length);
                if (totalSize < MAX_TOTAL_FOLDER_SIZE)
                {
                    return;
                }

                float deleteSizeThreshold = totalSize - MAX_TOTAL_FOLDER_SIZE * 0.9f;
                // 大于或者等于 MaxTotalFolderSize，则删除文件，直到 MaxTotalFolderSize * 0.9f 的文件
                long deletedSize = 0;
                foreach (FileInfo file in allFiles)
                {
                    file.Delete();
                    deletedSize += file.Length;
                    if (deletedSize >= deleteSizeThreshold)
                    {
                        break;
                    }
                }
            }
        }
    }
}
