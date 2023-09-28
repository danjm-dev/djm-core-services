using DJM.DependencyInjection;
using UnityEngine;

namespace DJM.CoreServices.ServiceLocator
{
    internal static class Bootstrapper
    {
        private static GameObject _persistantContext;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] // comment this out to prevent service locator initializing
        internal static void InitializeUtilities()
        {
            ResetStatics();

            _persistantContext = CreatePersistantContext();
            
            var gameObjectContext = _persistantContext.AddComponent<GameObjectContext>();

            var container = new DependencyContainer(gameObjectContext);
            container.Install(new CoreServiceInstaller());
            
            ServiceManager.Initialize(container);
        }

        private static void ResetStatics()
        {
            ServiceManager.Reset();
            if (_persistantContext == null) return;
            Object.Destroy(_persistantContext);
            _persistantContext = null;
        }

        private static GameObject CreatePersistantContext()
        {
            var contextGameObject = new GameObject($"[DJMPersistantContext]") { isStatic = true };
            Object.DontDestroyOnLoad(contextGameObject);
            return contextGameObject;
        }
    }
}