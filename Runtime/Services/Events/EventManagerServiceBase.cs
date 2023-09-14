using System;
using System.Collections.Generic;
using DJM.CoreServices.Interfaces;

namespace DJM.CoreServices.Services.Events
{
    /// <summary>
    /// Base abstract event manager implementation for classes implementing <see cref="IEventManager"/>.
    /// Each event is identified by a struct type, which is also used as the event parameter for subscribed listeners.
    /// </summary>
    public abstract class EventManagerServiceBase : IEventManager
    {
        private readonly Dictionary<Type, Delegate> _eventDictionary = new();
        protected readonly ILogger Logger;
        
        protected EventManagerServiceBase(ILogger logger) => Logger = logger;

        public virtual void Subscribe<T>(Action<T> listener) where T : struct
        {
            var eventId = typeof(T);
            
            if (listener is null)
            {
                var errorMessage = $"Subscription attempt to {eventId} event had null listener.";
                Logger.LogError(errorMessage, nameof(EventManagerServiceBase));
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
                    Logger.LogError(errorMessage, nameof(EventManagerServiceBase));
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