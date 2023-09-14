using System;

namespace DJM.CoreUtilities.Services.Logger
{
    internal sealed class LoggerEventHandler : IDisposable
    {
        private readonly IEventManager _eventManager;
        private readonly ILogger _logger;
        
        internal LoggerEventHandler(IEventManager eventManager, ILogger logger)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            _eventManager = eventManager;
            _logger = logger;
            Initialize();
#endif
        }

        private void Initialize()
        {
            _eventManager.Subscribe<LoggerEvent.Error>(OnLogError);
            _eventManager.Subscribe<LoggerEvent.Warning>(OnLogWarning);
            _eventManager.Subscribe<LoggerEvent.Info>(OnLogInfo);
        }
        
        public void Dispose()
        {
            _eventManager.Unsubscribe<LoggerEvent.Error>(OnLogError);
            _eventManager.Unsubscribe<LoggerEvent.Warning>(OnLogWarning);
            _eventManager.Unsubscribe<LoggerEvent.Info>(OnLogInfo);
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