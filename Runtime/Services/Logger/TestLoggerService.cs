using System;
using UnityEngine;
using ILogger = DJM.CoreServices.ILogger;

namespace DJM.CoreServices.Services.Logger
{
    public sealed class TestLoggerService : ILogger
    {
        public void LogError(string message, string context)
        {
            Debug.LogError(CreateMessage(message, context, LogLevel.Error));
        }

        public void LogWarning(string message, string context)
        {
            Debug.LogWarning(CreateMessage(message, context, LogLevel.Warning));
        }

        public void LogInfo(string message, string context)
        {
            Debug.Log(CreateMessage(message, context, LogLevel.Info));
        }

        public static string GetRawLogMessage(string message, string context, LogLevel level)
        {
            return CreateMessage(message, context, level);
        }
        
        private const string LogMessageTemplate = "<size=13><b><color=purple>TEST LOGGER</color> <color=blue>[{0}]</color></b></size> <color={1}>{2}</color>";
        private static string CreateMessage(string message, string context, LogLevel level)
        {
            var color = level switch
            {
                LogLevel.Info => "white",
                LogLevel.Warning => "yellow",
                LogLevel.Error => "red",
                _ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
            };

            return string.Format(LogMessageTemplate, context, color, message);
        }
    }
}