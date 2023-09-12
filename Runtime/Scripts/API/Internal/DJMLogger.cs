using System;
using UnityEngine;

namespace DJM.CoreUtilities
{
    internal static class DJMLogger
    {
        internal static bool EnableLogging = true;
        internal static LogLevel LoggingThreshold = LogLevel.Info;
        
        internal static void Log(string message, string context, LogLevel level = LogLevel.Info)
        {
            
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            
            if (!EnableLogging) return;
            if (level < LoggingThreshold) return;
            
            var timestamp = DateTime.Now.ToString("HH:mm:ss");
            var formattedMessage = $"<color=blue>DJM Logger</color> <color=green>[{timestamp}]</color> <color=purple>[{context}]</color> {message}";

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
        
        internal enum LogLevel
        {
            Info,
            Warning,
            Error
        }
    }
}