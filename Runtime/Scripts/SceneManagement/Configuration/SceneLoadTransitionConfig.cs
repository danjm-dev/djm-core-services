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
        
        [Space]
        
        [SerializeField] public SceneTransitionCanvas sceneTransitionCanvasPrefab;
        
        [Header("Transition Timing")] [Space]
        
        [SerializeField] [ConditionalEnableProperty(nameof(fadeInCanvas))] [Min(0f)] public float fadeInDuration;
        [SerializeField] [Min(0f)] public float loadStartDelay;
        [SerializeField] [Min(0f)] public float minimumLoadDuration;
        [SerializeField] [Min(0f)] public float activateNewSceneDelay;
        [SerializeField] [Min(0f)] public float fadeOutStartDelay;
        [SerializeField] [ConditionalEnableProperty(nameof(fadeOutCanvas))] [Min(0f)] public float fadeOutDuration;
        
        [Header("Animation")] [Space]
        
        [SerializeField] [ConditionalEnableProperty(nameof(fadeInCanvas))] public Ease fadeInEase = Ease.InOutSine;
        [SerializeField] [ConditionalEnableProperty(nameof(fadeOutCanvas))] public Ease fadeOutEase = Ease.InOutSine;


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
        }
    }
}