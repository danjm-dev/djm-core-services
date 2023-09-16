using DJM.CoreServices.DependencyInjection;
using DJM.CoreServices.DependencyInjection.ComponentContext;
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

            var globalContainer = new DependencyContainer(CreateGlobalGameObjectContext());
            globalContainer.Install(new GlobalServiceInstaller());
            
            // temp
            globalContainer.Resolve<MusicControllerEventHandler>().Initialize();
            globalContainer.Resolve<SoundControllerEventHandler>().Initialize();
            globalContainer.Resolve<LoggerEventHandler>().Initialize();
            globalContainer.Resolve<SceneLoaderEventHandler>().Initialize();
            
            DJMGlobalService.Initialize(globalContainer);
        }
        
        private static void ResetEnvironment()
        {
            
        }

        private static GameObjectContext CreateGlobalGameObjectContext()
        {
            var contextGameObject = new GameObject($"[{nameof(DJMGlobalService)}]") { isStatic = true };
            Object.DontDestroyOnLoad(contextGameObject);
            return contextGameObject.AddComponent<GameObjectContext>();
        }
    }
}