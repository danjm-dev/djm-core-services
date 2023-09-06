using System;
using DJM.CoreUtilities.EventManagement;

namespace DJM.CoreUtilities.SceneManagement
{
    internal readonly struct FadeOutStartEvent
    {
        internal static void Trigger()
        {
            GlobalEventManager.Instance.TriggerEvent(new FadeOutStartEvent());
        }
        internal static void Subscribe(Action<FadeOutStartEvent> listener)
        {
            GlobalEventManager.Instance.Subscribe(listener);
        }
        internal static void Unsubscribe(Action<FadeOutStartEvent> listener)
        {
            GlobalEventManager.Instance.Unsubscribe(listener);
        }
    }
    
    internal readonly struct FadeOutEndEvent
    {
        internal static void Trigger()
        {
            GlobalEventManager.Instance.TriggerEvent(new FadeOutEndEvent());
        }
        internal static void Subscribe(Action<FadeOutEndEvent> listener)
        {
            GlobalEventManager.Instance.Subscribe(listener);
        }
        internal static void Unsubscribe(Action<FadeOutEndEvent> listener)
        {
            GlobalEventManager.Instance.Unsubscribe(listener);
        }
    }
}