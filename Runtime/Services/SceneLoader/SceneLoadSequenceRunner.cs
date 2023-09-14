using System.Collections;
using UnityEngine;

namespace DJM.CoreUtilities.Services.SceneLoader
{
    internal static class SceneLoadSequenceRunner
    {
        // internal static void Run(string sceneName, SceneLoadSequenceConfig loadSequenceConfig)
        // {
        //     DJMContext.DelegateStartCoroutine(SceneLoadSequenceCoroutine(sceneName, loadSequenceConfig));
        // }
        //
        // private static IEnumerator SceneLoadSequenceCoroutine(string sceneName, SceneLoadSequenceConfig loadSequenceConfig)
        // {
        //     // start
        //     var transitionCanvas = DJMContext.Add(loadSequenceConfig.transitionCanvasPrefab);
        //     SceneLoaderEvent.StartedEvent();
        //     
        //
        //     // show canvas
        //     
        //     yield return Delay(loadSequenceConfig.fadeIn.delay);
        //     SceneLoaderEvent.ShowTransitionCanvasStartedEvent();
        //     yield return transitionCanvas.ShowCoroutine(loadSequenceConfig.fadeIn);
        //     SceneLoaderEvent.ShowTransitionCanvasCompletedEvent();
        //     
        //     yield return Delay(loadSequenceConfig.loadNewScene.loadStartDelay);
        //     // load new scene
        //     
        //     
        //     SceneLoaderEvent.NewSceneLoadStartedEvent();
        //     var sceneLoader = new SceneLoaderService();
        //     yield return sceneLoader.LoadScene
        //     (
        //         sceneName, 
        //         loadSequenceConfig.loadNewScene.minimumLoadDuration, 
        //         SceneLoaderEvent.OnLoadProgressUpdate
        //     );
        //     SceneLoaderEvent.NewSceneLoadCompletedEvent();
        //     
        //     // activate new scene
        //
        //     yield return Delay(loadSequenceConfig.loadNewScene.loadCompleteSceneActivationDelay);
        //     yield return sceneLoader.ActivateScene();
        //
        //     // hide loadSequenceConfig canvas
        //     
        //     yield return Delay(loadSequenceConfig.fadeOut.delay);
        //     SceneLoaderEvent.HideTransitionCanvasStartedEvent();
        //     yield return transitionCanvas.HideCoroutine(loadSequenceConfig.fadeOut);
        //     SceneLoaderEvent.HideTransitionCanvasCompletedEvent();
        //
        //     // done
        //     
        //     SceneLoaderEvent.CompletedEvent();
        //     Object.Destroy(transitionCanvas.gameObject);
        // }
        //
        // private static IEnumerator Delay(float delay)
        // {
        //     yield return new WaitForSeconds(delay);
        // }
    }
}