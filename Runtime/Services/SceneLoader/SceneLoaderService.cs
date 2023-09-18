using System;
using System.Threading;
using System.Threading.Tasks;
using DJM.EventManager;
using UnityEngine.SceneManagement;

namespace DJM.CoreServices.Services.SceneLoader
{
    internal sealed class SceneLoaderService : ISceneLoader
    {
        private readonly IDebugLogger _debugLogger;
        private readonly IEventManager _eventManager;
        
        private CancellationTokenSource _cancellationTokenSource;

        public SceneLoaderService(IDebugLogger debugLogger, IEventManager eventManager)
        {
            _debugLogger = debugLogger;
            _eventManager = eventManager;
        }

        public void LoadScene(string sceneName) => StartLoadingSceneAsync(sceneName);
        public void CancelLoadingScene() => _cancellationTokenSource?.Cancel();

        private async void StartLoadingSceneAsync(string sceneName)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                _debugLogger.LogInfo($"Started loading Scene: {sceneName}.", nameof(SceneLoaderService));
                await LoadSceneAsync(sceneName, _cancellationTokenSource.Token);
                _debugLogger.LogInfo($"Successfully loaded Scene: {sceneName}.", nameof(SceneLoaderService));
            }
            catch (TaskCanceledException)
            {
                _debugLogger.LogInfo($"Cancelled loading scene: {sceneName}.", nameof(SceneLoaderService));
            }
            catch (Exception exception)
            {
                _debugLogger.LogError($"Failed to load scene: {sceneName}. Error: {exception.Message}", nameof(SceneLoaderService));
            }
            finally
            {
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }

        private async Task LoadSceneAsync(string sceneName, CancellationToken cancellationToken)
        {
            _eventManager.TriggerEvent(new SceneLoaderEvent.LoadStarted());
            
            var taskCompletionSource = new TaskCompletionSource<bool>();
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);

            asyncOperation.allowSceneActivation = false;

            asyncOperation.completed += _ => taskCompletionSource.TrySetResult(true);

            while (!asyncOperation.isDone)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _eventManager.TriggerEvent(new SceneLoaderEvent.LoadCancelled());
                    asyncOperation.allowSceneActivation = false;
                    taskCompletionSource.TrySetCanceled();
                    break;
                }

                if (asyncOperation.progress >= 0.9f)
                {
                    _eventManager.TriggerEvent(new SceneLoaderEvent.ActivatingNewScene());
                    asyncOperation.allowSceneActivation = true;
                }
                
                _eventManager.TriggerEvent(new SceneLoaderEvent.LoadProgress(asyncOperation.progress));
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