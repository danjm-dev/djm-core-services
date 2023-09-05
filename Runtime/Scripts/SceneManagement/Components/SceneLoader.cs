using System.Collections;
using DG.Tweening;
using DJM.CoreUtilities.TweenExtensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DJM.CoreUtilities
{
    public sealed class SceneLoader : MonoBehaviour
    {
        [SerializeField] private SceneLoadTransitionConfig defaultLoadTransition;
        
        public void Reset() => defaultLoadTransition = null;
        
        public void LoadScene(string sceneName, SceneLoadTransitionConfig loadTransitionConfig)
        {
            if (loadTransitionConfig is null) LoadScene(sceneName);
            else StartCoroutine(LoadSceneCoroutine(sceneName, loadTransitionConfig));
        }
        
        public void LoadScene(string sceneName)
        {
            if (defaultLoadTransition is null) SceneManager.LoadScene(sceneName);
            else StartCoroutine(LoadSceneCoroutine(sceneName, defaultLoadTransition));
        }
        
        private IEnumerator LoadSceneCoroutine(string sceneName, SceneLoadTransitionConfig transitionConfig)
        {
            var transitionCanvas  = Instantiate(transitionConfig.sceneTransitionCanvasPrefab, transform);
            
            transitionCanvas.onFadeInStart?.Invoke();
            yield return StartCoroutine(transitionCanvas.CanvasGroupFader.FadeCanvasGroupAlphaCoroutine
            (
                1f, 
                transitionConfig.fadeInDuration, 
                transitionConfig.fadeInEase
            ));
            transitionCanvas.onFadeInEnd?.Invoke();


            yield return new WaitForSeconds(transitionConfig.newSceneLoadStartDelay);
            transitionCanvas.onLoadStart?.Invoke();
            
            // start scene load
            var sceneLoadAsyncOperation = SceneManager.LoadSceneAsync(sceneName);
            sceneLoadAsyncOperation.allowSceneActivation = false;
            
            var sceneLoadProgress = new DynamicFloatTween(0f);
            do
            {
                sceneLoadProgress.SetTarget(sceneLoadAsyncOperation.progress, transitionConfig.minimumLoadDuration);
                transitionCanvas.onSetLoadProgress?.Invoke(sceneLoadProgress.Value);
                yield return null;
            } 
            while (sceneLoadAsyncOperation.progress < 0.9f);
            
            sceneLoadProgress.SetTarget(1f, transitionConfig.minimumLoadDuration);
            while (!sceneLoadProgress.AtTarget)
            {
                transitionCanvas.onSetLoadProgress?.Invoke(sceneLoadProgress.Value);
                yield return null;
            }
            transitionCanvas.onSetLoadProgress?.Invoke(sceneLoadProgress.Value); // is this necessary?
            transitionCanvas.onLoadEnd?.Invoke();
            
            // wait for delay
            yield return new WaitForSeconds(transitionConfig.newSceneActivationDelay);
            
            
            // complete load, and wait till actually done
            sceneLoadAsyncOperation.allowSceneActivation = true;
            while (!sceneLoadAsyncOperation.isDone) yield return null;
            transitionCanvas.onNewSceneActivation?.Invoke();
            
            
            transitionCanvas.onFadeOutStart?.Invoke();
            yield return StartCoroutine(transitionCanvas.CanvasGroupFader.FadeCanvasGroupAlphaCoroutine
            (
                0f, 
                transitionConfig.fadeOutDuration, 
                transitionConfig.fadeOutEase
            ));
            transitionCanvas.onFadeOutEnd?.Invoke();
            Destroy(transitionCanvas.gameObject);
        }
    }
}