using DJM.DependencyInjection;
using DJM.DependencyInjection.ComponentContext;
using UnityEngine;

namespace DJM.CoreServices.Bootstrap
{
    internal static class Bootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        internal static void InitializeUtilities()
        {
            ResetEnvironment();

            var globalContainer = new DependencyContainer(CreateGlobalGameObjectContext());
            globalContainer.Install(new GlobalServiceInstaller());
            
            DJMGlobalService.Initialize(globalContainer);
        }
        
        private static void ResetEnvironment()
        {
            
        }

        private static GameObjectContext CreateGlobalGameObjectContext()
        {
            var contextGameObject = new GameObject($"[{nameof(DJMGlobalService)}]") { isStatic = true };
            Object.DontDestroyOnLoad(contextGameObject);
            return contextGameObject.AddComponent<GameObjectContext>();
        }
    }
}