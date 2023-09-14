using DJM.CoreUtilities.Bootstrap;
using DJM.CoreUtilities.Services.Events;
using DJM.CoreUtilities.Services.Logger;

namespace DJM.CoreUtilities.API
{
    public static class DJMLogger
    {
        private static readonly IEventManager EventManager = DJMServiceContext.Instance.EventManager;
        
        public static void LogError(string message, string context)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            EventManager.TriggerEvent(new LoggerEvent.Error(message, context));
#endif
        }
        
        public static void LogWarning(string message, string context)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            EventManager.TriggerEvent(new LoggerEvent.Warning(message, context));
#endif
        }
        
        public static void LogInfo(string message, string context)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            EventManager.TriggerEvent(new LoggerEvent.Info(message, context));
#endif
        }
    }
}