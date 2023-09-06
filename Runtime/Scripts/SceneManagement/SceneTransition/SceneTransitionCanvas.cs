using System;
using UnityEngine;
using UnityEngine.Events;

namespace DJM.CoreUtilities.SceneManagement
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroupFader))]
    public sealed class SceneTransitionCanvas : MonoBehaviour
    {
        public CanvasGroupFader CanvasGroupFader { get; private set; }
        
        [SerializeField] private UnityEvent onStart;
        [SerializeField] private UnityEvent onEnd;

        [SerializeField] private SceneTransitionEvents.FadePhase fadeInPhaseEvents;
        [SerializeField] private SceneTransitionEvents.LoadPhase loadPhaseEvents;
        [SerializeField] private SceneTransitionEvents.ActivatePhase activatePhaseEvents;
        [SerializeField] private SceneTransitionEvents.FadePhase fadeOutPhaseEvents;
        
        
        private void Awake()
        {
            var canvas = GetComponent<Canvas>();
            CanvasGroupFader = GetComponent<CanvasGroupFader>();
            
            canvas.sortingOrder = short.MaxValue;
            CanvasGroupFader.SetCanvasGroupAlpha(0f);
        }

        private void Start()
        {
            FadeInStartEvent.Subscribe(OnFadeInStartEvent);
            FadeInEndEvent.Subscribe(OnFadeInEndEvent);
            LoadProgressEvent.Subscribe(OnLoadProgressEvent);
        }
        
        private void OnFadeInStartEvent(FadeInStartEvent fadeInStartEvent)
        {
            fadeInPhaseEvents.onFadeStart?.Invoke();
        }
        private void OnFadeInEndEvent(FadeInEndEvent fadeInEndEvent)
        {
            fadeInPhaseEvents.onFadeEnd?.Invoke();
        }

        private void OnLoadProgressEvent(LoadProgressEvent loadProgressEvent)
        {
            loadPhaseEvents.onLoadProgress?.Invoke(loadProgressEvent.Progress);
        }

        private void OnDestroy()
        {
            FadeInStartEvent.Unsubscribe(OnFadeInStartEvent);
            FadeInEndEvent.Unsubscribe(OnFadeInEndEvent);
            LoadProgressEvent.Unsubscribe(OnLoadProgressEvent);
        }
    }
}