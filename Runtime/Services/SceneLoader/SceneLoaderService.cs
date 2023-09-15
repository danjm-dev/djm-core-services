using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace DJM.CoreServices.Services.SceneLoader
{
    internal sealed class SceneLoaderService : ISceneLoader
    {
        private CancellationTokenSource _cancellationTokenSource;
        private ILogger _logger;

        internal SceneLoaderService(ILogger logger) => _logger = logger;
        
        public void LoadScene(string sceneName) => StartLoadingSceneAsync(sceneName);
        public void CancelLoadingScene() => _cancellationTokenSource?.Cancel();

        private async void StartLoadingSceneAsync(string sceneName)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                _logger.LogInfo($"Started loading Scene: {sceneName}.", nameof(SceneLoaderService));
                await LoadSceneAsync(sceneName, _cancellationTokenSource.Token);
                _logger.LogInfo($"Successfully loaded Scene: {sceneName}.", nameof(SceneLoaderService));
            }
            catch (TaskCanceledException)
            {
                _logger.LogInfo($"Cancelled loading scene: {sceneName}.", nameof(SceneLoaderService));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Failed to load scene: {sceneName}. Error: {exception.Message}", nameof(SceneLoaderService));
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