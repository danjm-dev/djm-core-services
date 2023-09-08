using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DJM.CoreUtilities.SceneManagement
{
    public static class SceneLoadManager
    {
        public static void LoadScene(string sceneName, SceneTransitionConfig transitionConfig)
        {
            if (transitionConfig is null) LoadScene(sceneName);
            else DJMComponentContext.Instance.StartCoroutine(LoadSceneCoroutine(sceneName, transitionConfig));
        }
        
        public static void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
        
        private static IEnumerator LoadSceneCoroutine(string sceneName, SceneTransitionConfig transitionConfig)
        {
            // start
            
            SceneLoadEvents.StartedEvent();
            var transitionCanvas = DJMComponentContext.Instance.InstantiateChild(transitionConfig.transitionCanvasPrefab);

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
            Object.Destroy(transitionCanvas.gameObject);
        }

        private static IEnumerator Delay(float delay)
        {
            yield return new WaitForSeconds(delay);
        }
    }
}