using DJM.CoreServices.MonoServices.AudioSourcePool;
using DJM.CoreServices.MonoServices.LoadingScreen;
using DJM.CoreServices.Services.ApplicationController;
using DJM.CoreServices.Services.DebugLogger;
using DJM.CoreServices.Services.Music;
using DJM.CoreServices.Services.PersistantEventManager;
using DJM.CoreServices.Services.SceneLoader;
using DJM.CoreServices.Services.ScopedEventManager;
using DJM.CoreServices.Services.TransientSound;
using DJM.DependencyInjection;

namespace DJM.CoreServices.ServiceLocator
{
    internal sealed class CoreServiceInstaller : IInstaller
    {
        public void InstallBindings(IBindableContainer container)
        {
            // services
            container.Bind<IPersistantEventManager>().To<PersistantEventManagerService>().FromNew().AsSingle();
            container.Bind<IScopedEventManager>().To<ScopedEventManagerService>().FromNew().AsSingle();
            
            container.Bind<IDebugLogger>().To<DebugLogger>().FromNew().AsSingle();
            
            container.Bind<IMusicService>().To<MusicService>().FromNew().AsSingle();
            container.Bind<ITransientSoundService>().To<TransientSoundService>().FromNew().AsSingle();
            
            container.Bind<ISceneLoader>().To<SceneLoaderService>().FromNew().AsSingle();
            
            container.Bind<IApplicationController>().To<ApplicationControllerService>().FromNew().AsSingle();

            // mono services
            container.Bind<IAudioSourcePool>().To<AudioSourcePoolService>().FromNewComponentOnNewGameObject().AsSingle();
            container.Bind<ILoadingScreenService>().To<LoadingScreenService>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}