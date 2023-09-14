﻿using System;
using UnityEngine;
using ILogger = DJM.CoreServices.Interfaces.ILogger;

namespace DJM.CoreServices.Services.Logger
{
    public sealed class LoggerService : ILogger
    {
        private readonly bool _enableLogging;
        private readonly LogLevel _loggingThreshold;

        public LoggerService(bool enableLogging, LogLevel loggingThreshold)
        {
            _enableLogging = enableLogging;
            _loggingThreshold = loggingThreshold;
        }
        
        public void LogError(string message, string context)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if(!_enableLogging) return;
            
            Debug.LogError(CreateMessage(message, context, LogLevel.Error));
#endif
        }

        public void LogWarning(string message, string context)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if(!_enableLogging) return;
            if(_loggingThreshold > LogLevel.Warning) return;
            
            Debug.LogWarning(CreateMessage(message, context, LogLevel.Warning));
#endif
        }

        public void LogInfo(string message, string context)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if(!_enableLogging) return;
            if(_loggingThreshold > LogLevel.Info) return;
            
            Debug.Log(CreateMessage(message, context, LogLevel.Info));
#endif
        }
        
        
        private const string LogMessageTemplate = "<size=13><b><color=orange>DJMLogger</color> <color=lightblue>[{0}]</color></b></size> <color={1}>{2}</color>";
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