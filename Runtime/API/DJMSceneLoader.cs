using DJM.CoreUtilities.Bootstrap;
using DJM.CoreUtilities.Services.Events;
using DJM.CoreUtilities.Services.SceneLoader;

namespace DJM.CoreUtilities.API
{
    public static class DJMSceneLoader
    {
        private static readonly IEventManager EventManager = DJMServiceContext.Instance.EventManager;

        public static void Load(string sceneName) =>
            EventManager.TriggerEvent(new SceneLoaderEvent.LoadScene(sceneName));
        
        public static void Cancel(string sceneName) =>
            EventManager.TriggerEvent(new SceneLoaderEvent.CancelLoadingScene());
    }
}