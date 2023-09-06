namespace DJM.CoreUtilities.EventManagement
{
    /// <summary>
    /// Event manager class responsible for managing generic struct events.
    /// Extends default event manager behavior, allowing for itself to be reset, and events removed. 
    /// Each event is identified by a struct type, which is also used as the event parameter for subscribed listeners.
    /// </summary>
    public sealed class EventManager : EventManagerBase
    {
        /// <summary>
        /// Resets event manager, removing all events.
        /// </summary>
        public void Reset() => EventDictionary.Clear();
        
        /// <summary>
        /// Clears listeners from specified event.
        /// </summary>
        /// <typeparam name="T">The type of event to remove. Must be a struct.</typeparam>
        public void ClearEvent<T>() where T : struct
        {
            var eventId = typeof(T);
            EventDictionary.Remove(eventId);
        }
    }
}