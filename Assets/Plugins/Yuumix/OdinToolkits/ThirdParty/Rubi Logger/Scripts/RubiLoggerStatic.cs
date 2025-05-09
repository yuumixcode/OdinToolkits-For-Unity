using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Rubickanov.Logger
{
    public static class RubiLoggerStatic
    {
        private static readonly Color defaultCategoryColor = new Color(0.7882f, 0.7882f, 0.7882f);
        public static RubiLogger.LogAddedHandler LogAdded;
        
        public static void Log(LogLevel logLevel, object message, string senderName, LogOutput logOutput = LogOutput.Console,  string categoryName = null, Color? categoryColor = null)
        {
            if (categoryColor == null)
            {
                categoryColor = defaultCategoryColor;
            }

            if (categoryName == null)
            {
                categoryName = "NonMono";
            }
            
            string generatedMessage =
                LogMessageGenerator.GenerateLogMessage(logLevel, message, categoryName, (Color)categoryColor, senderName);

            switch (logOutput)
            {
                case LogOutput.Console:
                    ConsoleLogMessage(logLevel, generatedMessage, senderName);
                    break;

                case LogOutput.Screen:
                    ScreenLog(generatedMessage);
                    break;

                case LogOutput.File:
                    WriteToFile(logLevel, message, categoryName, senderName, RubiConstants.DEFAULT_PATH);
                    break;

                case LogOutput.ConsoleAndScreen:
                    ConsoleLogMessage(logLevel, generatedMessage, senderName);
                    ScreenLog(generatedMessage);
                    break;

                case LogOutput.ConsoleAndFile:
                    ConsoleLogMessage(logLevel, generatedMessage, senderName);
                    WriteToFile(logLevel, message, categoryName, senderName, RubiConstants.DEFAULT_PATH);
                    break;

                case LogOutput.ScreenAndFile:
                    ScreenLog(generatedMessage);
                    WriteToFile(logLevel, message, categoryName, senderName, RubiConstants.DEFAULT_PATH);
                    break;

                case LogOutput.All:
                    ConsoleLogMessage(logLevel, generatedMessage, senderName);
                    ScreenLog(generatedMessage);
                    WriteToFile(logLevel, message, categoryName, senderName, RubiConstants.DEFAULT_PATH);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(logOutput), logOutput, null);
            }
        }

        private static void WriteToFile(LogLevel logLevel, object message, string categoryName, string sender, string path)
        {
            string fileLog = LogMessageGenerator.GenerateFileLog(logLevel, message, categoryName, sender);
            FileLogWriter.FileLog(fileLog, path);
        }

        private static void ScreenLog(string generatedMessage)
        {
            LogAdded?.Invoke(generatedMessage);
        }

        private static void ConsoleLogMessage(LogLevel logLevel, string generatedMessage, string sender)
        {
            switch (logLevel)
            {
                case LogLevel.Info:
                    Debug.Log(generatedMessage);
                    break;
                case LogLevel.Warn:
                    Debug.LogWarning(generatedMessage);
                    break;
                case LogLevel.Error:
                    Debug.LogError(generatedMessage);
                    break;
                default:
                    Debug.Log(generatedMessage);
                    break;
            }
        }
    }
    
}

