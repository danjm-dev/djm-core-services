using UnityEngine;

namespace DJM.CoreUtilities
{
    [CreateAssetMenu(fileName = "SceneTransitionConfig", menuName = "ScriptableObject/SceneManagement/SceneTransitionConfig")]
    public sealed class SceneTransitionConfig : ScriptableObject
    {
        [SerializeField] public SceneTransitionCanvas transitionCanvasPrefab;
        
        [Space] [SerializeField] public SceneTransitionPhase.FadePhaseConfig fadeInPhaseConfig;
        [Space] [SerializeField] public SceneTransitionPhase.LoadPhaseConfig loadPhaseConfig;
        [Space] [SerializeField] public SceneTransitionPhase.ActivatePhaseConfig activatePhaseConfig;
        [Space] [SerializeField] public SceneTransitionPhase.FadePhaseConfig fadeOutPhaseConfig;
    }
}