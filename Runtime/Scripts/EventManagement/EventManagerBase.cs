using System;
using System.Collections.Generic;
using UnityEngine;

namespace DJM.CoreUtilities.EventManagement
{
    /// <summary>
    /// Base abstract event manager implementation for classes implementing <see cref="IEventManager"/>.
    /// Each event is identified by a struct type, which is also used as the event parameter for subscribed listeners.
    /// </summary>
    public abstract class EventManagerBase : IEventManager
    {
        protected readonly Dictionary<Type, Delegate> EventDictionary = new();

        public virtual void Subscribe<T>(Action<T> listener) where T : struct
        {
            var eventId = typeof(T);
            
            if (listener is null)
            {
                var errorMessage = EventManagerErrorMessages
                    .SubscribeNullListener(GetType().Name, eventId);
                Debug.LogError(errorMessage);
                return;
            }
            
            if (EventDictionary.ContainsKey(eventId))
            {
                EventDictionary[eventId] = Delegate.Combine(EventDictionary[eventId], listener);
                return;
            }

            EventDictionary[eventId] = listener;
        }
        
        public virtual void Unsubscribe<T>(Action<T> listener) where T : struct
        {
            var eventId = typeof(T);
            if (!EventDictionary.ContainsKey(eventId)) return;
            EventDictionary[eventId] = Delegate.Remove(EventDictionary[eventId], listener);
            
            if (EventDictionary[eventId] is null) EventDictionary.Remove(eventId);
        }
        
        public virtual void TriggerEvent<T>(T eventInstance) where T : struct
        {
            var eventId = typeof(T);
            if (!EventDictionary.ContainsKey(eventId)) return;
            
            foreach (var listener in EventDictionary[eventId].GetInvocationList())
            {
                try
                {
                    ((Action<T>)listener)?.Invoke(eventInstance);
                }
                catch (Exception exception)
                {
                    var errorMessage = EventManagerErrorMessages
                        .EventTriggerException(GetType().Name, eventId, exception);
                    Debug.LogError(errorMessage);
                }
            }
        }
        
        public void ClearAllEvents() => EventDictionary.Clear();
        
        public void ClearEvent<T>() where T : struct
        {
            var eventId = typeof(T);
            EventDictionary.Remove(eventId);
        }
    }
}