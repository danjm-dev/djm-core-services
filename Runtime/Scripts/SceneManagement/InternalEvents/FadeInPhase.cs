using System;
using DJM.CoreUtilities.EventManagement;

namespace DJM.CoreUtilities.SceneManagement
{
    internal readonly struct FadeInStartEvent
    {
        internal static void Trigger()
        {
            InternalGlobalEventManager.Instance.TriggerEvent(new FadeInStartEvent());
        }
        internal static void Subscribe(Action<FadeInStartEvent> listener)
        {
            InternalGlobalEventManager.Instance.Subscribe(listener);
        }
        internal static void Unsubscribe(Action<FadeInStartEvent> listener)
        {
            InternalGlobalEventManager.Instance.Unsubscribe(listener);
        }
    }
    
    internal readonly struct FadeInEndEvent
    {
        internal static void Trigger()
        {
            InternalGlobalEventManager.Instance.TriggerEvent(new FadeInEndEvent());
        }
        internal static void Subscribe(Action<FadeInEndEvent> listener)
        {
            InternalGlobalEventManager.Instance.Subscribe(listener);
        }
        internal static void Unsubscribe(Action<FadeInEndEvent> listener)
        {
            InternalGlobalEventManager.Instance.Unsubscribe(listener);
        }
    }
}