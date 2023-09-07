using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DJM.CoreUtilities.SceneManagement
{
    public sealed class SceneLoadManager : MonoBehaviour
    {
        [SerializeField] private SceneTransitionConfig defaultTransitionConfig;

        public void Reset() => defaultTransitionConfig = null;
        
        public void LoadScene(string sceneName, SceneTransitionConfig transitionConfig)
        {
            if (transitionConfig is null) LoadScene(sceneName);
            else StartCoroutine(LoadSceneCoroutine(sceneName, transitionConfig));
        }
        
        public void LoadScene(string sceneName)
        {
            if (defaultTransitionConfig is null) SceneManager.LoadScene(sceneName);
            else StartCoroutine(LoadSceneCoroutine(sceneName, defaultTransitionConfig));
        }
        
        private IEnumerator LoadSceneCoroutine(string sceneName, SceneTransitionConfig transitionConfig)
        {
            // start
            
            SceneLoadEvents.StartedEvent();
            var transitionCanvas  = Instantiate(transitionConfig.transitionCanvasPrefab, transform);

            // show canvas
            
            yield return Delay(transitionConfig.fadeIn.delay);
            SceneLoadEvents.ShowTransitionCanvasStartedEvent();
            yield return transitionCanvas.ShowCoroutine(transitionConfig.fadeIn);
            SceneLoadEvents.ShowTransitionCanvasCompletedEvent();
            
            // load new scene
            
            yield return Delay(transitionConfig.loadNewScene.loadStartDelay);
            SceneLoadEvents.NewSceneLoadStartedEvent();
            var sceneLoader = new SceneLoader();
            yield return sceneLoader.LoadScene
            (
                sceneName, 
                transitionConfig.loadNewScene.minimumLoadDuration, 
                SceneLoadEvents.OnLoadProgressUpdate
            );
            SceneLoadEvents.NewSceneLoadCompletedEvent();
            
            // activate new scene

            yield return Delay(transitionConfig.loadNewScene.loadCompleteSceneActivationDelay);
            yield return sceneLoader.ActivateScene();

            // hide transitionConfig canvas
            
            yield return Delay(transitionConfig.fadeOut.delay);
            SceneLoadEvents.HideTransitionCanvasStartedEvent();
            yield return transitionCanvas.HideCoroutine(transitionConfig.fadeIn);
            SceneLoadEvents.HideTransitionCanvasCompletedEvent();

            // done
            
            SceneLoadEvents.CompletedEvent();
            Destroy(transitionCanvas.gameObject);
        }

        private static IEnumerator Delay(float delay)
        {
            yield return new WaitForSeconds(delay);
        }
    }
}