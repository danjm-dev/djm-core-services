using UnityEngine;

namespace DJM.CoreServices
{
    public interface IAudioSourcePool
    {
        public AudioSource GetAudioSource();
        public void ReleaseAudioSource(AudioSource audioSource);
    }
}