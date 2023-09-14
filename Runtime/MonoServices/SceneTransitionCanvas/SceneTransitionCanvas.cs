using System.Collections;
using DJM.CoreServices.Services.SceneLoader;
using DJM.CoreServices.Temp.Components.UI;
using UnityEngine;

namespace DJM.CoreServices.MonoServices.SceneTransitionCanvas
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
        private void OnDestroy()
        {
            events.DisableEventListeners();
        }

        internal IEnumerator ShowCoroutine(SceneLoadSequenceConfig.FadeTransitionConfig config)
        {
            yield return _canvasGroupFader.FadeCanvasGroupAlphaCoroutine(1f, config.duration, config.ease);
        }
        
        internal IEnumerator HideCoroutine(SceneLoadSequenceConfig.FadeTransitionConfig config)
        {
            yield return _canvasGroupFader.FadeCanvasGroupAlphaCoroutine(0f, config.duration, config.ease);
        }
    }
}