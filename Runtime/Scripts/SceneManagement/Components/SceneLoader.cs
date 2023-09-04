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
            StartCoroutine(LoadSceneCoroutine(sceneName, loadTransitionConfig));
        }
        
        public void LoadScene(string sceneName)
        {
            if (defaultLoadTransition is null) SceneManager.LoadScene(sceneName);
            else StartCoroutine(LoadSceneCoroutine(sceneName, defaultLoadTransition));
        }
        
        private IEnumerator LoadSceneCoroutine(string sceneName, SceneLoadTransitionConfig transitionConfig)
        {
            // create canvas
            var canvas  = Instantiate(transitionConfig.transitionCanvasPrefab, transform);
            var sceneTransitionCanvas = canvas.GetComponent<SceneTransitionCanvas>();
            
            // show canvas
            yield return sceneTransitionCanvas.ShowCanvasCoroutine(transitionConfig);
            
            // start scene load
            var sceneLoadAsyncOperation = SceneManager.LoadSceneAsync(sceneName);
            sceneLoadAsyncOperation.allowSceneActivation = false;

            // wait for load to complete
            if (transitionConfig.progressBar)
            {
                var progressBar = canvas.GetComponent<SceneTransitionProgressBar>();
                yield return progressBar.FillCoroutine(sceneLoadAsyncOperation, transitionConfig);
            }
            else
                while (sceneLoadAsyncOperation.progress < 0.9f) yield return null;
            
            // swap scenes
            sceneLoadAsyncOperation.allowSceneActivation = true;
            
            // wait for delay
            yield return new WaitForSeconds(transitionConfig.loadCompleteDelayDuration);
            
            // hide then destroy canvas
            yield return sceneTransitionCanvas.HideCanvasCoroutine(transitionConfig);
            Destroy(canvas);
        }
    }
}