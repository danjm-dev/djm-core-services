using DJM.CoreServices.Bootstrap;
using DJM.CoreServices.Services.SceneLoader;
using DJM.EventManager;

namespace DJM.CoreServices.API
{
    public static class DJMSceneLoader
    {
        private static readonly IEventManager EventManager = DJMGlobalService.Instance.ResolvableContainer.Resolve<IEventManager>();

        public static void Load(string sceneName) =>
            EventManager.TriggerEvent(new SceneLoaderEvent.LoadScene(sceneName));
        
        public static void Cancel(string sceneName) =>
            EventManager.TriggerEvent(new SceneLoaderEvent.CancelLoadingScene());
    }
}