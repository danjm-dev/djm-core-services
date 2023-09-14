using System;
using DJM.CoreUtilities.Services.Events;

namespace DJM.CoreUtilities.Services.Logger
{
    internal sealed class LogEventHandler : IDisposable
    {
        private readonly IEventManagerService _eventManagerService;
        private readonly ILoggerService _loggerService;
        
        internal LogEventHandler(IEventManagerService eventManagerService, ILoggerService loggerService)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            _eventManagerService = eventManagerService;
            _loggerService = loggerService;
            Initialize();
#endif
        }

        private void Initialize()
        {
            _eventManagerService.Subscribe<LogEvent.Error>(OnLogError);
            _eventManagerService.Subscribe<LogEvent.Warning>(OnLogWarning);
            _eventManagerService.Subscribe<LogEvent.Info>(OnLogInfo);
        }
        
        public void Dispose()
        {
            _eventManagerService.Unsubscribe<LogEvent.Error>(OnLogError);
            _eventManagerService.Unsubscribe<LogEvent.Warning>(OnLogWarning);
            _eventManagerService.Unsubscribe<LogEvent.Info>(OnLogInfo);
        }
        
        private void OnLogError(LogEvent.Error eventData)
        {
            _loggerService.LogError(eventData.Message, eventData.Context);
        }

        private void OnLogWarning(LogEvent.Warning eventData)
        {
            _loggerService.LogWarning(eventData.Message, eventData.Context);
        }

        private void OnLogInfo(LogEvent.Info eventData)
        {
            _loggerService.LogInfo(eventData.Message, eventData.Context);
        }
    }
}