using System.Collections;
using DJM.CoreUtilities.Extensions;
using UnityEngine;

namespace DJM.CoreUtilities
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class SceneTransitionCanvas : MonoBehaviour
    {
        private Canvas _canvas;
        public CanvasGroup CanvasGroup { get; private set; }
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            CanvasGroup = GetComponent<CanvasGroup>();

            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            _canvas.sortingOrder = short.MaxValue;
            CanvasGroup.alpha = 0f;
        }
        
        public IEnumerator ShowCanvasCoroutine(SceneLoadTransitionConfig transitionConfig)
        {
            if (transitionConfig.fadeInCanvas)
            {
                yield return CanvasGroup.FadeInCanvasAsync
                (
                    transitionConfig.fadeInDuration,
                    transitionConfig.fadeInEase
                );
            }
            CanvasGroup.alpha = 1f;
        }
        
        public IEnumerator HideCanvasCoroutine(SceneLoadTransitionConfig transitionConfig)
        {
            if (transitionConfig.fadeOutCanvas)
            {
                yield return CanvasGroup.FadeOutCanvasAsync
                (
                    transitionConfig.fadeOutDuration,
                    transitionConfig.fadeOutEase
                );
            }
            CanvasGroup.alpha = 0f;
        }
    }
}