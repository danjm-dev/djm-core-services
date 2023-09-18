using UnityEngine;

namespace DJM.CoreServices
{
    public interface ITransientSoundService
    {
        public bool IsMuted { get; }
        public float Volume { get; }
        
        public void Mute();
        public void UnMute();
        public void SetVolume(float volume);
        public void PlaySound(AudioClip sound, float volumeScale = 1f, float pitch = 1f);
        public void PlaySoundRandomPitch(AudioClip sound, float volumeScale = 1f, float minPitch = 0.95f, float maxPitch = 1.05f);
    }
}