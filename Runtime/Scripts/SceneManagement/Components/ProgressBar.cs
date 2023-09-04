using UnityEngine;
using UnityEngine.UI;

namespace DJM.CoreUtilities
{
    public sealed class ProgressBar : MonoBehaviour
    {
        [SerializeField] public CanvasGroup canvasGroup;
        [SerializeField] public Image fillImage;

        private void Awake()
        {
            canvasGroup.alpha = 0f;
            fillImage.fillAmount = 0f;
        }
    }
}