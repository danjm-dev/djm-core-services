using System;
using DJM.CoreUtilities.EventManagement;

namespace DJM.CoreUtilities.SceneManagement
{
    internal readonly struct FadeOutStartEvent
    {
        internal static void Trigger()
        {
            InternalGlobalEventManager.Instance.TriggerEvent(new FadeOutStartEvent());
        }
        internal static void Subscribe(Action<FadeOutStartEvent> listener)
        {
            InternalGlobalEventManager.Instance.Subscribe(listener);
        }
        internal static void Unsubscribe(Action<FadeOutStartEvent> listener)
        {
            InternalGlobalEventManager.Instance.Unsubscribe(listener);
        }
    }
    
    internal readonly struct FadeOutEndEvent
    {
        internal static void Trigger()
        {
            InternalGlobalEventManager.Instance.TriggerEvent(new FadeOutEndEvent());
        }
        internal static void Subscribe(Action<FadeOutEndEvent> listener)
        {
            InternalGlobalEventManager.Instance.Subscribe(listener);
        }
        internal static void Unsubscribe(Action<FadeOutEndEvent> listener)
        {
            InternalGlobalEventManager.Instance.Unsubscribe(listener);
        }
    }
}