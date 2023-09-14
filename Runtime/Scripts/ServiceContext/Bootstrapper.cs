using DJM.CoreUtilities.MonoBehaviors.Audio;
using DJM.CoreUtilities.Services.Events;
using DJM.CoreUtilities.Services.Logger;
using DJM.CoreUtilities.Services.Music;
using DJM.CoreUtilities.Services.SceneLoader;
using DJM.CoreUtilities.Services.Sound;
using UnityEngine;

namespace DJM.CoreUtilities.ServiceContext
{
    internal static class Bootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        internal static void InitializeUtilities()
        {
            ResetEnvironment();
            
            ILoggerService loggerService = new LoggerService(true, LogLevel.Info);
            IEventManagerService eventManagerService = new EventManagerService(loggerService);
            
            
            // service context
            var serviceContext = DJMServiceContext.Initialize(eventManagerService, loggerService);
            
            // monoBehavior services
            var audioSourcePool = serviceContext.GetMonoBehaviorService<AudioSourcePool>();
            
            // services
            
            IMusicService musicService = new MusicService(audioSourcePool, loggerService);
            ISoundService soundService = new SoundService(audioSourcePool, loggerService);
            ISceneLoaderService sceneLoaderService = new SceneLoaderService(loggerService);
            
            // event handlers
            // TODO - DEAL WITH DISPOSABLE
            var logEventHandler = new LogEventHandler(eventManagerService, loggerService);
            var musicEventHandler = new MusicEventHandler(eventManagerService, musicService);
            var soundEventHandler = new SoundEventHandler(eventManagerService, soundService);
            var sceneLoaderEventHandler = new SceneLoaderEventHandler(eventManagerService, sceneLoaderService);




        }
        
        private static void ResetEnvironment()
        {
            
        }
    }
}