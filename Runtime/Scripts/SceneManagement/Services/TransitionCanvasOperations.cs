using System.Collections;
using DJM.CoreUtilities.Extensions;
using UnityEngine;

namespace DJM.CoreUtilities
{
    internal static class TransitionCanvasOperations
    {
        public static IEnumerator ShowCanvas(CanvasGroup canvas, SceneLoadTransitionConfig transitionConfig)
        {
            // show canvas
            if (transitionConfig.fadeInCanvas)
            {
                yield return canvas.FadeInCanvasAsync
                (
                    transitionConfig.fadeInDuration,
                    transitionConfig.fadeInEase
                );
            }
            canvas.alpha = 1f;
        }
        
        public static IEnumerator HideCanvas(CanvasGroup canvas, SceneLoadTransitionConfig transitionConfig)
        {
            // show canvas
            if (transitionConfig.fadeInCanvas)
            {
                yield return canvas.FadeInCanvasAsync
                (
                    transitionConfig.fadeInDuration,
                    transitionConfig.fadeInEase
                );
            }
            canvas.alpha = 1f;
        }
    }
}