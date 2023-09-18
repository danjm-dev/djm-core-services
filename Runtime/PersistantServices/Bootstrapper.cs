using DJM.DependencyInjection;
using UnityEngine;

namespace DJM.CoreServices.PersistantServices
{
    internal static class Bootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        internal static void InitializeUtilities()
        {
            ResetPersistantServices();

            var container = new DependencyContainer(CreatePersistantGameObjectContext());
            container.Install(new PersistantServiceInstaller());
            
            PersistantServiceManager.Initialize(container);
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