using DJM.CoreUtilities.ServiceContext;
using DJM.CoreUtilities.Services.Events;
using DJM.CoreUtilities.Services.Logger;

namespace DJM.CoreUtilities
{
    public static class DJMLogger
    {
        private static readonly IEventManagerService EventManagerService = DJMServiceContext.Instance.EventManagerService;
        
        public static void LogError(string message, string context)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            EventManagerService.TriggerEvent(new LogEvent.Error(message, context));
#endif
        }
        
        public static void LogWarning(string message, string context)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            EventManagerService.TriggerEvent(new LogEvent.Warning(message, context));
#endif
        }
        
        public static void LogInfo(string message, string context)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            EventManagerService.TriggerEvent(new LogEvent.Info(message, context));
#endif
        }
    }
}