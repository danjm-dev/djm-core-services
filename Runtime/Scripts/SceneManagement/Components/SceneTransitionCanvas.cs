using UnityEngine;
using UnityEngine.Events;

namespace DJM.CoreUtilities
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroupFader))]
    public sealed class SceneTransitionCanvas : MonoBehaviour
    {
        public CanvasGroupFader CanvasGroupFader { get; private set; }

        [Header("Transition Events")]
        
        public UnityEvent onFadeInStart;
        public UnityEvent onFadeInEnd;
        
        public UnityEvent onLoadStart;
        public UnityEvent<float> onSetLoadProgress;
        public UnityEvent onLoadEnd;
        
        public UnityEvent onNewSceneActivation;
        
        public UnityEvent onFadeOutStart;
        public UnityEvent onFadeOutEnd;
        
        private void Awake()
        {
            var canvas = GetComponent<Canvas>();
            CanvasGroupFader = GetComponent<CanvasGroupFader>();

            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = short.MaxValue;
            CanvasGroupFader.SetCanvasGroupAlpha(0f);
        }
    }
}