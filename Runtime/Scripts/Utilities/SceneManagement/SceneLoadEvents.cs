namespace DJM.CoreUtilities.SceneManagement
{
    public static class SceneLoadEvents
    {
        internal static void StartedEvent() => DJMEvents.TriggerEvent(new Started());
        internal static void ShowTransitionCanvasStartedEvent() => DJMEvents.TriggerEvent(new ShowTransitionCanvasStarted());
        internal static void ShowTransitionCanvasCompletedEvent() => DJMEvents.TriggerEvent(new ShowTransitionCanvasCompleted());
        internal static void NewSceneLoadStartedEvent() => DJMEvents.TriggerEvent(new NewSceneLoadStarted());
        internal static void OnLoadProgressUpdate(float progress) => DJMEvents.TriggerEvent(new NewSceneLoadProgress(progress));
        internal static void NewSceneLoadCompletedEvent() => DJMEvents.TriggerEvent(new NewSceneLoadCompleted());
        internal static void HideTransitionCanvasStartedEvent() => DJMEvents.TriggerEvent(new HideTransitionCanvasStarted());
        internal static void HideTransitionCanvasCompletedEvent() => DJMEvents.TriggerEvent(new HideTransitionCanvasCompleted());
        internal static void CompletedEvent() => DJMEvents.TriggerEvent(new Completed());
        
        internal readonly struct Started { }
        internal readonly struct ShowTransitionCanvasStarted { }
        internal readonly struct ShowTransitionCanvasCompleted { }
        internal readonly struct NewSceneLoadStarted { }
        internal readonly struct NewSceneLoadProgress
        {
            internal readonly float Progress;
            internal NewSceneLoadProgress(float progress) => Progress = progress;
        }
        internal readonly struct NewSceneLoadCompleted { }
        internal readonly struct NewSceneActivated { }
        internal readonly struct HideTransitionCanvasStarted{ }
        internal readonly struct HideTransitionCanvasCompleted { }
        internal readonly struct Completed { }
    }
}