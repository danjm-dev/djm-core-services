namespace DJM.CoreUtilities.EventManagement
{
    /// <summary>
    /// Singleton event manager class responsible for managing global generic struct events.
    /// Each event is identified by a struct type, which is also used as the event parameter for subscribed listeners.
    /// </summary>
    public sealed class GlobalEventManager : EventManagerBase
    {
        private static GlobalEventManager _instance;
        
        /// <summary>
        /// Gets the singleton instance of the GlobalEventManager.
        /// </summary>
        public static GlobalEventManager Instance => _instance ??= new GlobalEventManager();

        /// <summary>
        /// Private constructor ensures that the object is instantiated only inside this class.
        /// </summary>
        private GlobalEventManager() { }
    }
}