using System;
using System.Collections;
using DJM.CoreUtilities.TweenExtensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DJM.CoreUtilities
{
    public sealed class SceneLoader : MonoBehaviour
    {
        private readonly SceneLoaderEvents _sceneLoaderEvents = new();
        
        [SerializeField] private SceneTransitionConfig defaultTransition;

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
            _sceneLoaderEvents.InvokeStart();
            
            // fade in
            yield return RunFadeInPhase(transitionConfig.fadeInPhaseConfig, transitionCanvas.CanvasGroupFader);
            
            AsyncOperation sceneLoadOperation = null;
            
            // scene load
            yield return RunLoadPhase
            (
                transitionConfig.loadPhaseConfig,
                sceneName, 
                operation => sceneLoadOperation = operation
            );
            
            // activate new scene
            yield return RunActivatePhase(transitionConfig.activatePhaseConfig, sceneLoadOperation);
            
            // fade out
            yield return RunFadeOutPhase(transitionConfig.fadeOutPhaseConfig, transitionCanvas.CanvasGroupFader);
            
            // transition complete
            _sceneLoaderEvents.InvokeEnd();
            Destroy(transitionCanvas.gameObject);
        }

        private IEnumerator RunFadeInPhase
        (
            SceneTransitionPhase.FadePhaseConfig config, 
            CanvasGroupFader canvasGroupFader
        )
        {
            yield return HandleDelay(config.delay);
            _sceneLoaderEvents.InvokeFadeInStart();
            
            yield return canvasGroupFader.FadeCanvasGroupAlphaCoroutine(1f, config.duration, config.ease);
            _sceneLoaderEvents.InvokeFadeInEnd();
        }
        
        private IEnumerator RunFadeOutPhase
        (
            SceneTransitionPhase.FadePhaseConfig config, 
            CanvasGroupFader canvasGroupFader
        )
        {
            yield return HandleDelay(config.delay);
            _sceneLoaderEvents.InvokeFadeOutStart();
            
            yield return canvasGroupFader.FadeCanvasGroupAlphaCoroutine(0f, config.duration, config.ease);
            _sceneLoaderEvents.InvokeFadeOutEnd();
        }

        private IEnumerator RunLoadPhase
        (
            SceneTransitionPhase.LoadPhaseConfig config, 
            string sceneName,
            Action<AsyncOperation> onComplete
        )
        {
            yield return HandleDelay(config.delay);
            
            _sceneLoaderEvents.InvokeLoadStart();
            
            var sceneLoadAsyncOperation = SceneManager.LoadSceneAsync(sceneName);
            sceneLoadAsyncOperation.allowSceneActivation = false;
            
            var progressHandler = new DynamicFloatTween(0f, config.minimumDuration);
            progressHandler.OnValueUpdate += _sceneLoaderEvents.InvokeLoadProgress;
            
            do
            {
                progressHandler.SetTarget(sceneLoadAsyncOperation.progress);
                yield return null;
            } 
            while (sceneLoadAsyncOperation.progress < 0.9f);
            
            progressHandler.SetTarget(1f);
            while (progressHandler.Value < 1f) yield return null;
            progressHandler.OnValueUpdate -= _sceneLoaderEvents.InvokeLoadProgress;
            
            _sceneLoaderEvents.InvokeLoadEnd();
            onComplete?.Invoke(sceneLoadAsyncOperation);
        }
        
        private IEnumerator RunActivatePhase
        (
            SceneTransitionPhase.ActivatePhaseConfig config, 
            AsyncOperation sceneLoadOperation
        )
        {
            yield return HandleDelay(config.delay);
            
            sceneLoadOperation.allowSceneActivation = true;
            while (!sceneLoadOperation.isDone) yield return null;
            
            _sceneLoaderEvents.InvokeActivate();
        }

        private static IEnumerator HandleDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
        }
    }
}