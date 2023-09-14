using DJM.CoreUtilities.ServiceContext;
using DJM.CoreUtilities.Services.Events;
using DJM.CoreUtilities.Services.SceneLoader;

namespace DJM.CoreUtilities.API
{
    public static class DJMSceneLoader
    {
        private static readonly IEventManagerService EventManagerService = DJMServiceContext.Instance.EventManagerService;

        public static void Load(string sceneName) =>
            EventManagerService.TriggerEvent(new SceneLoaderEvent.LoadScene(sceneName));
        
        public static void Cancel(string sceneName) =>
            EventManagerService.TriggerEvent(new SceneLoaderEvent.CancelLoadingScene());
    }
}