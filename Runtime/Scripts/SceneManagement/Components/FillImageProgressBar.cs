using UnityEngine;
using UnityEngine.UI;

namespace DJM.CoreUtilities
{
    public sealed class FillImageProgressBar : MonoBehaviour
    {
        [SerializeField] public Image fillImage;

        private void Awake() => fillImage.fillAmount = 0f;

        public void SetFill(float fillAmount) => fillImage.fillAmount = fillAmount;
    }
}