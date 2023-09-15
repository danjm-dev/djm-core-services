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
            
            var serviceContext = DJMServiceContext.Initialize();
            
            serviceContext.DependencyContainer.Install(new ServiceContextInstaller());


            // temp
            serviceContext.DependencyContainer.Resolve<MusicControllerEventHandler>().Initialize();
            serviceContext.DependencyContainer.Resolve<SoundControllerEventHandler>().Initialize();
            serviceContext.DependencyContainer.Resolve<LoggerEventHandler>().Initialize();
            serviceContext.DependencyContainer.Resolve<SceneLoaderEventHandler>().Initialize();
        }
        
        private static void ResetEnvironment()
        {
            
        }
    }
}