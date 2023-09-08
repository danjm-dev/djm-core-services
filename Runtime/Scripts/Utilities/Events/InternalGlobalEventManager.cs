namespace DJM.CoreUtilities.Events
{
    /// <summary>
    /// Internal singleton event manager class responsible for managing global generic struct events.
    /// Publicly accessible via DJMEvents facade.
    /// Each event is identified by a struct type, which is also used as the event parameter for subscribed listeners.
    /// </summary>
    internal sealed class InternalGlobalEventManager : EventManagerBase
    {
        private static InternalGlobalEventManager _instance;
        
        /// <summary>
        /// Gets the singleton instance of the InternalGlobalEventManager.
        /// </summary>
        internal static InternalGlobalEventManager Instance => _instance ??= new InternalGlobalEventManager();

        /// <summary>
        /// Private constructor ensures that the object is instantiated only inside this class.
        /// </summary>
        private InternalGlobalEventManager() { }
    }
}