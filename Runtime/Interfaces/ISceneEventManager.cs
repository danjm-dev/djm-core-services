using System;

namespace DJM.CoreServices
{
    /// <summary>
    /// Centralized event service for a scene. All listeners cleared when new scene loaded.
    /// Each event is identified by a struct type, which is also used as the event parameter for subscribed listeners.
    /// </summary>
    public interface ISceneEventManager
    {
        /// <summary>
        /// Subscribes a listener to an event, identified and parameterized by the struct type T.
        /// </summary>
        /// <param name="listener">The action to be invoked when the event is triggered.</param>
        /// <typeparam name="T">The struct type that both identifies the event and serves as the parameter for the listener.</typeparam>
        public void Subscribe<T>(Action<T> listener) where T : struct;
        
        /// <summary>
        /// Unsubscribes a listener from an event, identified and parameterized by the struct type T.
        /// </summary>
        /// <param name="listener">The action to be removed from the event's invocation list.</param>
        /// <typeparam name="T">The struct type that both identifies the event and serves as the parameter for the listener.</typeparam>
        public void Unsubscribe<T>(Action<T> listener) where T : struct;
        
        /// <summary>
        /// Triggers an event, identified and parameterized by the struct type T.
        /// </summary>
        /// <param name="eventInstance">An instance of type T that will be passed as the parameter to all subscribed listeners.</param>
        /// <typeparam name="T">The struct type that both identifies the event and serves as the parameter for the listener.</typeparam>
        public void TriggerEvent<T>(T eventInstance) where T : struct;
    }
}