using System;
using System.Collections;
using DJM.CoreUtilities.TweenExtensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DJM.CoreUtilities
{
    public sealed class SceneLoader : MonoBehaviour
    {
        private SceneLoadEventManager _eventManager;
        
        [SerializeField] private SceneTransitionConfig defaultTransition;

        private void Awake()
        {
            _eventManager = new SceneLoadEventManager();
        }

        public void Reset() => defaultTransition = null;
        
        public void LoadScene(string sceneName, SceneTransitionConfig transitionConfig)
        {
            if (transitionConfig is null) LoadScene(sceneName);
            else StartCoroutine(LoadSceneCoroutine(sceneName, transitionConfig));
        }
        
        public void LoadScene(string sceneName)
        {
            if (defaultTransition is null) SceneManager.LoadScene(sceneName);
            else StartCoroutine(LoadSceneCoroutine(sceneName, defaultTransition));
        }
        
        private IEnumerator LoadSceneCoroutine(string sceneName, SceneTransitionConfig transitionConfig)
        {
            var transitionCanvas  = Instantiate(transitionConfig.transitionCanvasPrefab, transform);
            transitionCanvas.onStart?.Invoke();
            
            // fade in
            yield return RunFadePhase
            (
                transitionConfig.fadeInPhaseConfig, 
                transitionCanvas.fadeInPhaseEvents,
                transitionCanvas.CanvasGroupFader, 
                1f
            );
            
            AsyncOperation sceneLoadOperation = null;
            
            // scene load
            yield return RunLoadPhase
            (
                transitionConfig.loadPhaseConfig, 
                transitionCanvas.loadPhaseEvents, 
                sceneName, 
                operation => sceneLoadOperation = operation
            );
            
            // activate new scene
            yield return RunActivatePhase
            (
                transitionConfig.activatePhaseConfig, 
                transitionCanvas.activatePhaseEvents,
                sceneLoadOperation
            );
            
            // fade out
            yield return RunFadePhase
            (
                transitionConfig.fadeOutPhaseConfig, 
                transitionCanvas.fadeOutPhaseEvents,
                transitionCanvas.CanvasGroupFader, 
                0f
            );
            
            // transition complete
            transitionCanvas.onEnd?.Invoke();
            Destroy(transitionCanvas.gameObject);
        }

        private static IEnumerator RunFadePhase
        (
            SceneTransitionPhase.FadePhaseConfig config, 
            SceneTransitionPhase.FadePhaseEvents events,
            CanvasGroupFader canvasGroupFader,
            float alphaTarget
        )
        {
            yield return HandleDelay(config.delay);
            events.onFadeStart?.Invoke();
            
            yield return canvasGroupFader.FadeCanvasGroupAlphaCoroutine(alphaTarget, config.duration, config.ease);
            events.onFadeEnd?.Invoke();
        }

        private static IEnumerator RunLoadPhase
        (
            SceneTransitionPhase.LoadPhaseConfig config, 
            SceneTransitionPhase.LoadPhaseEvents events,
            string sceneName,
            Action<AsyncOperation> onComplete
        )
        {
            yield return HandleDelay(config.delay);
            
            events.onLoadStart?.Invoke();
            
            var sceneLoadAsyncOperation = SceneManager.LoadSceneAsync(sceneName);
            sceneLoadAsyncOperation.allowSceneActivation = false;
            
            var progressHandler = new DynamicFloatTween(0f, config.minimumDuration);
            Action<float> progressUpdateHandler = progress => events.onLoadProgress?.Invoke(progress);
            progressHandler.OnValueUpdate += progressUpdateHandler;
            
            do
            {
                progressHandler.SetTarget(sceneLoadAsyncOperation.progress);
                yield return null;
            } 
            while (sceneLoadAsyncOperation.progress < 0.9f);
            
            progressHandler.SetTarget(1f);
            while (progressHandler.Value < 1f) yield return null;
            progressHandler.OnValueUpdate -= progressUpdateHandler;
            
            events.onLoadEnd?.Invoke();
            onComplete?.Invoke(sceneLoadAsyncOperation);
        }
        
        private static IEnumerator RunActivatePhase
        (
            SceneTransitionPhase.ActivatePhaseConfig config, 
            SceneTransitionPhase.ActivatePhaseEvents events,
            AsyncOperation sceneLoadOperation
        )
        {
            yield return HandleDelay(config.delay);
            
            sceneLoadOperation.allowSceneActivation = true;
            while (!sceneLoadOperation.isDone) yield return null;
            
            events.onActivate?.Invoke();
        }

        private static IEnumerator HandleDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
        }
    }
}