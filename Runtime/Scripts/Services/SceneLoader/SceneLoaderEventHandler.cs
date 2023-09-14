using System;
using DJM.CoreUtilities.Services.Events;

namespace DJM.CoreUtilities.Services.SceneLoader
{
    internal sealed class SceneLoaderEventHandler : IDisposable
    {
        private readonly IEventManagerService _eventManagerService;
        private readonly ISceneLoaderService _sceneLoaderService;
        
        internal SceneLoaderEventHandler(IEventManagerService eventManagerService, ISceneLoaderService sceneLoaderService)
        {
            _eventManagerService = eventManagerService;
            _sceneLoaderService = sceneLoaderService;
            
            _eventManagerService.Subscribe<SceneLoaderEvent.LoadScene>(OnLoadScene);
            _eventManagerService.Subscribe<SceneLoaderEvent.CancelLoadingScene>(OnCancelLoadingScene);
        }

        public void Dispose()
        {
            _eventManagerService.Unsubscribe<SceneLoaderEvent.LoadScene>(OnLoadScene);
            _eventManagerService.Unsubscribe<SceneLoaderEvent.CancelLoadingScene>(OnCancelLoadingScene);
        }
        
        private void OnLoadScene(SceneLoaderEvent.LoadScene eventData)
        {
            _sceneLoaderService.LoadScene(eventData.SceneName);
        }

        private void OnCancelLoadingScene(SceneLoaderEvent.CancelLoadingScene eventData)
        {
            _sceneLoaderService.CancelLoadingScene();
            _sceneLoaderService.CancelLoadingScene();
        }
    }
}