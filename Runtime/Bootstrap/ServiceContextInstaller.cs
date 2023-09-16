using DJM.CoreServices.DependencyInjection;
using DJM.CoreServices.MonoServices.AudioSource;
using DJM.CoreServices.Services.Events;
using DJM.CoreServices.Services.Logger;
using DJM.CoreServices.Services.MusicController;
using DJM.CoreServices.Services.SceneLoader;
using DJM.CoreServices.Services.SoundController;

namespace DJM.CoreServices.Bootstrap
{
    internal sealed class ServiceContextInstaller : IInstaller
    {
        public void InstallBindings(IContainer container)
        {
            // services
            container.Bind<ILogger>().To<LoggerService>().FromNew().AsSingle();
            container.Bind<IEventManager>().To<EventManagerService>().FromNew().AsSingle();
            
            container.Bind<IMusicController>().To<MusicControllerService>().FromNew().AsSingle();
            container.Bind<ISoundController>().To<SoundControllerService>().FromNew().AsSingle();
            container.Bind<ISceneLoader>().To<SceneLoaderService>().FromNew().AsSingle();

            // mono services
            container.Bind<AudioSourcePool>().FromNewComponentOnNewGameObject().AsSingle();
            
            // event handlers
            container.Bind<LoggerEventHandler>().FromNew().AsSingle();
            container.Bind<MusicControllerEventHandler>().FromNew().AsSingle();
            container.Bind<SoundControllerEventHandler>().FromNew().AsSingle();
            container.Bind<SceneLoaderEventHandler>().FromNew().AsSingle();
        }
    }
}