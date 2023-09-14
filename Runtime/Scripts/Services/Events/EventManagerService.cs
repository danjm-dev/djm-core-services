using DJM.CoreUtilities.Services.Logger;

namespace DJM.CoreUtilities.Services.Events
{
    /// <summary>
    /// Event manager class responsible for managing generic struct events.
    /// Each event is identified by a struct type, which is also used as the event parameter for subscribed listeners.
    /// </summary>
    public sealed class EventManagerService : EventManagerBase
    {
        public EventManagerService(ILoggerService loggerService) : base(loggerService) { }
    }
}