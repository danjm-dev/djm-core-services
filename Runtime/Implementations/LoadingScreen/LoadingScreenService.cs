using System;
using System.Threading.Tasks;
using DG.Tweening;
using DJM.DependencyInjection;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace DJM.CoreServices.LoadingScreen
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
        private GameObject _loadingScreen;
        
        private LoadingScreenConfig _loadingScreenConfig;
        
        private Tween _progressTween;
        private float _loadProgress;
        private float _loadProgressTarget;

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
            _loadingScreenConfig = Resources.LoadAll<LoadingScreenConfig>("Configuration")[0];

            if (_loadingScreenConfig is null)
            {
                _debugLogger.LogWarning
                (
                    $"No {nameof(LoadingScreenConfig)} found in Resources/Configuration. Using default loading screen.",
                    nameof(LoadingScreenService)
                );

                _loadingScreenConfig = ScriptableObject.CreateInstance<LoadingScreenConfig>();
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
            _progressTween?.Kill();
            DOTween.Complete(_canvasGroup);
            DOTween.Kill(_canvasGroup);
        }

        /// <inheritdoc/>
        public async Task Show()
        {
            gameObject.SetActive(true);
            
            if (_loadingScreenConfig.fadeInDuration <= 0f) _canvasGroup.alpha = 1f;
            
            else await _canvasGroup
                .DOFade(1f, _loadingScreenConfig.fadeInDuration)
                .SetEase(_loadingScreenConfig.fadeInEase)
                .AsyncWaitForCompletion();
        }
        
        /// <inheritdoc/>
        public async Task Hide()
        {
            if (_loadingScreenConfig.fadeOutDuration <= 0f) _canvasGroup.alpha = 0f;
            
            else await _canvasGroup
                .DOFade(0f, _loadingScreenConfig.fadeOutDuration)
                .SetEase(_loadingScreenConfig.fadeOutEase)
                .AsyncWaitForCompletion();
            
            gameObject.SetActive(false);
        }

        /// <inheritdoc/>
        public void SetLoadProgress(float progress)
        {
            if(_loadProgressTarget >= 1f) return;
            _loadProgressTarget = Mathf.Clamp01(progress);
            if (_progressTween is null || !_progressTween.IsPlaying()) StartProgressTween();
        }

        private void StartProgressTween()
        {
            _progressTween?.Kill();

            if (Mathf.Approximately(_loadProgress, _loadProgressTarget))
            {
                _progressTween = null;
                return;
            }

            var duration = _loadingScreenConfig.minimumLoadDuration * Mathf.Abs(_loadProgressTarget - _loadProgress);
            _progressTween = DOTween
                .To(()=> _loadProgress, x=> _loadProgress = x, _loadProgressTarget, duration)
                .OnComplete(StartProgressTween);
        }

        /// <inheritdoc/>
        public async Task CompleteLoadProgress()
        {
            _loadProgressTarget = 1f;
            StartProgressTween();

            if (_progressTween is not null && _progressTween.IsPlaying()) await _progressTween.AsyncWaitForCompletion();
            await Task.Delay(TimeSpan.FromSeconds(_loadingScreenConfig.loadCompleteDelay));
        }
        
        public async Task CancelLoadProgress()
        {
            _progressTween?.Kill();
            
            // placeholder await - may want to do something here
            await Task.Delay(TimeSpan.FromSeconds(_loadingScreenConfig.loadCompleteDelay));
        }
    }
}