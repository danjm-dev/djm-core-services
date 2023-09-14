using UnityEngine;

namespace DJM.CoreUtilities.MonoBehaviors.Audio
{
    public sealed class PlayAudioEffectOnStart : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;
        private void Start()
        {
            DJMSound.PlayTransientSound(audioClip);
            Destroy(this);
        }
    }
}