using UnityEngine;
using UnityEngine.UI;

namespace DJM.CoreServices.LoadingScreen
{
    [RequireComponent(typeof(RectTransform))]
    public sealed class CustomLoadingScreen : MonoBehaviour
    {
        [SerializeField] public Image progressBar;

        private void Awake() => SetProgressBarFill(0f);
        
        public void SetProgressBarFill(float fillAmount)
        {
            if(progressBar is null) return;
            progressBar.fillAmount = fillAmount;
        }
    }
}