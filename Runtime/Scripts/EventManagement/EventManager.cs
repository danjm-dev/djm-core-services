namespace DJM.CoreUtilities.EventManagement
{
    /// <summary>
    /// Event manager class responsible for managing generic struct events.
    /// Extends default event manager behavior, allowing for events to be cleared. 
    /// Each event is identified by a struct type, which is also used as the event parameter for subscribed listeners.
    /// </summary>
    public sealed class EventManager : EventManagerBase
    {
        /// <summary>
        /// Removes all listeners from all events.
        /// </summary>
        public void ClearAllEvents() => EventDictionary.Clear();
        
        /// <summary>
        /// Removes all listeners from specified event.
        /// </summary>
        /// <typeparam name="T">The type of event to remove listeners from. Must be a struct.</typeparam>
        public void ClearEvent<T>() where T : struct
        {
            var eventId = typeof(T);
            EventDictionary.Remove(eventId);
        }
    }
}