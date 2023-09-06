using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DJM.CoreUtilities
{
    [RequireComponent(typeof(CanvasGroupFader))]
    public sealed class FillImageProgressBar : MonoBehaviour
    {
        private CanvasGroupFader _canvasGroupFader;
        
        [SerializeField] public Image fillImage;

        private void Awake()
        {
            _canvasGroupFader = GetComponent<CanvasGroupFader>();
            fillImage.fillAmount = 0f;
        }

        public void SetFill(float fillAmount) => fillImage.fillAmount = fillAmount;

        public void FadeOut()
        {
            StartCoroutine(_canvasGroupFader.FadeCanvasGroupAlphaCoroutine(0f, 0.2f, Ease.InOutSine));
        }
    }
}