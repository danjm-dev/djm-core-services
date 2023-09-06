using UnityEngine;
using UnityEngine.Events;

namespace DJM.CoreUtilities
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroupFader))]
    public sealed class SceneTransitionCanvas : MonoBehaviour
    {
        public CanvasGroupFader CanvasGroupFader { get; private set; }
        
        public UnityEvent onStart;
        public UnityEvent onEnd;

        public SceneTransitionPhase.FadePhaseEvents fadeInPhaseEvents;
        public SceneTransitionPhase.LoadPhaseEvents loadPhaseEvents;
        public SceneTransitionPhase.ActivatePhaseEvents activatePhaseEvents;
        public SceneTransitionPhase.FadePhaseEvents fadeOutPhaseEvents;
        
        
        private void Awake()
        {
            var canvas = GetComponent<Canvas>();
            CanvasGroupFader = GetComponent<CanvasGroupFader>();
            
            canvas.sortingOrder = short.MaxValue;
            CanvasGroupFader.SetCanvasGroupAlpha(0f);
        }
    }
}