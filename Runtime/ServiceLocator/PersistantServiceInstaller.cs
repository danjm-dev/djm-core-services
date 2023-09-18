using DJM.CoreServices.MonoServices.AudioSource;
using DJM.CoreServices.MonoServices.LoadingScreen;
using DJM.CoreServices.Services.ApplicationController;
using DJM.CoreServices.Services.DebugLogger;
using DJM.CoreServices.Services.Music;
using DJM.CoreServices.Services.SceneEventManager;
using DJM.CoreServices.Services.SceneLoader;
using DJM.CoreServices.Services.TransientSound;
using DJM.DependencyInjection;
using DJM.EventManager;

namespace DJM.CoreServices.ServiceLocator
{
    internal sealed class PersistantServiceInstaller : IInstaller
    {
        public void InstallBindings(IBindableContainer container)
        {
            // services
            container.Bind<IEventManager>().To<EventManagerService>().FromNew().AsSingle();

            container.Bind<ISceneEventManager>().To<SceneEventManagerService>().FromNew().AsSingle();
            container.Bind<IDebugLogger>().To<DebugLogger>().FromNew().AsSingle();
            container.Bind<IMusicService>().To<MusicService>().FromNew().AsSingle();
            container.Bind<ITransientSoundService>().To<TransientSoundService>().FromNew().AsSingle();
            container.Bind<ISceneLoader>().To<SceneLoaderService>().FromNew().AsSingle();
            container.Bind<IApplicationController>().To<ApplicationControllerService>().FromNew().AsSingle();

            // mono services
            container.Bind<AudioSourcePool>().FromNewComponentOnNewGameObject().AsSingle();
            container.Bind<LoadingScreenService>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}