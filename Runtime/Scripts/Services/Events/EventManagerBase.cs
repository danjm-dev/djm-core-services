using System;
using System.Collections.Generic;
using DJM.CoreUtilities.Services.Logger;

namespace DJM.CoreUtilities.Services.Events
{
    /// <summary>
    /// Base abstract event manager implementation for classes implementing <see cref="IEventManagerService"/>.
    /// Each event is identified by a struct type, which is also used as the event parameter for subscribed listeners.
    /// </summary>
    public abstract class EventManagerBase : IEventManagerService
    {
        private readonly Dictionary<Type, Delegate> _eventDictionary = new();
        protected readonly ILoggerService LoggerService;
        
        protected EventManagerBase(ILoggerService loggerService) => LoggerService = loggerService;

        public virtual void Subscribe<T>(Action<T> listener) where T : struct
        {
            var eventId = typeof(T);
            
            if (listener is null)
            {
                var errorMessage = $"Subscription attempt to {eventId} event had null listener.";
                LoggerService.LogError(errorMessage, nameof(EventManagerBase));
                return;
            }
            
            if (_eventDictionary.ContainsKey(eventId))
            {
                _eventDictionary[eventId] = Delegate.Combine(_eventDictionary[eventId], listener);
                return;
            }

            _eventDictionary[eventId] = listener;
        }
        
        public virtual void Unsubscribe<T>(Action<T> listener) where T : struct
        {
            var eventId = typeof(T);
            if (!_eventDictionary.ContainsKey(eventId)) return;
            _eventDictionary[eventId] = Delegate.Remove(_eventDictionary[eventId], listener);
            
            if (_eventDictionary[eventId] is null) _eventDictionary.Remove(eventId);
        }
        
        public virtual void TriggerEvent<T>(T eventInstance) where T : struct
        {
            var eventId = typeof(T);
            if (!_eventDictionary.ContainsKey(eventId)) return;
            
            foreach (var listener in _eventDictionary[eventId].GetInvocationList())
            {
                try
                {
                    ((Action<T>)listener)?.Invoke(eventInstance);
                }
                catch (Exception exception)
                {
                    var errorMessage = $"Exception caught when triggering {eventId} event listener: {exception}";
                    LoggerService.LogError(errorMessage, nameof(EventManagerBase));
                }
            }
        }
        
        public void ClearAllEvents() => _eventDictionary.Clear();
        
        public void ClearEvent<T>() where T : struct
        {
            var eventId = typeof(T);
            _eventDictionary.Remove(eventId);
        }
    }
}