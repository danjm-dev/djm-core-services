using System.Threading.Tasks;
using DG.Tweening;
using DJM.DependencyInjection;
using UnityEngine;
using UnityEngine.UI;

namespace DJM.CoreServices.MonoServices.LoadingScreen
{
    /// <summary>
    /// Manages loading screen components. If manually instantiating, ensure Construct method is called before use.
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroup))]
    internal sealed class LoadingScreenService : MonoBehaviour, ILoadingScreenService
    {
        private IDebugLogger _debugLogger;
        
        private CanvasGroup _canvasGroup;

        private LoadingScreenConfig _loadingScreenConfig;

        private GameObject _loadingScreen;

        /// <summary>
        /// Dependency injection via method, as <see cref="LoadingScreenService"/> can not have a constructor.
        /// If manually instantiated, this must be called before use to prevent exceptions.
        /// </summary>
        /// <param name="debugLogger">The debug logger for logging diagnostic information.</param>
        [Inject]
        public void Construct(IDebugLogger debugLogger) => _debugLogger = debugLogger;
        
        private void Awake()
        {
            var canvas = GetComponent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();

            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = short.MaxValue;
            
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        private void Start()
        {
            _loadingScreenConfig = Resources.Load<LoadingScreenConfig>("Configuration");

            if (_loadingScreenConfig is null)
            {
                _debugLogger.LogWarning
                (
                    $"No {nameof(LoadingScreenConfig)} found in Resources/Configuration. Using default loading screen.",
                    nameof(LoadingScreenService)
                );

                _loadingScreenConfig = LoadingScreenConfig.Default();
            }

            _loadingScreen = new GameObject("LoadingScreen")
            {
                transform = { parent = transform }
            };
            
            var fillImage = _loadingScreen.AddComponent<Image>();
            fillImage.color = _loadingScreenConfig.backgroundColor;
            fillImage.rectTransform.anchorMin = Vector2.zero;
            fillImage.rectTransform.anchorMax = Vector2.one;
            fillImage.rectTransform.offsetMin = Vector2.zero;
            fillImage.rectTransform.offsetMax = Vector2.zero;
            
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            DOTween.Complete(_canvasGroup);
            DOTween.Kill(_canvasGroup);
        }

        /// <inheritdoc/>
        public async Task Show()
        {
            gameObject.SetActive(true);
            
            if (_loadingScreenConfig.fadeIn.duration <= 0f) _canvasGroup.alpha = 1f;
            
            else await _canvasGroup
                .DOFade(1f, _loadingScreenConfig.fadeIn.duration)
                .SetEase(_loadingScreenConfig.fadeIn.ease)
                .AsyncWaitForCompletion();
        }
        
        /// <inheritdoc/>
        public async Task Hide()
        {
            if (_loadingScreenConfig.fadeOut.duration <= 0f) _canvasGroup.alpha = 0f;
            
            else await _canvasGroup
                .DOFade(0f, _loadingScreenConfig.fadeOut.duration)
                .SetEase(_loadingScreenConfig.fadeOut.ease)
                .AsyncWaitForCompletion();
            
            gameObject.SetActive(false);
        }
    }
}