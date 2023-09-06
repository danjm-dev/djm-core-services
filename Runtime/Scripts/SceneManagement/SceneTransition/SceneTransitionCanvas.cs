using UnityEngine;
using UnityEngine.Events;

namespace DJM.CoreUtilities
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroupFader))]
    public sealed class SceneTransitionCanvas : MonoBehaviour
    {
        private ISceneLoaderEvents _sceneLoaderEvents;
        public CanvasGroupFader CanvasGroupFader { get; private set; }
        
        [SerializeField] private UnityEvent onStart;
        [SerializeField] private UnityEvent onEnd;

        [SerializeField] private SceneTransitionPhase.FadePhaseEvents fadeInPhaseEvents;
        [SerializeField] private SceneTransitionPhase.LoadPhaseEvents loadPhaseEvents;
        [SerializeField] private SceneTransitionPhase.ActivatePhaseEvents activatePhaseEvents;
        [SerializeField] private SceneTransitionPhase.FadePhaseEvents fadeOutPhaseEvents;
        
        
        private void Awake()
        {
            var canvas = GetComponent<Canvas>();
            CanvasGroupFader = GetComponent<CanvasGroupFader>();
            
            canvas.sortingOrder = short.MaxValue;
            CanvasGroupFader.SetCanvasGroupAlpha(0f);
        }

        public void RegisterSceneLoaderEvents(ISceneLoaderEvents sceneLoaderEvents)
        {
            _sceneLoaderEvents = sceneLoaderEvents;
            SubscribeToSceneLoaderEvents();
        }

        private void OnDestroy()
        {
            if(_sceneLoaderEvents is null) return;
            UnSubscribeFromSceneLoaderEvents();
        }


        private void SubscribeToSceneLoaderEvents()
        {
            _sceneLoaderEvents.Start += () => onStart?.Invoke();
        }
        
        private void UnSubscribeFromSceneLoaderEvents()
        {
            
        }
    }
}