using DJM.CoreServices.Bootstrap;
using DJM.CoreServices.Services.Logger;

namespace DJM.CoreServices.API
{
    public static class DJMLogger
    {
        private static readonly IEventManager EventManager = DJMServiceContext.Instance.DependencyContainer.Resolve<IEventManager>();
        
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