using UnityEngine;
using UnityEngine.Events;

namespace DJM.CoreUtilities.MonoServices.SceneTransitionCanvas
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
            // DJMEvents.Subscribe<SceneLoaderEvent.ShowTransitionCanvasStarted>(_ => fadeInEvents.onFadeStart.Invoke());
            // DJMEvents.Subscribe<SceneLoaderEvent.ShowTransitionCanvasCompleted>(_ => fadeInEvents.onFadeEnd.Invoke());
            //
            // // loading 
            // DJMEvents.Subscribe<SceneLoaderEvent.NewSceneLoadStarted>(_ => sceneLoadingEvents.onNewSceneLoadStarted.Invoke());
            // DJMEvents.Subscribe<SceneLoaderEvent.NewSceneLoadProgress>(progressEvent => sceneLoadingEvents.onNewSceneLoadProgress.Invoke(progressEvent.Progress));
            // DJMEvents.Subscribe<SceneLoaderEvent.NewSceneLoadCompleted>(_ => sceneLoadingEvents.onNewSceneLoadCompleted.Invoke());
            // DJMEvents.Subscribe<SceneLoaderEvent.NewSceneActivated>(_ => sceneLoadingEvents.onNewSceneActivated.Invoke());
            //
            // // fade out
            // DJMEvents.Subscribe<SceneLoaderEvent.HideTransitionCanvasStarted>(_ => fadeOutEvents.onFadeStart.Invoke());
            // DJMEvents.Subscribe<SceneLoaderEvent.HideTransitionCanvasCompleted>(_ => fadeOutEvents.onFadeEnd.Invoke());
        }
        
        internal void DisableEventListeners()
        {
            // fade in
            // DJMEvents.Unsubscribe<SceneLoaderEvent.ShowTransitionCanvasStarted>(_ => fadeInEvents.onFadeStart.Invoke());
            // DJMEvents.Unsubscribe<SceneLoaderEvent.ShowTransitionCanvasCompleted>(_ => fadeInEvents.onFadeEnd.Invoke());
            //
            // // loading 
            // DJMEvents.Unsubscribe<SceneLoaderEvent.NewSceneLoadStarted>(_ => sceneLoadingEvents.onNewSceneLoadStarted.Invoke());
            // DJMEvents.Unsubscribe<SceneLoaderEvent.NewSceneLoadProgress>(progressEvent => sceneLoadingEvents.onNewSceneLoadProgress.Invoke(progressEvent.Progress));
            // DJMEvents.Unsubscribe<SceneLoaderEvent.NewSceneLoadCompleted>(_ => sceneLoadingEvents.onNewSceneLoadCompleted.Invoke());
            // DJMEvents.Unsubscribe<SceneLoaderEvent.NewSceneActivated>(_ => sceneLoadingEvents.onNewSceneActivated.Invoke());
            //
            // // fade out
            // DJMEvents.Unsubscribe<SceneLoaderEvent.HideTransitionCanvasStarted>(_ => fadeOutEvents.onFadeStart.Invoke());
            // DJMEvents.Unsubscribe<SceneLoaderEvent.HideTransitionCanvasCompleted>(_ => fadeOutEvents.onFadeEnd.Invoke());
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