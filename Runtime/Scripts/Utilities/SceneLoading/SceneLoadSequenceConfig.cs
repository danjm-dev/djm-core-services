using DG.Tweening;
using DJM.CoreUtilities.Components;
using UnityEngine;

namespace DJM.CoreUtilities.SceneLoading
{
    [CreateAssetMenu(fileName = "SceneLoadSequenceConfig", menuName = "ScriptableObject/SceneManagement/SceneLoadSequenceConfig")]
    public sealed class SceneLoadSequenceConfig : ScriptableObject
    {
        [SerializeField] public SceneTransitionCanvas transitionCanvasPrefab;
        
        [Space] [SerializeField] internal FadeTransitionConfig fadeIn;
        [Space] [SerializeField] internal SceneLoadingConfig loadNewScene;
        [Space] [SerializeField] internal FadeTransitionConfig fadeOut;
        
        [System.Serializable]
        internal sealed class FadeTransitionConfig
        {
            [Min(0f)] public float delay;
            [Min(0f)] public float duration;
            public Ease ease = Ease.InOutSine;
        }
        
        [System.Serializable]
        internal sealed class SceneLoadingConfig
        {
            [Min(0f)] public float loadStartDelay;
            [Min(0f)] public float minimumLoadDuration;
            [Min(0f)] public float loadCompleteSceneActivationDelay;
        }
    }
    

}