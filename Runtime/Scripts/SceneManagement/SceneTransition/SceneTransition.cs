using UnityEngine;

namespace DJM.CoreUtilities.SceneManagement
{
    [CreateAssetMenu(fileName = "SceneTransition", menuName = "ScriptableObject/SceneManagement/SceneTransition")]
    public sealed class SceneTransition : ScriptableObject
    {
        [SerializeField] public SceneTransitionCanvas transitionCanvasPrefab;
        
        [Space] [SerializeField] internal SceneTransitionConfig.FadePhase fadeInPhase;
        [Space] [SerializeField] internal SceneTransitionConfig.LoadPhase loadPhase;
        [Space] [SerializeField] internal SceneTransitionConfig.ActivatePhase activatePhase;
        [Space] [SerializeField] internal SceneTransitionConfig.FadePhase fadeOutPhase;
    }
}