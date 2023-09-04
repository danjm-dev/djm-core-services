using UnityEngine;

namespace DJM.CoreUtilities
{
    public class PersistantSingletonComponent<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        protected virtual void Awake() => InitializeInstance();

        protected virtual void InitializeInstance()
        {
            if (!Application.isPlaying) return;
            
            if (Instance is null)
            {
                Instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);
        }
    }
}