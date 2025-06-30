using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Rubickanov.Logger
{
    public static class LogMessageGenerator
    {
        public static string GenerateFileLog(LogLevel logLevel, object message, string categoryName, Object sender)
        {
            return $"{DateTime.Now} [{logLevel}] [{categoryName}] [{sender.name}]: {message}";
        }
        
        public static string GenerateFileLog(LogLevel logLevel, object message, string categoryName, string sender)
        {
            return $"{DateTime.Now} [{logLevel}] [{categoryName}] [{sender}]: {message}";
        }
        
        public static string GenerateLogMessage(LogLevel logLevel, object message, string categoryName, Color categoryColor, Object sender)
        {
            string logTypeColor = RubiConstants.GetLogLevelColor(logLevel);
            string hexColor = "#" + ColorUtility.ToHtmlStringRGB(categoryColor);
            return
                $"<color={logTypeColor}>[{logLevel}]</color> <color={hexColor}>[{categoryName}] </color>[{sender.name}]: {message}";
        }
        
        public static string GenerateLogMessage(LogLevel logLevel, object message, string categoryName, Color categoryColor, string sender)
        {
            string logTypeColor = RubiConstants.GetLogLevelColor(logLevel);
            string hexColor = "#" + ColorUtility.ToHtmlStringRGB(categoryColor);
            return
                $"<color={logTypeColor}>[{logLevel}]</color> <color={hexColor}>[{categoryName}] </color>[{sender}]: {message}";
        }
    }
}