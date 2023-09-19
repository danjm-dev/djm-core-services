using DJM.DependencyInjection;
using UnityEngine;

namespace DJM.CoreServices.ServiceLocator
{
    internal static class Bootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] // comment this out to prevent service locator initializing
        internal static void InitializeUtilities()
        {
            ResetPersistantServices();

            var container = new DependencyContainer(CreatePersistantGameObjectContext());
            container.Install(new CoreServiceInstaller());
            
            ServiceManager.Initialize(container);
        }
        
        private static void ResetPersistantServices()
        {
            
        }

        private static GameObjectContext CreatePersistantGameObjectContext()
        {
            var contextGameObject = new GameObject($"[PersistantServiceContext]") { isStatic = true };
            Object.DontDestroyOnLoad(contextGameObject);
            return contextGameObject.AddComponent<GameObjectContext>();
        }
    }
}