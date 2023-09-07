using DJM.CoreUtilities.EventManagement;

namespace DJM.CoreUtilities.SceneManagement
{
    internal static class SceneLoadEvents
    {
        internal static void StartedEvent() => GameEvents.TriggerEvent(new Started());
        internal static void ShowTransitionCanvasStartedEvent() => GameEvents.TriggerEvent(new ShowTransitionCanvasStarted());
        internal static void ShowTransitionCanvasCompletedEvent() => GameEvents.TriggerEvent(new ShowTransitionCanvasCompleted());
        internal static void NewSceneLoadStartedEvent() => GameEvents.TriggerEvent(new NewSceneLoadStarted());
        internal static void OnLoadProgressUpdate(float progress) => GameEvents.TriggerEvent(new NewSceneLoadProgress(progress));
        internal static void NewSceneLoadCompletedEvent() => GameEvents.TriggerEvent(new NewSceneLoadCompleted());
        internal static void HideTransitionCanvasStartedEvent() => GameEvents.TriggerEvent(new HideTransitionCanvasStarted());
        internal static void HideTransitionCanvasCompletedEvent() => GameEvents.TriggerEvent(new HideTransitionCanvasCompleted());
        internal static void CompletedEvent() => GameEvents.TriggerEvent(new Completed());
        
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