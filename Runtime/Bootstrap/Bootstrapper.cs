using DJM.CoreServices.Interfaces;
using DJM.CoreServices.MonoServices.AudioSource;
using DJM.CoreServices.Services.Events;
using DJM.CoreServices.Services.Logger;
using DJM.CoreServices.Services.MusicController;
using DJM.CoreServices.Services.SceneLoader;
using DJM.CoreServices.Services.SoundController;
using UnityEngine;
using ILogger = DJM.CoreServices.Interfaces.ILogger;

namespace DJM.CoreServices.Bootstrap
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