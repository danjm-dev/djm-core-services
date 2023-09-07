using DJM.CoreUtilities.EventManagement;
using UnityEngine;
using UnityEngine.Events;

namespace DJM.CoreUtilities.SceneManagement
{
    [System.Serializable]
    internal class SceneTransitionCanvasEvents
    {
        [SerializeField] private FadeEvents fadeInEvents;
        [SerializeField] private SceneLoadingEvents sceneLoadingEvents;
        [SerializeField] private FadeEvents fadeOutEvents;
        
        internal void EnableEventListeners()
        {
            // fade in
            GameEvents.Subscribe<SceneLoadEvents.ShowTransitionCanvasStarted>(_ => fadeInEvents.onFadeStart.Invoke());
            GameEvents.Subscribe<SceneLoadEvents.ShowTransitionCanvasCompleted>(_ => fadeInEvents.onFadeEnd.Invoke());
            
            // loading 
            GameEvents.Subscribe<SceneLoadEvents.NewSceneLoadStarted>(_ => sceneLoadingEvents.onNewSceneLoadStarted.Invoke());
            GameEvents.Subscribe<SceneLoadEvents.NewSceneLoadProgress>(progressEvent => sceneLoadingEvents.onNewSceneLoadProgress.Invoke(progressEvent.Progress));
            GameEvents.Subscribe<SceneLoadEvents.NewSceneLoadCompleted>(_ => sceneLoadingEvents.onNewSceneLoadCompleted.Invoke());
            GameEvents.Subscribe<SceneLoadEvents.NewSceneActivated>(_ => sceneLoadingEvents.onNewSceneActivated.Invoke());
            
            // fade out
            GameEvents.Subscribe<SceneLoadEvents.HideTransitionCanvasStarted>(_ => fadeOutEvents.onFadeStart.Invoke());
            GameEvents.Subscribe<SceneLoadEvents.HideTransitionCanvasCompleted>(_ => fadeOutEvents.onFadeEnd.Invoke());
        }
        
        internal void DisableEventListeners()
        {
            // fade in
            GameEvents.Unsubscribe<SceneLoadEvents.ShowTransitionCanvasStarted>(_ => fadeInEvents.onFadeStart.Invoke());
            GameEvents.Unsubscribe<SceneLoadEvents.ShowTransitionCanvasCompleted>(_ => fadeInEvents.onFadeEnd.Invoke());
            
            // loading 
            GameEvents.Unsubscribe<SceneLoadEvents.NewSceneLoadStarted>(_ => sceneLoadingEvents.onNewSceneLoadStarted.Invoke());
            GameEvents.Unsubscribe<SceneLoadEvents.NewSceneLoadProgress>(progressEvent => sceneLoadingEvents.onNewSceneLoadProgress.Invoke(progressEvent.Progress));
            GameEvents.Unsubscribe<SceneLoadEvents.NewSceneLoadCompleted>(_ => sceneLoadingEvents.onNewSceneLoadCompleted.Invoke());
            GameEvents.Unsubscribe<SceneLoadEvents.NewSceneActivated>(_ => sceneLoadingEvents.onNewSceneActivated.Invoke());
            
            // fade out
            GameEvents.Unsubscribe<SceneLoadEvents.HideTransitionCanvasStarted>(_ => fadeOutEvents.onFadeStart.Invoke());
            GameEvents.Unsubscribe<SceneLoadEvents.HideTransitionCanvasCompleted>(_ => fadeOutEvents.onFadeEnd.Invoke());
        }
        
        
        [System.Serializable]
        private sealed class FadeEvents
        {
            public UnityEvent onFadeStart;
            public UnityEvent onFadeEnd;
        }
        
        [System.Serializable]
        private sealed class SceneLoadingEvents
        {
            public UnityEvent onNewSceneLoadStarted;
            public UnityEvent<float> onNewSceneLoadProgress;
            public UnityEvent onNewSceneLoadCompleted;
            public UnityEvent onNewSceneActivated;
        }
    }
}