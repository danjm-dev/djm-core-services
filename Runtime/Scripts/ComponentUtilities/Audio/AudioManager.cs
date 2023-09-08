using UnityEngine;

namespace DJM.CoreUtilities.Audio
{
    public sealed class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource musicAudioSource;
        [SerializeField] private AudioSource effectsAudioSource;

        public void PlaySoundEffect(AudioClip audioClip)
        {
            effectsAudioSource.PlayOneShot(audioClip);
        }
    }
}