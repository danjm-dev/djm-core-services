using UnityEngine;

namespace DJM.CoreUtilities.Common
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        protected virtual void Awake() => InitializeInstance();

        private void InitializeInstance()
        {
            if (!Application.isPlaying) return;
            if (Instance is null) Instance = this as T;
            else Destroy(gameObject);
        }
    }
}