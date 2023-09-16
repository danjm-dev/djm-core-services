using System;
using DJM.CoreServices.Bootstrap;

namespace DJM.CoreServices.API
{
    /// <summary>
    /// Static utility class for managing and dispatching global events within the Unity project.
    /// Global events allow different parts of the game to communicate and respond to events efficiently.
    /// </summary>
    public static class DJMEvents
    {
        private static readonly IEventManager EventManager = DJMGlobalService.Instance.ResolvableContainer.Resolve<IEventManager>();
        /// <summary>
        /// Subscribes a listener method to a global event of a specified type.
        /// </summary>
        /// <typeparam name="T">The type of event to subscribe to.</typeparam>
        /// <param name="listener">The method to execute when the event is triggered.</param>
        public static void Subscribe<T>(Action<T> listener) where T : struct
        {
            EventManager.Subscribe(listener);
        }
        
        /// <summary>
        /// Unsubscribes a listener method from a global event of a specified type.
        /// </summary>
        /// <typeparam name="T">The type of event to unsubscribe from.</typeparam>
        /// <param name="listener">The method that was previously subscribed to the event.</param>
        public static void Unsubscribe<T>(Action<T> listener) where T : struct
        {
            EventManager.Unsubscribe(listener);
        }

        /// <summary>
        /// Triggers a global event of a specified type, passing the event data as a parameter.
        /// </summary>
        /// <typeparam name="T">The type of event to trigger.</typeparam>
        /// <param name="eventInstance">The event data to be passed to the event listeners.</param>
        public static void TriggerEvent<T>(T eventInstance) where T : struct
        {
            EventManager.TriggerEvent(eventInstance);
        }
    }
}