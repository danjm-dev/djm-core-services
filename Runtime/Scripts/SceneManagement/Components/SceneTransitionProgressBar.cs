using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DJM.CoreUtilities
{
    public sealed class SceneTransitionProgressBar : MonoBehaviour
    {
        [SerializeField] public CanvasGroup canvasGroup;
        [SerializeField] public Image fillImage;

        private void Awake()
        {
            canvasGroup.alpha = 0f;
            fillImage.fillAmount = 0f;
        }
        
        public IEnumerator FillBarCoroutine
        (
            AsyncOperation sceneLoadOperation, 
            SceneLoadTransitionConfig transitionConfig
        )
        {
            canvasGroup.alpha = 1f;
            do
            {
                SetFillTarget(sceneLoadOperation.progress, transitionConfig.progressBarMinLoadDuration);
                yield return null;
            } 
            while (sceneLoadOperation.progress < 0.9f);
            
            SetFillTarget(1f, transitionConfig.progressBarMinLoadDuration);
            while (fillImage.fillAmount < 1f) yield return null;
            canvasGroup.alpha = 0f;
        }

        private void SetFillTarget(float target, float minDuration)
        {
            DOTween.Kill(fillImage);
            var duration = minDuration * (target - fillImage.fillAmount);
            fillImage.DOFillAmount(target, duration);
        }
    }
}