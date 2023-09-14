using System;
using System.Threading;
using System.Threading.Tasks;
using DJM.CoreUtilities.Services.Logger;
using UnityEngine.SceneManagement;

namespace DJM.CoreUtilities.Services.SceneLoader
{
    internal sealed class SceneLoaderService : ISceneLoaderService
    {
        private CancellationTokenSource _cancellationTokenSource;
        private ILoggerService _loggerService;

        internal SceneLoaderService(ILoggerService loggerService) => _loggerService = loggerService;
        
        public void LoadScene(string sceneName) => StartLoadingSceneAsync(sceneName);
        public void CancelLoadingScene() => _cancellationTokenSource?.Cancel();

        private async void StartLoadingSceneAsync(string sceneName)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                _loggerService.LogInfo($"Started loading Scene: {sceneName}.", nameof(SceneLoaderService));
                await LoadSceneAsync(sceneName, _cancellationTokenSource.Token);
                _loggerService.LogInfo($"Successfully loaded Scene: {sceneName}.", nameof(SceneLoaderService));
            }
            catch (TaskCanceledException)
            {
                _loggerService.LogInfo($"Cancelled loading scene: {sceneName}.", nameof(SceneLoaderService));
            }
            catch (Exception exception)
            {
                _loggerService.LogError($"Failed to load scene: {sceneName}. Error: {exception.Message}", nameof(SceneLoaderService));
            }
            finally
            {
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }

        private static async Task LoadSceneAsync(string sceneName, CancellationToken cancellationToken)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);

            asyncOperation.allowSceneActivation = false;

            asyncOperation.completed += _ => taskCompletionSource.TrySetResult(true);

            while (!asyncOperation.isDone)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    asyncOperation.allowSceneActivation = false;
                    taskCompletionSource.TrySetCanceled();
                    break;
                }

                if (asyncOperation.progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;
                }

                await Task.Yield();
            }

            await taskCompletionSource.Task;
        }
        
        
        
        
        // private AsyncOperation _sceneLoadOperation;
        //
        // internal IEnumerator LoadScene(string sceneName, float minDuration, Action<float> progressCallback)
        // {
        //     _sceneLoadOperation = SceneManager.LoadSceneAsync(sceneName);
        //     _sceneLoadOperation.allowSceneActivation = false;
        //     
        //     var progress = new DynamicFloatTween(0f, minDuration);
        //     progress.OnValueUpdate += progressCallback;
        //     
        //     do
        //     {
        //         progress.SetTarget(_sceneLoadOperation.progress);
        //         yield return null;
        //     } 
        //     while (_sceneLoadOperation.progress < 0.9f);
        //     
        //     progress.SetTarget(1f);
        //     while (progress.Value < 1f) yield return null;
        //     progress.OnValueUpdate -= progressCallback;
        // }
        //
        // internal IEnumerator ActivateScene()
        // {
        //     _sceneLoadOperation.allowSceneActivation = true;
        //     while (!_sceneLoadOperation.isDone) yield return null;
        // }
    }
}