using System;

namespace DJM.CoreUtilities.Services.Events
{
    public static class EventManagerErrorMessages
    {
        private const string SubscribeNullListenerErrorMessage = 
            "{0}: Subscription attempt to {1} event had null listener.";

        private const string EventTriggerExceptionErrorMessage = 
            "{0}: Exception caught when triggering {1} event listener: {2}";
        
        public static string SubscribeNullListener(string nameOfEventManager, Type eventId)
        {
            return string.Format(SubscribeNullListenerErrorMessage, nameOfEventManager, eventId);
        }
        
        public static string EventTriggerException(string nameOfEventManager, Type eventId, Exception exception)
        {
            return string.Format(EventTriggerExceptionErrorMessage, nameOfEventManager, eventId, exception);
        }
    }
}