using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DJM.CoreUtilities
{
    internal static class ProgressBarOperations
    {
        public static IEnumerator FillAsync(ProgressBar progressBar, AsyncOperation loadOperation, float minDuration)
        {
            progressBar.canvasGroup.alpha = 1f;
            
            do
            {
                SetImageFillTarget(progressBar.fillImage,loadOperation.progress, minDuration);
                yield return null;
            } 
            while (loadOperation.progress < 0.9f);
            
            SetImageFillTarget(progressBar.fillImage,1f, minDuration);
            while (progressBar.fillImage.fillAmount < 1f) yield return null;
            
            progressBar.canvasGroup.alpha = 0f;
        }

        private static void SetImageFillTarget(Image progressBarFillImage, float target, float minDuration)
        {
            DOTween.Kill(progressBarFillImage);
            var duration = minDuration * (target - progressBarFillImage.fillAmount);
            progressBarFillImage.DOFillAmount(target, duration);
        }
    }
}