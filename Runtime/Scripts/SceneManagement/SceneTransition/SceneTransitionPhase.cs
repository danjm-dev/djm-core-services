using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace DJM.CoreUtilities
{
    [System.Serializable]
    public static class SceneTransitionPhase
    {
        // fade phase
        
        [System.Serializable]
        public sealed class FadePhaseConfig
        {
            [Min(0f)] public float delay;
            [Min(0f)] public float duration;
            public Ease ease = Ease.InOutSine;
        }
        
        [System.Serializable]
        public sealed class FadePhaseEvents
        {
            public UnityEvent onFadeStart;
            public UnityEvent onFadeEnd;
        }
    
        // load phase
        
        [System.Serializable]
        public sealed class LoadPhaseConfig
        {
            [Min(0f)] public float delay;
            [Min(0f)] public float minimumDuration;
        }
        
        [System.Serializable]
        public sealed class LoadPhaseEvents
        {
            public UnityEvent onLoadStart;
            public UnityEvent<float> onLoadProgress;
            public UnityEvent onLoadEnd;
        }
    
        // activate phase
        
        [System.Serializable]
        public sealed class ActivatePhaseConfig
        {
            [Min(0f)] public float delay;
        }
        
        [System.Serializable]
        public sealed class ActivatePhaseEvents
        {
            public UnityEvent onActivate;
        }
    }
}