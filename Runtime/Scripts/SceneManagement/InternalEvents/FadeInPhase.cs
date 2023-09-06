using System;
using DJM.CoreUtilities.EventManagement;

namespace DJM.CoreUtilities.SceneManagement
{
    internal readonly struct FadeInStartEvent
    {
        internal static void Trigger()
        {
            GlobalEventManager.Instance.TriggerEvent(new FadeInStartEvent());
        }
        internal static void Subscribe(Action<FadeInStartEvent> listener)
        {
            GlobalEventManager.Instance.Subscribe(listener);
        }
        internal static void Unsubscribe(Action<FadeInStartEvent> listener)
        {
            GlobalEventManager.Instance.Unsubscribe(listener);
        }
    }
    
    internal readonly struct FadeInEndEvent
    {
        internal static void Trigger()
        {
            GlobalEventManager.Instance.TriggerEvent(new FadeInEndEvent());
        }
        internal static void Subscribe(Action<FadeInEndEvent> listener)
        {
            GlobalEventManager.Instance.Subscribe(listener);
        }
        internal static void Unsubscribe(Action<FadeInEndEvent> listener)
        {
            GlobalEventManager.Instance.Unsubscribe(listener);
        }
    }
}