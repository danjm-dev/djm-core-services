using System;
using System.Collections;
using DJM.CoreUtilities.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DJM.CoreUtilities.SceneLoading
{
    internal sealed class SceneLoaderService
    {
        private AsyncOperation _sceneLoadOperation;
        
        internal IEnumerator LoadScene(string sceneName, float minDuration, Action<float> progressCallback)
        {
            _sceneLoadOperation = SceneManager.LoadSceneAsync(sceneName);
            _sceneLoadOperation.allowSceneActivation = false;
            
            var progress = new DynamicFloatTween(0f, minDuration);
            progress.OnValueUpdate += progressCallback;
            
            do
            {
                progress.SetTarget(_sceneLoadOperation.progress);
                yield return null;
            } 
            while (_sceneLoadOperation.progress < 0.9f);
            
            progress.SetTarget(1f);
            while (progress.Value < 1f) yield return null;
            progress.OnValueUpdate -= progressCallback;
        }

        internal IEnumerator ActivateScene()
        {
            _sceneLoadOperation.allowSceneActivation = true;
            while (!_sceneLoadOperation.isDone) yield return null;
        }
    }
}