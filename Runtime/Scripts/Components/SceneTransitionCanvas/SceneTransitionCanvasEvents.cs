using DJM.CoreUtilities.SceneLoading;
using UnityEngine;
using UnityEngine.Events;

namespace DJM.CoreUtilities.Components
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
            DJMEvents.Subscribe<SceneLoadEvents.ShowTransitionCanvasStarted>(_ => fadeInEvents.onFadeStart.Invoke());
            DJMEvents.Subscribe<SceneLoadEvents.ShowTransitionCanvasCompleted>(_ => fadeInEvents.onFadeEnd.Invoke());
            
            // loading 
            DJMEvents.Subscribe<SceneLoadEvents.NewSceneLoadStarted>(_ => sceneLoadingEvents.onNewSceneLoadStarted.Invoke());
            DJMEvents.Subscribe<SceneLoadEvents.NewSceneLoadProgress>(progressEvent => sceneLoadingEvents.onNewSceneLoadProgress.Invoke(progressEvent.Progress));
            DJMEvents.Subscribe<SceneLoadEvents.NewSceneLoadCompleted>(_ => sceneLoadingEvents.onNewSceneLoadCompleted.Invoke());
            DJMEvents.Subscribe<SceneLoadEvents.NewSceneActivated>(_ => sceneLoadingEvents.onNewSceneActivated.Invoke());
            
            // fade out
            DJMEvents.Subscribe<SceneLoadEvents.HideTransitionCanvasStarted>(_ => fadeOutEvents.onFadeStart.Invoke());
            DJMEvents.Subscribe<SceneLoadEvents.HideTransitionCanvasCompleted>(_ => fadeOutEvents.onFadeEnd.Invoke());
        }
        
        internal void DisableEventListeners()
        {
            // fade in
            DJMEvents.Unsubscribe<SceneLoadEvents.ShowTransitionCanvasStarted>(_ => fadeInEvents.onFadeStart.Invoke());
            DJMEvents.Unsubscribe<SceneLoadEvents.ShowTransitionCanvasCompleted>(_ => fadeInEvents.onFadeEnd.Invoke());
            
            // loading 
            DJMEvents.Unsubscribe<SceneLoadEvents.NewSceneLoadStarted>(_ => sceneLoadingEvents.onNewSceneLoadStarted.Invoke());
            DJMEvents.Unsubscribe<SceneLoadEvents.NewSceneLoadProgress>(progressEvent => sceneLoadingEvents.onNewSceneLoadProgress.Invoke(progressEvent.Progress));
            DJMEvents.Unsubscribe<SceneLoadEvents.NewSceneLoadCompleted>(_ => sceneLoadingEvents.onNewSceneLoadCompleted.Invoke());
            DJMEvents.Unsubscribe<SceneLoadEvents.NewSceneActivated>(_ => sceneLoadingEvents.onNewSceneActivated.Invoke());
            
            // fade out
            DJMEvents.Unsubscribe<SceneLoadEvents.HideTransitionCanvasStarted>(_ => fadeOutEvents.onFadeStart.Invoke());
            DJMEvents.Unsubscribe<SceneLoadEvents.HideTransitionCanvasCompleted>(_ => fadeOutEvents.onFadeEnd.Invoke());
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