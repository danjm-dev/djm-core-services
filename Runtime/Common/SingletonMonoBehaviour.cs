using UnityEngine;

namespace DJM.CoreServices.Common
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        protected virtual void Awake() => InitializeInstance();
        protected virtual void OnDestroy()
        {
            if(Instance == this) Instance = null;
        }

        private void InitializeInstance()
        {
            if (!Application.isPlaying) return;
            if (Instance is null) Instance = this as T;
            else Destroy(gameObject);
        }
    }
}