using System;
using DJM.CoreServices.Services.SceneLoader;
using DJM.DependencyInjection;
using DJM.EventManager;

namespace DJM.CoreServices.Services.ScopedEventManager
{
    internal sealed class ScopedEventManagerService : EventManagerBase, IScopedEventManager, IInitializable, IDisposable
    {
        private readonly IEventManager _eventManager;

        public ScopedEventManagerService(IEventManager eventManager) => _eventManager = eventManager;
        
        public void Initialize()
        {
            _eventManager.Subscribe<SceneLoaderEvent.ActivatingNewScene>(OnSceneChange);
        }
        
        public void Dispose()
        {
            _eventManager.Unsubscribe<SceneLoaderEvent.ActivatingNewScene>(OnSceneChange);
        }

        private void OnSceneChange(SceneLoaderEvent.ActivatingNewScene eventData) => ClearAllEvents();
    }
}