using UnityEngine;
using UnityEngine.UI;

namespace DJM.CoreServices.LoadingScreen
{
    [RequireComponent(typeof(Image))]
    internal sealed class LoadingScreenBackground : MonoBehaviour
    {
        private Image _fillImage;
        private void Awake()
        {
            _fillImage = GetComponent<Image>();
            _fillImage.rectTransform.anchorMin = Vector2.zero;
            _fillImage.rectTransform.anchorMax = Vector2.one;
            _fillImage.rectTransform.offsetMin = Vector2.zero;
            _fillImage.rectTransform.offsetMax = Vector2.zero;
        }
        
        internal static LoadingScreenBackground Create(Transform parent)
        {
            var newGameObject = new GameObject(nameof(LoadingScreenBackground))
            {
                transform = { parent = parent }
            };
            return newGameObject.AddComponent<LoadingScreenBackground>();
        }

        internal void SetColor(Color color)
        {
            _fillImage.color = color;
        }
    }
}