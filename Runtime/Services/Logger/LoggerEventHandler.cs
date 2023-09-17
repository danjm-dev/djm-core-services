using System;
using DJM.EventManager;

namespace DJM.CoreServices.Services.Logger
{
    public sealed class LoggerEventHandler : IInitializable, IDisposable
    {
        private readonly IEventManager _eventManager;
        private readonly ILogger _logger;
        
        public LoggerEventHandler(IEventManager eventManager, ILogger logger)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            _eventManager = eventManager;
            _logger = logger;
#endif
        }

        public void Initialize()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            _eventManager.Subscribe<LoggerEvent.Error>(OnLogError);
            _eventManager.Subscribe<LoggerEvent.Warning>(OnLogWarning);
            _eventManager.Subscribe<LoggerEvent.Info>(OnLogInfo);
#endif
        }
        
        public void Dispose()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            _eventManager.Unsubscribe<LoggerEvent.Error>(OnLogError);
            _eventManager.Unsubscribe<LoggerEvent.Warning>(OnLogWarning);
            _eventManager.Unsubscribe<LoggerEvent.Info>(OnLogInfo);
#endif
        }
        
        private void OnLogError(LoggerEvent.Error eventData)
        {
            _logger.LogError(eventData.Message, eventData.Context);
        }

        private void OnLogWarning(LoggerEvent.Warning eventData)
        {
            _logger.LogWarning(eventData.Message, eventData.Context);
        }

        private void OnLogInfo(LoggerEvent.Info eventData)
        {
            _logger.LogInfo(eventData.Message, eventData.Context);
        }
    }
}