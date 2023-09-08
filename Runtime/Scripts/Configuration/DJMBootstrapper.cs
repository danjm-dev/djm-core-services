using DJM.CoreUtilities.Audio;
using UnityEngine;

namespace DJM.CoreUtilities.Configuration
{
    internal static class DJMBootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        internal static void InitializeUtilities()
        {
            // reset
            ResetStaticUtilities();

            // add context root component
            var djmContextRoot = CreateGameObject<DJMContextComponent>(null, "[DJMContext]");
            
            // add sub-components
            djmContextRoot.CreateComponentOnNewGameObjectAsChild<AudioManager>();
        }

        private static T CreateGameObject<T>(Transform parent = null, string name = null) where T : MonoBehaviour
        {
            var obj = new GameObject(name ?? typeof(T).Name);
            return obj.AddComponent<T>();
        }

        private static void ResetStaticUtilities()
        {
            
        }
    }
}