using UnityEngine;

namespace DJM.CoreServices
{
    public interface ISoundController
    {
        public void SetMute(bool mute);
        public void SetVolume(float volume);
        public void PlayTransientSound(AudioClip transientSound, float volumeScale, float pitch);
    }
}