using System.Collections;
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
            StartCoroutine(LoadSceneASync(sceneName, loadTransitionConfig));
        }
        
        public void LoadScene(string sceneName)
        {
            if (defaultLoadTransition is null) LoadSceneSync(sceneName);
            else StartCoroutine(LoadSceneASync(sceneName, defaultLoadTransition));
        }
        
        private void LoadSceneSync(string sceneName) => SceneManager.LoadScene(sceneName);
        
        private IEnumerator LoadSceneASync(string sceneName, SceneLoadTransitionConfig transitionConfig)
        {
            var canvas  = Instantiate(transitionConfig.transitionCanvasPrefab, transform);
            var mainCanvasGroup = canvas.GetComponent<CanvasGroup>();
            
            yield return TransitionCanvasOperations.ShowCanvas(mainCanvasGroup, transitionConfig);
            
            // start scene load
            var sceneLoadAsyncOperation = SceneManager.LoadSceneAsync(sceneName);
            sceneLoadAsyncOperation.allowSceneActivation = false;

            if (transitionConfig.progressBar)
                yield return ProgressBarOperations.FillAsync
                (
                    canvas.GetComponent<ProgressBar>(),
                    sceneLoadAsyncOperation, 
                    transitionConfig.progressBarMinLoadDuration
                );
            else
                yield return sceneLoadAsyncOperation;
            
            sceneLoadAsyncOperation.allowSceneActivation = true;
            yield return new WaitForSeconds(transitionConfig.loadCompleteDelayDuration);
            
            yield return TransitionCanvasOperations.HideCanvas(mainCanvasGroup, transitionConfig);
        }
    }
}