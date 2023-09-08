using System.Collections;
using UnityEngine;

namespace DJM.CoreUtilities.SceneManagement
{
    internal static class SceneLoadSequenceRunner
    {
        internal static void Run(string sceneName, SceneLoadSequenceConfig loadSequenceConfig)
        {
            DJMContext.DelegateStartCoroutine(SceneLoadSequenceCoroutine(sceneName, loadSequenceConfig));
        }
        
        private static IEnumerator SceneLoadSequenceCoroutine(string sceneName, SceneLoadSequenceConfig loadSequenceConfig)
        {
            // start
            var transitionCanvas = DJMContext.Add(loadSequenceConfig.transitionCanvasPrefab);
            SceneLoadEvents.StartedEvent();
            

            // show canvas
            
            yield return Delay(loadSequenceConfig.fadeIn.delay);
            SceneLoadEvents.ShowTransitionCanvasStartedEvent();
            yield return transitionCanvas.ShowCoroutine(loadSequenceConfig.fadeIn);
            SceneLoadEvents.ShowTransitionCanvasCompletedEvent();
            
            yield return Delay(loadSequenceConfig.loadNewScene.loadStartDelay);
            // load new scene
            
            
            SceneLoadEvents.NewSceneLoadStartedEvent();
            var sceneLoader = new SceneLoaderService();
            yield return sceneLoader.LoadScene
            (
                sceneName, 
                loadSequenceConfig.loadNewScene.minimumLoadDuration, 
                SceneLoadEvents.OnLoadProgressUpdate
            );
            SceneLoadEvents.NewSceneLoadCompletedEvent();
            
            // activate new scene

            yield return Delay(loadSequenceConfig.loadNewScene.loadCompleteSceneActivationDelay);
            yield return sceneLoader.ActivateScene();

            // hide loadSequenceConfig canvas
            
            yield return Delay(loadSequenceConfig.fadeOut.delay);
            SceneLoadEvents.HideTransitionCanvasStartedEvent();
            yield return transitionCanvas.HideCoroutine(loadSequenceConfig.fadeOut);
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