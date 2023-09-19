using DG.Tweening;
using UnityEngine;

namespace DJM.CoreServices.LoadingScreen
{
    [CreateAssetMenu(fileName = "LoadingScreenConfig", menuName = "DJM Core Services/Loading Screen/LoadingScreenConfig")]
    public class LoadingScreenConfig : ScriptableObject
    {
        [SerializeField] public Color backgroundColor = Color.black;
        [SerializeField] public CustomLoadingScreen customLoadingScreenPrefab;
        
        [Header("Fade In")]
        [Min(0f)] public float fadeInDuration = 0.5f;
        public Ease fadeInEase = Ease.InOutSine;
        
        [Header("Load Progress")]
        [Min(0f)] public float minimumLoadDuration = 0.2f;
        [Min(0f)] public float loadCompleteDelay = 0f;
        
        [Header("Fade Out")]
        [Min(0f)] public float fadeOutDuration = 0.5f;
        public Ease fadeOutEase = Ease.InOutSine;
    }
}
