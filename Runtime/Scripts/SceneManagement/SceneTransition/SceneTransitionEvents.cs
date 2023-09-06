using UnityEngine.Events;

namespace DJM.CoreUtilities.SceneManagement
{
    internal static class SceneTransitionEvents
    {
        [System.Serializable]
        internal sealed class FadePhase
        {
            public UnityEvent onFadeStart;
            public UnityEvent onFadeEnd;
        }
        
        [System.Serializable]
        internal sealed class LoadPhase
        {
            public UnityEvent onLoadStart;
            public UnityEvent<float> onLoadProgress;
            public UnityEvent onLoadEnd;
        }
        
        [System.Serializable]
        internal sealed class ActivatePhase
        {
            public UnityEvent onActivate;
        }
    }
}