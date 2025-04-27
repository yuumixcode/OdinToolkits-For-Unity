using System.Collections.Generic;
using System.IO;

namespace Rubickanov.Logger
{
    public static class FileLogWriter
    {
        private static Queue<string> logQueue = new Queue<string>();
        private static bool isWriting = false;

        public static void FileLog(string message, string path)
        {
            logQueue.Enqueue(message);
            WriteLogsToFile(path);
        }

        private static async void WriteLogsToFile(string path)
        {
            if (isWriting) return;

            isWriting = true;

            while (logQueue.Count > 0)
            {
                string message = logQueue.Dequeue();

                string directoryPath = Path.GetDirectoryName(path);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using (StreamWriter logFile = new StreamWriter(path, true))
                {
                    await logFile.WriteLineAsync(message);
                }
            }

            isWriting = false;
        }
    }
}