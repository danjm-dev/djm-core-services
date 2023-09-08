using UnityEngine;

namespace DJM.CoreUtilities.Components
{
    internal sealed class PlayAudioEffectOnDestroy : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;
        private void OnDestroy()
        {
            DJMAudio.PlayAudioEffect(audioClip);
            Destroy(this);
        }
    }
}