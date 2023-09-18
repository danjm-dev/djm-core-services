using DG.Tweening;
using UnityEngine;

namespace DJM.CoreServices.MonoServices.LoadingScreen
{
    [CreateAssetMenu(fileName = "LoadingScreenConfig", menuName = "DJM/CoreServices/LoadingScreen/LoadingScreenConfig")]
    public sealed class LoadingScreenConfig : ScriptableObject
    {
        [SerializeField] public Color backgroundColor = Color.black;
        
        [Space] [SerializeField] internal FadeConfig fadeIn;
        [Space] [SerializeField] internal LoadingConfig loading;
        [Space] [SerializeField] internal FadeConfig fadeOut;
        
        [System.Serializable]
        internal sealed class FadeConfig
        {
            [Min(0f)] public float delay = 0f;
            [Min(0f)] public float duration = 0.5f;
            public Ease ease = Ease.InOutSine;
        }
        
        [System.Serializable]
        internal sealed class LoadingConfig
        {
            [Min(0f)] public float loadStartDelay = 0f;
            [Min(0f)] public float minimumLoadDuration = 0.2f;
            [Min(0f)] public float loadCompleteDelay = 0f;
        }

        internal static LoadingScreenConfig Default()
        {
            var defaultInstance = CreateInstance<LoadingScreenConfig>();
            defaultInstance.fadeIn = new FadeConfig();
            defaultInstance.loading = new LoadingConfig();
            defaultInstance.fadeOut = new FadeConfig();
            return defaultInstance;
        }
        
    }
}