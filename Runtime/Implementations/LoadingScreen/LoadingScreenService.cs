using System;
using System.Threading.Tasks;
using DG.Tweening;
using DJM.DependencyInjection;
using UnityEngine;

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
        private LoadingScreenConfig _loadingScreenConfig;
        
        private CanvasGroup _canvasGroup;

        private CustomLoadingScreen _customLoadingScreen;
        private LoadingScreenBackground _background;
        
        private float _loadProgress;
        private float _loadProgressTarget;

        private Tween _loadProgressTween;

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
            var loadingScreenConfigs = Resources.LoadAll<LoadingScreenConfig>("Configuration");
            
            _debugLogger.LogInfo
            (
                $"Found {loadingScreenConfigs.Length} instances of {nameof(LoadingScreenConfig)} in Resources/Configuration.",
                nameof(LoadingScreenService)
            );
            
            _loadingScreenConfig = loadingScreenConfigs.Length > 0 
                ? loadingScreenConfigs[0] 
                : ScriptableObject.CreateInstance<LoadingScreenConfig>();

            _background = LoadingScreenBackground.Create(transform);
            _background.SetColor(_loadingScreenConfig.backgroundColor);

            if (_loadingScreenConfig.customLoadingScreenPrefab is not null)
            {
                _customLoadingScreen = Instantiate(_loadingScreenConfig.customLoadingScreenPrefab, transform);
            }
            
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _loadProgressTarget = 0f;
            _loadProgress = 0f;
            if(_customLoadingScreen is null) return;
            _customLoadingScreen.SetLoadProgress(0f);
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
            
            if (_loadingScreenConfig.fadeInDuration <= 0f) _canvasGroup.alpha = 1f;
            
            else await _canvasGroup
                .DOFade(1f, _loadingScreenConfig.fadeInDuration)
                .SetEase(_loadingScreenConfig.fadeInEase)
                .SetUpdate(true)
                .AsyncWaitForCompletion();
            
            if(_customLoadingScreen is not null) 
                _customLoadingScreen.onSetupDelayStart?.Invoke(_loadingScreenConfig.loadStartDelay);
            await Task.Delay(TimeSpan.FromSeconds(_loadingScreenConfig.loadStartDelay));
        }
        
        /// <inheritdoc/>
        public async Task Hide()
        {
            if (_loadingScreenConfig.fadeOutDuration <= 0f) _canvasGroup.alpha = 0f;
            
            else await _canvasGroup
                .DOFade(0f, _loadingScreenConfig.fadeOutDuration)
                .SetEase(_loadingScreenConfig.fadeOutEase)
                .SetUpdate(true)
                .AsyncWaitForCompletion();
            
            gameObject.SetActive(false);
        }

        /// <inheritdoc/>
        public void SetLoadProgress(float progress)
        {
            _loadProgressTarget = Mathf.Clamp01(progress);
            _loadProgressTween?.Kill();

            var duration = _loadingScreenConfig.minimumLoadDuration * (_loadProgressTarget - _loadProgress);
            _loadProgressTween = DOTween
                .To(() => _loadProgress, val => _loadProgress = val, _loadProgressTarget, duration)
                .SetUpdate(true)
                .OnUpdate(UpdateCustomLoadingScreen);
        }

        private void UpdateCustomLoadingScreen()
        {
            if(_customLoadingScreen == null) return;
            _customLoadingScreen.SetLoadProgress(_loadProgress);
        }

        /// <inheritdoc/>
        public async Task CompleteLoadProgress()
        {
            SetLoadProgress(1f);
            while (_loadProgress < 1f) await Task.Yield();
            
            if(_customLoadingScreen is not null) 
                _customLoadingScreen.onShutdownDelayStart?.Invoke(_loadingScreenConfig.loadCompleteDelay);
            await Task.Delay(TimeSpan.FromSeconds(_loadingScreenConfig.loadCompleteDelay));
        }
    }
}