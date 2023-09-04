using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace DJM.CoreUtilities.Extensions
{
    public static class CanvasGroupExtensions
    {
        public static IEnumerator FadeInCanvasAsync(this CanvasGroup canvasGroup, float duration, Ease ease)
        {
            yield return canvasGroup
                .DOFade(1f, duration)
                .SetEase(ease)
                .WaitForCompletion();
        }
        
        public static IEnumerator FadeOutCanvasAsync(this CanvasGroup canvasGroup, float duration, Ease ease)
        {
            yield return canvasGroup
                .DOFade(1f, duration)
                .SetEase(ease)
                .WaitForCompletion();
        }
    }
}