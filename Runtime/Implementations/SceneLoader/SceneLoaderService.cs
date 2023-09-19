using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace DJM.CoreServices.SceneLoader
{
    /// <summary>
    /// Service for handling scene loading operations.
    /// </summary>
    internal sealed class SceneLoaderService : ISceneLoader
    {
        private readonly ILoadingScreenService _loadingScreenService;
        private readonly IDebugLogger _debugLogger;
        private readonly IPersistantEventManager _eventManager;
        
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneLoaderService"/> class.
        /// </summary>
        /// <param name="loadingScreenService">The service used for handling the loading screen.</param>
        /// <param name="debugLogger">The logger for debugging and error reporting.</param>
        /// <param name="eventManager">The persistant event manager for triggering loading-related events.</param>
        public SceneLoaderService(ILoadingScreenService loadingScreenService, IDebugLogger debugLogger, IPersistantEventManager eventManager)
        {
            _loadingScreenService = loadingScreenService;
            _debugLogger = debugLogger;
            _eventManager = eventManager;
        }

        /// <inheritdoc/>
        public void LoadScene(string sceneName) => StartLoadingSceneAsync(sceneName);
        
        /// <inheritdoc/>
        public void CancelLoadingScene() => _cancellationTokenSource?.Cancel();

        private async void StartLoadingSceneAsync(string sceneName)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                await _loadingScreenService.Show();
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
                await _loadingScreenService.Hide();
            }
        }

        private async Task LoadSceneAsync(string sceneName, CancellationToken cancellationToken)
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
                    await _loadingScreenService.CancelLoadProgress();
                    taskCompletionSource.TrySetCanceled();
                    break;
                }

                if (asyncOperation.progress >= 0.9f)
                {
                    await _loadingScreenService.CompleteLoadProgress();
                    _eventManager.TriggerEvent(new SceneLoaderEvent.ActivatingNewScene());
                    asyncOperation.allowSceneActivation = true;
                    break;
                }
                
                _loadingScreenService.SetLoadProgress(asyncOperation.progress);
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