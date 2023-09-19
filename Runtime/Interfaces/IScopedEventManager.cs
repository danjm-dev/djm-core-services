using DJM.EventManager;

namespace DJM.CoreServices
{
    /// <summary>
    /// Centralized event service scoped to the lifecycle of a scene. All listeners cleared when new scene loaded.
    /// Each event is identified by a struct type, which is also used as the event parameter for subscribed listeners.
    /// </summary>
    public interface IScopedEventManager : IEventManager
    {
    }
}