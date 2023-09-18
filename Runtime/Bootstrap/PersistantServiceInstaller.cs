using DJM.CoreServices.MonoServices.AudioSource;
using DJM.CoreServices.Services.Logger;
using DJM.CoreServices.Services.MusicController;
using DJM.CoreServices.Services.SceneLoader;
using DJM.CoreServices.Services.TransientSoundController;
using DJM.DependencyInjection;
using DJM.EventManager;

namespace DJM.CoreServices.Bootstrap
{
    internal sealed class PersistantServiceInstaller : IInstaller
    {
        public void InstallBindings(IBindableContainer container)
        {
            // services
            container.Bind<ILoggerService>().To<LoggerService>().FromNew().AsSingle();
            container.Bind<IEventManager>().To<EventManagerService>().FromNew().AsSingle();
            container.Bind<IMusicController>().To<MusicControllerService>().FromNew().AsSingle();
            container.Bind<ITransientSoundController>().To<TransientSoundService>().FromNew().AsSingle();
            container.Bind<ISceneLoader>().To<SceneLoaderService>().FromNew().AsSingle();

            // mono services
            container.Bind<AudioSourcePool>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}