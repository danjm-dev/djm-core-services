using System.Collections;
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
        private readonly IMonoBehaviorDelegator _monoBehaviorDelegator;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneLoaderService"/> class.
        /// </summary>
        /// <param name="loadingScreenService">The service used for handling the loading screen.</param>
        /// <param name="debugLogger">The logger for debugging and error reporting.</param>
        /// <param name="eventManager">The persistant event manager for triggering loading-related events.</param>
        public SceneLoaderService(ILoadingScreenService loadingScreenService, IDebugLogger debugLogger, IPersistantEventManager eventManager, IMonoBehaviorDelegator monoBehaviorDelegator)
        {
            _loadingScreenService = loadingScreenService;
            _debugLogger = debugLogger;
            _eventManager = eventManager;
            _monoBehaviorDelegator = monoBehaviorDelegator;
        }

        /// <inheritdoc/>
        public void LoadScene(string sceneName)
        {
            _monoBehaviorDelegator.DelegateStartCoroutine(StartLoadingSceneCoroutine(sceneName));
        }

        private IEnumerator StartLoadingSceneCoroutine(string sceneName)
        {
            yield return _loadingScreenService.Show();
            _debugLogger.LogInfo($"Started loading Scene: {sceneName}.", nameof(SceneLoaderService));
            yield return LoadSceneAsync(sceneName);
            _debugLogger.LogInfo($"Successfully loaded Scene: {sceneName}.", nameof(SceneLoaderService));
            yield return _loadingScreenService.Hide();
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;
            
            do
            {
                _loadingScreenService.SetLoadProgress(asyncOperation.progress);
                yield return null;
            }
            while (asyncOperation.progress < 0.9f);

            yield return _loadingScreenService.CompleteLoadProgress();
            asyncOperation.allowSceneActivation = true;
            
            while (!asyncOperation.isDone) yield return null;
            _eventManager.TriggerEvent(new SceneLoaderEvent.ActivatingNewScene());
        }
    }
}