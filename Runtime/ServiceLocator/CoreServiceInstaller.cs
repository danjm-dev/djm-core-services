using DJM.CoreServices.ApplicationController;
using DJM.CoreServices.AudioSourcePool;
using DJM.CoreServices.DebugLogger;
using DJM.CoreServices.LoadingScreen;
using DJM.CoreServices.Music;
using DJM.CoreServices.PersistantEventManager;
using DJM.CoreServices.SceneLoader;
using DJM.CoreServices.ScopedEventManager;
using DJM.CoreServices.TransientSound;
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
            
            container.Bind<IDebugLogger>().To<DebugLoggerService>().FromNew().AsSingle();
            
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