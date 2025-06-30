using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Utilities;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Yuumix.OdinToolkits.Common.Logger
{
    public static class WriteToFileExtension
    {
        // 自动刷新间隔（毫秒）
        const int FlushIntervalMs = 2000;

        // 最大缓冲区大小（字节）
        const int MaxBufferSize = 8192;

        // 最大队列长度
        const int MaxQueueSize = 10000;

        // 最大文件大小（MB）
        const int MaxFileSizeMb = 10;

        // 日志文件夹最大总大小（100 MB）
        const long MaxTotalFolderSize = 100 * 1024 * 1024;

        static readonly ConcurrentQueue<string> LogQueue = new ConcurrentQueue<string>();
        static DateTime _lastFlushTime = DateTime.Now;
        static readonly StringBuilder LogBuffer = new StringBuilder(4096);
        static string _lastLogFilePath;
        static readonly Thread LogWriterThread;
        static readonly CancellationTokenSource CancellationToken;
        static string EditorStageLogSaveFolderPath => OdinToolkitsRuntimeConfig.Instance.editorStageLogSavePath;
        static string RuntimeStageLogSaveFolderPath => OdinToolkitsRuntimeConfig.Instance.RuntimeStageLogSavePath;

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

        public static void Execute(string message, LogType logType)
        {
            if (LogQueue.Count >= MaxQueueSize)
            {
                YuumixLogger.LogError("日志队列数量大于等于 " + MaxQueueSize + "，日志输出数量似乎异常，检查是否存在每帧输出");
                return;
            }

            var logEntry = BuildLogEntry(message, logType);
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
                while (LogQueue.TryDequeue(out var logEntry))
                {
                    LogBuffer.Append(logEntry);

                    // 检查缓冲区大小是否超过限制
                    if (LogBuffer.Length >= MaxBufferSize)
                    {
                        FlushBuffer();
                    }
                }

                if ((DateTime.Now - _lastFlushTime).TotalMilliseconds >= FlushIntervalMs)
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
                var basePath = CurrentLogFilePath.Replace(".log", "");
                var index = 1;

                string archiveFilePath;
                do
                {
                    archiveFilePath = $"{basePath}_Archive_{index}.log";
                    index++;
                } while (File.Exists(archiveFilePath));

                return archiveFilePath;
            }

            void CheckFileSizeAndRename()
            {
                if (File.Exists(CurrentLogFilePath))
                {
                    var fileInfo = new FileInfo(CurrentLogFilePath);
                    if (fileInfo.Length >= MaxFileSizeMb * 1024 * 1024)
                    {
                        var archiveFilePath = GetArchiveFilePath();
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

                var allFiles = Directory.GetFiles(logFolderPath)
                    .Select(f => new FileInfo(f))
                    .OrderBy(f => f.LastWriteTime)
                    .ToList();
                var totalSize = allFiles.Sum(file => file.Length);
                if (totalSize < MaxTotalFolderSize)
                {
                    return;
                }

                var deleteSizeThreshold = totalSize - MaxTotalFolderSize * 0.9f;
                // 大于或者等于 MaxTotalFolderSize，则删除文件，直到 MaxTotalFolderSize * 0.9f 的文件
                long deletedSize = 0;
                foreach (var file in allFiles)
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
