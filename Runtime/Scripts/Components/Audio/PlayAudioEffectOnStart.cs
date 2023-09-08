using UnityEngine;

namespace DJM.CoreUtilities.Components
{
    public sealed class PlayAudioEffectOnStart : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;
        private void Start()
        {
            DJMAudio.PlayAudioEffect(audioClip);
            Destroy(this);
        }
    }
}