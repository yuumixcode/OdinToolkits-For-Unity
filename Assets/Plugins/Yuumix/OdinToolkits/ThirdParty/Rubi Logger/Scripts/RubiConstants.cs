using System.Collections.Generic;

namespace Rubickanov.Logger
{
    public static class RubiConstants
    {
        public static readonly string DEFAULT_PATH = "Logs/rubi_log.txt";
        
        public static Dictionary<LogLevel, string> LOG_LEVEL_COLORS = new Dictionary<LogLevel, string>
        {
            { LogLevel.Debug, "#FFFFFF" },
            { LogLevel.Info, "#00B4D8" },
            { LogLevel.Warn, "#FFFF00" },
            { LogLevel.Error, "#FF0000" }
        };
        
        public static string GetLogLevelColor(LogLevel logLevel)
        {
            return LOG_LEVEL_COLORS[logLevel];
        }
    }

    
    // Be sure to add new log levels to the dictionary in RubiConstants class
    public enum LogLevel
    {
        Debug,
        Info,
        Warn,
        Error
    }

    public enum LogOutput
    {
        Console,
        Screen,
        File,
        ConsoleAndScreen,
        ConsoleAndFile,
        ScreenAndFile,
        All
    }
}