using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace DJM.CoreUtilities.SceneManagement
{
    [System.Serializable]
    internal static class SceneTransitionConfig
    {
        [System.Serializable]
        internal sealed class FadePhase
        {
            [Min(0f)] public float delay;
            [Min(0f)] public float duration;
            public Ease ease = Ease.InOutSine;
        }
        
        [System.Serializable]
        internal sealed class LoadPhase
        {
            [Min(0f)] public float delay;
            [Min(0f)] public float minimumDuration;
        }
        
        [System.Serializable]
        internal sealed class ActivatePhase
        {
            [Min(0f)] public float delay;
        }
    }
}