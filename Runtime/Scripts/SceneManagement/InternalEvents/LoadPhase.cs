using System;
using DJM.CoreUtilities.EventManagement;

namespace DJM.CoreUtilities.SceneManagement
{
    internal readonly struct LoadStartEvent
    {
    }
    
    internal readonly struct LoadProgressEvent
    {
        internal readonly float Progress;
        private LoadProgressEvent(float progress) => Progress = progress;

        internal static void Trigger(float progress)
        {
            InternalGlobalEventManager.Instance.TriggerEvent(new LoadProgressEvent(progress));
        }
        internal static void Subscribe(Action<LoadProgressEvent> listener)
        {
            InternalGlobalEventManager.Instance.Subscribe(listener);
        }
        internal static void Unsubscribe(Action<LoadProgressEvent> listener)
        {
            InternalGlobalEventManager.Instance.Unsubscribe(listener);
        }
    }
    
    internal readonly struct LoadEndEvent
    {
    }
}