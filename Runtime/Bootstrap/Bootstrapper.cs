using DJM.CoreUtilities.MonoServices.AudioSource;
using DJM.CoreUtilities.Services.Events;
using DJM.CoreUtilities.Services.Logger;
using DJM.CoreUtilities.Services.MusicController;
using DJM.CoreUtilities.Services.SceneLoader;
using DJM.CoreUtilities.Services.SoundController;
using UnityEngine;

namespace DJM.CoreUtilities.Bootstrap
{
    internal static class Bootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        internal static void InitializeUtilities()
        {
            ResetEnvironment();
            
            ILogger logger = new LoggerService(true, LogLevel.Info);
            IEventManager eventManager = new EventManagerService(logger);
            
            
            // service context
            var serviceContext = DJMServiceContext.Initialize(eventManager, logger);
            
            // monoBehavior services
            var audioSourcePool = serviceContext.GetMonoBehaviorService<AudioSourcePool>();
            
            // services
            
            IMusicController musicController = new MusicControllerService(audioSourcePool, logger);
            ISoundController soundController = new SoundControllerService(audioSourcePool, logger);
            ISceneLoader sceneLoader = new SceneLoaderService(logger);
            
            // event handlers
            // TODO - DEAL WITH DISPOSABLE
            var logEventHandler = new LoggerEventHandler(eventManager, logger);
            var musicEventHandler = new MusicControllerEventHandler(eventManager, musicController);
            var soundEventHandler = new SoundControllerEventHandler(eventManager, soundController);
            var sceneLoaderEventHandler = new SceneLoaderEventHandler(eventManager, sceneLoader);
        }
        
        private static void ResetEnvironment()
        {
            
        }
    }
}