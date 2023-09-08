using UnityEngine;

namespace DJM.CoreUtilities.Configuration
{
    internal static class DJMBootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        internal static void InitializeUtilities()
        {
            ResetStaticUtilities();
            
            CreateGameObject<DJMPersistentComponentContext>();
            
        }

        private static void CreateGameObject<T>(string name = null) where T : MonoBehaviour
        {
            var obj = new GameObject(name ?? typeof(T).Name);
            obj.AddComponent<T>();
        }

        private static void ResetStaticUtilities()
        {
            
        }
    }
}