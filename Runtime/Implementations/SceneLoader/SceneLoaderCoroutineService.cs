using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DJM.CoreServices.SceneLoader
{
    internal sealed class SceneLoaderCoroutineService : ISceneLoader
    {
        private readonly ILoadingScreenService _loadingScreenService;
        private readonly IDebugLogger _debugLogger;
        private readonly IPersistantEventManager _eventManager;
        

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneLoaderService"/> class.
        /// </summary>
        /// <param name="loadingScreenService">The service used for handling the loading screen.</param>
        /// <param name="debugLogger">The logger for debugging and error reporting.</param>
        /// <param name="eventManager">The persistant event manager for triggering loading-related events.</param>
        public SceneLoaderCoroutineService(ILoadingScreenService loadingScreenService, IDebugLogger debugLogger, IPersistantEventManager eventManager)
        {
            _loadingScreenService = loadingScreenService;
            _debugLogger = debugLogger;
            _eventManager = eventManager;
        }

        /// <inheritdoc/>
        public void LoadScene(string sceneName)
        {
            
        }

        /// <inheritdoc/>
        public void CancelLoadingScene()
        {
            
        }

        private IEnumerable LoadSceneCoroutine(string sceneName)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);

            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {

                if (asyncOperation.progress >= 0.9f)
                {
                    //await _loadingScreenService.CompleteLoadProgress();
                    _eventManager.TriggerEvent(new SceneLoaderEvent.ActivatingNewScene());
                    asyncOperation.allowSceneActivation = true;
                    break;
                }
                
                _loadingScreenService.SetLoadProgress(asyncOperation.progress);
                yield return null;
            }
        }
    }
}