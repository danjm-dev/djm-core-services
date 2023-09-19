using UnityEngine;
using UnityEngine.Events;

namespace DJM.CoreServices.LoadingScreen
{
    [RequireComponent(typeof(RectTransform))]
    public sealed class CustomLoadingScreen : MonoBehaviour
    {
        public UnityEvent<float> onSetupDelayStart;
        public UnityEvent<float> onLoadProgress;
        public UnityEvent<float> onShutdownDelayStart;

        private void Awake() => SetLoadProgress(0f);

        public void SetLoadProgress(float fillAmount) => onLoadProgress?.Invoke(fillAmount);
    }
}