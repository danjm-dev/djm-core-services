using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace DJM.CoreUtilities.Components
{
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class CanvasGroupFader : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private void Awake() => _canvasGroup = GetComponent<CanvasGroup>();
        
        /// <summary>
        /// Fades the alpha of a canvas group from its current value to the specified target alpha over a given duration.
        /// </summary>
        /// <param name="targetAlpha">The target alpha value the canvas group should reach at the end of the fade.</param>
        /// <param name="unscaledFadeDuration">The duration it takes for the alpha to transition either from 0 to 1 or from 1 to 0. The actual transition time will scale based on the current alpha and the target alpha.</param>
        /// <param name="ease">The type of easing function to apply for the fade transition</param>
        /// <returns>An IEnumerator used by Unity's coroutine system.</returns>
        public IEnumerator FadeCanvasGroupAlphaCoroutine(float targetAlpha, float unscaledFadeDuration, Ease ease)
        {
            var fadeDuration = Mathf.Abs(_canvasGroup.alpha - targetAlpha) * unscaledFadeDuration;
            
            yield return _canvasGroup
                .DOFade(targetAlpha, fadeDuration)
                .SetEase(ease)
                .WaitForCompletion();
        }

        /// <summary>
        /// Sets the alpha value of the associated canvas group.
        /// </summary>
        /// <param name="alpha">The alpha value to set, ranging from 0 (fully transparent) to 1 (fully opaque).</param>
        public void SetAlpha(float alpha) => _canvasGroup.alpha = alpha;
    }
}