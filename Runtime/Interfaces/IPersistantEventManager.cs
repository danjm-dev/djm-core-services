using DJM.EventManager;

namespace DJM.CoreServices
{
    /// <summary>
    /// Centralized persistant event manager used internally by DJM Core Services. Subscribers persist through scene loading.
    /// Should not be used for non persistant event listeners (use IScopedEventManager instead). If it is, manually ensure all non-persistant listeners are unsubscribed before the new scene is loaded.
    /// Each event is identified by a struct type, which is also used as the event parameter for subscribed listeners.
    /// </summary>
    public interface IPersistantEventManager : ISubscribableEventManager, ITriggerableEventManager, IClearableEventManager
    {
    }
}