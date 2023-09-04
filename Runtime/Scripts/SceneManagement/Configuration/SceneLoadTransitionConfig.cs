using DG.Tweening;
using DJM.CoreUtilities.Attributes;
using UnityEngine;

namespace DJM.CoreUtilities
{
    [CreateAssetMenu(fileName = "SceneLoadTransitionConfig", menuName = "ScriptableObject/SceneManagement/SceneLoadTransitionConfig")]
    public sealed class SceneLoadTransitionConfig : ScriptableObject
    {
        [SerializeField] public bool fadeInCanvas;
        [SerializeField] public bool fadeOutCanvas;
        [SerializeField] public bool progressBar;
        
        [Space]
        
        [SerializeField] public GameObject transitionCanvasPrefab;
        
        [Header("Transition Timing")] [Space]
        
        [SerializeField] [ConditionalShowProperty(nameof(fadeInCanvas))] [Min(0f)] public float fadeInDuration;
        [SerializeField] [ConditionalShowProperty(nameof(progressBar))] [Min(0f)] public float progressBarMinLoadDuration;
        [SerializeField] [Min(0f)] public float loadCompleteDelayDuration;
        [SerializeField] [ConditionalShowProperty(nameof(fadeOutCanvas))] [Min(0f)] public float fadeOutDuration;
        
        [Header("Animation")] [Space]
        
        [SerializeField] [ConditionalShowProperty(nameof(fadeInCanvas))] public Ease fadeInEase = Ease.InOutSine;
        [SerializeField] [ConditionalShowProperty(nameof(fadeOutCanvas))] public Ease fadeOutEase = Ease.InOutSine;


        private void OnValidate()
        {
            if (!fadeInCanvas)
            {
                fadeInDuration = 0f;
                fadeInEase = Ease.InOutSine;
            }

            if (!fadeOutCanvas)
            {
                fadeOutDuration = 0f;
                fadeOutEase = Ease.InOutSine;
            }

            if (!progressBar)
            {
                progressBarMinLoadDuration = 0f;
            }
        }
    }
}