using System;
using UnityEngine;

namespace DJM.CoreUtilities
{
    public static class DJMLogger
    {
        public static bool EnableLogging = true;
        public static LogLevel LoggingThreshold = LogLevel.Info;
        
        public static void Log(string message, string context, LogLevel level = LogLevel.Info)
        {
            
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            
            if (!EnableLogging) return;
            if (level < LoggingThreshold) return;
            
            var formattedMessage = $"<size=13><b><color=orange>DJMLogger</color> <color=lightblue>[{context}]</color></b></size> <color={level.LogColor()}>{message}</color>";

            switch (level)
            {
                case LogLevel.Info:
                    Debug.Log(formattedMessage);
                    break;
                case LogLevel.Warning:
                    Debug.LogWarning(formattedMessage);
                    break;
                case LogLevel.Error:
                    Debug.LogError(formattedMessage);
                    break;
            }
#endif
        }

        private static string LogColor(this LogLevel level)
        {
            return level switch
            {
                LogLevel.Info => "white",
                LogLevel.Warning => "yellow",
                LogLevel.Error => "red",
                _ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
            };
        }
        
        public enum LogLevel
        {
            Info,
            Warning,
            Error
        }
    }
}