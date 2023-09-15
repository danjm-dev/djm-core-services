using System.Collections.Generic;
using DJM.CoreServices.MonoServices.AudioSource;
using DJM.CoreServices.Services.Events;
using DJM.CoreServices.Services.Logger;
using DJM.CoreServices.Services.MusicController;
using DJM.CoreServices.Services.SceneLoader;
using DJM.CoreServices.Services.SoundController;
using UnityEngine;

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
            
            // mono services
            var audioSourcePool = serviceContext.GetMonoBehaviorService<AudioSourcePool>();
            
            // services
            
            IMusicController musicController = new MusicControllerService(audioSourcePool, logger);
            ISoundController soundController = new SoundControllerService(audioSourcePool, logger);
            ISceneLoader sceneLoader = new SceneLoaderService(logger);
            
            // event handlers
            var eventHandlers = new List<IEventHandler>
            {
                new LoggerEventHandler(eventManager, logger),
                new MusicControllerEventHandler(eventManager, musicController),
                new SoundControllerEventHandler(eventManager, soundController),
                new SceneLoaderEventHandler(eventManager, sceneLoader)
            };
            foreach (var eventHandler in eventHandlers) eventHandler.Initialize(); // temp - will eventually plug into context lifecycle
            // need to dispose too

        }
        
        private static void ResetEnvironment()
        {
            
        }
    }
}