using System.Collections;
using DJM.CoreUtilities.UIAnimators;
using UnityEngine;

namespace DJM.CoreUtilities.SceneManagement
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroupFader))]
    public sealed class SceneTransitionCanvas : MonoBehaviour
    {
        private CanvasGroupFader _canvasGroupFader;

        [SerializeField] private SceneTransitionCanvasEvents events;
        
        private void Awake()
        {
            var canvas = GetComponent<Canvas>();
            _canvasGroupFader = GetComponent<CanvasGroupFader>();
            
            canvas.sortingOrder = short.MaxValue;
            _canvasGroupFader.SetAlpha(0f);
        }

        private void Start() => events.EnableEventListeners();
        private void OnDestroy() => events.DisableEventListeners();

        internal IEnumerator ShowCoroutine(SceneTransitionConfig.FadeTransitionConfig config)
        {
            yield return _canvasGroupFader.FadeCanvasGroupAlphaCoroutine(1f, config.duration, config.ease);
        }
        
        internal IEnumerator HideCoroutine(SceneTransitionConfig.FadeTransitionConfig config)
        {
            yield return _canvasGroupFader.FadeCanvasGroupAlphaCoroutine(0f, config.duration, config.ease);
        }
    }
}