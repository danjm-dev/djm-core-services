using System;
using System.Collections;
using DJM.CoreUtilities.EventManagement;
using DJM.CoreUtilities.TweenExtensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DJM.CoreUtilities.SceneManagement
{
    public sealed class SceneLoader : MonoBehaviour
    {
        [SerializeField] private SceneTransition defaultTransition;

        public void Reset() => defaultTransition = null;
        
        public void LoadScene(string sceneName, SceneTransition transition)
        {
            if (transition is null) LoadScene(sceneName);
            else StartCoroutine(LoadSceneCoroutine(sceneName, transition));
        }
        
        public void LoadScene(string sceneName)
        {
            if (defaultTransition is null) SceneManager.LoadScene(sceneName);
            else StartCoroutine(LoadSceneCoroutine(sceneName, defaultTransition));
        }
        
        private IEnumerator LoadSceneCoroutine(string sceneName, SceneTransition transition)
        {
            var transitionCanvas  = Instantiate(transition.transitionCanvasPrefab, transform);
            //_sceneLoaderEvents.InvokeStart();
            
            // fade in
            yield return RunFadeInPhase(transition.fadeInPhase, transitionCanvas.CanvasGroupFader);
            
            AsyncOperation sceneLoadOperation = null;
            
            // scene load
            yield return RunLoadPhase
            (
                transition.loadPhase,
                sceneName, 
                operation => sceneLoadOperation = operation
            );
            
            // activate new scene
            yield return RunActivatePhase(transition.activatePhase, sceneLoadOperation);
            
            // fade out
            yield return RunFadeOutPhase(transition.fadeOutPhase, transitionCanvas.CanvasGroupFader);
            
            // transition complete
            //_sceneLoaderEvents.InvokeEnd();
            Destroy(transitionCanvas.gameObject);
        }

        private IEnumerator RunFadeInPhase
        (
            SceneTransitionConfig.FadePhase config, 
            CanvasGroupFader canvasGroupFader
        )
        {
            yield return HandleDelay(config.delay);
            FadeInStartEvent.Trigger();
            
            yield return canvasGroupFader.FadeCanvasGroupAlphaCoroutine(1f, config.duration, config.ease);
            FadeInEndEvent.Trigger();
        }
        
        private IEnumerator RunFadeOutPhase
        (
            SceneTransitionConfig.FadePhase config, 
            CanvasGroupFader canvasGroupFader
        )
        {
            yield return HandleDelay(config.delay);
            FadeOutStartEvent.Trigger();
            
            yield return canvasGroupFader.FadeCanvasGroupAlphaCoroutine(0f, config.duration, config.ease);
            FadeOutEndEvent.Trigger();
        }

        private IEnumerator RunLoadPhase
        (
            SceneTransitionConfig.LoadPhase config, 
            string sceneName,
            Action<AsyncOperation> onComplete
        )
        {
            yield return HandleDelay(config.delay);
            
            //_sceneLoaderEvents.InvokeLoadStart();

            Action<float> updateProgress = LoadProgressEvent.Trigger;
            
            var sceneLoadAsyncOperation = SceneManager.LoadSceneAsync(sceneName);
            sceneLoadAsyncOperation.allowSceneActivation = false;
            
            var progressHandler = new DynamicFloatTween(0f, config.minimumDuration);
            progressHandler.OnValueUpdate += updateProgress;
            
            do
            {
                progressHandler.SetTarget(sceneLoadAsyncOperation.progress);
                yield return null;
            } 
            while (sceneLoadAsyncOperation.progress < 0.9f);
            
            progressHandler.SetTarget(1f);
            while (progressHandler.Value < 1f) yield return null;
            progressHandler.OnValueUpdate -= updateProgress;
            
            //_sceneLoaderEvents.InvokeLoadEnd();
            onComplete?.Invoke(sceneLoadAsyncOperation);
        }
        
        private IEnumerator RunActivatePhase
        (
            SceneTransitionConfig.ActivatePhase config, 
            AsyncOperation sceneLoadOperation
        )
        {
            yield return HandleDelay(config.delay);
            
            sceneLoadOperation.allowSceneActivation = true;
            while (!sceneLoadOperation.isDone) yield return null;
            
            //_sceneLoaderEvents.InvokeActivate();
        }

        private static IEnumerator HandleDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
        }
    }
}