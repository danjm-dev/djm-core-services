using System;

namespace DJM.CoreUtilities.Services.SceneLoader
{
    internal sealed class SceneLoaderEventHandler : IDisposable
    {
        private readonly IEventManager _eventManager;
        private readonly ISceneLoader _sceneLoader;
        
        internal SceneLoaderEventHandler(IEventManager eventManager, ISceneLoader sceneLoader)
        {
            _eventManager = eventManager;
            _sceneLoader = sceneLoader;
            
            _eventManager.Subscribe<SceneLoaderEvent.LoadScene>(OnLoadScene);
            _eventManager.Subscribe<SceneLoaderEvent.CancelLoadingScene>(OnCancelLoadingScene);
        }

        public void Dispose()
        {
            _eventManager.Unsubscribe<SceneLoaderEvent.LoadScene>(OnLoadScene);
            _eventManager.Unsubscribe<SceneLoaderEvent.CancelLoadingScene>(OnCancelLoadingScene);
        }
        
        private void OnLoadScene(SceneLoaderEvent.LoadScene eventData)
        {
            _sceneLoader.LoadScene(eventData.SceneName);
        }

        private void OnCancelLoadingScene(SceneLoaderEvent.CancelLoadingScene eventData)
        {
            _sceneLoader.CancelLoadingScene();
            _sceneLoader.CancelLoadingScene();
        }
    }
}